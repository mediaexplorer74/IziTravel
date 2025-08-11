// ********************************************************************
// Type: Izi.Travel.Shell.Core.Converters.BoolToVisibilityConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
    /// <summary>
    /// Converts a boolean value to a Visibility value (Visible when true, Collapsed when false)
    /// </summary>
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool boolValue))
                return Visibility.Collapsed;
                
            // If parameter is "Inverse", invert the boolean value
            if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
                boolValue = !boolValue;
                
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Visibility visibility))
                return false;
                
            bool result = visibility == Visibility.Visible;
            // If parameter is "Inverse", invert the boolean value
            if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
                result = !result;
                
            return result;
        }
    }
}
