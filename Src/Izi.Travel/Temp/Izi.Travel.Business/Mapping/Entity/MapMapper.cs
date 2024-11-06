// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MapMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Helper;
using Izi.Travel.Client.Entities;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MapMapper : MapperBase<MapInfo, Map>
  {
    public override Map Convert(MapInfo source) => throw new NotImplementedException();

    public override MapInfo ConvertBack(Map target)
    {
      if (target == null)
        return (MapInfo) null;
      MapInfo mapInfo = new MapInfo();
      GeoLocation[] geoLocationArray = GeoLocationHelper.Parse(target.Bounds);
      if (geoLocationArray != null && geoLocationArray.Length > 1)
        mapInfo.Bounds = new GeoRectangle()
        {
          Left = geoLocationArray[0].Latitude,
          Bottom = geoLocationArray[0].Longitude,
          Right = geoLocationArray[1].Latitude,
          Top = geoLocationArray[1].Longitude
        };
      if (!string.IsNullOrWhiteSpace(target.Route))
        mapInfo.Route = GeoLocationHelper.Parse(target.Route);
      return mapInfo;
    }
  }
}
