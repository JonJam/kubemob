using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Converters
{
    [Preserve(AllMembers = true)]
    public class NotEmptyToBoolConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool notEmpty = false;

            switch (value)
            {
                case string s:
                    notEmpty = !string.IsNullOrWhiteSpace(s);
                    break;
                case IEnumerable e:
                    notEmpty = e.GetEnumerator().MoveNext();
                    break;
                default:
                    notEmpty = value != null;
                    break;
            }

            return bool.TryParse(parameter?.ToString(), out bool flip) && flip ? !notEmpty : notEmpty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
