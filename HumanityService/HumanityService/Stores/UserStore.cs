using HumanityService.DataContracts;
using HumanityService.Stores.Interfaces;
using System.Threading.Tasks;

namespace HumanityService.Stores
{
    public class UserStore : IUserStore
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

        public Task<Ngo> GetNgo(string ngo)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUser(string username)
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
