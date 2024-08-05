using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AppointmentAPI.Services
{
    public class UsersService
    {
        private readonly HaircutSalonDbContext _context;

        public UsersService(HaircutSalonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetUsersQuery()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUsersIDQuery(int id)
        {
            var users = await _context.Users.FindAsync(id);
            return users;
        }

        public async Task<Users> UpdateUsersIDQuery(int id,Users users) {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.UserID = id;
                existingUser.UserName = users.UserName;
                existingUser.Email = users.Email;
                existingUser.PasswordHash = users.PasswordHash;
                existingUser.RoleID = users.RoleID;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            return await _context.Users.FindAsync(id); ;
        }

        public async Task<Users> AddUsersIDQuery(Users users)
        {
            Users newUser = new Users();
            newUser.UserName = users.UserName;
            newUser.Email = users.Email;
            newUser.PasswordHash = users.PasswordHash;
            newUser.RoleID = users.RoleID;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }



        public async Task<bool> DeleteUsersQuery(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return false;
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        public async Task<bool> RegistrationQuery(Users users) {
            bool userExists = await _context.Users.AnyAsync(x => x.Email == users.Email || x.UserName == users.UserName);

            if (userExists) {
                return false;
            }
            else {
                Users account = new Users();
              account.UserName = users.UserName;
                account.PasswordHash = users.PasswordHash;
                account.Email = users.Email;
                account.RoleID = 2;
                _context.Users.Add(account);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

}