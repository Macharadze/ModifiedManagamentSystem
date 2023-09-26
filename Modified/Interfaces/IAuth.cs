using Modified.Models;

namespace Modified.Interfaces
{
    public interface IAuth
    {
        Task<ServiceResponse<int>> Register(User user, string password,string email);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

    }
}
