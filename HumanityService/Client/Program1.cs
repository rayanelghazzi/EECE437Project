using HumanityService.DataContracts;
using HumanityService.Stores.Sql;
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
                ConnectionString = "Server=tcp:humanityservice.database.windows.net,1433;Initial Catalog=HumanityServiceDB;Persist Security Info=False;User ID=rayanelghazzi;Password=Abcdefg1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            };

            IOptions<SqlDatabaseSettings> options = Options.Create(databaseSettings);
            SqlConnectionFactory connectionFactory = new SqlConnectionFactory(options);
            SqlUserStore store = new SqlUserStore(connectionFactory);

            var user = new User
            {
                FirstName = "Leonel",
                LastName = "Messi",
                Email = "leomessi@hotmail.com",
                Username = "leomessi",
                Password = "Abcdefg1234",
                PhoneNumber = "70467876"

            };

            await store.AddUser(user);

        }
    }
}
