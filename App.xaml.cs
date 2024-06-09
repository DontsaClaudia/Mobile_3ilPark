using Microsoft.Maui.Controls;
namespace Mobile_3ilPark
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Loginxaml());
        }
    }
}
