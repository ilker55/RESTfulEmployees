using Newtonsoft.Json;
using RESTfulEmployeesLibrary.Models;
using RESTfulEmployeesLibrary.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

        /// <summary>
        /// Get all users for the given page with the given search text
        /// </summary>
        /// <param name="page">The page number</param>
        /// <param name="searchName">The name to search for</param>
        /// <returns>List of users</returns>
        public async Task<IList<User>?> GetUsers(int? page, string searchName)
        {
            try
            {
                // Create query parameters
                var queryParams = new Dictionary<string, string>();
                if (searchName != null)
                    queryParams.Add("name", searchName);
                if (page != null)
                    queryParams.Add("page", page.ToString()!);

                // Create uri with query parameters
                var uriBuilder = new UriBuilder("v2/users")
                {
                    Query = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"))
                };

                var response = await _httpClient.GetAsync(uriBuilder.Uri.AbsoluteUri);
                if (response.IsSuccessStatusCode)
                {
                    // Parse response
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

        /// <summary>
        /// Get a user for the given user ID
        /// </summary>
        /// <param name="id">The user ID</param>
        /// <returns>The user</returns>
        public async Task<User?> GetUser(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v2/users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    // Parse response
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(json);
                }

                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Creates an user for the given data
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The created user</returns>
        public async Task<User?> CreateUser(User user)
        {
            try
            {
                if (user == null)
                    return null;

                // Create content body
                var body = JsonContent.Create(new
                {
                    name = user.Name,
                    email = user.Email,
                    gender = user.Gender,
                    status = user.Status
                });

                var response = await _httpClient.PostAsync($"v2/users", body);
                if (response.IsSuccessStatusCode)
                {
                    // Parse response
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(json);
                }

                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Updates the user with the given data
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The updated user</returns>
        public async Task<User?> UpdateUser(User user)
        {
            try
            {
                if (user == null)
                    return null;

                // Create content body
                var body = JsonContent.Create(new
                {
                    name = user.Name,
                    email = user.Email,
                    gender = user.Gender,
                    status = user.Status
                });

                var response = await _httpClient.PutAsync($"v2/users/{user.Id}", body);
                if (response.IsSuccessStatusCode)
                {
                    // Parse response
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(json);
                }

                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Deletes the user for the given user ID
        /// </summary>
        /// <param name="id">The user ID</param>
        /// <returns>True on success, False on failure</returns>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"v2/users/{id}");
                if (response.IsSuccessStatusCode)
                    return true;

                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
