using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.Services;
using HumanityService.Stores;
using Microsoft.Extensions.Options;

namespace Client
{
    class Program1
    {
        static public User CreateUser1()
        {
            var locationUser = new Location
            {
                Coordinates = "9w83r09q397-e097r-90-9er909re",
                Description = "Jounieh, facing IHJB, 2nd building to the left, 2nd floor"
            };

            return new User
            {
                FirstName = "Chirstiano",
                LastName = "Ronaldo",
                Email = "cr7@hotmail.com",
                Username = "crisronaldo",
                Password = "Abcdefg1234",
                PhoneNumber = "70467873",
                Location = locationUser
            };
        }

        static public User CreateUser2()
        {
            var locationUser = new Location
            {
                Coordinates = "9w83r09q397-e097r-90-9e2389re",
                Description = "Bsalim, facing IHJB, 2nd building to the left, 2nd floor"
            };

            return new User
            {
                FirstName = "leo",
                LastName = "messi",
                Email = "leomessi@hotmail.com",
                Username = "leomessi",
                Password = "Abcdefg1234",
                PhoneNumber = "71467873",
                Location = locationUser
            };
        }

        static public Ngo CreateNgo()
        {
            var locationNgo = new Location
            {
                Coordinates = "9w83r09q397-e097r-9323er909re",
                Description = "Beirut, Sassine Square, Starbucks building, 2nd floor"
            };

            return new Ngo
            {
                Name = "Tamanna",
                RegistrationNumber = "8934098340",
                Email = "tamanna@hotmail.com",
                Username = "tamanna",
                Password = "Abcdefg1234",
                PhoneNumber = "70467886",
                WebsiteAddress = "tamanna.com",
                Description = "We aim to help homeless kids have a better future",
                Location = locationNgo
            };
        }

        static public CreateCampaignRequest CreateCampaignRequest()
        {
            return new CreateCampaignRequest
            {
                Name = "Clothes Donation Campaign for the Homeless",
                NgoUsername = "tamanna",
                NgoName = "Tamanna",
                Type = "Donation",
                Category = "Clothes",
                Target = 1000,
                Description = "During winter, more than 5000 children in beirut and its suburbs suffer from extreme conditions ..."
            };
        }

        //static public AnswerCampaignRequest CreateAnswerCampaignRequest()
        //{
        //    return new AnswerCampaignRequest
        //    {
        //        Username = "leomessi",
        //        CampaignId = "fd081e82-521d-4f32-868d-58a3d3f60baf",
        //        TimeWindowStart = 160800000,
        //        TimeWindowEnd = 1608034525,
        //        OtherInfo = "I have 3 bags of clothes to donate"
        //    };
        //}

        //static  public AnswerDeliveryDemandRequest CreateAnswerDeliveryDemandRequest()
        //{
        //    return new AnswerDeliveryDemandRequest
        //    {
        //        Username = "crisronaldo",
        //        DeliveryDemandId = "11bf96d8-99fe-40f9-a84e-36576065462c",
        //        TimeWindowStart = 161800000,
        //        TimeWindowEnd = 1608030000,
        //        OtherInfo = ""
        //    };
        //}

        static public ValidateDeliveryRequest CreateValidateDeliveryRequestPickup()
        {
            return new ValidateDeliveryRequest()
            {
                ValidationType = "Pickup",
                ContributionId = "241e4943-a9c3-4de9-9225-dc500c18cd88",
                DeliveryCode = "214746"
            };
        }

        static public ValidateDeliveryRequest CreateValidateDeliveryRequestDestination()
        {
            return new ValidateDeliveryRequest()
            {
                ValidationType = "Destination",
                CampaignId = "fd081e82-521d-4f32-868d-58a3d3f60baf",
                DeliveryCode = "214746"
            };
        }

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            SqlDatabaseSettings databaseSettings = new SqlDatabaseSettings
            {
                ConnectionString = "Server=DESKTOP-LSFHGPT;Database=HumanityServiceDb;Trusted_Connection=true"
            };

            IOptions<SqlDatabaseSettings> options = Options.Create(databaseSettings);
            SqlConnectionFactory connectionFactory = new SqlConnectionFactory(options);
            LocationStore locationStore = new LocationStore(connectionFactory);
            UserStore userStore = new UserStore(connectionFactory, locationStore);
            TransactionStore transactionStore = new TransactionStore(connectionFactory, locationStore);
            TransactionService transactionService = new TransactionService(transactionStore);


            var user1 = CreateUser1();
            var user2 = CreateUser2();
            var ngo = CreateNgo();

            var createCampaignRequest = CreateCampaignRequest();
            //var answerCamapaignRequest = CreateAnswerCampaignRequest();
            //var answerDeliveryDemandRequest = CreateAnswerDeliveryDemandRequest();
            var validateDeliveryRequestPickup = CreateValidateDeliveryRequestPickup();
            var validateDeliveryRequestDestination = CreateValidateDeliveryRequestDestination();

            //await userStore.AddNgo(ngo);
            //await userStore.AddUser(user1);
            //await userStore.AddUser(user2);
            //await transactionService.CreateCampaign(createCampaignRequest);


            //await transactionService.AnswerCampaign(answerCamapaignRequest);


            //await transactionService.AnswerDeliveryDemand(answerDeliveryDemandRequest);

            //await transactionService.ValidateDelivery(validateDeliveryRequestPickup);
            await transactionService.ValidateDelivery(validateDeliveryRequestDestination);
            

            //var getuser = await userStore.GetUser("leomessi");
            //await userStore.DeleteUser("leomessi");
            //Console.WriteLine(getuser.Location.Description);

            //await userStore.DeleteProcess()

        }

    }
}
