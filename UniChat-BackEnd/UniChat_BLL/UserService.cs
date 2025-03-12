using System.Threading.Tasks;
using UniChat_BLL.Interfaces;
using UniChat_BLL.Dto;
using System.Security.Cryptography;
using System.Text;

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
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(userDTO.PasswordHash));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                userDTO.PasswordHash = builder.ToString();
            }

            return _userRepository.CreateUser(userDTO);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public bool UpdateUser(int id, CreateEditUserDto userDTO)
        {
            return _userRepository.UpdateUser(id, userDTO);
        }

    }
}
