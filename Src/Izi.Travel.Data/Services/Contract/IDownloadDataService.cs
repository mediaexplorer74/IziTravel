// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Services.Contract.IDownloadDataService
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Entities.Download.Query;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Data.Services.Contract
{
  public interface IDownloadDataService
  {
    DownloadObject CreateOrUpdateDownloadObject(DownloadObject downloadObject);

    DownloadObject GetDownloadObject(int id);

    DownloadObject FindDownloadObject(string uid, string language, DownloadStatus? status);

    void UpdateDownloadObjectStatus(
      string uid,
      string language,
      DownloadStatus status,
      bool ignoreReferencedObjects = false);

    DownloadObject[] GetDownloadObjectList(DownloadObjectListQuery query);

    int GetDownloadObjectCount(DownloadObjectListQuery query);

    DownloadObject[] GetDownloadObjectChildren(DownloadObjectChildrenQuery query);

    int GetDownloadObjectChildrenCount(DownloadObjectChildrenQuery query);

    DownloadObjectChildrenExtendedResult GetDownloadObjectChildrenExtended(
      DownloadObjectChildrenExtendedQuery query);

    void DeleteDownloadObjectList(DownloadObjectDeleteListQuery query);

    void AddDownloadObjectLink(int objectId, int parentId);

    void RemoveDownloadObjectLink(int objectId, int parentId);

    void CreateDownloadMedia(List<DownloadMedia> downloadMediaItems);

    DownloadMedia CreateOrUpdateDownloadMedia(DownloadMedia downloadMedia);

    DownloadMedia[] GetDownloadMediaList(DownloadMediaListQuery query);

    string[] GetDownloadExclusiveMediaPathList(string uid, string language);

    void Save(
      IEnumerable<DownloadObject> downloadObjects,
      IEnumerable<DownloadObjectLink> downloadObjectLinks,
      IEnumerable<DownloadMedia> downloadMediaItems);

    void Clear(string uid, string language);

    void DownloadBatchUpdate(DownloadBatchItem[] items);
  }
}
