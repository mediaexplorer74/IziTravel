// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TimeSpanDataSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public abstract class TimeSpanDataSource : DataSource<TimeSpan>
  {
    protected int Max;
    protected int Step;

    protected TimeSpanDataSource(int max, int step)
    {
      this.Max = max;
      this.Step = step;
    }

    public override bool IsEmpty => this.Max - 1 == 0 || this.Step == 0;

    protected int ComputeRelativeTo(int value, int delta)
    {
      int max = this.Max;
      return max <= 0 ? value : (max + value + delta * this.Step) % max + (max <= value ? max : 0);
    }
  }
}
