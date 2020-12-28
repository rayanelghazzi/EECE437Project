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
            // Tries if user password is matching with database and returns boolean
            try
            {
                var user = await _userService.GetUser(username);
                return new AuthenticationResult
                {
                    PasswordIsValid = user.Password == password
                };
            }
            catch
            {
                return new AuthenticationResult
                {
                    PasswordIsValid = false
                };
            }
        
        }

        public async Task<AuthenticationResult> LoginNgo(string username, string password)
        {
            // Tries if user password is matching with database and returns boolean
            try
            {
                var ngo = await _userService.GetNgo(username);
                return new AuthenticationResult
                {
                    PasswordIsValid = ngo.Password == password
                };
            }
            catch
            {
                return new AuthenticationResult
                {
                    PasswordIsValid = false
                };
            }
        }
    }
}
