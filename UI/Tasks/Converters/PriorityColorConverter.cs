using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Domain.Enums;
using UI.Tasks.Constants;

namespace UI.Tasks.Converters
{
    public class PriorityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (value is not ProjectPriorities priority)
            {
                brush.Color = PriorityColors.Default;
                return brush;
            }

            brush.Color = ConvertPriorityToColor(priority);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Color ConvertPriorityToColor(ProjectPriorities priority)
        {
            return priority switch
            {
                ProjectPriorities.Critical => PriorityColors.Critical,
                ProjectPriorities.High => PriorityColors.High,
                ProjectPriorities.Normal => PriorityColors.Normal,
                ProjectPriorities.Minor => PriorityColors.Minor,
                _ => PriorityColors.Default,
            };
        }
    }
}