using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Domain.Enums;

namespace UI.Tasks.Converters
{
    internal class PriorityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (value is not ProjectPriorities priority)
            {
                brush.Color = Color.FromRgb(255, 255, 255);
                return brush;
            }

            brush.Color = ConvertPriorityToColor(priority);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color ConvertPriorityToColor(ProjectPriorities priority) =>
            priority switch
            {
                ProjectPriorities.Critical => Color.FromRgb(211, 93, 110),
                ProjectPriorities.High => Color.FromRgb(239, 176, 140),
                ProjectPriorities.Normal => Color.FromRgb(90, 164, 105),
                _ => Color.FromRgb(197, 215, 189),
            };
    }
}