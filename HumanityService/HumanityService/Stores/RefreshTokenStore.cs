using HumanityService.DataContracts;
using HumanityService.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Stores
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        public Task AddRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRefreshToken(string refreshTokenId, string username)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetRefreshToken(string refreshTokenId, string username)
        {
            throw new NotImplementedException();
        }
    }
}
