﻿using AppointmentAPI.Entities;
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

        Task<bool> RegisterUsers(Users users);

        Task<Users> AuthenticateUser(LoginUsers login);
        Task<Users> GetUsersByEmail(string email);
        string GenerateJSONWebToken(Users user);
    }
}
