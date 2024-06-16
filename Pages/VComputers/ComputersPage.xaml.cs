using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_3ilPark
{
    public partial class ComputersPage : ContentPage
    {
        private readonly ApiService _apiService;
        private List<Computers> _allComputers;
        private ObservableCollection<Computers> _computers;
        string token = Preferences.Get("AuthToken", string.Empty);

        public ComputersPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadComputers();
        }

        private async void LoadComputers()
        {
            _allComputers = await _apiService.GetComputersAsync(token);
            _computers = new ObservableCollection<Computers>(_allComputers);
            ComputersCollectionView.ItemsSource = _computers;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue;
            ComputersCollectionView.ItemsSource = string.IsNullOrEmpty(keyword) ?
                _computers :
                new ObservableCollection<Computers>(_allComputers.Where(c => c.Name.ToLower().Contains(keyword.ToLower()) ||
                                                                              c.Manufacturer.ToLower().Contains(keyword.ToLower()) ||
                                                                              c.Model.ToLower().Contains(keyword.ToLower()) ||
                                                                              c.Caption.ToLower().Contains(keyword.ToLower())));
        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedComputer = e.CurrentSelection.FirstOrDefault() as Computers;
            if (selectedComputer != null)
            {
                await Navigation.PushAsync(new ComputersDetailPage(selectedComputer));
                ComputersCollectionView.SelectedItem = null; 

            }
        }
    }
}