// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.MtgObjectStatusMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Client.Entities;

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class MtgObjectStatusMapper : MapperBase<MtgObjectStatus, PublicationStatus>
  {
    public override PublicationStatus Convert(MtgObjectStatus source)
    {
      if (source == MtgObjectStatus.Published)
        return PublicationStatus.Published;
      return source == MtgObjectStatus.Limited ? PublicationStatus.Limited : PublicationStatus.Unknown;
    }

    public override MtgObjectStatus ConvertBack(PublicationStatus target)
    {
      return target == PublicationStatus.Limited ? MtgObjectStatus.Limited : MtgObjectStatus.Published;
    }
  }
}
