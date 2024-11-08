// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectBookmarkMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Data.Entities.Local;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectBookmarkMapper : MapperBase<MtgObject, Bookmark>
  {
    public override Bookmark Convert(MtgObject source)
    {
      if (source == null)
        return (Bookmark) null;
      Bookmark bookmark = new Bookmark()
      {
        Uid = source.Uid,
        ParentUid = source.ParentUid,
        Title = source.MainContent != null ? source.MainContent.Title : (string) null,
        Language = source.Language,
        Type = source.Type.ToString(),
        ContentProviderUid = source.ContentProvider != null ? source.ContentProvider.Uid : (string) null,
        ImageUid = source.MainImageMedia != null ? source.MainImageMedia.Uid : (string) null
      };
      if (source.Location != null)
      {
        bookmark.Latitude = source.Location.Latitude;
        bookmark.Longitude = source.Location.Longitude;
      }
      return bookmark;
    }

    public override MtgObject ConvertBack(Bookmark target)
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

      //RnD
      //if (!string.IsNullOrWhiteSpace(target.Type))
      //  Enum.TryParse<MtgObjectType>(target.Type, true, out result);
      
      MtgObject mtgObject1 = new MtgObject();
      mtgObject1.Uid = target.Uid;
      mtgObject1.ParentUid = target.ParentUid;
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
      MtgObject mtgObject3 = mtgObject1;
      Location location = new Location();
      location.Latitude = target.Latitude;
      location.Longitude = target.Longitude;
      mtgObject3.Location = location;
      return mtgObject1;
    }
  }
}
