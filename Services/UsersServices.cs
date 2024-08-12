﻿using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentAPI.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersServiceRepository _repository;
        private readonly HaircutSalonDbContext _context;
        private readonly IConfiguration _configuration;


        public UsersServices(IUsersServiceRepository repository, HaircutSalonDbContext context, IConfiguration configuration)
        {
            _repository = repository;
            _context = context;
            _configuration = configuration;
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

        public async Task<Users> UpdateUsersByID(int id,Users users) {
         return await _repository.UpdateUsersByID(id, users);
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


        public async Task<bool> RegisterUsers(Users users) {
            bool usersTemp= await _repository.RegisterUsers(users);
            return usersTemp;
        }


        public async Task<Users> GetUsersByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> AuthenticateUser(LoginUsers login)
        {
            var user = await _context.Users
               .Include(u => u.Role) // Include Role information
               .SingleOrDefaultAsync(u => u.Email == login.Email && u.PasswordHash == login.PasswordHash);

            return user;
        }

        public string GenerateJSONWebToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, userInfo.Role.RoleName),
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.UserID?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.PreferredUsername, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)

            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}