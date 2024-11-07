// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.MediaService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Client;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  internal sealed class MediaService : IMediaService
  {
    private const string BingMapsApiKey = "AiB2pL5Hz7u1WZZ2sO2QQq-m1vqZH-firsZJ4B0e9Jeg5tbHiwB-u0pyEdiwAumW";
    private const string BingMapsApiUrl = "http://dev.virtualearth.net/REST/v1/";
    private readonly IziTravelClient _iziTravelClient;
    private readonly ImageFormatMapper _imageFormatMapper;
    private readonly ImageExtensionMapper _imageExtensionMapper;

    public MediaService(
      IziTravelClient iziTravelClient,
      ImageFormatMapper imageFormatMapper,
      ImageExtensionMapper imageExtensionMapper)
    {
      this._iziTravelClient = iziTravelClient;
      this._imageFormatMapper = imageFormatMapper;
      this._imageExtensionMapper = imageExtensionMapper;
    }

    public string GetLocalDirectory() => "Content";

    public string GetLocalPath(string url)
    {
      return Path.Combine(this.GetLocalDirectory(), BitConverter.ToString(new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(url))).Replace("-", "").ToLower());
    }

    public string GetImageOrPlaceholderUrl(MtgObject mtgObject, Izi.Travel.Business.Entities.Media.ImageFormat imageFormat)
    {
      if (mtgObject == null || mtgObject.ContentProvider == null)
        return (string) null;
      if (mtgObject.MainContent != null && mtgObject.MainContent.Images != null)
      {
        Izi.Travel.Business.Entities.Data.Media media = ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) mtgObject.MainContent.Images).FirstOrDefault<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == Izi.Travel.Business.Entities.Data.MediaType.Story));
        if (media != null)
          return this.GetImageUrl(media.Uid, mtgObject.ContentProvider.Uid, imageFormat, Izi.Travel.Business.Entities.Media.ImageExtension.Jpg, false);
      }
      return this.GetPlaceholderUrl(mtgObject.Type);
    }

    public string GetPlaceholderUrl(Izi.Travel.Business.Entities.Data.MtgObjectType mtgObjectType)
    {
      switch (mtgObjectType)
      {
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Museum:
          return "/Assets/Images/image.placeholder.museum.png";
        case Izi.Travel.Business.Entities.Data.MtgObjectType.Tour:
          return "/Assets/Images/image.placeholder.tour.png";
        case Izi.Travel.Business.Entities.Data.MtgObjectType.TouristAttraction:
          return "/Assets/Images/image.placeholder.touristattraction.png";
        default:
          return "/Assets/Images/image.placeholder.exhibit.png";
      }
    }

    public string GetImageUrl(
      string mediaUid,
      string contentProviderUid,
      Izi.Travel.Business.Entities.Media.ImageFormat imageFormat,
      Izi.Travel.Business.Entities.Media.ImageExtension imageExtension = Izi.Travel.Business.Entities.Media.ImageExtension.Jpg,
      bool ignoreLocal = false)
    {
      Izi.Travel.Client.Entities.ImageFormat imageFormat1 = this._imageFormatMapper.Convert(imageFormat);
      Izi.Travel.Client.Entities.ImageExtension imageExtension1 = this._imageExtensionMapper.Convert(imageExtension);
      Uri mediaImageUri = this._iziTravelClient.GetMediaImageUri(mediaUid, contentProviderUid, imageFormat1, imageExtension1);
      return !(mediaImageUri != (Uri) null) ? (string) null : this.TryGetLocalUrl(mediaImageUri.OriginalString, ignoreLocal);
    }

    public string GetAudioUrl(string mediaUid, string contentProviderUid, bool ignoreLocal = false)
    {
      Uri mediaAudioUri = this._iziTravelClient.GetMediaAudioUri(mediaUid, contentProviderUid);
      return !(mediaAudioUri != (Uri) null) ? (string) null : this.TryGetLocalUrl(mediaAudioUri.OriginalString, ignoreLocal);
    }

    public string GetVideoUrl(string mediaUid, string contentProviderUid, bool ignoreLocal = false)
    {
      Uri mediaVideoUri = this._iziTravelClient.GetMediaVideoUri(mediaUid, contentProviderUid);
      return !(mediaVideoUri != (Uri) null) ? (string) null : this.TryGetLocalUrl(mediaVideoUri.OriginalString, ignoreLocal);
    }

    public string GetBingMapImageUrl(
      GeoCoordinate coordinate,
      double zoomLevel,
      int width,
      int height,
      bool ignoreLocal = false)
    {
      return this.TryGetLocalUrl(string.Format("{0}Imagery/Map/Road/{1},{2}/{3}?mapSize={4},{5}&format=png&key={6}", (object) "http://dev.virtualearth.net/REST/v1/", (object) coordinate.Latitude.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture), (object) coordinate.Longitude.ToString("0.000000", (IFormatProvider) CultureInfo.InvariantCulture), (object) zoomLevel, (object) width, (object) height, (object) "AiB2pL5Hz7u1WZZ2sO2QQq-m1vqZH-firsZJ4B0e9Jeg5tbHiwB-u0pyEdiwAumW"), ignoreLocal);
    }

    public string GetFeaturedImageUrl(string mediaUid)
    {
      Uri featuredContentImageUri = this._iziTravelClient.GetFeaturedContentImageUri(mediaUid);
      return !(featuredContentImageUri != (Uri) null) ? (string) null : featuredContentImageUri.OriginalString;
    }

    private string TryGetLocalUrl(string url, bool ignoreLocal)
    {
      if (ignoreLocal)
        return url;
      string localPath = this.GetLocalPath(url);
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (storeForApplication.FileExists(localPath))
        {
          string isolatedStoragePath = ApplicationManager.IsolatedStoragePath;
          if (isolatedStoragePath == null)
            return (string) null;
          url = new Uri(new Uri(isolatedStoragePath), new Uri(localPath, UriKind.Relative)).AbsoluteUri;
        }
      }
      return url;
    }
  }
}
