// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.IMediaService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Data.Entities.Common; //using System.Device.Location;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface IMediaService
  {
    string GetPlaceholderUrl(MtgObjectType mtgObjectType);

    string GetImageOrPlaceholderUrl(MtgObject mtgObject, ImageFormat imageFormat);

    string GetImageUrl(
      string mediaUid,
      string contentProviderUid,
      ImageFormat imageFormat,
      ImageExtension imageExtension = ImageExtension.Jpg,
      bool ignoreLocal = false);

    string GetAudioUrl(string mediaUid, string contentProviderUid, bool ignoreLocal = false);

    string GetVideoUrl(string mediaUid, string contentProviderUid, bool ignoreLocal = false);

    string GetBingMapImageUrl(
      GeoCoordinate coordinate,
      double zoomLevel,
      int width,
      int height,
      bool ignoreLocal = false);

    string GetLocalDirectory();

    string GetLocalPath(string url);

    string GetFeaturedImageUrl(string mediaUid);
  }
}
