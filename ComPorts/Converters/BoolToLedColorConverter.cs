using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ComPorts.Converters
{
    public class BoolToLedColorConverter : IValueConverter
    {
        private readonly SolidColorBrush ActivatedColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#E28413");
        private readonly SolidColorBrush DeactivatedColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#D6DBD2");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
            {
                throw new InvalidOperationException("The target must be a Brush");
            }

            return (bool)value ? ActivatedColor : DeactivatedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
