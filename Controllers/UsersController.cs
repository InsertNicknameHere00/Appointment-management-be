using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersService;

        public UsersController(IUsersServices usersService)
        {
            _usersService = usersService;
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

        [HttpPost("Admin / update")]
        public async Task<IActionResult> UpdateAdminByID([FromHeader] int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.UpdateAdminByID(userId, users);
            return Ok(userTemp);
        }
    }
}
