using System.Threading.Tasks;
using HumanityService.DataContracts.Requests;
using HumanityService.Services.Interfaces;
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

        //[HttpGet("login")]
        //public async Task<IActionResult> Login(LoginRequest request)
        //{

        //}

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout(LogoutRequest request)
        //{

        //}

        //[HttpGet("refresh-access-token")]
        //public async Task<IActionResult> RefreshAccessToken(RefreshTokenRequest request)
        //{

        //}
    }
}
