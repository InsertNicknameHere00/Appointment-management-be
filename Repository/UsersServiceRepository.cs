using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;

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

        public async Task<bool> ForgottenPassword(Users users) {
            var existingUser = await _context.Users.FindAsync(users.Email);
            var tokenTemp= ResetTokenCheck(users.ResetToken, users);
            if (existingUser != null && tokenTemp.Result==true)
            {
                users.PasswordHash = users.PasswordHash;
                _context.Users.Update(users);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ResetTokenCheck(string token, Users users) {
            var tempToken = users.ResetToken;
            var inputToken = token;
            if (inputToken == tempToken)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> VerificationTokenCheck(string token, Users users)
        {
            var tempToken = users.VerificationToken;
            var inputToken = token;
            if (inputToken == tempToken)
            {
                return true;
            }
            return false;

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
                Random random = new Random();

                Users account = new Users();
                account.FirstName = users.FirstName;
                account.LastName = users.LastName;
                account.PasswordHash = users.PasswordHash;
                account.Email = users.Email;
                account.PhoneNumber = users.PhoneNumber;
                account.StartDate = DateTime.UtcNow;
                account.EndDate = DateTime.UtcNow.AddHours(24);
                account.ResetToken = random.Next(6).ToString();
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

        public async Task<bool> ClearVerificationToken(Users users) {
            var existingUser = await _context.Users.FindAsync(users.Email);
            if (existingUser != null)
            {
                existingUser.StartDate = null;
                existingUser.EndDate = null;
                existingUser.VerificationToken = null;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ClearResetToken(Users users)
        {
            var existingUser = await _context.Users.FindAsync(users.Email);
            if (existingUser != null)
            {
                existingUser.StartDate = null;
                existingUser.EndDate = null;
                existingUser.ResetToken = null;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<string> GenerateResetToken(Users users)
        {
            var existingUser = await _context.Users.FindAsync(users.UserID);
            if (existingUser != null)
            {
                Random random = new Random();
                existingUser.StartDate = DateTime.UtcNow;
                existingUser.EndDate = DateTime.UtcNow.AddHours(24);
                existingUser.ResetToken = random.Next(6).ToString();
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                return existingUser.ResetToken.ToString();
            }
            return "Not found";
        }
    }

}