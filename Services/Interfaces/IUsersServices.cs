using AppointmentAPI.Entities;
namespace AppointmentAPI.Services
{
    public interface IUsersServices
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUsersByID(int id);
        Task<Users> AddUsers(Users users);
        Task<Users> UpdateUsers(int id, Users users);
        Task<Users> UpdateAdminByID(int id, Users users);
        Task<Users> ForgottenPassword(int id, Users users);
        Task<bool> DeleteUsers(int id);
        Task<Users> GetUserByEmail(string email);
        Task<bool> RegisterUsers(Users users);
        Task<string> ConfirmEmail(Users user, string token);
        Task<Users> AuthenticateUser(LoginUsers login);
        string GenerateJSONWebToken(Users user);
    }
}
