using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using AppointmentAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentAPI.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersServiceRepository _repository;
        private readonly HaircutSalonDbContext _context;
        private readonly IConfiguration _configuration;
        private EmailRequest _emailRequest = new EmailRequest();
        private IEmailSendService _emailSendService;
        private readonly ICloudinaryService _cloudinaryService;

        public UsersServices(IUsersServiceRepository repository, HaircutSalonDbContext context, IConfiguration configuration, IEmailSendService emailSendService, ICloudinaryService cloudinaryService)
        {
            _repository = repository;
            _context = context;
            _configuration = configuration;
            _emailSendService = emailSendService;
            _cloudinaryService = cloudinaryService;
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

        public async Task<Users> UpdateUsers(int id, Users users)
        {
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
            var usersTemp = await _repository.ForgottenPassword(users);
            return usersTemp;
        }


        public async Task<bool> RegisterUsers(Users users) {
            bool usersTemp = await _repository.RegisterUsers(users);

            var token = GenerateJSONWebToken(users);

            if (usersTemp == true) { 
            _emailRequest.ToEmail = users.Email;
            _emailRequest.Subject = "Email Verification";
            _emailRequest.Body = _configuration.GetSection("urls").Value + "/api/Users/ConfirmEmail?Email=" + users.Email+"&token="+token;
            await _emailSendService.SendEmail(_emailRequest);
            }

            return usersTemp;
        }


        public async Task<Users> GetUserByEmail(string email)
        {
            var usersTemp=await _repository.GetUserByEmail(email);
            return usersTemp;
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
                new Claim(JwtRegisteredClaimNames.PreferredUsername, userInfo.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)

                //new Claim("Role name", userInfo.Role.RoleName),
                //new Claim("User ID", userInfo.UserID?.ToString() ?? string.Empty),
                //new Claim("User name", userInfo.UserName),
                //new Claim("Email", userInfo.Email)

            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ConfirmEmail(Users users, string token)
        {
            var result = await _repository.ConfirmUserEmail(users, token);

            return result;

        }

        public async Task UpdateAvatarAsync(int userId, IFormFile file, string fileName)
        {
            var user = this._repository.GetUserByID(userId);

            if (file != null)
            {
                var imageUrl = await this._cloudinaryService.UploadPhotoAsync(file, fileName);

                user.Picture = imageUrl;
            }

            this._repository.UpdateUsers(userId, user);
        }
    }

}