using Windows.UI.Xaml.Controls;
using CryptoApp.Models;
using CryptoApp.Views;

namespace CryptoApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedCurrency = e.AddedItems[0] as Currency;
                if (selectedCurrency != null)
                {
                    Frame.Navigate(typeof(DetailPage), selectedCurrency);
                }
            }
        }
    }
}
