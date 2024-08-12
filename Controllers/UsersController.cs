using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersService;
        private IConfiguration _configuration;

        public UsersController(IUsersServices usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userTemp = await _usersService.GetAllUsers();
            return Ok(userTemp);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUserID([FromHeader] int userId) {
            var userTemp = await _usersService.GetUsersByID(userId);
            return Ok(userTemp);
        }

        [HttpPost("userId")]
        public async Task<IActionResult> AddUser([FromBody] Users users) {
            var userTemp = await _usersService.AddUsers(users);
            return Ok(userTemp);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Users users) {
            var userTemp = await _usersService.RegisterUsers(users);
            return Ok(userTemp);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUserID([FromHeader] int id) {
            var userTemp = await _usersService.DeleteUsers(id);
            return Ok(userTemp);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUsersByID([FromHeader] int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.UpdateUsersByID(userId, users);
            return Ok(userTemp);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsers login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.PasswordHash))
            {
                return BadRequest("Invalid login request");
            }

            // Authenticate user
            var user = await AuthenticateUser(login);

            if (user != null)
            {
                // Generate JWT token
                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }



        private async Task<Users> AuthenticateUser(LoginUsers login)
        {
            var user = await _usersService.GetUsersByEmail(login.Email);

            if (user != null && user.PasswordHash == login.PasswordHash)
            {
                return user;
            }

            return null;
        }

        private string GenerateJSONWebToken(Users usersInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, usersInfo.RoleID?.ToString() ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.NameId, usersInfo.UserID?.ToString() ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.PreferredUsername, usersInfo.UserName),
        new Claim(JwtRegisteredClaimNames.Email, usersInfo.Email)
    };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
