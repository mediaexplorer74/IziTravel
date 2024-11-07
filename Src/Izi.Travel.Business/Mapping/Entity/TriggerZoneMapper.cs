// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.TriggerZoneMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Mapping.Enum;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class TriggerZoneMapper : MapperBase<Izi.Travel.Business.Entities.Data.TriggerZone, Izi.Travel.Client.Entities.TriggerZone>
  {
    private readonly TriggerZoneTypeMapper _triggerZoneTypeMapper;

    public TriggerZoneMapper(TriggerZoneTypeMapper triggerZoneTypeMapper)
    {
      this._triggerZoneTypeMapper = triggerZoneTypeMapper;
    }

    public override Izi.Travel.Client.Entities.TriggerZone Convert(Izi.Travel.Business.Entities.Data.TriggerZone source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.TriggerZone ConvertBack(Izi.Travel.Client.Entities.TriggerZone target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.TriggerZone) null;
      Izi.Travel.Business.Entities.Data.TriggerZone triggerZone = new Izi.Travel.Business.Entities.Data.TriggerZone()
      {
        Type = this._triggerZoneTypeMapper.ConvertBack(target.Type)
      };
      switch (triggerZone.Type)
      {
        case Izi.Travel.Business.Entities.Data.TriggerZoneType.Polygon:
          triggerZone.PolygonPath = GeoLocationHelper.Parse(target.PolygonCorners);
          break;
        case Izi.Travel.Business.Entities.Data.TriggerZoneType.Circle:
          triggerZone.CircleCenter = new GeoLocation()
          {
            Latitude = target.CircleLatitude,
            Longitude = target.CircleLongitude,
            Altitude = target.CircleAltitude
          };
          triggerZone.CircleRadius = target.CircleRadius;
          break;
      }
      return triggerZone;
    }
  }
}
