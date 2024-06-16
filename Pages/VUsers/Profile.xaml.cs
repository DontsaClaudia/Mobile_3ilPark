using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System;
using System.Xml;

namespace Mobile_3ilPark
{
    public partial class Profile : ContentPage
    {
        private readonly ApiService _apiService;
        public Users User { get; set; }

        string token = Preferences.Get("AuthToken", string.Empty);

        public Profile()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadUserDataAsync();
        }

        private async void LoadUserDataAsync()
        {
            try
            {
                var userId = Preferences.Get("UserId", 0); 
                if (userId != 0)
                {
                    var user = await _apiService.GetUserAsync(userId, token);
                    if (user != null)
                    {
                        User = user;
                        BindUserData();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Unable to load user data.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "User not logged in.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void BindUserData()
        {
            BindingContext = User;

            NameLabel.Text = User.Name;
            var loginDate = Preferences.Get("LoginDate", string.Empty);
            JoinedDateLabel.Text = !string.IsNullOrEmpty(loginDate) ? $"Joined {loginDate}" : "Joined on an unknown date";
            UserNameLabel.Text = User.Name;
            UserEmailLabel.Text = User.Email;
            UserPhoneLabel.Text = User.PhoneNumber;

            // Initiales pour l'avatar
            if (!string.IsNullOrEmpty(User.Name))
            {
                AvatarLabel.Text = string.Join("", User.Name.Split(' ')[0][0], User.Name.Split(' ')[1][0]);
            }
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirm)
            {
                
                Preferences.Remove("AuthToken");
                Preferences.Remove("UserId");
                Preferences.Remove("LoginDate");

                
                Application.Current.MainPage = new NavigationPage(new Loginxaml());
            }
        }
    }
}