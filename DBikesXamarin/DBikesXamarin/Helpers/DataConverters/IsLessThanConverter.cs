using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DBikesXamarin
{
    public class IsLessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Messy casts required, "value" needs casting from obj to int, "parameter" is in quotes so is trickier
           if((int)value <= int.Parse(parameter.ToString())) { 
                return true; 
            }
            else return false; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
