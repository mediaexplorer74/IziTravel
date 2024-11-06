// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.DoubleExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class DoubleExtensions
  {
    public static double CheckBound(this double value, double maximum)
    {
      return value.CheckBound(0.0, maximum);
    }

    public static double CheckBound(this double value, double minimum, double maximum)
    {
      if (value <= minimum)
        value = minimum;
      else if (value >= maximum)
        value = maximum;
      return value;
    }

    public static bool AlmostEquals(this double a, double b, double precision = 4.94065645841247E-324)
    {
      return Math.Abs(a - b) <= precision;
    }
  }
}
