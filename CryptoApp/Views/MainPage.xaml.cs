using Windows.UI.Xaml.Controls;
using CryptoApp.Models;
using CryptoApp.Views;
using Windows.UI.Xaml;

namespace CryptoApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void DarkThemeButton_Click(object sender, RoutedEventArgs e)
        {
            App.ThemeManager.LoadTheme(ThemeManager.DarkThemePath);
        }
            
        private void LightThemeButton_Click(object sender, RoutedEventArgs e)
        {
            App.ThemeManager.LoadTheme(ThemeManager.LightThemePath);
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
