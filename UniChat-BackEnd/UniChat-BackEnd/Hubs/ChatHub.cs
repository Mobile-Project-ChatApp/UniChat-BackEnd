using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Hubs
{
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

        // TODO: Add user id to the message
        public async Task SendMessage(int roomId, string message)
        {
            MessageDto savedMessage = _messageService.SendMessage(roomId, 1, message);
            
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", savedMessage);
        }

        public async Task JoinRoom(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            
            await Clients.Group(roomId.ToString()).SendAsync("UserJoined", Context.User.Identity.Name);
        }

        public async Task LeaveRoom(int roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }
    }
}

