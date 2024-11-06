// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.IntExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class IntExtensions
  {
    public static double CheckBound(this int value, int maximum) => value.CheckBound(0, maximum);

    public static double CheckBound(this int value, int minimum, int maximum)
    {
      if (value <= minimum)
        value = minimum;
      else if (value >= maximum)
        value = maximum;
      return (double) value;
    }
  }
}
