using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Tasks.Converters
{
    public class DeadlineColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (!DateTime.TryParse(value?.ToString(), out DateTime deadline))
            {
                brush.Color = Color.FromRgb(255, 255, 255);
                return brush;
            }

            var dayOffset = ConvertToWeekdays(DateTime.Now, deadline);
            brush.Color = ConvertDayOffsetToColor(dayOffset);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        // TODO: move to a separate class
        private int ConvertToWeekdays(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return -1;
            }

            var days = 0;
            while (startDate <= endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                }

                startDate = startDate.AddDays(1);
            }

            return days;
        }

        private Color ConvertDayOffsetToColor(int offset) =>
            offset switch
            {
                _ when offset < 0 => Color.FromRgb(140, 0, 0),
                _ when offset < 1 => Color.FromRgb(189, 32, 0),
                _ when offset < 2 => Color.FromRgb(250, 30, 14),
                _ when offset < 5 => Color.FromRgb(255, 190, 15),
                _ => Color.FromRgb(155, 155, 155),
            };
    }
}