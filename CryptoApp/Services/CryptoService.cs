using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoApp.Services
{
    public class CryptoService
    {
        private readonly HttpClient _httpClient;

        public CryptoService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<Currency> GetCurrencyByNameOrCodeAsync(string searchTerm)
        {
            var url = $"https://api.coincap.io/v2/assets";
            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinCapResponse>(response);

            return data.Data.FirstOrDefault(c => c.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                  c.Symbol.Equals(searchTerm, StringComparison.OrdinalIgnoreCase));
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

        public async Task<decimal> GetConversionRateAsync(string fromCurrencySymbol, string toCurrencySymbol)
        {
            var fromCurrencyId = await GetCurrencyIdBySymbolAsync(fromCurrencySymbol);
            var toCurrencyId = await GetCurrencyIdBySymbolAsync(toCurrencySymbol);

            if (fromCurrencyId == null || toCurrencyId == null)
            {
                throw new Exception("One of the currency symbols could not be found.");
            }

            var url = $"https://api.coincap.io/v2/assets/{fromCurrencyId}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);
            var fromRateUsd = data["data"]["priceUsd"].Value<decimal>();

            url = $"https://api.coincap.io/v2/assets/{toCurrencyId}";
            response = await _httpClient.GetStringAsync(url);
            data = JObject.Parse(response);
            var toRateUsd = data["data"]["priceUsd"].Value<decimal>();

            return fromRateUsd / toRateUsd;
        }
        private async Task<string> GetCurrencyIdBySymbolAsync(string currencySymbol)
        {
            var url = $"https://api.coincap.io/v2/assets?search={currencySymbol}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var currency = data["data"]
                .FirstOrDefault(c => c["symbol"].Value<string>().Equals(currencySymbol, StringComparison.OrdinalIgnoreCase));

            return currency?["id"]?.Value<string>();
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
