// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.PlaybackTypeMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class PlaybackTypeMapper : MapperBase<Izi.Travel.Business.Entities.Data.PlaybackType, Izi.Travel.Client.Entities.PlaybackType>
  {
    public override Izi.Travel.Client.Entities.PlaybackType Convert(Izi.Travel.Business.Entities.Data.PlaybackType source)
    {
      if (source == Izi.Travel.Business.Entities.Data.PlaybackType.Sequential)
        return Izi.Travel.Client.Entities.PlaybackType.Sequential;
      return source == Izi.Travel.Business.Entities.Data.PlaybackType.Random ? Izi.Travel.Client.Entities.PlaybackType.Random : Izi.Travel.Client.Entities.PlaybackType.Unknown;
    }

    public override Izi.Travel.Business.Entities.Data.PlaybackType ConvertBack(Izi.Travel.Client.Entities.PlaybackType target)
    {
      if (target == Izi.Travel.Client.Entities.PlaybackType.Sequential)
        return Izi.Travel.Business.Entities.Data.PlaybackType.Sequential;
      return target == Izi.Travel.Client.Entities.PlaybackType.Random ? Izi.Travel.Business.Entities.Data.PlaybackType.Random : Izi.Travel.Business.Entities.Data.PlaybackType.Unknown;
    }
  }
}
