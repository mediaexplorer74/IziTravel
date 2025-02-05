// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Display.MathHelpers
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Display
{
  public static class MathHelpers
  {
    public const double Epsilon = 0.001;

    public static bool IsCloseEnoughTo(this double d1, double d2) => Math.Abs(d1 - d2) < 0.001;

    public static bool IsCloseEnoughOrSmallerThan(this double d1, double d2) => d1 < d2 + 0.001;

    public static double NudgeToClosestPoint(this double currentValue, int nudgeValue)
    {
      return Math.Floor(currentValue * 10.0 / (double) nudgeValue + 0.001) / 10.0 * (double) nudgeValue;
    }
  }
}
