using HumanityService.DataContracts.NgoDataContracts;
using HumanityService.DataContracts.UserDataContracts;
using HumanityService.Services.Interfaces;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class UserService : IUserService
    {
        public Task AddNgo(Ngo ngo)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteNgo(string ngo)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteUser(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task<NgoInfo> GetNgoInfo(string ngo)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserInfo> GetUserInfo(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateNgo(Ngo ngo)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
