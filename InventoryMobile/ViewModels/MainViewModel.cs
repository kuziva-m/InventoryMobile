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

        // CORRECT: Changed to partial properties to fix the AOT/WinRT warning
        [ObservableProperty]
        private ObservableCollection<ProductDto> _products = new();

        [ObservableProperty]
        private bool _isLoading;

        public MainViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            // Prevent multiple loads at the same time
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
                // Log the error to see what went wrong in the debug output
                Debug.WriteLine($"[ViewModel] Error loading products: {ex.Message}");
            }
            finally
            {
                // CORRECT: This ensures IsLoading is always set to false,
                // even if an error occurs.
                IsLoading = false;
            }
        }
    }
}