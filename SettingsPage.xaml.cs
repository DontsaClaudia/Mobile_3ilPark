using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System;
using System.Linq;

namespace Mobile_3ilPark
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            notificationSwitch.IsToggled = Preferences.Get("EnableNotifications", true);
            string theme = Preferences.Get("AppTheme", "Light");
            themePicker.SelectedItem = theme;
        }

        private void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            Preferences.Set("EnableNotifications", notificationSwitch.IsToggled);
            Preferences.Set("AppTheme", themePicker.SelectedItem.ToString());

            ApplyTheme(themePicker.SelectedItem.ToString());
        }

        private void ApplyTheme(string theme)
        {
            if (theme == "Dark")
            {
                Application.Current.Resources = (ResourceDictionary)Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.GetType() == typeof(ResourceDictionary) && ((ResourceDictionary)d).MergedDictionaries.Any(md => md is ResourceDictionary rd && rd.Source.OriginalString.Contains("DarkTheme")));
            }
            else
            {
                Application.Current.Resources = (ResourceDictionary)Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.GetType() == typeof(ResourceDictionary) && ((ResourceDictionary)d).MergedDictionaries.Any(md => md is ResourceDictionary rd && rd.Source.OriginalString.Contains("LightTheme")));
            }
        }
    }
}