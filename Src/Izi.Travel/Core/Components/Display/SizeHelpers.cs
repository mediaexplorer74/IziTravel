// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Display.SizeHelpers
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Display
{
  public static class SizeHelpers
  {
    public static readonly Size WvgaPhysicalResolution = new Size(480.0, 800.0);
    public static readonly Size Hd720PhysicalResolution = new Size(720.0, 1280.0);
    public static readonly Size WxgaPhysicalResolution = new Size(768.0, 1280.0);
    public static readonly Size FullHd1080PhysicalResolution = new Size(1080.0, 1920.0);

    public static double GetHypotenuse(this Size rect)
    {
      return Math.Sqrt(Math.Pow(rect.Width, 2.0) + Math.Pow(rect.Height, 2.0));
    }

    public static Size Scale(this Size size, double scaleFactor)
    {
      Size size1 = new Size();
      double height = size.Height;
      size1.Height = double.IsInfinity(height) ? height : height * scaleFactor;
      double width = size.Width;
      size1.Width = double.IsInfinity(width) ? width : width * scaleFactor;
      return size1;
    }

    public static double GetWidthInInchesFromDiagonal(double diagonal, double aspectRatio)
    {
      if (aspectRatio.IsCloseEnoughTo(16.0 / 9.0))
        return diagonal * DisplayConstants.DiagonalToWidthRatio16To9;
      if (aspectRatio.IsCloseEnoughTo(5.0 / 3.0))
        return diagonal * DisplayConstants.DiagonalToWidthRatio15To9;
      if (aspectRatio.IsCloseEnoughTo(0.0))
        return 0.0;
      throw new ArgumentOutOfRangeException(nameof (aspectRatio));
    }

    public static string GetFriendlyAspectRatio(double aspectRatio)
    {
      if (aspectRatio.IsCloseEnoughTo(16.0 / 9.0))
        return "16:9";
      if (aspectRatio.IsCloseEnoughTo(5.0 / 3.0))
        return "15:9";
      if (aspectRatio.IsCloseEnoughTo(0.0))
        return "N/A";
      throw new ArgumentOutOfRangeException(nameof (aspectRatio));
    }

    public static double GetAspectRatioFromFriendlyName(string aspectRatio)
    {
      if (aspectRatio.Trim() == "16:9")
        return 16.0 / 9.0;
      return aspectRatio.Trim() == "15:9" ? 5.0 / 3.0 : 0.0;
    }

    public static double GetDiagonalFromWidth(double width, double aspectRatio)
    {
      return Math.Sqrt(Math.Pow(width, 2.0) + Math.Pow(width * aspectRatio, 2.0));
    }

    public static Size MakeSize(double width, double aspectRatio)
    {
      double width1 = width;
      return new Size(width1, width1 * aspectRatio);
    }

    public static Size MakeSizeFromDiagonal(double diagonal, double aspectRatio)
    {
      return SizeHelpers.MakeSize(SizeHelpers.GetWidthInInchesFromDiagonal(diagonal, aspectRatio), aspectRatio);
    }

    public static Size Round(this Size normalSize)
    {
      return new Size((double) (int) normalSize.Width, (double) (int) normalSize.Height);
    }
  }
}
