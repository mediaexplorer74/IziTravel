// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Primitives.TimeSpanPickerBasePage
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Primitives
{
  public abstract class TimeSpanPickerBasePage : 
    ValuePickerBasePage<TimeSpan>,
    ITimeSpanPickerPage<TimeSpan>,
    IValuePickerPage<TimeSpan>
  {
    protected override ValueWrapper<TimeSpan> GetNewWrapper(TimeSpan? value)
    {
      return (ValueWrapper<TimeSpan>) new TimeSpanWrapper(value.GetValueOrDefault(TimeSpan.FromMinutes(30.0)));
    }

    public TimeSpan Maximum { get; set; }

    public TimeSpan Minimum { get; set; }

    public TimeSpan StepFrequency { get; set; }

    public override TimeSpan? Value
    {
      set
      {
        if (!value.HasValue)
          return;
        base.Value = new TimeSpan?(value.Value.CheckBound(this.Minimum, this.Maximum));
      }
    }
  }
}
