// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.FloatExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class FloatExtensions
  {
    public static double CheckBound(this float value, float maximum)
    {
      return value.CheckBound(0.0f, maximum);
    }

    public static double CheckBound(this float value, float minimum, float maximum)
    {
      if ((double) value <= (double) minimum)
        value = minimum;
      else if ((double) value >= (double) maximum)
        value = maximum;
      return (double) value;
    }

    public static bool AlmostEquals(this float a, float b, double precision = 1.4012984643248171E-45)
    {
      return (double) Math.Abs(a - b) <= precision;
    }
  }
}
