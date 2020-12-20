using System.Threading.Tasks;
using HumanityService.DataContracts.Requests;
using HumanityService.DataContracts.Results;
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

        [HttpGet("login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var result = await _authenticationService.LoginUser(request.Username, request.Password);
            return Ok(result);
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginNgo(LoginRequest request)
        {
            var result = await _authenticationService.LoginNgo(request.Username, request.Password);
            return Ok(result);
        }
    }
}
