using HumanityService.Services.Interfaces;
using HumanityService.Stores.Interfaces;
using System;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IUserService _userService;

        public AuthenticationService(IRefreshTokenStore refreshTokenStore, IUserService userService)
        {
            _refreshTokenStore = refreshTokenStore;
            _userService = userService;
        }

        public Task Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout(string username, string accessTokenId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAccessToken(string username, string oldAccessToken, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
