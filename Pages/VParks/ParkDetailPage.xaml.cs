using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System;

namespace Mobile_3ilPark
{
    public partial class ParkDetailPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly Parks _park;
        private readonly string _token = Preferences.Get("AuthToken", string.Empty);

        public ParkDetailPage(Parks park)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _park = park;
            BindingContext = _park;
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var newName = await DisplayPromptAsync("Edit Name", "Enter new name:", initialValue: _park.Name);
            var newAddress = await DisplayPromptAsync("Edit Address", "Enter new address:", initialValue: _park.Address);

            if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(newAddress))
            {
                _park.Name = newName;
                _park.Address = newAddress;
                await _apiService.UpdateParkAsync(_park.Id, _park, _token);
                await DisplayAlert("Success", "Park details updated successfully.", "OK");
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this park?", "Yes", "No");
            if (confirm)
            {
                await _apiService.DeleteParkAsync(_park.Id, _token);
                await DisplayAlert("Deleted", "Park deleted", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}