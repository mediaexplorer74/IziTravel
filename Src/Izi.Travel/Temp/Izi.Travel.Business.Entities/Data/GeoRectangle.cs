// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.GeoRectangle
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Microsoft.Phone.Maps.Controls;
using System.Device.Location;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class GeoRectangle
  {
    public double Left { get; set; }

    public double Bottom { get; set; }

    public double Right { get; set; }

    public double Top { get; set; }

    public LocationRectangle ToLocationRectangle()
    {
      return LocationRectangle.CreateBoundingRectangle(new GeoCoordinate(this.Left, this.Top), new GeoCoordinate(this.Right, this.Bottom));
    }
  }
}
