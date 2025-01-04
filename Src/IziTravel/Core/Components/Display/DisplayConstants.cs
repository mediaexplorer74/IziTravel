// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Display.DisplayConstants
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Display
{
  public static class DisplayConstants
  {
    public const double AspectRatio16To9 = 1.7777777777777777;
    public const double AspectRatio15To9 = 1.6666666666666667;
    public static readonly double DiagonalToWidthRatio16To9 = 9.0 / Math.Sqrt(Math.Pow(16.0, 2.0) + Math.Pow(9.0, 2.0));
    public static readonly double DiagonalToWidthRatio15To9 = 9.0 / Math.Sqrt(Math.Pow(15.0, 2.0) + Math.Pow(9.0, 2.0));
    public const double BaselineDiagonalInInches15To9HighRes = 4.5;
    public const double BaselineDiagonalInInches15To9LoRes = 4.0;
    public const double BaselineDiagonalInInches16To9 = 4.3;
    internal static readonly double BaselineWidthInInches = 4.5 * DisplayConstants.DiagonalToWidthRatio15To9;
    internal const int BaselineWidthInViewPixels = 480;
  }
}
