using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using TileMeUpDomain.Models;
using static System.Net.WebRequestMethods;

namespace TileMeUpMobile.Data
{
    public class UserService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        const string api_url = @"http://localhost:8080/api/User/{0}{1}";

        public UserService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = new User();

            Uri uri = new Uri(string.Format(api_url, "Get/", userId));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<User>(content, _serializerOptions);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                user.ErrorMessage = ex.Message;
            }
            return user;
        }

        public async Task<List<User>> GetAll(int? _page=null)
        {
            var users = new List<User>();

            Uri uri = new Uri(string.Format(api_url, _page, null));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    users = JsonSerializer.Deserialize<List<User>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return users;
        }


        public async Task<User> CreateUserAsync(User user)
        {
            Uri uri = new Uri(string.Format(api_url, "Create", null));
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(uri, user);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<User>(content, _serializerOptions);
                }
                else
                    user.ErrorMessage = response.StatusCode.ToString();

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }            
        }
    }
}