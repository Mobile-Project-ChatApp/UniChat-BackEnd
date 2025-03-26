using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniChat_BLL.Dto
{
    public class CreateEditUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? Semester { get; set; }
        public string? Study { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
