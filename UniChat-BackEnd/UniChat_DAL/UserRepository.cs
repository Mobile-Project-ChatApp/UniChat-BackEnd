using UniChat_BLL.Interfaces;
using UniChat_DAL.Data;
using UniChat_BLL.Dto;
using UniChat_DAL.Entities;

namespace UniChat_DAL;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<UserDto> GetAllUsers()
    {
        try
        {
            return _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                ProfilePicture = u.ProfilePicture,
                CreatedAt = u.CreatedAt
            }).ToList();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while getting all users", e);
        }
    }

    public UserDto GetUserById(int id)
    {
        try
        {
            UserEntity? user = _context.Users.Find(id);

            if (user == null)
            {
                throw new Exception($"User not found");
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                ProfilePicture = user.ProfilePicture,
                CreatedAt = user.CreatedAt,
                Semester = user.Semester,
                Study = user.Study,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiry = user.RefreshTokenExpiry

            };
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while getting the user", e);
        }
    }

    public bool CreateUser(CreateEditUserDto userDTO)
    {
        try
        {
            bool usernameExists = _context.Users.Any(u => u.Username == userDTO.Username);

            if (usernameExists)
            {
                throw new Exception($"Username with the name {userDTO.Username} already exists");
            }

            UserEntity user = new UserEntity
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                ProfilePicture = userDTO.ProfilePicture,
                Semester = userDTO.Semester,
                Study = userDTO.Study,
                CreatedAt = userDTO.CreatedAt

            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while creating user", e);
        }
    }

    public bool DeleteUser(int id)
    {
        try
        {
            UserEntity? user = _context.Users.Find(id);

            if (user == null)
            {
                throw new Exception($"User not found");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while deleting user", e);
        }
    }

    public bool UpdateUser(int id, CreateEditUserDto userDTO)
    {
        try
        {
            UserEntity? user = _context.Users.Find(id);

            if (user == null)
            {
                throw new Exception($"User not found");
            }

            user.Username = userDTO.Username;
            user.Email = userDTO.Email;
            user.PasswordHash = userDTO.PasswordHash;
            user.ProfilePicture = userDTO.ProfilePicture;
            user.CreatedAt = userDTO.CreatedAt;
            user.Semester = userDTO.Semester;
            user.Study = userDTO.Study;
            user.RefreshToken = userDTO.RefreshToken;
            user.RefreshTokenExpiry = userDTO.RefreshTokenExpiry;

            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while updating user", e);
        }
    }

    public UserDto? GetUserByUsername(string username)
    {
        try
        {
            UserEntity? user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                CreatedAt = user.CreatedAt
            };
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while getting the user by username", e);
        }
    }

    public UserDto? GetUserByRefreshToken(string refreshToken)
    {
        try
        {
            UserEntity? user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                CreatedAt = user.CreatedAt
            };
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while getting the user by refresh token", e);
        }
    }

    public void UpdateRefreshToken(int id, string refreshToken, DateTime refreshTokenExpiry)
    {
        try
        {
            UserEntity? user = _context.Users.Find(id);
            if (user == null)
            {
                throw new Exception($"User not found");
            }
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = refreshTokenExpiry;
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while updating the refresh token", e);
        }
    }
}
