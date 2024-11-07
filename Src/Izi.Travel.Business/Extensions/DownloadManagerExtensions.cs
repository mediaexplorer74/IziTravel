// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Extensions.DownloadManagerExtensions
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

#nullable disable
namespace Izi.Travel.Business.Extensions
{
  public static class DownloadManagerExtensions
  {
    private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
    {
      ContractResolver = (IContractResolver) new MtgContentContractResolver()
    };

    public static DownloadObject ToDownloadObject(this MtgObject mtgObject)
    {
      if (mtgObject == null || mtgObject.MainContent == null)
        return (DownloadObject) null;
      return new DownloadObject()
      {
        Uid = mtgObject.Uid,
        Language = mtgObject.MainContent.Language,
        Type = mtgObject.Type.ToDownloadObjectType(),
        Title = mtgObject.MainContent.Title,
        Latitude = mtgObject.Location != null ? mtgObject.Location.Latitude : 0.0,
        Longitude = mtgObject.Location != null ? mtgObject.Location.Longitude : 0.0,
        Data = JsonSerializerHelper.SerializeToByteArray<MtgObject>(mtgObject, DownloadManagerExtensions.JsonSerializerSettings),
        RegionUid = mtgObject.Location == null || mtgObject.Location.CityUid == null ? (string) null : mtgObject.Location.CityUid,
        Number = mtgObject.Location == null || mtgObject.Location.Number == null ? (string) null : mtgObject.Location.Number,
        Hash = mtgObject.Hash,
        Status = DownloadStatus.Created
      };
    }

    public static MtgObject ToMtgObject(this DownloadObject downloadObject)
    {
      if (downloadObject == null || downloadObject.Data == null)
        return (MtgObject) null;
      MtgObject mtgObject = JsonSerializerHelper.DeserializeFromByteArray<MtgObject>(downloadObject.Data);
      if (mtgObject == null)
        return (MtgObject) null;
      mtgObject.AccessType = MtgObjectAccessType.Offline;
      return mtgObject;
    }

    public static DownloadObjectType ToDownloadObjectType(this MtgObjectType mtgObjectType)
    {
      DownloadObjectType result;
      return !Enum.TryParse<DownloadObjectType>(mtgObjectType.ToString(), true, out result) ? DownloadObjectType.Unknown : result;
    }
  }
}
