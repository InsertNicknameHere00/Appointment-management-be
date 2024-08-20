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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersService;
        private IConfiguration _configuration;
        private EmailRequest _emailRequest = new EmailRequest();
        private IEmailSendService _emailSendService;

        public UsersController(IUsersServices usersService, IConfiguration configuration, IEmailSendService emailSendService)
        {
            _usersService = usersService;
            _configuration = configuration;
            _emailSendService = emailSendService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userTemp = await _usersService.GetAllUsers();
            return Ok(userTemp);
        }

        [HttpGet("ByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromBody] Users users) {
            var userTemp = await _usersService.GetUserByEmail(users.Email);
            return Ok(userTemp);
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetUserID([FromQuery] int userId) {
            var userTemp = await _usersService.GetUsersByID(userId);
            return Ok(userTemp);
        }

        [HttpPost("Admin/Add")]
        public async Task<IActionResult> AddUser([FromBody] Users users) {
            var userTemp = await _usersService.AddUsers(users);
            return Ok(userTemp);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] Users users) {
            var userTemp = await _usersService.RegisterUsers(users);


            return Ok(userTemp);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUserID([FromQuery] int userId) {
            var userTemp = await _usersService.DeleteUsers(userId);
            return Ok(userTemp);
        }

        [HttpPost("Admin/Update/{userId}")]
        public async Task<IActionResult> UpdateAdminByID([FromHeader] int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.UpdateAdminByID(userId, users);
            return Ok(userTemp);
        }

        [HttpPost("Update/{userId}")]
        public async Task<IActionResult> UpdateUser([FromHeader] int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.UpdateUsers(userId, users);
            return Ok(userTemp);
        }

        [HttpPost("ForgottenPassword")]
        public async Task<IActionResult> ForgottenPassword([FromHeader] int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.ForgottenPassword(userId, users);
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
            //dummy push test
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string email, [FromQuery]string token)
        {
            var user = await _usersService.GetUserByEmail(email);
            if (user == null || token == null)
            {
                return NotFound();
            }
            else
            {
                token = token.Replace(" ", "+");
                var result = await _usersService.ConfirmEmail(user, token);
                if (result.Equals(user))
                {
                    return Ok("Verification succeded");
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}
