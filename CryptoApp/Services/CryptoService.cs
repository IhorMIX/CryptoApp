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

            foreach (var currency in data.Data)
            {
                currency.Markets = await GetCurrencyMarketsAsync(currency.Id);
            }

            return data.Data;
        }

        private async Task<List<Market>> GetCurrencyMarketsAsync(string currencyId, int limit = 10)
        {
            var url = $"https://api.coincap.io/v2/assets/{currencyId}/markets?limit={limit}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinCapMarketsResponse>(response);
            return data.Data;
        }


        private class CoinCapMarketsResponse
        {
            public List<Market> Data { get; set; }
        }

        private class CoinCapResponse
        {
            public List<Currency> Data { get; set; }
        }
    }
}
