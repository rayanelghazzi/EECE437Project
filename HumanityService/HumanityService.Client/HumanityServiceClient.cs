using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HumanityService
{
    class Client
    {
        private readonly HttpClient _httpClient;
        private Uri baseAddress = new Uri("ENTER BASE URL HERE");

        public Client()
        {
            var httpClient = new HttpClient { BaseAddress = baseAddress };
            _httpClient = httpClient;
        }

        public async Task LoginUser(LoginRequest login)
        {

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
