using Windows.UI.Xaml.Controls;
using CryptoApp.ViewModels;
using Windows.UI.Xaml;

namespace CryptoApp.Views
{
    public sealed partial class CurrencyConverterPage : Page
    {
        private readonly CurrencyConverterViewModel _viewModel;

        public CurrencyConverterPage()
        {
            this.InitializeComponent();
            _viewModel = new CurrencyConverterViewModel();
            DataContext = _viewModel;
        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Convert button clicked");
            await _viewModel.ConvertCurrencyAsync();
        }

        private void SetEnglishLanguage_Click(object sender, RoutedEventArgs e)
        {
            var localizationViewModel = (LocalizationViewModel)this.Resources["LocalizationViewModel"];
            localizationViewModel.UpdateLanguage("en-US");
        }

        private void SetUkrainianLanguage_Click(object sender, RoutedEventArgs e)
        {
            var localizationViewModel = (LocalizationViewModel)this.Resources["LocalizationViewModel"];
            localizationViewModel.UpdateLanguage("uk-UA");
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
