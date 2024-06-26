using System.ComponentModel;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;

namespace CryptoApp.ViewModels
{
    public class LocalizationViewModel : INotifyPropertyChanged
    {
        private ResourceLoader _resourceLoader;

        public LocalizationViewModel()
        {
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string SearchPlaceholder => _resourceLoader.GetString("SearchPlaceholder");
        public string SearchButton => _resourceLoader.GetString("SearchButton");
        public string DarkThemeButton => _resourceLoader.GetString("DarkThemeButton");
        public string LightThemeButton => _resourceLoader.GetString("LightThemeButton");

        public string Back => _resourceLoader.GetString("Back");
        public string PriceUsd => _resourceLoader.GetString("PriceUsd");
        public string VolumeUsd24Hr => _resourceLoader.GetString("VolumeUsd24Hr");
        public string ChangePercent24Hr => _resourceLoader.GetString("ChangePercent24Hr");
        public string Markets => _resourceLoader.GetString("Markets");
        public void UpdateLanguage(string languageCode)
        {
            ApplicationLanguages.PrimaryLanguageOverride = languageCode;
            _resourceLoader = ResourceLoader.GetForCurrentView();
            OnPropertyChanged(string.Empty);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
