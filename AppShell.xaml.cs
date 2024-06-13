using Microsoft.Maui.Controls;
using Mobile_3ilPark;


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
            Routing.RegisterRoute("ComputersDetailPage", typeof(ComputersDetailPage));
            Routing.RegisterRoute("RoomsPage", typeof(RoomsPage));
            Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
            
        }
    }
}