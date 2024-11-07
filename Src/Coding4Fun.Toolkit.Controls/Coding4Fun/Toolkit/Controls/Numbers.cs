// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Numbers
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public static class Numbers
  {
    public static float Max(params int[] numbers) => (float) ((IEnumerable<int>) numbers).Max();

    public static float Min(params int[] numbers) => (float) ((IEnumerable<int>) numbers).Min();

    public static float Max(params float[] numbers) => ((IEnumerable<float>) numbers).Max();

    public static float Min(params float[] numbers) => ((IEnumerable<float>) numbers).Min();

    public static double Max(params double[] numbers) => ((IEnumerable<double>) numbers).Max();

    public static double Min(params double[] numbers) => ((IEnumerable<double>) numbers).Min();
  }
}
