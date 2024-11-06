// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Helper.MediaHelper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Helper
{
  public static class MediaHelper
  {
    private static readonly MtgObjectType[] MtgObjectLocationMapTypeList = new MtgObjectType[3]
    {
      MtgObjectType.Museum,
      MtgObjectType.Tour,
      MtgObjectType.TouristAttraction
    };

    public static Izi.Travel.Business.Entities.Data.Media[] GetMediaList(MtgObject mtgObject)
    {
      List<Izi.Travel.Business.Entities.Data.Media> mediaList1 = new List<Izi.Travel.Business.Entities.Data.Media>();
      if (mtgObject != null)
      {
        if (mtgObject.MainContent != null)
        {
          if (mtgObject.MainContent.Images != null)
            mediaList1.AddRange((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) mtgObject.MainContent.Images);
          if (mtgObject.MainContent.Audio != null)
            mediaList1.AddRange((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) mtgObject.MainContent.Audio);
          if (mtgObject.MainContent.Video != null)
            mediaList1.AddRange(((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) mtgObject.MainContent.Video).Where<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.Story)));
        }
        if (mtgObject.Publisher != null && mtgObject.Publisher.MainContent != null && mtgObject.Publisher.MainContent.Images != null)
          mediaList1.AddRange((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) mtgObject.Publisher.MainContent.Images);
        if (mtgObject.Sponsors != null)
          mediaList1.AddRange(((IEnumerable<Sponsor>) mtgObject.Sponsors).Where<Sponsor>((Func<Sponsor, bool>) (x => x.Images != null)).SelectMany<Sponsor, Izi.Travel.Business.Entities.Data.Media>((Func<Sponsor, IEnumerable<Izi.Travel.Business.Entities.Data.Media>>) (x => (IEnumerable<Izi.Travel.Business.Entities.Data.Media>) x.Images)));
        if (((IEnumerable<MtgObjectType>) MediaHelper.MtgObjectLocationMapTypeList).Contains<MtgObjectType>(mtgObject.Type) && mtgObject.Location != null)
        {
          List<Izi.Travel.Business.Entities.Data.Media> mediaList2 = mediaList1;
          Izi.Travel.Business.Entities.Data.Media media = new Izi.Travel.Business.Entities.Data.Media();
          media.Uid = Guid.NewGuid().ToString("D");
          media.Format = MediaFormat.LocationMap;
          media.Tag = (object) mtgObject.Location;
          mediaList2.Add(media);
        }
      }
      return mediaList1.ToArray();
    }

    public static string[] GetMediaUrlList(Izi.Travel.Business.Entities.Data.Media media, string contentProviderUid)
    {
      if (media != null)
      {
        switch (media.Format)
        {
          case MediaFormat.Image:
            switch (media.Type)
            {
              case MediaType.Story:
                return new string[4]
                {
                  ServiceFacade.MediaService.GetImageUrl(media.Uid, contentProviderUid, ImageFormat.Low120X90, ignoreLocal: true),
                  ServiceFacade.MediaService.GetImageUrl(media.Uid, contentProviderUid, ImageFormat.Low480X360, ignoreLocal: true),
                  ServiceFacade.MediaService.GetImageUrl(media.Uid, contentProviderUid, ImageFormat.High240X180, ignoreLocal: true),
                  ServiceFacade.MediaService.GetImageUrl(media.Uid, contentProviderUid, ImageFormat.High800X600, ignoreLocal: true)
                };
              case MediaType.Map:
                return new string[1]
                {
                  ServiceFacade.MediaService.GetImageUrl(media.Uid, contentProviderUid, ImageFormat.Undefined, ignoreLocal: true)
                };
              case MediaType.BrandLogo:
              case MediaType.SponsorLogo:
                return new string[1]
                {
                  ServiceFacade.MediaService.GetImageUrl(media.Uid, contentProviderUid, ImageFormat.Undefined, ImageExtension.Png, true)
                };
            }
            break;
          case MediaFormat.Audio:
            return new string[1]
            {
              ServiceFacade.MediaService.GetAudioUrl(media.Uid, contentProviderUid, true)
            };
          case MediaFormat.Video:
            return new string[1]
            {
              ServiceFacade.MediaService.GetVideoUrl(media.Uid, contentProviderUid, true)
            };
          case MediaFormat.LocationMap:
            if (media.Tag is Location tag)
              return new string[1]
              {
                ServiceFacade.MediaService.GetBingMapImageUrl(tag.ToGeoCoordinate(), 12.0, 480, 125, true)
              };
            break;
        }
      }
      return new string[0];
    }
  }
}
