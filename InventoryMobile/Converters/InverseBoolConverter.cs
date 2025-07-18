using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace InventoryMobile.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        // CORRECT: Updated signatures to allow for nullable parameters
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !(bool)value!;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !(bool)value!;
        }
    }
}