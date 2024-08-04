using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userTemp = await _usersService.GetUsersQuery();
            return Ok(userTemp);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUserID([FromHeader] int userId) {
            var userTemp = await _usersService.GetUsersIDQuery(userId);
            return Ok(userTemp);
        }

        [HttpPost("userId")]
        public async Task<IActionResult> PostUsersID([FromBody] Users users) {
            var userTemp = await _usersService.PostUsersIDQuery(users);
            return Ok(userTemp);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Users users) {
            var userTemp = await _usersService.RegistrationQuery(users);
            return Ok(userTemp);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUserID([FromHeader] int id) {
            var userTemp = await _usersService.DeleteUsersQuery(id);
            return Ok(userTemp);
        }
    }
}
