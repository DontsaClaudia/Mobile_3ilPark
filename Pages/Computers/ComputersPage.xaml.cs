using Microsoft.Maui.Controls;
using Mobile_3ilPark.Models;
using Mobile_3ilPark.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_3ilPark;

public partial class ComputersPage : ContentPage
{
    private readonly ApiService _apiService;
    private List<Computers> _allComputers;
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
        ComputersCollectionView.ItemsSource = _allComputers;

    } 

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue;
        ComputersCollectionView.ItemsSource = string.IsNullOrEmpty(keyword) ?
            _allComputers :
            _allComputers.Where(c => c.Name.ToLower().Contains(keyword.ToLower()) ||
                                      c.Manufacturer.ToLower().Contains(keyword.ToLower()) ||
                                      c.Model.ToLower().Contains(keyword.ToLower()) ||
                                      c.Caption.ToLower().Contains(keyword.ToLower()));
    }
}