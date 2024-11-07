// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TimeSpanPicker
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using Coding4Fun.Toolkit.Controls.Primitives;
using System;
using System.Globalization;
using System.Windows;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class TimeSpanPicker : ValuePickerBase<TimeSpan>
  {
    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof (Max), typeof (TimeSpan), typeof (ValuePickerBase<TimeSpan>), new PropertyMetadata((object) TimeSpan.FromHours(24.0), new PropertyChangedCallback(TimeSpanPicker.OnLimitsChanged)));
    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof (Min), typeof (TimeSpan), typeof (ValuePickerBase<TimeSpan>), new PropertyMetadata((object) TimeSpan.Zero, new PropertyChangedCallback(TimeSpanPicker.OnLimitsChanged)));
    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(nameof (Step), typeof (TimeSpan), typeof (ValuePickerBase<TimeSpan>), new PropertyMetadata((object) TimeSpan.FromSeconds(1.0)));

    public TimeSpanPicker()
    {
      this.DefaultStyleKey = (object) typeof (TimeSpanPicker);
      this.Value = new TimeSpan?(TimeSpan.FromMinutes(30.0));
      this.DialogTitle = ValuePickerResources.TimeSpanPickerTitle;
    }

    protected internal override void UpdateValueString()
    {
      if (this.Value.HasValue)
      {
        TimeSpan time = this.Value.Value;
        if (!string.IsNullOrEmpty(this.ValueStringFormat))
        {
          this.ValueString = TimeSpanFormat.Format(time, this.ValueStringFormat);
          return;
        }
      }
      this.ValueString = string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.ValueStringFormat ?? this.ValueStringFormatFallback, (object) this.Value);
    }

    public TimeSpan Max
    {
      get => (TimeSpan) this.GetValue(TimeSpanPicker.MaxProperty);
      set => this.SetValue(TimeSpanPicker.MaxProperty, (object) value);
    }

    public TimeSpan Min
    {
      get => (TimeSpan) this.GetValue(TimeSpanPicker.MinProperty);
      set => this.SetValue(TimeSpanPicker.MinProperty, (object) value);
    }

    private static void OnLimitsChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is TimeSpanPicker timeSpanPicker))
        return;
      timeSpanPicker.ValidateBounds();
    }

    private void ValidateBounds()
    {
      if (this.Min < TimeSpan.Zero)
        this.Min = TimeSpan.Zero;
      if (this.Max > TimeSpan.MaxValue)
        this.Max = TimeSpan.MaxValue;
      if (this.Max < this.Min)
        this.Max = this.Min;
      if (this.Value.HasValue)
        this.Value = new TimeSpan?(this.Value.Value.CheckBound(this.Min, this.Max));
      else
        this.Value = new TimeSpan?(this.Min);
    }

    public TimeSpan Step
    {
      get => (TimeSpan) this.GetValue(TimeSpanPicker.StepProperty);
      set => this.SetValue(TimeSpanPicker.StepProperty, (object) value);
    }

    protected override void NavigateToNewPage(object page)
    {
      if (page is ITimeSpanPickerPage<TimeSpan> timeSpanPickerPage)
      {
        timeSpanPickerPage.Minimum = this.Min;
        timeSpanPickerPage.Maximum = this.Max;
        timeSpanPickerPage.StepFrequency = this.Step;
      }
      base.NavigateToNewPage(page);
    }
  }
}
