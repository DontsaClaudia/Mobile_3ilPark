using Microsoft.Maui.Controls;

namespace Mobile_3ilPark
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//MainPage");


        }
    }
}