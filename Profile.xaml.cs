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
                var userId = Preferences.Get("UserId", 0); // Get the stored user ID
                if (userId != 0)
                {
                    var user = await _apiService.GetUserAsync(userId);
                    if (user != null)
                    {
                        BindUserData(user);
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

        private void BindUserData(Users user)
        {
            NameLabel.Text = user.Name;
            JoinedDateLabel.Text = $"Joined {DateTime.Now.ToString("MMMM dd, yyyy")}";
            UserNameLabel.Text = user.Name;
            UserEmailLabel.Text = user.Email;
            UserPhoneLabel.Text = user.PhoneNumber;

            // Initiales pour l'avatar
            if (!string.IsNullOrEmpty(user.Name))
            {
                AvatarLabel.Text = string.Join("", user.Name.Split(' ')[0][0], user.Name.Split(' ')[1][0]);
            }
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirm)
            {
                // Clear stored user information
                Preferences.Remove("AuthToken");
                Preferences.Remove("UserId");

                // Navigate back to the login page
                Application.Current.MainPage = new NavigationPage(new Loginxaml());
            }
        }
    }
}