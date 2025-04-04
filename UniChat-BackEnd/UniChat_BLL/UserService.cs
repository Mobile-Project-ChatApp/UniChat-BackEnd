using System.Threading.Tasks;
using UniChat_BLL.Interfaces;
using UniChat_BLL.Dto;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace UniChat_BLL
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public UserDto GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public bool CreateUser(CreateEditUserDto userDTO)
        {
            var passwordHasher = new PasswordHasher<object>();
            userDTO.PasswordHash = passwordHasher.HashPassword(null, userDTO.PasswordHash);

            return _userRepository.CreateUser(userDTO);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public bool UpdateUser(int id, CreateEditUserDto userDTO)
        {
            var passwordHasher = new PasswordHasher<object>();
            userDTO.PasswordHash = passwordHasher.HashPassword(null, userDTO.PasswordHash);
            return _userRepository.UpdateUser(id, userDTO);
        }

        public UserDto? GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public UserDto? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public UserDto? GetUserByRefreshToken(string refreshToken)
        {
            return _userRepository.GetUserByRefreshToken(refreshToken);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(null, passwordHash, password);

            return result == PasswordVerificationResult.Success;
        }

        public void UpdateRefreshToken(int userId, string refreshToken, DateTime expiry)
        {
            UserDto user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            CreateEditUserDto createEditUserDTO = new CreateEditUserDto
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                ProfilePicture = user.ProfilePicture,
                CreatedAt = user.CreatedAt,
                RefreshToken = refreshToken, // Assign new refresh token
                RefreshTokenExpiry = expiry  // Assign new expiry date
            };

            _userRepository.UpdateUser(user.Id, createEditUserDTO); // Assuming this method commits the changes to the database
        }

    }
}
