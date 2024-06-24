namespace CryptoApp.Models
{
    public class Market
    {
        public string ExchangeId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }
        public decimal PriceQuote { get; set; }
        public decimal priceUsd { get; set; }

    }
}

