// ********************************************************************
// Type: Izi.Travel.Core.Components.Display.ApplicationBarSizeInformation
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

#nullable disable
namespace Izi.Travel.Core.Components.Display
{
  public class ApplicationBarSizeInformation
  {
    public const double DefaultSizeNormalHeight = 72.0;
    public const double DefaultSize1080P55Height = 66.0;
    public const double DefualtSize1080P6Height = 55.0;
    private readonly double _defaultSize;
    private readonly double _defaultOpacitySize;

    public double DefaultSize => this._defaultSize;

    public double DefaultOpacitySize => this._defaultOpacitySize;

    public double DefaultOpacitySizeNegative => -this._defaultOpacitySize;

    public ApplicationBarSizeInformation()
    {
      DisplayInformationEx displayInformationEx = DisplayInformationEx.Default;
      if (displayInformationEx.PhysicalResolution == SizeHelpers.FullHd1080PhysicalResolution)
      {
        this._defaultSize = displayInformationEx.PhysicalDiagonal < 6.0 ? 66.0 : 55.0;
        this._defaultOpacitySize = this._defaultSize;
      }
      else
      {
        this._defaultSize = 72.0;
        this._defaultOpacitySize = 0.0;
      }
    }
  }
}
