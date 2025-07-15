using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Core.Application.DTOs;
using Inventory.Core.Application.Interfaces;
using Inventory.Core.Domain.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace InventoryMobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IInventoryService _inventoryService;

        [ObservableProperty]
        private ObservableCollection<ProductDto> _products;

        [ObservableProperty]
        private bool _isLoading;

        public MainViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _products = new ObservableCollection<ProductDto>();
        }

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            IsLoading = true;
            Products.Clear();
            var products = await _inventoryService.GetAllProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
            }
            IsLoading = false;
        }
    }
}