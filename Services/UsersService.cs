using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Users> PostUsersIDQuery(string username, string email, string password, int roleId)
        {
            Users newUser = new Users();
            newUser.username = username;
            newUser.email = email;
            newUser.password = password;
            newUser.roleID = roleId;

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
            return _context.Users.Any(e => e.userID == id);
        }

        public async Task<bool> RegistrationQuery(string username, string email, string password) {
            bool userExists = await _context.Users.AnyAsync(x => x.email == email || x.username == username);

            if (userExists) {
                return false;
            }
            else {
                Users account = new Users();
              account.username = username;
                account.password = password;
                account.email = email;
                _context.Users.Add(account);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

}