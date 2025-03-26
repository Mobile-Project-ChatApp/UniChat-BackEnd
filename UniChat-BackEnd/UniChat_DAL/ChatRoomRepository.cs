using UniChat_BLL.Dto;
using UniChat_BLL.Interfaces;
using UniChat_DAL.Data;
using UniChat_DAL.Entities;

namespace UniChat_DAL;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly AppDbContext _context;

    public ChatRoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<ChatRoomDto> GetAllChatRooms()
    {
        return _context.ChatRooms.Select(c => new ChatRoomDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt,
        }).ToList();
    }

    public ChatRoomDto GetChatRoomById(int id)
    {
      var chatRoom = _context.ChatRooms.Find(id);
      if (chatRoom == null)
      {
        throw new Exception("Chat room not found");
      }
      return new ChatRoomDto() {
        Id = chatRoom.Id,
        Name = chatRoom.Name,
        Description = chatRoom.Description,
        CreatedAt = chatRoom.CreatedAt
      };
    }

    public bool CreateChatRoom(CreateEditChatRoomDto chatRoomDto)
    {
        var chatRoom = new ChatRoom
        {
            Name = chatRoomDto.Name,
            Description = chatRoomDto.Description
        };

        _context.ChatRooms.Add(chatRoom);
        _context.SaveChanges();

        return true;
    }

    public bool UpdateChatRoom(int id, CreateEditChatRoomDto chatRoomDto)
    {
        var chatRoom = _context.ChatRooms.Find(id);
        if (chatRoom == null)
        {
            throw new Exception("Chat room not found");
        }

        chatRoom.Name = chatRoomDto.Name;
        chatRoom.Description = chatRoomDto.Description;

        _context.SaveChanges();

        return true;
    }

    public bool DeleteChatRoom(int id)
    {
        var chatRoom = _context.ChatRooms.Find(id);
        if (chatRoom == null)
        {
            throw new Exception("Chat room not found");
        }

        _context.ChatRooms.Remove(chatRoom);
        _context.SaveChanges();

        return true;
    }

    public bool AddUserToChatRoom(int chatRoomId, int userId)
    {
        var chatRoom = _context.ChatRooms.Find(chatRoomId);
        if (chatRoom == null)
        {
            throw new Exception("Chat room not found");
        } 

        var user = _context.Users.Find(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        } 

        chatRoom.Members.Add(user);
        _context.SaveChanges();

        return true;
    }

    public bool RemoveUserFromChatRoom(int chatRoomId, int userId)
    {
        var chatRoom = _context.ChatRooms.Find(chatRoomId);
        if (chatRoom == null)
        {
            throw new Exception("Chat room not found");
        }

        var user = _context.Users.Find(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        chatRoom.Members.Remove(user);
        _context.SaveChanges();

        return true;
    }
}

