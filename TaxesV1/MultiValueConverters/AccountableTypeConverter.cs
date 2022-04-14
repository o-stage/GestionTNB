using System;
using System.Globalization;
using System.Windows.Data;
using TaxesV1.Resources;

namespace TaxesV1.MultiValueConverters
{
    public class AccountableTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value as string)
            {
                case "PF": return Strings.Individual;
                case "PM": return Strings.BodyCorporate;
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string type)
            {
                if (type == Strings.Individual) return "PF";
                if (type == Strings.BodyCorporate) return "PM";
            }

            return "";
        }
    }
}