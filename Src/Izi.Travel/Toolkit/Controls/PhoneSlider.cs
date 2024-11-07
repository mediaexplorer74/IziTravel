// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.PhoneSlider
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls
{
  public class PhoneSlider : Slider
  {
    private bool _isSettingValue;
    public static readonly DependencyProperty TickFrequencyProperty = DependencyProperty.Register(nameof (TickFrequency), typeof (double), typeof (PhoneSlider), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(PhoneSlider.OnTickFrequencyChanged)));

    public PhoneSlider() => this.DefaultStyleKey = (object) typeof (PhoneSlider);

    public double TickFrequency
    {
      get => (double) this.GetValue(PhoneSlider.TickFrequencyProperty);
      set => this.SetValue(PhoneSlider.TickFrequencyProperty, (object) value);
    }

    private static void OnTickFrequencyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!PhoneSlider.IsValidChange(e.NewValue))
        throw new ArgumentException(PhoneSlider.TickFrequencyProperty.ToString());
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
      if (!this._isSettingValue && this.TickFrequency > 0.0)
      {
        double num1 = Math.Round(newValue / this.TickFrequency) * this.TickFrequency;
        double num2 = Math.Abs(num1 - newValue) >= Math.Abs(this.Maximum - newValue) ? this.Maximum : num1;
        if (newValue != num2)
        {
          this._isSettingValue = true;
          this.Value = num2;
          this._isSettingValue = false;
          return;
        }
      }
      base.OnValueChanged(oldValue, newValue);
    }

    private static bool IsValidDoubleValue(object value)
    {
      double d = (double) value;
      return !double.IsNaN(d) && !double.IsInfinity(d);
    }

    private static bool IsValidChange(object value)
    {
      return PhoneSlider.IsValidDoubleValue(value) && (double) value >= 0.0;
    }
  }
}
