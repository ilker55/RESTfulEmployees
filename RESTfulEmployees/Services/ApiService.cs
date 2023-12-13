using Newtonsoft.Json;
using RESTfulEmployeesLibrary.Models;
using RESTfulEmployeesLibrary.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RESTfulEmployees.Services
{
    internal class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://gorest.co.in/public/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IList<User>?> GetUsers(int page)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v2/users?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<User>>(json);
                }

                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
