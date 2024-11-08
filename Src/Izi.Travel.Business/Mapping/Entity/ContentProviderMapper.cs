// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.ContentProviderMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class ContentProviderMapper : MapperBase<Izi.Travel.Business.Entities.Data.ContentProvider, Izi.Travel.Client.Entities.ContentProvider>
  {
    public override Izi.Travel.Client.Entities.ContentProvider Convert(Izi.Travel.Business.Entities.Data.ContentProvider source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.ContentProvider ConvertBack(
      Izi.Travel.Client.Entities.ContentProvider target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.ContentProvider) null;
      Izi.Travel.Business.Entities.Data.ContentProvider contentProvider = new Izi.Travel.Business.Entities.Data.ContentProvider();
      contentProvider.Uid = target.Uid;
      contentProvider.Name = target.Name;
      contentProvider.Copyright = target.Copyright;
      return contentProvider;
    }
  }
}
