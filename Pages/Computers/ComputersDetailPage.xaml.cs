using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System;
using System.Threading.Tasks;

namespace Mobile_3ilPark
{
    [QueryProperty(nameof(ComputerId), "computerId")]
    public partial class ComputersDetailPage : ContentPage
    {
        private readonly ApiService _apiService;
        private Computers _computer;
        public int ComputerId { get; set; }

        public ComputersDetailPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadComputerDetails();
        }

        private async Task LoadComputerDetails()
        {
            Console.WriteLine($"Loading details for computer ID: {ComputerId}");
            _computer = await _apiService.GetComputerByIdAsync(ComputerId);
            if (_computer != null)
            {
                BindingContext = _computer;
                Console.WriteLine($"Loaded details for computer: {_computer.Name}");
            }
            else
            {
                Console.WriteLine("Failed to load computer details.");
            }
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            string newName = await DisplayPromptAsync("Edit Computer", "Enter new name", initialValue: _computer.Name);
            if (!string.IsNullOrEmpty(newName))
            {
                _computer.Name = newName;
                await _apiService.UpdateComputerAsync(_computer);
                BindingContext = null;
                BindingContext = _computer;
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Delete Computer", "Are you sure you want to delete this computer?", "Yes", "No");
            if (confirm)
            {
                await _apiService.DeleteComputerAsync(_computer.Id);
                await Shell.Current.GoToAsync(".."); // Navigate back
            }
        }
    }
}