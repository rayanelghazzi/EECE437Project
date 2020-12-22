using HumanityService.DataContracts;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HumanityService.Client
{
    class HumanityServiceClient
    {
        private readonly HttpClient _httpClient;
        private Uri baseAddress = new Uri("ENTER BASE URL HERE");

        public HumanityServiceClient()
        {
            var httpClient = new HttpClient { BaseAddress = baseAddress };
            _httpClient = httpClient;
        }

        public async Task SignUp(User user)
        {

        }

        public async Task GetUserInfo(string username)
        {

        }


        public async Task SignUp(Ngo ngo)
        {

        }

        public async Task GetNgoInfo(string username)
        {
            var responseMessage = await _httpClient.GetAsync($"api/profile/{username}");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var fetchedStudent = JsonConvert.DeserializeObject<User>(json);
            return fetchedStudent;
        }

        public async Task<AuthenticationResult> LoginUser(LoginRequest loginRequest)
        {
            string json = JsonConvert.SerializeObject(loginRequest);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/authentication/login-user")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(request);
            await EnsureSuccessOrThrowAsync(responseMessage);
            string recievedJson = await responseMessage.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<AuthenticationResult>(recievedJson);
            return authResult;
        }

        public async Task<AuthenticationResult> LoginNgo(LoginRequest loginRequest)
        {
            string json = JsonConvert.SerializeObject(loginRequest);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/authentication/login-ngo")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(request);
            await EnsureSuccessOrThrowAsync(responseMessage);
            string recievedJson = await responseMessage.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<AuthenticationResult>(recievedJson);
            return authResult;
        }


        public async Task<Campaign> GetCampaign(string campaignId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/transactions/campaigns/{campaignId}");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var fetchedCampaign = JsonConvert.DeserializeObject<Campaign>(json);
            return fetchedCampaign;
        }

        public async Task<GetCampaignsResult> GetCampaigns(GetCampaignsRequest request)
        {
            var responseMessage = await _httpClient.GetAsync($"api/transactions/campaigns");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var getCampaignsResult = JsonConvert.DeserializeObject<GetCampaignsResult>(json);
            return getCampaignsResult;
        }

        public async Task MatchCampaign(MatchCampaignRequest request)
        {
            var responseMessage = await _httpClient.GetAsync($"api/transactions/campaigns/match");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var getCampaignsResult = JsonConvert.DeserializeObject<GetCampaignsResult>(json);
            return getCampaignsResult;
        }

        public async Task CreateCampaign(CreateCampaignRequest request)
        {

        }

        public async Task AnswerCampaign(string campaignId, AnswerCampaignRequest request)
        {

        }

        public async Task EditCampaign(string campaignId, EditCampaignRequest request)
        {

        }

        public async Task CancelCampaign(string campaignId)
        {

        }

        public async Task<DeliveryDemand> GetDeliveryDemand(string deliveryDemandId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/transactions/delivery-demands/{deliveryDemandId}");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var fetchedDeliveryDemand = JsonConvert.DeserializeObject<DeliveryDemand>(json);
            return fetchedDeliveryDemand;
        }

        public async Task GetDeliveryDemands(GetDeliveryDemandsRequest request)
        {

        }

        public async Task MatchDeliveryDemand(MatchDeliveryDemandRequest request)
        {

        }

        public async Task AnswerDeliveryDemand(string deliveryDemandId, AnswerDeliveryDemandRequest request)
        {

        }

        public async Task<Contribution> GetContribution(string contributionId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/transactions/contributions/{contributionId}");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var fetchedContribution = JsonConvert.DeserializeObject<Contribution>(json);
            return fetchedContribution;
        }

        public async Task GetContributions(GetContributionsRequest request)
        {

        }

        public async Task ApproveContribution(string contributionId)
        {

        }

        public async Task CancelContribution(string contributionId)
        {

        }

        public async Task<Process> GetProcess(string processId)
        {
            var responseMessage = await _httpClient.GetAsync($"api/transactions/processes/{processId}");
            await EnsureSuccessOrThrowAsync(responseMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var fetchedProcess = JsonConvert.DeserializeObject<Process>(json);
            return fetchedProcess;
        }

        public async Task GetProcesses(GetProcessesRequest request)
        {

        }

        public async Task ValidateDelivery(ValidateDeliveryRequest request)
        {

        }

        private static async Task EnsureSuccessOrThrowAsync(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                string message = $"{await responseMessage.Content.ReadAsStringAsync()}";
                throw new HumanityServiceException(message, responseMessage.StatusCode);
            }
        }

        //public async Task AddUser(User user)
        //{
        //    string json = JsonConvert.SerializeObject(user);
        //    HttpResponseMessage responseMessage = await _httpClient.PostAsync("api/profile", new StringContent(json, Encoding.UTF8,
        //        "application/json"));
        //    EnsureSuccessOrThrow(responseMessage);
        //}

        //public async Task<User> GetUser(string username)
        //{
        //    var responseMessage = await _httpClient.GetAsync($"api/profile/{username}");
        //    EnsureSuccessOrThrow(responseMessage);
        //    string json = await responseMessage.Content.ReadAsStringAsync();
        //    var fetchedStudent = JsonConvert.DeserializeObject<User>(json);
        //    return fetchedStudent;
        //}

        //public async Task UpdateUser(User user)
        //{
        //    var body = new UpdateUserRequestBody
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        ProfilePictureId = user.ProfilePictureId
        //    };

        //    string json = JsonConvert.SerializeObject(body);
        //    HttpResponseMessage responseMessage = await _httpClient.PutAsync($"api/profile/{user.Username}", new StringContent(json,
        //        Encoding.UTF8, "application/json"));
        //    EnsureSuccessOrThrow(responseMessage);
        //}
    }
}
