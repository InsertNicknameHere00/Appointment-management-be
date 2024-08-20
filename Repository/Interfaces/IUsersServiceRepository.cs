﻿using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IUsersServiceRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUsersByID(int id);
        Users GetUserByID(int id);
        Task<Users> AddUsers(Users users);
        Task<Users> UpdateUsers(int id, Users users);
        Task<Users> UpdateAdminByID(int id, Users users);
        Task<Users> ForgottenPassword(Users users);
        Task<Users> GetUserByEmail(string email);
        Task<bool> GenerateResetToken(Users users);
        Task<bool> GenerateVerificationToken(Users users);
        Task<bool> DeleteUsers(int id);

        Task<bool> RegisterUsers(Users users);
        Task<bool> ConfirmUserEmail(Users users, string token);
    }
}
