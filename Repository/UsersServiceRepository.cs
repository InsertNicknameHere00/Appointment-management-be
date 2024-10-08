﻿using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Runtime.InteropServices;
using System.Text;

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
            var existingUser = await GetUserByEmail(users.Email);
            var tokenTemp= ResetTokenCheck(users.ResetToken, users);
            if (existingUser != null && tokenTemp.Result==true)
            {
                Random random=new Random();
                existingUser.PasswordHash = random.Next(11).ToString();
                _context.Users.Update(existingUser);
                ClearResetToken(existingUser);
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
        public async Task<string> RegisterUsers(Users users)
        {
           // Random random = new Random();
            if (RegisteredUserExists(users).Result == true)
            {
                Users account = new Users();
                account.FirstName = users.FirstName;
                account.LastName = users.LastName;
                account.PasswordHash = users.PasswordHash;
                account.Email = users.Email;
                account.PhoneNumber = users.PhoneNumber;
                account.StartDate = DateTime.UtcNow;
                account.EndDate = DateTime.UtcNow.AddHours(24);
                account.VerificationToken = await GenerateSecurityToken(128);
                account.RoleID = 2;
                account.VerificationStatus = "Pending ...";
                _context.Users.Add(account);
                await _context.SaveChangesAsync();
                return account.VerificationToken;
            }
            else 
            {
                var existingUser = await GetUserByEmail(users.Email);
                if (existingUser.EndDate < DateTime.UtcNow)
                {
                    existingUser.StartDate = DateTime.UtcNow;
                    existingUser.EndDate = DateTime.UtcNow.AddHours(24);
                    existingUser.VerificationToken = await GenerateSecurityToken(128);
                    _context.Users.Update(existingUser);
                    await _context.SaveChangesAsync();
                    return existingUser.VerificationToken;
                }
            }
            return "Not found";
        }

        public async Task<bool> ConfirmUserEmail(Users users, string token)
        {
            if (RegisteredUserExists(users).Result == false)
            {
                users.VerificationStatus = "Verified";
                _context.Users.Update(users);
                ClearVerificationToken(users);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ClearVerificationToken(Users users) {
            var existingUser = await GetUserByEmail(users.Email);
            if (existingUser != null)
            {
                existingUser.StartDate = null;
                existingUser.EndDate = null;
                existingUser.VerificationToken = null;
                return true;
            }
            return false;
        }

        public async Task<bool> ClearResetToken(Users users)
        {
            var existingUser = await GetUserByEmail(users.Email);
            if (existingUser != null)
            {
                existingUser.StartDate = null;
                existingUser.EndDate = null;
                existingUser.ResetToken = null;
                return true;
            }
            return false;
        }

        public async Task<string> GenerateResetToken(Users users)
        {
            var existingUser = await GetUserByEmail(users.Email);
            if (existingUser != null)
            {
                existingUser.StartDate = DateTime.UtcNow;
                existingUser.EndDate = DateTime.UtcNow.AddHours(24);
                existingUser.ResetToken = await GenerateSecurityToken(128);
                return existingUser.ResetToken.ToString();
            }
            return "Not found";
        }

        public async Task<string> GenerateSecurityToken(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                // Select a random character from the validChars string
                result.Append(validChars[random.Next(validChars.Length)]);
            }

            return result.ToString();
        }
    }

}