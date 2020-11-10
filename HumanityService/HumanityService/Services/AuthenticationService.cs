using HumanityService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout(string username, string accessToken, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAccessToken(string accessToken, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
