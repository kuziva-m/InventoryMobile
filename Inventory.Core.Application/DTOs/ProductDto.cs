using System.Collections.ObjectModel;

namespace Inventory.Core.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ObservableCollection<ProductVariantDto> Variants { get; set; } = new ObservableCollection<ProductVariantDto>();
        public string? OptionNames { get; set; }
        public string VariantsDisplay { get; set; } = string.Empty;
        public int TotalQuantity => Variants?.Sum(v => v.Quantity) ?? 0;
    }
}