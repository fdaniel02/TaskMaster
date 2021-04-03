using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using UI.Tasks.Constants;

namespace UI.Tasks.Converters
{
    public class DeadlineColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (!DateTime.TryParse(value?.ToString(), out var deadline))
            {
                brush.Color = DeadlineColors.Default;
                return brush;
            }

            var dayOffset = ConvertToWeekdays(DateTime.Now.Date, deadline.Date);
            brush.Color = ConvertDayOffsetToColor(dayOffset);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        // TODO: move to a separate class
        private int ConvertToWeekdays(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return -1;
            }

            var days = 0;
            while (startDate < endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                }

                startDate = startDate.AddDays(1);
            }

            return days;
        }

        private Color ConvertDayOffsetToColor(int offset)
        {
            return offset switch
            {
                _ when offset < 0 => DeadlineColors.Today,
                _ when offset < 1 => DeadlineColors.Tomorrow,
                _ when offset < 2 => DeadlineColors.TwoDays,
                _ when offset < 5 => DeadlineColors.OneWeek,
                _ => DeadlineColors.Default,
            };
        }
    }
}