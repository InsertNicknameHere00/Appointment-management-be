using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<IActionResult> PostUsers(Users user) {
            var userTemp = await _usersService.PostUsersQuery(user);
            return Ok(userTemp);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostUsersID(int userId,Users user) {
            var userTemp = await _usersService.PostUsersIDQuery(userId,user);
            return Ok(userTemp);
        }

        [HttpPost("{register}")]
        public async Task<IActionResult> RegisterUser(Users user) {
        var userTemp= await _usersService.RegistrationQuery(user);
            return Ok(userTemp);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserID(int userID) {
            var userTemp = await _usersService.DeleteUsersQuery(userID);
            return Ok(userTemp);
        }
    }
}
