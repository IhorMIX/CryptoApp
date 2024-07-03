using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CryptoApp.Models;
using CryptoApp.Services;
using System;

namespace CryptoApp.ViewModels
{
    public class CurrencyConverterViewModel : INotifyPropertyChanged
    {
        private readonly CryptoService _cryptoService;
        private ObservableCollection<Currency> _currencies;
        private Currency _fromCurrency;
        private Currency _toCurrency;
        private string _amount;
        private string _result;

        public CurrencyConverterViewModel()
        {
            _cryptoService = new CryptoService();
            _ = InitializeCurrenciesAsync();
        }

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }

        public Currency FromCurrency
        {
            get => _fromCurrency;
            set
            {
                _fromCurrency = value;
                OnPropertyChanged();
            }
        }

        public Currency ToCurrency
        {
            get => _toCurrency;
            set
            {
                _toCurrency = value;
                OnPropertyChanged();
            }
        }

        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        private async Task InitializeCurrenciesAsync()
        {
            var currencies = await _cryptoService.GetTopCurrenciesAsync();
            Currencies = new ObservableCollection<Currency>(currencies);
        }

        public async Task ConvertCurrencyAsync()
        {
            if (decimal.TryParse(Amount, out decimal amount))
            {
                var rate = await _cryptoService.GetConversionRateAsync(FromCurrency.Symbol, ToCurrency.Symbol);
                var convertedAmount = amount * rate;
                Result = $"{Amount} {FromCurrency.Symbol} = {convertedAmount:F2} {ToCurrency.Symbol}";
            }
            else
            {
                Result = "Invalid sum";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
