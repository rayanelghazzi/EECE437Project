using HumanityService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Stores.Interfaces
{
    public interface IUserStore
    {
        Task AddUser(User user);
        Task<User> GetUser(string username);
        Task DeleteUser(string username);
        Task UpdateUser(User user);

        Task AddNgo(Ngo ngo);
        Task<Ngo> GetNgo(string ngo);
        Task DeleteNgo(string ngo);
        Task UpdateNgo(Ngo ngo);
    }
}
