using Microsoft.Maui.Controls;



namespace Mobile_3ilPark
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("ParkPage", typeof(ParkPage));
            Routing.RegisterRoute("ParkDetailPage", typeof(ParkDetailPage));
            Routing.RegisterRoute("ComputersPage", typeof(ComputersPage));
            Routing.RegisterRoute("ComputersDetailPage", typeof(ComputersDetailPage));
            Routing.RegisterRoute("RoomsPage", typeof(RoomsPage));
            Routing.RegisterRoute("Profile", typeof(Profile));
            Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
            
        }
    }
}