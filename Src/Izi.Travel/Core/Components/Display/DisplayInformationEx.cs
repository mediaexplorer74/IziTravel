// ********************************************************************
// Type: Izi.Travel.Core.Components.Display.DisplayInformationEx
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel;
#nullable disable
namespace Izi.Travel.Core.Components.Display
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
      // Copy from Default instance
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

    private DisplayInformationEx(Size physicalSize, Size physicalResolution, DisplayInformationSource informationSource)
    {
      this.PhysicalSize = physicalSize;
      this.PhysicalDiagonal = this.PhysicalSize.GetHypotenuse();
      this.PhysicalResolution = physicalResolution;
      this.AspectRatio = physicalSize.Height / physicalSize.Width;
      this.RawDpi = physicalResolution.Width / physicalSize.Width;
      this.AbsoluteScaleFactorBeforeNormalizing = this.PhysicalSize.Width / DisplayConstants.BaselineWidthInInches;
      // In UWP, view pixels are Effective Pixels
      double rawPpv = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
      this.RawPixelsPerViewPixel = rawPpv;
      this.ViewResolution = new Size(physicalResolution.Width / rawPpv, physicalResolution.Height / rawPpv);
      this.ViewPixelsPerInch = this.RawDpi / this.RawPixelsPerViewPixel;
      this.ViewPixelsPerHostPixel = 1.0;
      this.HostPixelsPerViewPixel = 1.0;
      this.InformationSource = informationSource;
    }

    static DisplayInformationEx()
    {
      if (!DesignMode.DesignModeEnabled)
        DisplayInformationEx.Default = DisplayInformationEx.CreateForCurrentView();
      else
        DisplayInformationEx.Default = new DisplayInformationEx(SizeHelpers.MakeSizeFromDiagonal(4.5, 5.0 / 3.0), SizeHelpers.MakeSize(SizeHelpers.WxgaPhysicalResolution.Width, 5.0 / 3.0), DisplayInformationSource.DesignTimeFallback);
    }

    private static DisplayInformationEx CreateForCurrentView()
    {
      var di = DisplayInformation.GetForCurrentView();
      double rawPpv = di.RawPixelsPerViewPixel;
      var bounds = Window.Current.Bounds;
      // Effective (view) resolution in EP
      var viewResolution = new Size(bounds.Width, bounds.Height);
      // Physical resolution in raw pixels
      var physicalResolution = new Size(viewResolution.Width * rawPpv, viewResolution.Height * rawPpv);
      double rawDpi = di.RawDpiX;
      if (rawDpi <= 0)
      {
        // Fallback: approximate DPI from scale
        rawDpi = 96.0 * rawPpv;
      }
      var physicalSize = new Size(physicalResolution.Width / rawDpi, physicalResolution.Height / rawDpi);
      return new DisplayInformationEx(physicalSize, physicalResolution, DisplayInformationSource.Hardware);
    }

    private static DisplayInformationEx CreateForLegacyHardware()
    {
      // UWP fallback: assume 5" 16:9 device if data is unavailable
      var di = DisplayInformation.GetForCurrentView();
      double rawPpv = di.RawPixelsPerViewPixel;
      var bounds = Window.Current.Bounds;
      var viewResolution = new Size(bounds.Width, bounds.Height);
      var physicalResolution = new Size(viewResolution.Width * rawPpv, viewResolution.Height * rawPpv);
      double widthInches = 5.0 * DisplayConstants.DiagonalToWidthRatio16To9;
      var physicalSize = new Size(widthInches, widthInches * (physicalResolution.Height / physicalResolution.Width));
      return new DisplayInformationEx(physicalSize, physicalResolution, DisplayInformationSource.LegacyDefault);
    }

    private double GenerateRawPixelsPerViewPixel() => DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
  }
}

