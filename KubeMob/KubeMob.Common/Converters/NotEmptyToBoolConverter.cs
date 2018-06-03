using System;
using System.Collections;
using System.Globalization;
using System.Linq;
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
            switch (value)
            {
                case string s:
                    return !string.IsNullOrWhiteSpace(s);
                case IEnumerable e:
                    return e.GetEnumerator().MoveNext();
            }

            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
