using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using TileMeUpDomain.Models;
using static System.Net.WebRequestMethods;

namespace TileMeUpMobile.Data
{
    public class WallService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        const string api_url = @"https://localhost:7027/api/Wall/{0}{1}";

        public WallService()
        { 
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Wall>> GetAll(int? _page=null)
        {
            var walls = new List<Wall>();

            Uri uri = new Uri(string.Format(api_url, _page, null));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    walls = JsonSerializer.Deserialize<List<Wall>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return walls;
        }

        public async Task<List<Wall>> GetWallsAsync(int userId)
        {
            var walls = new List<Wall>();

            Uri uri = new Uri(string.Format(api_url, "GetByCreator/", userId));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    walls = JsonSerializer.Deserialize<List<Wall>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return walls;
        }

        public async Task<Wall> GetWall(int wallId)
        {
            var wall = new Wall();

            Uri uri = new Uri(string.Format(api_url, "Get/", wallId));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    wall = JsonSerializer.Deserialize<Wall>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return wall;
        }

        public async Task<Wall> CreateWallAsync(Wall wall)
        {
            Uri uri = new Uri(string.Format(api_url, "Create", null));
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(uri, wall);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    wall = JsonSerializer.Deserialize<Wall>(content, _serializerOptions);                    
                }
                else
                    wall.ErrorMessage = response.StatusCode.ToString();

                return wall;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }
    }
}