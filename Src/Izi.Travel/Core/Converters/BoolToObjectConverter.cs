// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Converters.BoolToObjectConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Core.Converters
{
  public class BoolToObjectConverter : DependencyObject, IValueConverter
  {
    public static readonly DependencyProperty PositiveValueProperty = DependencyProperty.Register(nameof (PositiveValue), typeof (object), typeof (BoolToObjectConverter), new PropertyMetadata((object) null));
    public static readonly DependencyProperty NegativeValueProperty = DependencyProperty.Register(nameof (NegativeValue), typeof (object), typeof (BoolToObjectConverter), new PropertyMetadata((object) null));

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

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return !(value is bool flag) || !flag ? this.NegativeValue : this.PositiveValue;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
