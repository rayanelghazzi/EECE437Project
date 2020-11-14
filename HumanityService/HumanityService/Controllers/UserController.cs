using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanityService.DataContracts.NgoDataContracts;
using HumanityService.DataContracts.UserDataContracts;
using HumanityService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {

        }

        [HttpGet("users/{username}")]
        public async Task<IActionResult> GetUserInfo(string username)
        {
            
        }

        [HttpDelete("users/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {

        }

        [HttpPut("users/{username}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {

        }

        [HttpPost("ngo")]
        public async Task<IActionResult> AddNgo([FromBody] Ngo ngo)
        {

        }

        [HttpGet("ngo/{username}")]
        public async Task<IActionResult> GetNgoInfo(string username)
        {

        }

        [HttpDelete("ngo/{username}")]
        public async Task<IActionResult> DeleteNgo(string username)
        {

        }

        [HttpPut("ngo/{username}")]
        public async Task<IActionResult> UpdateNgo([FromBody] Ngo ngo)
        {

        }
    }
}
