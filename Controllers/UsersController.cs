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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserID(int userId) {
            var userTemp = await _usersService.GetUsersIDQuery(userId);
            return Ok(userTemp);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostUsersID(string username, string email, string password, int roleId) {
            var userTemp = await _usersService.PostUsersIDQuery(username,email,password, roleId);
            return Ok(userTemp);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(string username, string email, string password) {
            var userTemp = await _usersService.RegistrationQuery(username, email, password);
            return Ok(userTemp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserID(int userID) {
            var userTemp = await _usersService.DeleteUsersQuery(userID);
            return Ok(userTemp);
        }
    }
}
