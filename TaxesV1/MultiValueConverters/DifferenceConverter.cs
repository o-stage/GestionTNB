using System;
using System.Globalization;
using System.Windows.Data;
using TaxesV1.Resources;

namespace TaxesV1.MultiValueConverters
{
    public class DifferenceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double && values[1] is double)
            {
                return ((double)values[0] - (double)values[1]).ToString();
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { };
        }
    }
}