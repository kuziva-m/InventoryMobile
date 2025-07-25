using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Core.Application.DTOs;
using Inventory.Core.Application.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InventoryMobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IInventoryService _inventoryService;

        // CORRECT: Using partial properties to fix the AOT/WinRT warning
        [ObservableProperty]
        private ObservableCollection<ProductDto> _products = new();

        [ObservableProperty]
        private bool _isLoading;

        private bool _isInitialized;

        public MainViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            Products.Add(new ProductDto { Name = "Test Product", CategoryName = "Test Category", VariantsDisplay = "Test Variant" });
            IsLoading = false;
            System.Diagnostics.Debug.WriteLine("[MainViewModel] Constructor called");
        }

        public async Task InitializeAsync()
        {
            System.Diagnostics.Debug.WriteLine("[MainViewModel] InitializeAsync called");
            if (_isInitialized)
                return;

            await MainThread.InvokeOnMainThreadAsync(() => IsLoading = true);
            
            try
            {
                await Task.Run(async () =>
                {
                    var products = await _inventoryService.GetAllProductsAsync();
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        Products.Clear();
                        foreach (var product in products)
                        {
                            Products.Add(product);
                        }
                    });
                });
                
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ViewModel] Error loading products: {ex.Message}");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Could not load products. Please try again.", "OK");
                    }
                });
            }
            finally
            {
                await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);
            }
        }

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            System.Diagnostics.Debug.WriteLine("[MainViewModel] LoadProductsAsync called");
            if (IsLoading)
                return;

            IsLoading = true;
            try
            {
                Products.Clear();
                var products = await _inventoryService.GetAllProductsAsync();
                System.Diagnostics.Debug.WriteLine($"[MainViewModel] Products loaded: {products.Count()}");
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ViewModel] Error loading products: {ex.Message}");
                // CORRECT: Added a null check to fix the compiler warning.
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Could not load products. Please try again.", "OK");
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}