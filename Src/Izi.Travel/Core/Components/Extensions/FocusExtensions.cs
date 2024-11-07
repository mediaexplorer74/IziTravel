// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Extensions.FocusExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Extensions
{
  public class FocusExtensions
  {
    public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof (bool), typeof (FocusExtensions), new PropertyMetadata((object) false, new PropertyChangedCallback(FocusExtensions.OnIsFocusedPropertyChanged)));

    public static bool GetIsFocused(Control control)
    {
      return control != null ? (bool) control.GetValue(FocusExtensions.IsFocusedProperty) : throw new ArgumentNullException(nameof (control));
    }

    public static void SetIsFocused(Control control, bool value)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      control.SetValue(FocusExtensions.IsFocusedProperty, (object) value);
    }

    private static void OnIsFocusedPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Control control) || !(bool) e.NewValue)
        return;
      control.Focus();
    }
  }
}
