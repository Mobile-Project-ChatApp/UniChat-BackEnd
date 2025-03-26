namespace UniChat_DAL.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfilePicture { get; set; }
    public int? Semester { get; set; }
    public string? Study { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<UserChatroom> UserChatrooms { get; set; }
}
