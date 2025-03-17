using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_DAL.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Semester { get; set; }
    public string? Study { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
