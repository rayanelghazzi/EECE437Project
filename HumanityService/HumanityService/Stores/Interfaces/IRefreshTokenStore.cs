using HumanityService.DataContracts;
using System.Threading.Tasks;

namespace HumanityService.Stores.Interfaces
{
    public interface IRefreshTokenStore
    {
        Task AddRefreshToken(RefreshToken refreshToken);

        Task<RefreshToken> GetRefreshToken(string refreshTokenId, string username);

        Task DeleteRefreshToken(string refreshTokenId, string username);
    }
}
