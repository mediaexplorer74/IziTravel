// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Extensions.FrameworkElementExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Extensions
{
  public static class FrameworkElementExtensions
  {
    public static readonly DependencyProperty MarginBottomProperty = DependencyProperty.RegisterAttached("MarginBottom", typeof (double), typeof (FrameworkElementExtensions), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(FrameworkElementExtensions.OnMarginBottomPropertyChanged)));

    private static void OnMarginBottomPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is FrameworkElement frameworkElement1) || !(e.NewValue is double))
        return;
      FrameworkElement frameworkElement2 = frameworkElement1;
      Thickness margin = frameworkElement2.Margin;
      double left = margin.Left;
      margin = frameworkElement1.Margin;
      double top = margin.Top;
      margin = frameworkElement1.Margin;
      double right = margin.Right;
      double newValue = (double) e.NewValue;
      frameworkElement2.Margin = new Thickness(left, top, right, newValue);
    }

    public static void SetMarginBottom(DependencyObject element, double value)
    {
      element.SetValue(FrameworkElementExtensions.MarginBottomProperty, (object) value);
    }

    public static double GetMarginBottom(DependencyObject element)
    {
      return (double) element.GetValue(FrameworkElementExtensions.MarginBottomProperty);
    }
  }
}
