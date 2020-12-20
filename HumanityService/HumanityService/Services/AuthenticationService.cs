using HumanityService.DataContracts.Results;
using HumanityService.Services.Interfaces;
using HumanityService.Stores.Interfaces;
using System;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthenticationResult> LoginUser(string username, string password)
        {
            var user = await _userService.GetUser(username);
            return new AuthenticationResult
            {
                PasswordIsValid = user.Password == password
            };
        }

        public async Task<AuthenticationResult> LoginNgo(string username, string password)
        {
            var user = await _userService.GetNgo(username);
            return new AuthenticationResult
            {
                PasswordIsValid = user.Password == password
            };
        }
    }
}
