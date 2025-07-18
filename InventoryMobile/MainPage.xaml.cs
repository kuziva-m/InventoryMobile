using InventoryMobile.ViewModels;

namespace InventoryMobile;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // The ViewModel now handles its own initialization logic.
        if (BindingContext is MainViewModel vm)
        {
            await vm.InitializeAsync();
        }
    }
}