using Microsoft.Maui.Controls;


namespace Mobile_3ilPark
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("Parcs", typeof(Parcs));
            Routing.RegisterRoute("Profile", typeof(Profile));
            Routing.RegisterRoute("ComputersPage", typeof(ComputersPage));
            Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
            
        }
    }
}