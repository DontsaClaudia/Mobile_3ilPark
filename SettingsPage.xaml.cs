using Microsoft.Maui.Controls;

namespace Mobile_3ilPark
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            // Charger les param�tres existants (si disponible)
            notificationSwitch.IsToggled = Preferences.Get("EnableNotifications", true);
            themePicker.SelectedItem = Preferences.Get("AppTheme", "Light");
        }

        private void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            // Enregistrer les param�tres
            Preferences.Set("EnableNotifications", notificationSwitch.IsToggled);
            Preferences.Set("AppTheme", themePicker.SelectedItem.ToString());

            DisplayAlert("Settings", "Settings saved successfully", "OK");
        }
    }
}