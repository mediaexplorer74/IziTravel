// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.MtgObjectDownloadService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Components;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Data.Entities.Common;
using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Entities.Download.Query;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  internal class MtgObjectDownloadService : IMtgObjectDownloadService
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (MtgObjectDownloadService));
    private readonly IDownloadDataService _dataService;

    public MtgObjectDownloadService(IDownloadDataService dataService)
    {
      this._dataService = dataService;
    }

    public Task<MtgObject[]> GetMtgObjectListAsync(MtgObjectListFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<MtgObject[]>.Factory.StartNew((Func<MtgObject[]>) (() =>
      {
        try
        {
          DownloadObjectListQuery query = new DownloadObjectListQuery()
          {
            UidList = filter.UidList,
            Offset = filter.Offset,
            Limit = filter.Limit,
            Languages = filter.Languages,
            Types = MtgObjectDownloadService.GetDownloadTypes((IEnumerable<MtgObjectType>) filter.Types),
            Query = filter.Query,
            RegionUid = filter.Region,
            Statuses = new DownloadStatus[1]
            {
              DownloadStatus.Completed
            }
          };
          if (filter.Location != null)
          {
            query.Center = new GeoCoordinate()
            {
              Latitude = filter.Location.Latitude,
              Longitude = filter.Location.Longitude
            };
            query.Radius = filter.Radius;
          }
          DownloadObject[] downloadObjectList = this._dataService.GetDownloadObjectList(query);
          if (downloadObjectList == null || downloadObjectList.Length == 0)
            return (MtgObject[]) null;
          List<MtgObject> mtgObjectList = new List<MtgObject>();
          foreach (DownloadObject downloadObject in (IEnumerable<DownloadObject>) ((IEnumerable<DownloadObject>) downloadObjectList).OrderBy<DownloadObject, int>((Func<DownloadObject, int>) (x => filter.Languages == null || filter.Languages.Length == 0 ? 0 : Array.IndexOf<string>(filter.Languages, x.Language))))
          {
            MtgObject mtgObject = downloadObject.ToMtgObject();
            if (filter.Form == MtgObjectForm.Full)
              this.LoadMtgObjectChildren(downloadObject.Id, mtgObject, filter.GetContentIntersection());
            mtgObjectList.Add(mtgObject);
          }
          return mtgObjectList.ToArray();
        }
        catch (Exception ex)
        {
          MtgObjectDownloadService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task<int> GetMtgObjectCountAsync(MtgObjectListFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<int>.Factory.StartNew((Func<int>) (() =>
      {
        try
        {
          return this._dataService.GetDownloadObjectCount(new DownloadObjectListQuery()
          {
            Languages = filter.Languages,
            Types = MtgObjectDownloadService.GetDownloadTypes((IEnumerable<MtgObjectType>) filter.Types),
            Statuses = new DownloadStatus[1]
            {
              DownloadStatus.Completed
            }
          });
        }
        catch (Exception ex)
        {
          MtgObjectDownloadService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task<MtgObject[]> GetMtgObjectChildrenAsync(MtgObjectChildrenFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<MtgObject[]>.Factory.StartNew((Func<MtgObject[]>) (() =>
      {
        try
        {
          DownloadObject[] downloadObjectChildren = this._dataService.GetDownloadObjectChildren(new DownloadObjectChildrenQuery()
          {
            Offset = filter.Offset,
            Limit = filter.Limit,
            ParentUid = filter.Uid,
            Languages = filter.Languages,
            Types = MtgObjectDownloadService.GetDownloadTypes((IEnumerable<MtgObjectType>) filter.Types),
            Statuses = new DownloadStatus[1]
            {
              DownloadStatus.Completed
            }
          });
          if (downloadObjectChildren == null || downloadObjectChildren.Length == 0)
            return (MtgObject[]) null;
          List<MtgObject> mtgObjectList = new List<MtgObject>();
          foreach (DownloadObject downloadObject in downloadObjectChildren)
          {
            MtgObject mtgObject = downloadObject.ToMtgObject();
            if (filter.Form == MtgObjectForm.Full)
              this.LoadMtgObjectChildren(downloadObject.Id, mtgObject, filter.GetContentIntersection());
            mtgObjectList.Add(mtgObject);
          }
          return mtgObjectList.ToArray();
        }
        catch (Exception ex)
        {
          MtgObjectDownloadService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task<MtgChildrenListResult> GetMtgObjectChildrenExtendedAsync(
      MtgObjectChildrenExtendedFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<MtgChildrenListResult>.Factory.StartNew((Func<MtgChildrenListResult>) (() =>
      {
        try
        {
          if (string.IsNullOrWhiteSpace(filter.Uid) || filter.Languages == null || filter.Languages.Length == 0)
            return (MtgChildrenListResult) null;
          DownloadObject downloadObject = this._dataService.FindDownloadObject(filter.Uid, filter.Languages[0], new DownloadStatus?(DownloadStatus.Completed));
          if (downloadObject == null || downloadObject.Data == null)
            return (MtgChildrenListResult) null;
          MtgObject mtgObject = JsonSerializerHelper.DeserializeFromByteArray<MtgObject>(downloadObject.Data);
          if (mtgObject == null)
            return (MtgChildrenListResult) null;
          DownloadObjectChildrenExtendedQuery query = new DownloadObjectChildrenExtendedQuery()
          {
            PageUid = filter.PageUid,
            PageExhibitNumber = filter.PageExhibitNumber,
            Offset = filter.Offset,
            Limit = filter.Limit,
            ParentUid = filter.Uid,
            Languages = filter.Languages,
            Types = MtgObjectDownloadService.GetDownloadTypes((IEnumerable<MtgObjectType>) filter.Types),
            Statuses = new DownloadStatus[1]
            {
              DownloadStatus.Completed
            }
          };
          if (mtgObject.Type == MtgObjectType.Collection && mtgObject.MainContent != null && mtgObject.MainContent.Playback != null)
            query.Order = mtgObject.MainContent.Playback.Order;
          DownloadObjectChildrenExtendedResult childrenExtended = this._dataService.GetDownloadObjectChildrenExtended(query);
          if (childrenExtended == null)
            return (MtgChildrenListResult) null;
          return new MtgChildrenListResult()
          {
            Data = ((IEnumerable<DownloadObject>) childrenExtended.DownloadObjects).Select<DownloadObject, MtgObject>((Func<DownloadObject, MtgObject>) (x => x.ToMtgObject())).Where<MtgObject>((Func<MtgObject, bool>) (x => x != null)).ToArray<MtgObject>(),
            Metadata = new MtgChildrenListResultMetadata()
            {
              Offset = childrenExtended.Offset,
              Limit = childrenExtended.Limit,
              TotalCount = childrenExtended.TotalCount,
              PageLeft = childrenExtended.PageLeft,
              PageRight = childrenExtended.PageRight,
              PageCurrent = childrenExtended.PageCurrent,
              PageTotal = childrenExtended.PageTotal
            }
          };
        }
        catch (Exception ex)
        {
          MtgObjectDownloadService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task<MtgObject> GetMtgObjectAsync(MtgObjectFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<MtgObject>.Factory.StartNew((Func<MtgObject>) (() =>
      {
        try
        {
          DownloadObject downloadObject = this._dataService.FindDownloadObject(filter.Uid, filter.Languages == null || filter.Languages.Length == 0 ? (string) null : filter.Languages[0], new DownloadStatus?(DownloadStatus.Completed));
          MtgObject mtgObject = downloadObject.ToMtgObject();
          if (mtgObject == null)
            return (MtgObject) null;
          if (filter.Form == MtgObjectForm.Full)
            this.LoadMtgObjectChildren(downloadObject.Id, mtgObject, filter.GetContentIntersection());
          return mtgObject;
        }
        catch (Exception ex)
        {
          MtgObjectDownloadService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    private void LoadMtgObjectChildren(
      int objectId,
      MtgObject mtgObject,
      ContentSection contentIntersection)
    {
      if (mtgObject == null || mtgObject.MainContent == null || contentIntersection == ContentSection.None)
        return;
      DownloadObjectChildrenQuery query = new DownloadObjectChildrenQuery()
      {
        ParentId = new int?(objectId),
        Statuses = new DownloadStatus[1]
        {
          DownloadStatus.Completed
        }
      };
      List<DownloadObjectType> downloadObjectTypeList = new List<DownloadObjectType>();
      if ((contentIntersection & ContentSection.Children) == ContentSection.Children)
      {
        switch (mtgObject.Type)
        {
          case MtgObjectType.Museum:
            downloadObjectTypeList.Add(DownloadObjectType.Exhibit);
            break;
          case MtgObjectType.Tour:
            downloadObjectTypeList.Add(DownloadObjectType.TouristAttraction);
            break;
        }
      }
      else if (mtgObject.Type == MtgObjectType.Museum && (contentIntersection & ContentSection.Collections) == ContentSection.Collections)
        downloadObjectTypeList.Add(DownloadObjectType.Collection);
      query.Types = downloadObjectTypeList.ToArray();
      DownloadObject[] downloadObjectChildren = this._dataService.GetDownloadObjectChildren(query);
      if (downloadObjectChildren == null || downloadObjectChildren.Length == 0)
        return;
      mtgObject.MainContent.Children = ((IEnumerable<DownloadObject>) downloadObjectChildren).Where<DownloadObject>((Func<DownloadObject, bool>) (x =>
      {
        if (x.Data == null)
          return false;
        return x.Type == DownloadObjectType.Exhibit || x.Type == DownloadObjectType.TouristAttraction;
      })).Select<DownloadObject, MtgObject>((Func<DownloadObject, MtgObject>) (x => JsonSerializerHelper.DeserializeFromByteArray<MtgObject>(x.Data))).ToArray<MtgObject>();
      mtgObject.MainContent.Collections = ((IEnumerable<DownloadObject>) downloadObjectChildren).Where<DownloadObject>((Func<DownloadObject, bool>) (x => x.Data != null && x.Type == DownloadObjectType.Collection)).Select<DownloadObject, MtgObject>((Func<DownloadObject, MtgObject>) (x => JsonSerializerHelper.DeserializeFromByteArray<MtgObject>(x.Data))).ToArray<MtgObject>();
    }

    private static DownloadObjectType[] GetDownloadTypes(IEnumerable<MtgObjectType> types)
    {
      return types != null ? types.Select<MtgObjectType, DownloadObjectType>(new Func<MtgObjectType, DownloadObjectType>(MtgObjectDownloadService.GetDownloadType)).ToArray<DownloadObjectType>() : (DownloadObjectType[]) null;
    }

    private static DownloadObjectType GetDownloadType(MtgObjectType type)
    {
      switch (type)
      {
        case MtgObjectType.Museum:
          return DownloadObjectType.Museum;
        case MtgObjectType.Exhibit:
          return DownloadObjectType.Exhibit;
        case MtgObjectType.StoryNavigation:
          return DownloadObjectType.StoryNavigation;
        case MtgObjectType.Tour:
          return DownloadObjectType.Tour;
        case MtgObjectType.TouristAttraction:
          return DownloadObjectType.TouristAttraction;
        case MtgObjectType.Collection:
          return DownloadObjectType.Collection;
        default:
          return DownloadObjectType.Unknown;
      }
    }
  }
}
