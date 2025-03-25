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

        public ChatHub(MessageService messageService, ChatRoomService chatRoomService)
        {
            _messageService = messageService;
            _chatRoomService = chatRoomService;
        }

        public async Task SendMessage(int roomId, string message)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            var savedMessage = _messageService.SendMessage(roomId, int.Parse(userId), message);
            
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

