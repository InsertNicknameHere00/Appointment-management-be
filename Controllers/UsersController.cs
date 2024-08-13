﻿using System;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserID([FromHeader] int userId) {
            var userTemp = await _usersService.GetUsersByID(userId);
            return Ok(userTemp);
        }

        [HttpPost("Admin/Add")]
        public async Task<IActionResult> AddUser([FromBody] Users users) {
            var userTemp = await _usersService.AddUsers(users);
            return Ok(userTemp);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Users users) {
            var userTemp = await _usersService.RegisterUsers(users);
            return Ok(userTemp);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserID([FromHeader] int userId) {
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
        public async Task<IActionResult> UpdateUser([FromHeader]int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.UpdateUsers(userId,users);
            return Ok(userTemp);
        }

        [HttpPost("ForgottenPassword")]
        public async Task<IActionResult> ForgottenPassword([FromHeader] int userId, [FromBody] Users users)
        {
            var userTemp = await _usersService.ForgottenPassword(userId, users);
            return Ok(userTemp);
        }
    }
}
