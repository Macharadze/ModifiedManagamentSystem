using Modified.Dto;
using Modified.Models;

namespace Modified.Interfaces
{
    public interface IUser
    {
        Task<ServiceResponse<List<UserDto>>> GetUsers();
        Task<ServiceResponse<User>> GetUser(int id);

        Task<ServiceResponse<UserDto>> UpdateUser(int id,UserDto userDto);
        Task<ServiceResponse<List<UserDto>>> DeleteUser(int id);
     

    }
}
