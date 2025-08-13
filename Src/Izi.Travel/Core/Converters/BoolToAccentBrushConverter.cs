// ********************************************************************
// Type: Izi.Travel.Core.Converters.BoolToAccentBrushConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Core.Converters
{
    /// <summary>
    /// Converts a boolean value to an accent brush based on the application resources.
    /// </summary>
    public sealed class BoolToAccentBrushConverter : IValueConverter
    {
        private const string DarkBrushKey = "IziTravelDarkBrush";
        private const string BlueBrushKey = "IziTravelBlueBrush";
        private const string DefaultBrushKey = "SystemControlBackgroundBaseMediumBrush";

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // If parameter is "Inverse", invert the boolean value
            if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase) && value is bool boolValue)
                value = !boolValue;

            // Get the appropriate brush resource based on the boolean value
            var resourceKey = value is bool flag && flag ? BlueBrushKey : DarkBrushKey;

            // Try to get the brush from application resources, fall back to a default if not found
            if (Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is SolidColorBrush brush)
                return brush;

            // Fallback to a default brush if the requested one is not found
            return Application.Current.Resources[DefaultBrushKey] as SolidColorBrush ?? new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
}
