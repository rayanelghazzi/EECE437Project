using HumanityService.DataContracts;
using HumanityService.Stores;
using Microsoft.Extensions.Options;
using System;

namespace Client
{
    class Program1
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            SqlDatabaseSettings databaseSettings = new SqlDatabaseSettings
            {
                ConnectionString = "Server=DESKTOP-LSFHGPT;Database=HumanityServiceDb;Trusted_Connection=true"
            };

            IOptions<SqlDatabaseSettings> options = Options.Create(databaseSettings);
            SqlConnectionFactory connectionFactory = new SqlConnectionFactory(options);
            LocationStore locationStore = new LocationStore(connectionFactory);
            UserStore store = new UserStore(connectionFactory, locationStore);

            var location = new Location
            {
                Coordinates = "9w83r09q397-e097r-90-9er909re",
                Description = "Bsalim, facing MEIH, 2nd building to the left, 2nd floor"
            };

            var user = new User
            {
                FirstName = "Leonel",
                LastName = "Messi",
                Email = "leomessi@hotmail.com",
                Username = "leomessi",
                Password = "Abcdefg1234",
                PhoneNumber = "70467876",
                Location = location
            };

            //await store.AddUser(user);
            //var getuser = await store.GetUser("leomessi");
            await store.DeleteUser("leomessi");
            //Console.WriteLine(getuser.Location.Description);
            
        }
    }
}
