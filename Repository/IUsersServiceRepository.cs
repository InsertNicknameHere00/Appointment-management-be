using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IUsersServiceRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUsersByID(int id);
        Task<Users> AddUsers(Users users);
        Task<Users> UpdateUsersByID(int id, Users users);
        Task<Users> UpdateAdminByID(int id, Users users);
        Task<bool> DeleteUsers(int id);

        Task<bool> RegisterUsers(Users users);
    }
}
