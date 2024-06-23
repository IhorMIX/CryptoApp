using Windows.UI.Xaml.Controls;
using CryptoApp.Models;

namespace CryptoApp.Views
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Currency currency)
            {
                DataContext = currency;
            }
        }
        private void BackButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
