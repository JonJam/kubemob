using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Converters
{
    [Preserve(AllMembers = true)]
    public class CommandToAccessoryConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is ICommand command
                && command.CanExecute(null))
            {
                return "DisclosureIndicator";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
