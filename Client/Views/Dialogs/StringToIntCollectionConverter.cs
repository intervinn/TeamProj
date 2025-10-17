using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Client.Views.Dialogs;

public class StringToIntCollectionConverter : IValueConverter
{
    public static StringToIntCollectionConverter Default => new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ICollection<int> collection)
        {
            return string.Join(",", collection);
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            return str.Split(',').Select(s => int.TryParse(s.Trim(), out int i) ? i : 0).ToList();
        }
        return new List<int>();
    }
}
