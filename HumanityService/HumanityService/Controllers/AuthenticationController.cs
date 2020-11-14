using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanityService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login() //from body
        {

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout() //from body
        {

        }

        [HttpGet("refresh-access-token")]
        public async Task<IActionResult> RefreshAccessToken() //from body
        {

        }
    }
}
