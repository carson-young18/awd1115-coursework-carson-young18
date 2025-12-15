using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace FinalProject.Models
{
    public class RiotApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        private const string PATCH_CACHE_KEY = "LatestPatch";

        public RiotApiService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<string> GetPatch()
        {
            if (_cache.TryGetValue(PATCH_CACHE_KEY, out string patch))
            {
                return patch;
            }


            var response = await _httpClient.GetAsync("https://ddragon.leagueoflegends.com/api/versions.json");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var versions = JsonSerializer.Deserialize<List<string>>(json);

            patch = versions!.First();

            _cache.Set(
                PATCH_CACHE_KEY,
                patch,
                TimeSpan.FromHours(1)
             );

            return patch;
        }
    }
}
