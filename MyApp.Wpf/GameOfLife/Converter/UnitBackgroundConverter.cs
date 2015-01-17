using System;
using System.Windows.Data;
using System.Windows.Media;

namespace MyApp.Wpf.GameOfLife.Converter
{
    public class UnitBackgroundConverter : IValueConverter
    {
        private static readonly Brush whiteBrush = new SolidColorBrush(Colors.White);
        private static readonly Brush blackBrush = new SolidColorBrush(Colors.Black);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? whiteBrush : blackBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
