// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.TimeSpanExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class TimeSpanExtensions
  {
    public static TimeSpan CheckBound(this TimeSpan value, TimeSpan maximum)
    {
      return value.CheckBound(new TimeSpan(), maximum);
    }

    public static TimeSpan CheckBound(this TimeSpan value, TimeSpan minimum, TimeSpan maximum)
    {
      if (value < minimum)
        value = minimum;
      else if (value > maximum)
        value = maximum;
      return value;
    }
  }
}
