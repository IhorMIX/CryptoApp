using CryptoApp.Models;
using CryptoApp.ViewModels;
using CryptoApp.Views;
using System;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CryptoApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void DarkThemeButton_Click(object sender, RoutedEventArgs e)
        {
            var themeManager = (Application.Current.Resources["ThemeManager"] as ThemeManager);
            themeManager.LoadTheme(ThemeManager.DarkThemePath);
        }

        private void LightThemeButton_Click(object sender, RoutedEventArgs e)
        {
            var themeManager = (Application.Current.Resources["ThemeManager"] as ThemeManager);
            themeManager.LoadTheme(ThemeManager.LightThemePath);
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
