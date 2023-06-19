using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverters
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRainning = (bool)value;
            if (isRainning)
                return "Currently raining";
            return "Currently not raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRainning = (string)value;
            if (isRainning == "Currently raining")
                return true;
            return false;
        }
    }
}
