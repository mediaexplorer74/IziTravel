// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.TriggerZoneTypeMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class TriggerZoneTypeMapper : MapperBase<Izi.Travel.Business.Entities.Data.TriggerZoneType, Izi.Travel.Client.Entities.TriggerZoneType>
  {
    public override Izi.Travel.Client.Entities.TriggerZoneType Convert(Izi.Travel.Business.Entities.Data.TriggerZoneType source)
    {
      if (source == Izi.Travel.Business.Entities.Data.TriggerZoneType.Polygon)
        return Izi.Travel.Client.Entities.TriggerZoneType.Polygon;
      return source == Izi.Travel.Business.Entities.Data.TriggerZoneType.Circle ? Izi.Travel.Client.Entities.TriggerZoneType.Circle : Izi.Travel.Client.Entities.TriggerZoneType.Unknown;
    }

    public override Izi.Travel.Business.Entities.Data.TriggerZoneType ConvertBack(
      Izi.Travel.Client.Entities.TriggerZoneType target)
    {
      if (target == Izi.Travel.Client.Entities.TriggerZoneType.Circle)
        return Izi.Travel.Business.Entities.Data.TriggerZoneType.Circle;
      return target == Izi.Travel.Client.Entities.TriggerZoneType.Polygon ? Izi.Travel.Business.Entities.Data.TriggerZoneType.Polygon : Izi.Travel.Business.Entities.Data.TriggerZoneType.Unknown;
    }
  }
}
