using Microsoft.Maui.Controls;
using Mobile_3ilPark.Services;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mobile_3ilPark
{
    public partial class Loginxaml : ContentPage
    {
        private readonly ApiService _apiService;

        public Loginxaml()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var email = emailEntry.Text;
            var password = passwordEntry.Text;

           

            // Valider l'email
            if (!IsValidEmail(email))
            {
                emailErrorLabel.Text = "Please enter a valid email address.";
                emailErrorLabel.IsVisible = true;
                return;
            }

            // Valider le mot de passe
            if (!IsValidPassword(password))
            {
                passwordErrorLabel.Text = "Password must contain at least one uppercase letter, one digit, and one special character.";
                passwordErrorLabel.IsVisible = true;
                return;
            }

            emailErrorLabel.IsVisible = false;
            passwordErrorLabel.IsVisible = false;

            var result = await _apiService.LoginAsync(email, password);

            if (result.IsSuccess)
            {
                // Save the token or any other data as needed
                Preferences.Set("AuthToken", result.accessToken);
                Preferences.Set("UserId", result.UserId);
                Preferences.Set("LoginDate", DateTime.Now.ToString("MMMM dd, yyyy"));

                // Naviguer vers la WelcomePage après une connexion réussie
                await Navigation.PushAsync(new WelcomePage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Mot de passe doit contenir au moins une majuscule, un chiffre et un caractère spécial
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasDigits = new Regex(@"[0-9]+");
            var hasSpecialChars = new Regex(@"[\W_]+");

            return hasUpperCase.IsMatch(password) && hasLowerCase.IsMatch(password) && hasDigits.IsMatch(password) && hasSpecialChars.IsMatch(password);
        }

        
    }
}