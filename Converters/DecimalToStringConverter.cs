using System.Globalization;
using System.Windows.Data;

namespace TheLittleBookNest.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
                return decimalValue.ToString("0.##", culture); // Omvandlar decimal till sträng
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && decimal.TryParse(stringValue, NumberStyles.Any, culture, out var result) && result > 0)
                return result; // Returnerar decimal om det är ett giltigt värde > 0
            return Binding.DoNothing; // Förhindrar fel vid ogiltig inmatning
        }
    }
}
