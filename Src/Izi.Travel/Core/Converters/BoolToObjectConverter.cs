// ********************************************************************
// Type: Izi.Travel.Shell.Core.Converters.BoolToObjectConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  /// <summary>
  /// Converts a boolean value to one of two specified objects based on its value.
  /// </summary>
  public class BoolToObjectConverter : DependencyObject, IValueConverter
  {
    public static readonly DependencyProperty PositiveValueProperty = 
            DependencyProperty.Register(nameof (PositiveValue), typeof (object), 
                typeof (BoolToObjectConverter), new PropertyMetadata((object) null));
    public static readonly DependencyProperty NegativeValueProperty = 
            DependencyProperty.Register(nameof (NegativeValue), typeof (object), 
                typeof (BoolToObjectConverter), new PropertyMetadata((object) null));

    public object PositiveValue
    {
      get => this.GetValue(BoolToObjectConverter.PositiveValueProperty);
      set => this.SetValue(BoolToObjectConverter.PositiveValueProperty, value);
    }

    public object NegativeValue
    {
      get => this.GetValue(BoolToObjectConverter.NegativeValueProperty);
      set => this.SetValue(BoolToObjectConverter.NegativeValueProperty, value);
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value is bool boolValue)
      {
        // If parameter is "Inverse", invert the boolean value
        if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
          boolValue = !boolValue;
        
        return boolValue ? this.PositiveValue : this.NegativeValue;
      }
      return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      if (value == null)
        return false;
      
      bool isPositiveValue = value.Equals(this.PositiveValue);
      bool isNegativeValue = value.Equals(this.NegativeValue);
      
      // If parameter is "Inverse", invert the result
      if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
        return isNegativeValue;
      
      return isPositiveValue;
    }
  }
}
