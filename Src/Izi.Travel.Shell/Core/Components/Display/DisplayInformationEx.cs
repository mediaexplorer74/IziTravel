// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Display.DisplayInformationEx
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Info;
using System;
using System.ComponentModel;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Display
{
  public class DisplayInformationEx
  {
    private static readonly string RawDpiValueName = "RawDpiX";
    private static readonly string PhysicalScreenResolutionName = "PhysicalScreenResolution";

    public double PhysicalDiagonal { get; private set; }

    public Size PhysicalSize { get; private set; }

    public Size PhysicalResolution { get; private set; }

    public Size ViewResolution { get; private set; }

    public double ViewPixelsPerInch { get; private set; }

    public double RawDpi { get; private set; }

    public double AspectRatio { get; private set; }

    public double RawPixelsPerViewPixel { get; private set; }

    public double AbsoluteScaleFactorBeforeNormalizing { get; private set; }

    public double ViewPixelsPerHostPixel { get; private set; }

    public double HostPixelsPerViewPixel { get; private set; }

    public DisplayInformationSource InformationSource { get; private set; }

    public double GetViewPixelsForPhysicalSize(double inches) => inches * this.ViewPixelsPerInch;

    public static DisplayInformationEx Default { get; private set; }

    public DisplayInformationEx()
    {
      this.PhysicalDiagonal = DisplayInformationEx.Default.PhysicalDiagonal;
      this.PhysicalSize = DisplayInformationEx.Default.PhysicalSize;
      this.PhysicalResolution = DisplayInformationEx.Default.PhysicalResolution;
      this.ViewResolution = DisplayInformationEx.Default.ViewResolution;
      this.ViewPixelsPerInch = DisplayInformationEx.Default.ViewPixelsPerInch;
      this.RawDpi = DisplayInformationEx.Default.RawDpi;
      this.AspectRatio = DisplayInformationEx.Default.AspectRatio;
      this.RawPixelsPerViewPixel = DisplayInformationEx.Default.RawPixelsPerViewPixel;
      this.AbsoluteScaleFactorBeforeNormalizing = DisplayInformationEx.Default.AbsoluteScaleFactorBeforeNormalizing;
      this.ViewPixelsPerHostPixel = DisplayInformationEx.Default.ViewPixelsPerHostPixel;
      this.HostPixelsPerViewPixel = DisplayInformationEx.Default.HostPixelsPerViewPixel;
      this.InformationSource = DisplayInformationEx.Default.InformationSource;
    }

    public DisplayInformationEx(Size physicalSize, Size physicalResolution)
      : this(physicalSize, physicalResolution, DisplayInformationSource.Custom)
    {
    }

    private DisplayInformationEx(
      Size physicalSize,
      Size physicalResolution,
      DisplayInformationSource informationSource)
    {
      this.PhysicalSize = physicalSize;
      this.PhysicalDiagonal = this.PhysicalSize.GetHypotenuse();
      this.PhysicalResolution = physicalResolution;
      this.AspectRatio = physicalSize.Height / physicalSize.Width;
      if (!this.AspectRatio.IsCloseEnoughTo(physicalResolution.Height / physicalResolution.Width))
        throw new ArgumentOutOfRangeException(nameof (physicalResolution), "only square pixels supported");
      this.RawDpi = physicalResolution.Width / physicalSize.Width;
      this.AbsoluteScaleFactorBeforeNormalizing = this.PhysicalSize.Width / DisplayConstants.BaselineWidthInInches;
      this.RawPixelsPerViewPixel = this.GenerateRawPixelsPerViewPixel();
      double width = this.PhysicalResolution.Width / this.RawPixelsPerViewPixel;
      Size size = this.PhysicalResolution;
      double height = size.Height / this.RawPixelsPerViewPixel;
      this.ViewResolution = new Size(width, height);
      this.ViewPixelsPerInch = this.RawDpi / this.RawPixelsPerViewPixel;
      size = this.ViewResolution;
      double val1 = size.Width / Application.Current.Host.Content.ActualWidth;
      size = this.ViewResolution;
      double val2 = size.Height / Application.Current.Host.Content.ActualHeight;
      this.ViewPixelsPerHostPixel = Math.Min(val1, val2);
      this.HostPixelsPerViewPixel = 1.0 / this.ViewPixelsPerHostPixel;
      this.InformationSource = informationSource;
    }

    static DisplayInformationEx()
    {
      if (!DesignerProperties.IsInDesignTool)
        DisplayInformationEx.Default = DisplayInformationEx.CreateForHardwareOrLegacyFallback();
      else
        DisplayInformationEx.Default = new DisplayInformationEx(SizeHelpers.MakeSizeFromDiagonal(4.5, 5.0 / 3.0), SizeHelpers.MakeSize(SizeHelpers.WxgaPhysicalResolution.Width, 5.0 / 3.0), DisplayInformationSource.DesignTimeFallback);
    }

    private static DisplayInformationEx CreateForHardwareOrLegacyFallback()
    {
      object propertyValue;
      if (!DeviceExtendedProperties.TryGetValue(DisplayInformationEx.PhysicalScreenResolutionName, out propertyValue))
        return DisplayInformationEx.CreateForLegacyHardware();
      Size physicalResolution = (Size) propertyValue;
      if (!DeviceExtendedProperties.TryGetValue(DisplayInformationEx.RawDpiValueName, out propertyValue) || (double) propertyValue == 0.0)
        return DisplayInformationEx.CreateForLegacyHardware();
      double num = (double) propertyValue;
      return new DisplayInformationEx(new Size(physicalResolution.Width / num, physicalResolution.Height / num), physicalResolution, DisplayInformationSource.Hardware);
    }

    private static DisplayInformationEx CreateForLegacyHardware()
    {
      double num = (double) Application.Current.Host.Content.ScaleFactor / 100.0;
      double width1 = Application.Current.Host.Content.ActualWidth * num;
      double height = Application.Current.Host.Content.ActualHeight * num;
      double d1 = height / width1;
      Size physicalResolution = new Size(width1, height);
      double width2 = !d1.IsCloseEnoughTo(5.0 / 3.0) ? 4.3 * DisplayConstants.DiagonalToWidthRatio16To9 : (num <= 1.0 ? 4.0 * DisplayConstants.DiagonalToWidthRatio15To9 : 4.5 * DisplayConstants.DiagonalToWidthRatio15To9);
      return new DisplayInformationEx(new Size(width2, width2 * d1), physicalResolution, DisplayInformationSource.LegacyDefault);
    }

    private double GenerateRawPixelsPerViewPixel()
    {
      return (this.PhysicalResolution.Width / Math.Min(480.0 * Math.Max(1.0, this.PhysicalSize.Width / DisplayConstants.BaselineWidthInInches), this.PhysicalResolution.Width)).NudgeToClosestPoint(1);
    }
  }
}
