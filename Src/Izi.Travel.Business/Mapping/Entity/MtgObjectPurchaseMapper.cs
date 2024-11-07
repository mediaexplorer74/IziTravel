// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectPurchaseMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectPurchaseMapper : MapperBase<MtgObject, Izi.Travel.Data.Entities.Local.Purchase>
  {
    public override Izi.Travel.Data.Entities.Local.Purchase Convert(MtgObject source)
    {
      if (source == null)
        return (Izi.Travel.Data.Entities.Local.Purchase) null;
      return new Izi.Travel.Data.Entities.Local.Purchase()
      {
        Uid = source.Uid,
        Language = source.Language,
        Type = source.Type.ToString(),
        Title = source.MainContent != null ? source.MainContent.Title : (string) null,
        ContentProviderUid = source.ContentProvider != null ? source.ContentProvider.Uid : (string) null,
        ImageUid = source.MainImageMedia != null ? source.MainImageMedia.Uid : (string) null
      };
    }

    public override MtgObject ConvertBack(Izi.Travel.Data.Entities.Local.Purchase target)
    {
      if (target == null)
        return (MtgObject) null;
      Content content1 = new Content()
      {
        Title = target.Title,
        Language = target.Language
      };
      if (!string.IsNullOrWhiteSpace(target.ImageUid))
      {
        Content content2 = content1;
        Media[] mediaArray = new Media[1];
        Media media = new Media();
        media.Uid = target.ImageUid;
        media.Type = MediaType.Story;
        mediaArray[0] = media;
        content2.Images = mediaArray;
      }
      MtgObjectType result = MtgObjectType.Unknown;
      if (!string.IsNullOrWhiteSpace(target.Type))
        Enum.TryParse<MtgObjectType>(target.Type, true, out result);
      MtgObject mtgObject1 = new MtgObject();
      mtgObject1.Uid = target.Uid;
      mtgObject1.Type = result;
      mtgObject1.Languages = new string[1]
      {
        target.Language
      };
      mtgObject1.Content = new Content[1]{ content1 };
      MtgObject mtgObject2 = mtgObject1;
      ContentProvider contentProvider = new ContentProvider();
      contentProvider.Uid = target.ContentProviderUid;
      mtgObject2.ContentProvider = contentProvider;
      return mtgObject1;
    }
  }
}
