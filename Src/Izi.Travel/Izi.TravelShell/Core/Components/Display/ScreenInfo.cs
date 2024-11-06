// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Display.ScreenInfo
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Display
{
  public class ScreenInfo
  {
    public static readonly ScreenInfo NullValue = new ScreenInfo(0.0, 0.0, 0.0);

    public Size PhysicalSize { get; private set; }

    public Size PhysicalResolution { get; private set; }

    public double AspectRatio { get; private set; }

    public ScreenInfo(double physicalDiagonal, double physicalResolutionWidth, double aspectRatio)
      : this(SizeHelpers.MakeSizeFromDiagonal(physicalDiagonal, aspectRatio), SizeHelpers.MakeSize(physicalResolutionWidth, aspectRatio))
    {
    }

    public ScreenInfo(Size physicalSize, Size physicalResolution)
    {
      this.PhysicalSize = physicalSize;
      this.PhysicalResolution = physicalResolution;
      if (this.PhysicalResolution.Width == 0.0)
      {
        this.AspectRatio = 0.0;
      }
      else
      {
        this.AspectRatio = physicalSize.Height / physicalSize.Width;
        if (!this.AspectRatio.IsCloseEnoughTo(physicalResolution.Height / physicalResolution.Width))
          throw new ArgumentOutOfRangeException(nameof (physicalResolution), "only square pixels supported");
      }
    }
  }
}
