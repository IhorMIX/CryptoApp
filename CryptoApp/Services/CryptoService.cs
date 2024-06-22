using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoApp.Models;
using Newtonsoft.Json;

namespace CryptoApp.Services
{
    public class CryptoService
    {
        private readonly HttpClient _httpClient;

        public CryptoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Currency>> GetTopCurrenciesAsync(int limit = 10)
        {
            var url = $"https://api.coincap.io/v2/assets?limit={limit}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinCapResponse>(response);
            return data.Data;
        }

        private class CoinCapResponse
        {
            public List<Currency> Data { get; set; }
        }
    }
}
