using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;

namespace Mobile_3ilPark
{
    public partial class ComputersDetailPage : ContentPage
    {
        private readonly ApiService _apiService;
        private Computers _computer;

        public ComputersDetailPage(Computers computer)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _computer = computer;
            BindingContext = _computer;
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            // Implémentez la logique de modification ici
            await DisplayAlert("Edit", $"Editing: {_computer.Name}", "OK");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {_computer.Name}?", "Yes", "No");
            if (confirm)
            {
                await _apiService.DeleteComputerAsync(_computer.Id);
                await DisplayAlert("Deleted", $"{_computer.Name} has been deleted.", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}