using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Mobile_3ilPark
{
    public partial class RoomsPage : ContentPage
    {
        private readonly ApiService _apiService;
        private List<Rooms> _allRooms;
        private ObservableCollection<Rooms> _rooms;

        public RoomsPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadRooms();
        }

        private async void LoadRooms()
        {
            _allRooms = await _apiService.GetRoomsAsync();
            _rooms = new ObservableCollection<Rooms>(_allRooms);
            RoomsCollectionView.ItemsSource = _rooms;
        }
        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            RoomsCollectionView.ItemsSource = string.IsNullOrEmpty(keyword) ?
                _rooms :
                new ObservableCollection<Rooms>(_allRooms.Where(r => r.Name.ToLower().Contains(keyword.ToLower())));
        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRoom = e.CurrentSelection.FirstOrDefault() as Rooms;
            if (selectedRoom != null)
            {
                await DisplayAlert("Room Selected", $"You selected: {selectedRoom.Name}", "OK");
                RoomsCollectionView.SelectedItem = null; // Clear selection
            }
        }
    }
}