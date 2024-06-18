using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System.Collections.ObjectModel;


namespace Mobile_3ilPark
{
    public partial class ParkPage : ContentPage
    {
        private readonly ApiService _apiService;
        private List<Parks> _allParks;
        private ObservableCollection<Parks> _parks;
        string token = Preferences.Get("AuthToken", string.Empty);

        public ParkPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadParks();
        }

        private async void LoadParks()
        {
            _allParks = await _apiService.GetParksAsync();
            _parks = new ObservableCollection<Parks>(_allParks);
            ParksCollectionView.ItemsSource = _parks;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            ParksCollectionView.ItemsSource = string.IsNullOrEmpty(keyword) ?
                _parks :
                new ObservableCollection<Parks>(_allParks.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) ||
                                                                     p.Address.ToLower().Contains(keyword.ToLower())));
        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPark = e.CurrentSelection.FirstOrDefault() as Parks;
            if (selectedPark != null)
            {
                await Navigation.PushAsync(new ParkDetailPage(selectedPark));
                ParksCollectionView.SelectedItem = null;
            }
        }
        
    }
}
