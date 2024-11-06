// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.SecondTimeSpanDataSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class SecondTimeSpanDataSource : TimeSpanDataSource
  {
    public SecondTimeSpanDataSource()
      : base(59, 1)
    {
    }

    public SecondTimeSpanDataSource(int max, int step)
      : base(max, step)
    {
    }

    protected override TimeSpan? GetRelativeTo(TimeSpan relativeDate, int delta)
    {
      return new TimeSpan?(new TimeSpan(relativeDate.Hours, relativeDate.Minutes, this.ComputeRelativeTo(relativeDate.Seconds, delta)));
    }
  }
}
