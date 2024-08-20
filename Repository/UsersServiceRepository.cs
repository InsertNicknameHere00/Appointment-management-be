using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AppointmentAPI.Repository
{
    public class UsersServiceRepository : IUsersServiceRepository
    {
        private readonly HaircutSalonDbContext _context;
        public UsersServiceRepository(HaircutSalonDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAllUsers() {
            var usersTemp = await _context.Users.ToListAsync();
            return usersTemp;
        }
        public async Task<Users> GetUserByEmail(string email) { 
        var usersTemp = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
            return usersTemp;
        }

        public async Task<Users> GetUsersByID(int id)
        {
            var users = await _context.Users.FindAsync(id);
            return users;
        }

        public Users GetUserByID(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public async Task<Users> AddUsers(Users users)
        {
            Users newUser = new Users();
            newUser.FirstName = users.FirstName;
            newUser.LastName = users.LastName;
            newUser.Email = users.Email;
            newUser.PasswordHash = users.PasswordHash;
            newUser.PhoneNumber = users.PhoneNumber;
            newUser.RoleID = users.RoleID;
            newUser.VerificationStatus = users.VerificationStatus;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<Users> UpdateAdminByID(int id, Users users)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.UserID = id;
                existingUser.FirstName = users.FirstName;
                existingUser.LastName = users.LastName;
                existingUser.Email = users.Email;
                existingUser.PasswordHash = users.PasswordHash;
                existingUser.PhoneNumber = users.PhoneNumber;
                existingUser.RoleID = users.RoleID;
                existingUser.VerificationStatus = users.VerificationStatus;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            return existingUser;
        }

        public async Task<Users> UpdateUsers(int id, Users users)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.UserID = id;
                existingUser.FirstName = users.FirstName;
                existingUser.LastName = users.LastName;
                existingUser.Email = users.Email;
                existingUser.PasswordHash = users.PasswordHash;
                existingUser.PhoneNumber = users.PhoneNumber;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            return existingUser;
        }

        public async Task<bool> DeleteUsers(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Users> ForgottenPassword(Users users) {
            var existingUser = await RegisteredUserExists(users);
            if (existingUser != null)
            {
                users.PasswordHash = users.PasswordHash;
                _context.Users.Update(users);
                await _context.SaveChangesAsync();
            }
            return users;
        }

        public async Task<bool> RegisteredUserExists(Users users) {
            bool userExists = await _context.Users.AnyAsync(x => x.Email == users.Email);
            if (userExists)
            {
                return false;
            }
            else {
                return true;
                    }
        }
        public async Task<bool> RegisterUsers(Users users)
        {

            if (RegisteredUserExists(users).Result==true)
            {
                Users account = new Users();
                account.FirstName = users.FirstName;
                account.LastName = users.LastName;
                account.PasswordHash = users.PasswordHash;
                account.Email = users.Email;
                account.PhoneNumber = users.PhoneNumber;
                //Temporary leaving RoleID hardcoded to 1, cause we lack RoleID 2 in table
                account.RoleID = 2;
                account.VerificationStatus = "Pending ...";
                _context.Users.Add(account);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ConfirmUserEmail(Users users, string token)
        {
            if (RegisteredUserExists(users).Result == false)
            {
                users.VerificationStatus = "Verified";
                _context.Users.Update(users);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}