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

        public async Task<Users> PostUsersIDQuery(Users users)
        {
            Users newUser = new Users();
            newUser.username = users.username;
            newUser.email = users.email;
            newUser.password = users.password;
            newUser.roleID = users.roleID;

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

        public async Task<bool> RegistrationQuery(Users users) {
            bool userExists = await _context.Users.AnyAsync(x => x.email == users.email || x.username == users.username);

            if (userExists) {
                return false;
            }
            else {
                Users account = new Users();
              account.username = users.username;
                account.password = users.password;
                account.email = users.email;
                _context.Users.Add(account);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

}