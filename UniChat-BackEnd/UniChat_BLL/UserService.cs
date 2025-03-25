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

    }
}
