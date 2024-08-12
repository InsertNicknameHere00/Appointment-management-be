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
            var user = await _usersService.AuthenticateUser(login);

            if (user != null)
            {
                // Generate JWT token
                var tokenString = _usersService.GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }
}
