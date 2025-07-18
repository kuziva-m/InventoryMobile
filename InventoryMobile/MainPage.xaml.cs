using InventoryMobile.ViewModels;
using System.Diagnostics;

namespace InventoryMobile;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await _viewModel.LoadProductsCommand.ExecuteAsync(null);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading products: {ex}");
            await DisplayAlert("Error", "Could not load products. Please try again later.", "OK");
        }
    }
}
