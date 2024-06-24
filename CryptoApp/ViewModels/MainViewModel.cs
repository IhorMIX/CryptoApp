using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CryptoApp.Models;
using CryptoApp.Services;
using System;

namespace CryptoApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CryptoService _cryptoService;
        private ObservableCollection<Currency> _allCurrencies;
        private ObservableCollection<Currency> _currencies;
        private string _searchTerm;

        public MainViewModel()
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

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChanged();
                    ExecuteSearchCommand();
                }
            }
        }

        private async Task InitializeCurrenciesAsync()
        {
            var currencies = await _cryptoService.GetTopCurrenciesAsync();
            _allCurrencies = new ObservableCollection<Currency>(currencies);
            Currencies = new ObservableCollection<Currency>(currencies);
        }

        private void ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                Currencies = new ObservableCollection<Currency>(_allCurrencies);
            }
            else
            {
                var filteredCurrencies = _allCurrencies
                    .Where(c => c.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                c.Symbol.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Currencies = new ObservableCollection<Currency>(filteredCurrencies);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
