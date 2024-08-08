using AppointmentAPI.Entities;
namespace AppointmentAPI.Services
{
    public interface IUsersServices
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUsersByID(int id);
        Task<Users> AddUsers(Users users);
        Task<Users> UpdateUsersByID(int id, Users users);
        Task<bool> DeleteUsers(int id);

        Task<bool> RegisterUsers(Users users);


        //Task<Users> AuthenticateUser(LoginUsers login);
        //string GenerateJSONWebToken(Users userInfo);
        Task<Users> GetUsersByEmail(string email);
    }
}
