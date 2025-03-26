using UniChat_BLL.Dto;

namespace UniChat_BLL.Interfaces
{
    public interface IUserRepository
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        bool CreateUser(CreateEditUserDto userDTO);
        bool DeleteUser(int id);
        bool UpdateUser(int id, CreateEditUserDto userDTO);
        UserDto? GetUserByUsername(string username);
        UserDto? GetUserByRefreshToken(string refreshToken);
        void UpdateRefreshToken(int id, string refreshToken, DateTime refreshTokenExpiry);
    }
}
