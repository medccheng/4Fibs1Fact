using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using TileMeUpDomain.Models;
using static System.Net.WebRequestMethods;

namespace TileMeUpMobile.Data
{
    public class ClosetService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        const string api_url = @"https://localhost:7027/api/Closet/{0}{1}";

        public ClosetService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Closet>> GetClosetsAsync(int userId)
        {
            var closets = new List<Closet>();

            Uri uri = new Uri(string.Format(api_url,"GetByCreator/", userId));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    closets = JsonSerializer.Deserialize<List<Closet>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return closets;
        }

        public async Task<List<Closet>> GetAll(int? _page = null)
        {
            var closets = new List<Closet>();

            Uri uri = new Uri(string.Format(api_url, _page, null));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    closets = JsonSerializer.Deserialize<List<Closet>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return closets;
        }

        public async Task<Closet> CreateClosetAsync(Closet closet)
        {
            Uri uri = new Uri(string.Format(api_url, "Create", null));
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(uri, closet);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    closet = JsonSerializer.Deserialize<Closet>(content, _serializerOptions);                    
                }
                else
                    closet.ErrorMessage = response.StatusCode.ToString();

                return closet;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }
    }
}