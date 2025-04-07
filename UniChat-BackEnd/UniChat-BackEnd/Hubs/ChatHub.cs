using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly MessageService _messageService;
        private readonly ChatRoomService _chatRoomService;
        private readonly UserService _userService;

        public ChatHub(MessageService messageService, ChatRoomService chatRoomService, UserService userService)
        {
            _messageService = messageService;
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        private async Task<(int userId, UserDto user)> GetUserInfo()
        {
            string userIdStr = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdStr == null)
            {
                await Clients.Caller.SendAsync("UserNotFound", "User not found");
                return (0, null);
            }

            int userId = int.Parse(userIdStr);
            UserDto user = _userService.GetUserById(userId);

            if (user == null)
            {
                await Clients.Caller.SendAsync("UserNotFound", "User not found");
                return (0, null);
            }

            return (userId, user);
        }

        private MinimalUserDto CreateMinimalUserDto(UserDto user)
        {
            return new MinimalUserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public async Task SendMessage(int roomId, string message)
        {
            (int userId, UserDto user) = await GetUserInfo();
            if (user == null) return;

            MessageDto savedMessage = _messageService.SendMessage(roomId, userId, message);

            if (savedMessage == null)
            {   
                await Clients.Caller.SendAsync("ErrorSendingMessage", "Error sending message");
                return;
            }

            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", savedMessage);
        }

        public async Task JoinRoom(int roomId)
        {
            try
            {
                (int userId, UserDto user) = await GetUserInfo();
                if (user == null) return;

                await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

                bool added = _chatRoomService.AddUserToChatRoom(roomId, userId);

                if (!added)
                {
                    await Clients.Caller.SendAsync("ErrorAddingUserToChatRoom", "Error adding user to chat room");
                    return;
                }

                await Clients.Group(roomId.ToString()).SendAsync("UserJoined", CreateMinimalUserDto(user));
            } catch (Exception ex)
            {
                Console.WriteLine("Exception in JoinRoom: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                await Clients.Caller.SendAsync("Error", "Unexpected error: " + ex.Message);
            }
        }

        public async Task LeaveRoom(int roomId)
        {
            try
            {
                (int userId, UserDto user) = await GetUserInfo();
                if (user == null) return;

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());

                bool removed = _chatRoomService.RemoveUserFromChatRoom(roomId, userId);

                if (!removed)
                {
                    await Clients.Caller.SendAsync("ErrorRemovingUserFromChatRoom", "Error removing user from chat room");
                    return;
                }

                await Clients.Group(roomId.ToString()).SendAsync("UserLeft", user.Username);
            } catch (Exception ex)
            {
                Console.WriteLine("Exception in LeaveRoom: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                await Clients.Caller.SendAsync("Error", "Unexpected error: " + ex.Message);
            }
        }
    }
}

