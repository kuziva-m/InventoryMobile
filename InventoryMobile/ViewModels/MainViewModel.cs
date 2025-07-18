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
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            await LoadProductsAsync();
        }

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            try
            {
                Products.Clear();
                var products = await _inventoryService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ViewModel] Error loading products: {ex.Message}");
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