// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.LocationMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class LocationMapper : MapperBase<Izi.Travel.Business.Entities.Data.Location, Izi.Travel.Client.Entities.Location>
  {
    public override Izi.Travel.Client.Entities.Location Convert(Izi.Travel.Business.Entities.Data.Location source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Location ConvertBack(Izi.Travel.Client.Entities.Location target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Location) null;
      Izi.Travel.Business.Entities.Data.Location location = new Izi.Travel.Business.Entities.Data.Location();
      location.Number = target.Number;
      location.Altitude = target.Altitude;
      location.CityUid = target.CityUid;
      location.CountryUid = target.CountryUid;
      location.CountryCode = target.CountryCode;
      location.Ip = target.Ip;
      location.Latitude = target.Latitude;
      location.Longitude = target.Longitude;
      return location;
    }
  }
}
