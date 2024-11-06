// Decompiled with JetBrains decompiler
// Type: BindableApplicationBar.Bindable
// Assembly: BindableApplicationBar, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A535FD52-CB2F-4C72-99D3-803485FA9E0A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BindableApplicationBar.dll

using Microsoft.Phone.Controls;
using System;
using System.Windows;

#nullable disable
namespace BindableApplicationBar
{
  public static class Bindable
  {
    public static readonly DependencyProperty ApplicationBarProperty = DependencyProperty.RegisterAttached("ApplicationBar", typeof (BindableApplicationBar.BindableApplicationBar), typeof (Bindable), new PropertyMetadata((object) null, new PropertyChangedCallback(Bindable.OnApplicationBarChanged)));

    public static BindableApplicationBar.BindableApplicationBar GetApplicationBar(DependencyObject d)
    {
      return (BindableApplicationBar.BindableApplicationBar) d.GetValue(Bindable.ApplicationBarProperty);
    }

    public static void SetApplicationBar(DependencyObject d, BindableApplicationBar.BindableApplicationBar value)
    {
      d.SetValue(Bindable.ApplicationBarProperty, (object) value);
    }

    private static void OnApplicationBarChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is PhoneApplicationPage parentPage))
        throw new InvalidOperationException("Bindable.ApplicationBar property needs to be set on a PhoneApplicationPage element.");
      BindableApplicationBar.BindableApplicationBar oldValue = (BindableApplicationBar.BindableApplicationBar) e.OldValue;
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d.GetValue(Bindable.ApplicationBarProperty);
      if (oldValue == bindableApplicationBar)
        return;
      oldValue?.Detach(parentPage);
      bindableApplicationBar?.Attach(parentPage);
    }
  }
}
