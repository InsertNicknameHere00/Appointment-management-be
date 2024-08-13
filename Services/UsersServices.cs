using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppointmentAPI.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersServiceRepository _repository;

        public UsersServices(IUsersServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var usersTemp = await _repository.GetAllUsers();
            return usersTemp;
        }

        public async Task<Users> GetUsersByID(int id)
        {
            var users = await _repository.GetUsersByID(id);
            return users;
        }

        public async Task<Users> UpdateUsers(int id, Users users) {
         return await _repository.UpdateUsers(id, users);
        }

        public async Task<Users> UpdateAdminByID(int id, Users users)
        {
            return await _repository.UpdateAdminByID(id, users);
        }

        public async Task<Users> AddUsers(Users users)
        {
          return await _repository.AddUsers(users);
        }



        public async Task<bool> DeleteUsers(int id)
        {
            var users = await _repository.DeleteUsers(id);
            if (users == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Users> ForgottenPassword(int id, Users users)
        {
            var usersTemp = await _repository.ForgottenPassword(id, users);
            return usersTemp;
        }


        public async Task<bool> RegisterUsers(Users users) {
            bool usersTemp= await _repository.RegisterUsers(users);
            return usersTemp;
        }
    }

}