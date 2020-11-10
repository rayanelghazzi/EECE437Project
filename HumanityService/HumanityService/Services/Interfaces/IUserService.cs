using HumanityService.DataContracts.NgoDataContracts;
using HumanityService.DataContracts.UserDataContracts;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<UserInfo> GetUserInfo(string username);
        Task DeleteUser(string username);
        Task UpdateUser(User user);
        Task AddNgo(Ngo ngo);
        Task<NgoInfo> GetNgoInfo(string ngo);
        Task DeleteNgo(string ngo);
        Task UpdateNgo(Ngo ngo);
    }
}
