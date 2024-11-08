// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Services.Implementation.DownloadDataService
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Context;
using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Entities.Download.Query;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

//RnD
using System.ComponentModel;
using Izi.Travel.Utility.Extensions;

#nullable disable
namespace Izi.Travel.Data.Services.Implementation
{
  public class DownloadDataService : IDownloadDataService
  {
    private const int DefaultListLimit = 20;

    public DownloadObject CreateOrUpdateDownloadObject(DownloadObject downloadObject)
    {
      if (downloadObject == null)
        throw new ArgumentNullException(nameof (downloadObject));
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        DownloadObject downloadObject1 = default;
            /* downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>(
                (Expression<Func<DownloadObject, bool>>) (x => x.Id == downloadObject.Id));*/
        if (downloadObject1 != null)
        {
          downloadObject1.Uid = downloadObject.Uid;
          downloadObject1.Language = downloadObject.Language;
          downloadObject1.Latitude = downloadObject.Latitude;
          downloadObject1.Longitude = downloadObject.Longitude;
          downloadObject1.RegionUid = downloadObject.RegionUid;
          downloadObject1.Data = downloadObject.Data;
          downloadObject1.Type = downloadObject.Type;
          downloadObject1.Hash = downloadObject.Hash;
        }
        else
          downloadDataContext.DownloadObjectTable.InsertOnSubmit(downloadObject);
        downloadDataContext.SubmitChanges();
        return downloadObject;
      }
    }

    public void UpdateDownloadObjectStatus(
      string uid,
      string language,
      DownloadStatus status,
      bool ignoreReferencedObjects = false)
    {
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        List<DownloadObject> downloadObjectList = new List<DownloadObject>();

        //RnD
        //downloadObjectList.Add(downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>(
        //    (Expression<Func<DownloadObject, bool>>) (x => x.Uid == uid && x.Language == language)));

        List<DownloadObject> source = downloadObjectList;
        if (source.Count <= 0)
          return;
        DownloadObject parent = source.First<DownloadObject>();
                
        IEnumerable<DownloadObject> downloadObjects = default;
        //    parent.ChildLinks.Select<DownloadObjectLink, DownloadObject>(
        //        (Func<DownloadObjectLink, DownloadObject>) (x => x.Object));

        if (ignoreReferencedObjects)
        {
            downloadObjects = default;/*downloadObjects.Where<DownloadObject>((Func<DownloadObject, bool>)
                (x =>
                {
                    return !x.ParentLinks.Any<DownloadObjectLink>(
                                        (Func<DownloadObjectLink, bool>)(y => y.ParentId != parent.Id
                                        && !y.Parent.ParentLinks.Any<DownloadObjectLink>()));
                }));*/
        }
        source.AddRange(downloadObjects);
        source.ForEach((Action<DownloadObject>) (x => x.Status = status));
        downloadDataContext.SubmitChanges();
      }
    }

    public DownloadObject GetDownloadObject(int id)
    {
        using (DownloadDataContext downloadDataContext = new DownloadDataContext())
        {
            return default;//downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>((Expression<Func<DownloadObject, bool>>)(x => x.Id == id));
        }
    }

    public DownloadObject[] GetDownloadObjectChildren(DownloadObjectChildrenQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      int? queryParentId = query.ParentId.HasValue ? query.ParentId : new int?(-1);
      int count1 = query.Offset ?? 0;
      int count2 = query.Limit ?? int.MaxValue;
      string queryParentUid = !string.IsNullOrWhiteSpace(query.ParentUid) ? query.ParentUid.Trim().ToLower() : string.Empty;
      string[] queryLanguages = query.Languages == null || query.Languages.Length == 0 ? new string[0] : ((IEnumerable<string>) query.Languages).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();
      DownloadObjectType[] queryTypes = query.Types == null || query.Types.Length == 0 ? (DownloadObjectType[]) Enum.GetValues(typeof (DownloadObjectType)) : query.Types;
      DownloadStatus[] queryStatuses = query.Statuses == null || query.Statuses.Length == 0 ? (DownloadStatus[]) Enum.GetValues(typeof (DownloadStatus)) : query.Statuses;
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        DownloadObject downloadObject = default;//downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>((Expression<Func<DownloadObject, bool>>) (x => (queryParentId == (int?) -1 || (int?) x.Id == queryParentId) && (string.IsNullOrEmpty(queryParentUid) || x.Uid.ToLower() == queryParentUid) && (queryLanguages.Length == 0 || queryLanguages.Contains<string>(x.Language.ToLower()))));
        if (downloadObject == null)
          return (DownloadObject[]) null;
                
        DownloadObject[] array = default;//downloadObject.ChildLinks.Where<DownloadObjectLink>((Func<DownloadObjectLink, bool>) (link => ((IEnumerable<DownloadObjectType>) queryTypes).Contains<DownloadObjectType>(link.Object.Type) && ((IEnumerable<DownloadStatus>) queryStatuses).Contains<DownloadStatus>(link.Object.Status))).Select<DownloadObjectLink, DownloadObject>((Func<DownloadObjectLink, DownloadObject>) (link => link.Object)).ToArray<DownloadObject>();
        return array.Length != 0 ? ((IEnumerable<DownloadObject>) array).Skip<DownloadObject>(count1).Take<DownloadObject>(count2).ToArray<DownloadObject>() : array;
      }
    }

    public int GetDownloadObjectChildrenCount(DownloadObjectChildrenQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      int? queryParentId = query.ParentId.HasValue ? query.ParentId : new int?(-1);
      string queryParentUid = !string.IsNullOrWhiteSpace(query.ParentUid) 
                ? query.ParentUid.Trim().ToLower() : string.Empty;

      string[] queryLanguages = query.Languages == null || query.Languages.Length == 0
                ? new string[0] : ((IEnumerable<string>) query.Languages)
                .Where<string>((Func<string, bool>)
                (x => !string.IsNullOrWhiteSpace(x)))
                .Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();

      DownloadObjectType[] queryTypes = query.Types == null || query.Types.Length == 0
                ? (DownloadObjectType[]) Enum.GetValues(typeof (DownloadObjectType))
                : query.Types;

      DownloadStatus[] queryStatuses = query.Statuses == null || query.Statuses.Length == 0
                ? (DownloadStatus[]) Enum.GetValues(typeof (DownloadStatus))
                : query.Statuses;

            using (DownloadDataContext downloadDataContext = new DownloadDataContext())
            {
                return default;//downloadDataContext.DownloadObjectLinkTable.Count<DownloadObjectLink>(
                    //(Expression<Func<DownloadObjectLink, bool>>)(x => (queryParentId == (int?)-1 
                    //|| (int?)x.ParentId == queryParentId) && (string.IsNullOrEmpty(queryParentUid) 
                    //|| x.Parent.Uid.ToLower() == queryParentUid) && (queryLanguages.Length == 0 
                    //|| queryLanguages.Contains<string>(x.Parent.Language.ToLower())) 
                    //&& queryTypes.Contains<DownloadObjectType>(x.Object.Type)
                    //&& queryStatuses.Contains<DownloadStatus>(x.Object.Status)));
            }
    }

    public DownloadObjectChildrenExtendedResult GetDownloadObjectChildrenExtended(
      DownloadObjectChildrenExtendedQuery query)
    {
      if (query == null || query.ParentUid == null || query.Languages == null)
        throw new ArgumentNullException(nameof (query));
      int? nullable = query.Offset;
      int count1 = nullable ?? 0;
      nullable = query.Limit;
      int count2 = nullable ?? 50;
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        DownloadObject downloadObject = default;//downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>((Expression<Func<DownloadObject, bool>>) (x => x.Uid == query.ParentUid && query.Languages.Contains<string>(x.Language)));
      
        if (downloadObject == null)
          return (DownloadObjectChildrenExtendedResult) null;

        IEnumerable<DownloadObject> source = default;//downloadObject.ChildLinks.Select<DownloadObjectLink, DownloadObject>((Func<DownloadObjectLink, DownloadObject>) (x => x.Object));
        
        if (query.Statuses != null)
          source = source.Where<DownloadObject>((Func<DownloadObject, bool>) (x => ((IEnumerable<DownloadStatus>) query.Statuses).Contains<DownloadStatus>(x.Status)));
       
        if (query.Types != null)
          source = source.Where<DownloadObject>((Func<DownloadObject, bool>) (x => ((IEnumerable<DownloadObjectType>) query.Types).Contains<DownloadObjectType>(x.Type)));
        
        List<DownloadObject> list = (query.Order == null || query.Order.Length == 0 
                    ? (IEnumerable<DownloadObject>) source.OrderBy<DownloadObject, string>(
                        (Func<DownloadObject, string>) (x => x.Number),
                        (IComparer<string>) new StringNumberComparer()) 
                    : (IEnumerable<DownloadObject>) source.OrderBy<DownloadObject, int>(
                        (Func<DownloadObject, int>) (x => Array.IndexOf<string>(query.Order, x.Uid))))
                        .ToList<DownloadObject>();
        
        if (!string.IsNullOrWhiteSpace(query.PageUid))
        {
          int index = list.FindIndex((Predicate<DownloadObject>) (x => x.Uid == query.PageUid));
          if (index < 0)
            return (DownloadObjectChildrenExtendedResult) null;
          count1 = index / count2 * count2;
        }
        else if (!string.IsNullOrWhiteSpace(query.PageExhibitNumber))
        {
          int index = list.FindIndex((Predicate<DownloadObject>)
              (x => x.Number == query.PageExhibitNumber));

          if (index < 0)
            return (DownloadObjectChildrenExtendedResult) null;

          count1 = index / count2 * count2;
        }
        int count3 = list.Count;
        int num1 = (int) Math.Ceiling((double) count3 / (double) count2);
        int num2 = count1 / count2;
        bool flag1 = num2 > 0;
        bool flag2 = num2 < num1 - 1;
        return new DownloadObjectChildrenExtendedResult()
        {
          DownloadObjects = list.Skip<DownloadObject>(count1).Take<DownloadObject>(count2).ToArray<DownloadObject>(),
          Offset = count1,
          Limit = count2,
          TotalCount = count3,
          PageTotal = num1,
          PageCurrent = num2,
          PageLeft = flag1,
          PageRight = flag2
        };
      }
    }

    public DownloadObject[] GetDownloadObjectList(DownloadObjectListQuery query)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DownloadDataService.c__DisplayClass7_0 cDisplayClass70 = new DownloadDataService.c__DisplayClass7_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.query = query;
      int? nullable;
      int num1;
      // ISSUE: reference to a compiler-generated field
      if (!cDisplayClass70.query.Offset.HasValue)
      {
        num1 = 0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        nullable = cDisplayClass70.query.Offset;
        num1 = nullable.Value;
      }
      int count1 = num1;
      // ISSUE: reference to a compiler-generated field
      nullable = cDisplayClass70.query.Limit;
      int num2;
      if (!nullable.HasValue)
      {
        num2 = 20;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        nullable = cDisplayClass70.query.Limit;
        num2 = nullable.Value;
      }
      int count2 = num2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.queryUidList = cDisplayClass70.query.UidList == null
                || cDisplayClass70.query.UidList.Length == 0
                ? new string[0] 
                : ((IEnumerable<string>) cDisplayClass70.query.UidList)
                .Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x)))
                .Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();

      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.queryParentUid = !string.IsNullOrWhiteSpace(cDisplayClass70.query.ParentUid)
                ? cDisplayClass70.query.ParentUid.Trim().ToLower() : string.Empty;

      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.queryLanguages = cDisplayClass70.query.Languages == null 
                || cDisplayClass70.query.Languages.Length == 0 
                ? new string[0]
                : ((IEnumerable<string>) cDisplayClass70.query.Languages)
                .Where<string>((Func<string, bool>)
                (x => !string.IsNullOrWhiteSpace(x)))
                .Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();
    
            // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.queryTypes = cDisplayClass70.query.Types == null || cDisplayClass70.query.Types.Length == 0 ? (DownloadObjectType[]) Enum.GetValues(typeof (DownloadObjectType)) : cDisplayClass70.query.Types;
      
            // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.queryRegionUid = !string.IsNullOrWhiteSpace(cDisplayClass70.query.RegionUid) 
                ? cDisplayClass70.query.RegionUid.Trim().ToLower()
                : string.Empty;
     
            // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field

      cDisplayClass70.queryString = !string.IsNullOrWhiteSpace(cDisplayClass70.query.Query)
                ? cDisplayClass70.query.Query.Trim().ToLower() 
                : string.Empty;

      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.queryStatuses = cDisplayClass70.query.Statuses == null 
                || cDisplayClass70.query.Statuses.Length == 0 
                ? (DownloadStatus[]) Enum.GetValues(typeof (DownloadStatus))
                : cDisplayClass70.query.Statuses;

      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        // ISSUE: reference to a compiler-generated field
        // RnD
        DownloadObject[] array = default;/* downloadDataContext.DownloadObjectTable.Where<DownloadObject>(
            (Expression<Func<DownloadObject, bool>>)(downloadObject
            => (cDisplayClass70.queryUidList.Length == 0 
            || cDisplayClass70.queryUidList.Contains<string>(downloadObject.Uid.ToLower()))
            && (cDisplayClass70.queryLanguages.Length == 0 
            || cDisplayClass70.queryLanguages.Contains<string>(downloadObject.Language.ToLower()))
            && cDisplayClass70.queryTypes.Contains<DownloadObjectType>(downloadObject.Type)
            && (string.IsNullOrEmpty(cDisplayClass70.queryRegionUid) 
            || downloadObject.RegionUid.ToLower() == cDisplayClass70.queryRegionUid) 
            && (string.IsNullOrEmpty(cDisplayClass70.queryString) 
            || downloadObject.Title.Contains(cDisplayClass70.queryString))
            && cDisplayClass70.queryStatuses.Contains<DownloadStatus>(downloadObject.Status)
            && (string.IsNullOrEmpty(cDisplayClass70.queryParentUid) 
            || downloadObject.ParentLinks.Any<DownloadObjectLink>(
                (Func<DownloadObjectLink, bool>)
                (x => x.Parent.Uid.ToLower() == cDisplayClass70.queryParentUid))) ))
                    .ToArray<DownloadObject>();
        */
           

        if (array.Length == 0)
          return array;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass70.query.Center != null && cDisplayClass70.query.Radius.HasValue)
        {
          // ISSUE: reference to a compiler-generated method
          array = ((IEnumerable<DownloadObject>) array)
                        .Where<DownloadObject>(new Func<DownloadObject, bool>(cDisplayClass70.CGetDownloadObjectListb__4)).ToArray<DownloadObject>();
        }
        return ((IEnumerable<DownloadObject>) array).Skip<DownloadObject>(count1).Take<DownloadObject>(count2).ToArray<DownloadObject>();
      }
    }

    public int GetDownloadObjectCount(DownloadObjectListQuery query)
    {
      string[] queryLanguages = query.Languages == null 
                || query.Languages.Length == 0 
                ? new string[0] 
                : ((IEnumerable<string>) query.Languages)
                .Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x)))
                .Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();
      DownloadObjectType[] queryTypes = query.Types == null 
                || query.Types.Length == 0 
                ? (DownloadObjectType[]) Enum.GetValues(typeof (DownloadObjectType)) : query.Types;
      DownloadStatus[] queryStatuses = query.Statuses == null 
                || query.Statuses.Length == 0 
                ? (DownloadStatus[])
                Enum.GetValues(typeof (DownloadStatus)) : query.Statuses;

            using (DownloadDataContext downloadDataContext = new DownloadDataContext())
            {
                return default;/*downloadDataContext.DownloadObjectTable.Count<DownloadObject>(
                    (Expression<Func<DownloadObject, bool>>)(
                    x => (queryLanguages.Length == 0 || queryLanguages.Contains<string>(
                        x.Language.ToLower())) && queryTypes.Contains<DownloadObjectType>(x.Type)
                        && queryStatuses.Contains<DownloadStatus>(x.Status)));*/
            }
    }

    public void DeleteDownloadObjectList(DownloadObjectDeleteListQuery query)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DownloadDataService.c__DisplayClass9_0 cDisplayClass90 
                = new DownloadDataService.c__DisplayClass9_0();
     
            // ISSUE: reference to a compiler-generated field
      cDisplayClass90.queryUidList = query.UidList == null 
                || query.UidList.Length == 0 
                ? new string[0]
                : ((IEnumerable<string>) query.UidList)
                .Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x)))
                .Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();
     
            // ISSUE: reference to a compiler-generated field
      cDisplayClass90.queryParentUid 
                = !string.IsNullOrWhiteSpace(query.ParentUid) 
                ? query.ParentUid.Trim().ToLower() 
                : string.Empty;
      
            // ISSUE: reference to a compiler-generated field
      cDisplayClass90.queryLanguages = query.Languages == null 
                || query.Languages.Length == 0
                ? new string[0] 
                : ((IEnumerable<string>) query.Languages)
                .Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x)))
                .Select<string, string>((Func<string, string>) (x => x.ToLower())).ToArray<string>();
      
            // ISSUE: reference to a compiler-generated field
      cDisplayClass90.queryTypes = query.Types == null 
                || query.Types.Length == 0 
                ? (DownloadObjectType[]) Enum.GetValues(typeof (DownloadObjectType)) : query.Types;
     
            // ISSUE: reference to a compiler-generated field
      cDisplayClass90.queryStatuses = query.Statuses == null || query.Statuses.Length == 0 ? (DownloadStatus[]) Enum.GetValues(typeof (DownloadStatus)) : query.Statuses;
     
            using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
                // ISSUE: reference to a compiler-generated field                
                DownloadObject[] array = default;
                /*downloadDataContext.DownloadObjectTable.Where<DownloadObject>(
                 (Expression<Func<DownloadObject, bool>>)
                (downloadObject => (cDisplayClass90.queryUidList.Length == 0 
                || cDisplayClass90.queryUidList.Contains<string>(downloadObject.Uid.ToLower())) 
                && (cDisplayClass90.queryLanguages.Length == 0 
                || cDisplayClass90.queryLanguages.Contains<string>
                (downloadObject.Language.ToLower()))
                && cDisplayClass90.queryTypes.Contains<DownloadObjectType>(downloadObject.Type)
                && cDisplayClass90.queryStatuses.Contains<DownloadStatus>(downloadObject.Status) 
                && (string.IsNullOrEmpty(cDisplayClass90.queryParentUid)
                || downloadObject.ParentLinks.Any<DownloadObjectLink>((Func<DownloadObjectLink, bool>) 
                (x => x.Parent.Uid.ToLower() == cDisplayClass90.queryParentUid)))))
                .ToArray<DownloadObject>();*/

        if (array.Length == 0)
          return;
        downloadDataContext.DownloadObjectTable.DeleteAllOnSubmit<DownloadObject>(
            (IEnumerable<DownloadObject>) array);
        downloadDataContext.SubmitChanges();
      }
    }

    public void AddDownloadObjectLink(int objectId, int parentId)
    {
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        downloadDataContext.DownloadObjectLinkTable.InsertOnSubmit(new DownloadObjectLink()
        {
          ObjectId = objectId,
          ParentId = parentId
        });
        downloadDataContext.SubmitChanges();
      }
    }

    public void RemoveDownloadObjectLink(int objectId, int parentId)
    {
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
                DownloadObjectLink entity = default;//downloadDataContext.DownloadObjectLinkTable.FirstOrDefault<DownloadObjectLink>((Expression<Func<DownloadObjectLink, bool>>) (x => x.ObjectId == objectId && x.ParentId == parentId));
        if (entity == null)
          return;
        downloadDataContext.DownloadObjectLinkTable.DeleteOnSubmit(entity);
        downloadDataContext.SubmitChanges();
      }
    }

    public DownloadObject FindDownloadObject(string uid, string language, DownloadStatus? status)
    {
      string queryUid = !string.IsNullOrWhiteSpace(uid) ? uid.ToLower() : string.Empty;
      string queryLanguage = !string.IsNullOrWhiteSpace(language) ? language.ToLower() : string.Empty;
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
                IQueryable<DownloadObject> source = default;//downloadDataContext.DownloadObjectTable.AsQueryable<DownloadObject>();
        if (status.HasValue)
          source = source.Where<DownloadObject>((Expression<Func<DownloadObject, bool>>) (x => (int?) x.Status == (int?) status));
        return source.FirstOrDefault<DownloadObject>((Expression<Func<DownloadObject, bool>>) (x => x.Uid.ToLower() == queryUid && x.Language.ToLower() == queryLanguage));
      }
    }

    public void CreateDownloadMedia(List<DownloadMedia> downloadMediaItems)
    {
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
        downloadDataContext.DownloadMediaTable.InsertAllOnSubmit<DownloadMedia>((IEnumerable<DownloadMedia>) downloadMediaItems);
    }

    public DownloadMedia CreateOrUpdateDownloadMedia(DownloadMedia downloadMedia)
    {
      if (downloadMedia == null)
        throw new ArgumentNullException(nameof (downloadMedia));
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        DownloadMedia downloadMedia1 = default;//downloadDataContext.DownloadMediaTable.FirstOrDefault<DownloadMedia>((Expression<Func<DownloadMedia, bool>>) (x => x.Id == downloadMedia.Id));
        if (downloadMedia1 != null)
        {
          downloadMedia1.ObjectId = downloadMedia.ObjectId;
          downloadMedia1.Path = downloadMedia.Path;
          downloadMedia1.Status = downloadMedia.Status;
        }
        else
          downloadDataContext.DownloadMediaTable.InsertOnSubmit(downloadMedia);
        downloadDataContext.SubmitChanges();
        return downloadMedia;
      }
    }

    public DownloadMedia[] GetDownloadMediaList(DownloadMediaListQuery query)
    {
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        IQueryable<DownloadMedia> source = default;//downloadDataContext.DownloadMediaTable.AsQueryable<DownloadMedia>();

                if (query.ObjectId.HasValue)
                {
                    source = source.Where<DownloadMedia>((Expression<Func<DownloadMedia, bool>>)
                        (x => x.ObjectId == query.ObjectId.Value));
                }

                if (query.Statuses != null)
                {
                    source = source.Where<DownloadMedia>((Expression<Func<DownloadMedia, bool>>)
                        (x => query.Statuses.Contains<DownloadStatus>(x.Status)));
                }
        return source.ToArray<DownloadMedia>();
      }
    }

    public string[] GetDownloadExclusiveMediaPathList(string uid, string language)
    {
      if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(language))
        return (string[]) null;

      uid = uid.Trim().ToLower();
      language = language.Trim().ToLower();

      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
                DownloadObject downloadObject = default;
                    /*downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>(
                        (Expression<Func<DownloadObject, bool>>) 
                        (x => x.Uid.ToLower() == uid && x.Language.ToLower() == language));*/
       
                if (downloadObject == null)
          return (string[]) null;

                HashSet<int> idHashSet = default;
                /*new HashSet<int>(
        downloadObject.ChildLinks.Select<DownloadObjectLink, int>(
            (Func<DownloadObjectLink, int>) (x => x.ObjectId)).Union<int>((IEnumerable<int>) new int[1]
            { downloadObject.Id  }));*/

        return default;/*downloadDataContext.DownloadMediaTable.Select(x => new
        {
          Path = x.Path,
          ObjectId = x.ObjectId
        }).ToList().GroupBy(x => x.Path).Where<IGrouping<string,
        f__AnonymousType0<string, int>>>(g => g.All(x => idHashSet.Contains(x.ObjectId)))
        .Select<IGrouping<string, f__AnonymousType0<string, int>>, 
        string>(x => x.Key).ToArray<string>();*/
      }
    }

    public void Save(
      IEnumerable<DownloadObject> downloadObjects,
      IEnumerable<DownloadObjectLink> downloadObjectLinks,
      IEnumerable<DownloadMedia> downloadMediaItems)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DownloadDataService.c__DisplayClass17_0 cDisplayClass170 
                = new DownloadDataService.c__DisplayClass17_0();

      // ISSUE: reference to a compiler-generated field
      cDisplayClass170.downloadObjects = downloadObjects;
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
                // ISSUE: reference to a compiler-generated field
                List<DownloadObject> list = default;
                    /* downloadDataContext.DownloadObjectTable.Where<DownloadObject>(
                        (Expression<Func<DownloadObject, bool>>) 
                        (x => cDisplayClass170.downloadObjects
                        .Select<DownloadObject, string>((Func<DownloadObject, string>)
                        (y => y.Uid + y.Language))
                        .Contains<string>(x.Uid + x.Language))).ToList<DownloadObject>();*/

        // ISSUE: reference to a compiler-generated field
        foreach (DownloadObject downloadObject1 in cDisplayClass170.downloadObjects)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          DownloadDataService.c__DisplayClass17_1 cDisplayClass171 
                        = new DownloadDataService.c__DisplayClass17_1();
          // ISSUE: reference to a compiler-generated field
          cDisplayClass171.downloadObject = downloadObject1;
          // ISSUE: reference to a compiler-generated method
          DownloadObject downloadObject2 = list.FirstOrDefault<DownloadObject>(
              new Func<DownloadObject, bool>(cDisplayClass171.SaveEb__0));
          if (downloadObject2 != null)
          {
            // ISSUE: reference to a compiler-generated field
            cDisplayClass171.downloadObject.Id = downloadObject2.Id;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Uid = cDisplayClass171.downloadObject.Uid;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Title = cDisplayClass171.downloadObject.Title;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Language = cDisplayClass171.downloadObject.Language;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Latitude = cDisplayClass171.downloadObject.Latitude;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Longitude = cDisplayClass171.downloadObject.Longitude;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.RegionUid = cDisplayClass171.downloadObject.RegionUid;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Data = cDisplayClass171.downloadObject.Data;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Type = cDisplayClass171.downloadObject.Type;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Hash = cDisplayClass171.downloadObject.Hash;
            // ISSUE: reference to a compiler-generated field
            downloadObject2.Status = cDisplayClass171.downloadObject.Status;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            downloadDataContext.DownloadObjectTable.InsertOnSubmit(cDisplayClass171.downloadObject);
          }
        }

        downloadDataContext.SubmitChanges();

        if (downloadObjectLinks == null || downloadMediaItems == null)
          return;
        Dictionary<DownloadObjectLink, DownloadObjectLink> dictionary1 = downloadObjectLinks.ToDictionary<DownloadObjectLink, DownloadObjectLink, DownloadObjectLink>((Func<DownloadObjectLink, DownloadObjectLink>) (x => x), (Func<DownloadObjectLink, DownloadObjectLink>) (x => new DownloadObjectLink()
        {
          ObjectId = x.Object.Id,
          ParentId = x.Parent.Id
        }));

        Dictionary<DownloadMedia, DownloadMedia> dictionary2 = downloadMediaItems.ToDictionary<DownloadMedia, DownloadMedia, DownloadMedia>((Func<DownloadMedia, DownloadMedia>) (x => x), (Func<DownloadMedia, DownloadMedia>) (x => new DownloadMedia()
        {
          ObjectId = x.Object.Id,
          Path = x.Path,
          Status = x.Status
        }));

        downloadDataContext.DownloadObjectLinkTable.InsertAllOnSubmit<DownloadObjectLink>((IEnumerable<DownloadObjectLink>) dictionary1.Values);
        downloadDataContext.DownloadMediaTable.InsertAllOnSubmit<DownloadMedia>((IEnumerable<DownloadMedia>) dictionary2.Values);
        downloadDataContext.SubmitChanges();

        dictionary1.ForEach<DownloadObjectLink, DownloadObjectLink>((Action<DownloadObjectLink, DownloadObjectLink>) ((k, v) =>
        {
          k.ObjectId = v.ObjectId;
          k.ParentId = v.ParentId;
        }));

        dictionary2.ForEach<DownloadMedia, DownloadMedia>((Action<DownloadMedia, DownloadMedia>) ((k, v) =>
        {
          k.Id = v.Id;
          k.ObjectId = v.ObjectId;
        }));
      }
    }

    public void Clear(string uid, string language)
    {
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        DownloadObject entity = default;//downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>((Expression<Func<DownloadObject, bool>>) (x => x.Uid == uid && x.Language == language));
        
        if (entity == null)
          return;
        
        //RnD
        //downloadDataContext.DownloadMediaTable.DeleteAllOnSubmit<DownloadMedia>(entity.ChildLinks.SelectMany<DownloadObjectLink, DownloadMedia>((Func<DownloadObjectLink, IEnumerable<DownloadMedia>>) (x => (IEnumerable<DownloadMedia>) x.Object.DownloadMediaItems)));
        //downloadDataContext.DownloadMediaTable.DeleteAllOnSubmit<DownloadMedia>((IEnumerable<DownloadMedia>) entity.DownloadMediaItems);
        //downloadDataContext.DownloadObjectLinkTable.DeleteAllOnSubmit<DownloadObjectLink>(entity.ChildLinks.SelectMany<DownloadObjectLink, DownloadObjectLink>((Func<DownloadObjectLink, IEnumerable<DownloadObjectLink>>) (x => (IEnumerable<DownloadObjectLink>) x.Object.ChildLinks)));
        //downloadDataContext.DownloadObjectLinkTable.DeleteAllOnSubmit<DownloadObjectLink>((IEnumerable<DownloadObjectLink>) entity.ChildLinks);
        //downloadDataContext.DownloadObjectTable.DeleteAllOnSubmit<DownloadObject>(entity.ChildLinks.Select<DownloadObjectLink, DownloadObject>((Func<DownloadObjectLink, DownloadObject>) (x => x.Object)).Where<DownloadObject>((Func<DownloadObject, bool>) (x => x.ParentLinks.Count<DownloadObjectLink>((Func<DownloadObjectLink, bool>) (y => y.Parent.Type != DownloadObjectType.Collection)) == 1)));
        downloadDataContext.DownloadObjectTable.DeleteOnSubmit(entity);
        downloadDataContext.SubmitChanges();
      }
    }

    public void DownloadBatchUpdate(DownloadBatchItem[] items)
    {
      if (items == null || items.Length == 0)
        return;
      using (DownloadDataContext downloadDataContext = new DownloadDataContext())
      {
        DownloadBatchItem[] deleteItems = ((IEnumerable<DownloadBatchItem>) items)
                    .Where<DownloadBatchItem>((Func<DownloadBatchItem, bool>)
                    (x => x.Action == DownloadBatchItemAction.Delete && x.DownloadObject != null 
                    && x.DownloadObject.Id > 0)).ToArray<DownloadBatchItem>();

        DownloadBatchItem[] createItems = ((IEnumerable<DownloadBatchItem>) items)
                    .Where<DownloadBatchItem>((Func<DownloadBatchItem, bool>)
                    (x => x.Action == DownloadBatchItemAction.Create && x.DownloadObject != null 
                    && x.DownloadObject.Id <= 0)).ToArray<DownloadBatchItem>();

        IEnumerable<DownloadObject> entities1 = ((IEnumerable<DownloadBatchItem>) createItems)
                    .Select<DownloadBatchItem, DownloadObject>((Func<DownloadBatchItem, DownloadObject>) 
                    (x => x.DownloadObject));

        IEnumerable<DownloadMedia> entities2 = ((IEnumerable<DownloadBatchItem>) createItems)
                    .SelectMany<DownloadBatchItem, DownloadMedia>(
            (Func<DownloadBatchItem, IEnumerable<DownloadMedia>>) (x => (IEnumerable<DownloadMedia>) 
            x.DownloadMediaList));

        downloadDataContext.DownloadObjectTable.InsertAllOnSubmit<DownloadObject>(entities1);
        downloadDataContext.DownloadMediaTable.InsertAllOnSubmit<DownloadMedia>(entities2);
        foreach (DownloadBatchItem downloadBatchItem1 in createItems)
        {
          DownloadBatchItem createItem = downloadBatchItem1;
          DownloadBatchItem downloadBatchItem2 = createItem;
          if (downloadBatchItem2.Children.Count != 0)
          {
            List<DownloadObjectLink> entities3 = new List<DownloadObjectLink>();
            foreach (KeyValuePair<string, int> child1 in downloadBatchItem2.Children)
            {
              KeyValuePair<string, int> child = child1;
              if (child.Value > 0)
              {
                entities3.Add(new DownloadObjectLink()
                {
                  ObjectId = child.Value,
                  Parent = downloadBatchItem2.DownloadObject
                });
              }
              else
              {
                DownloadBatchItem downloadBatchItem3 = ((IEnumerable<DownloadBatchItem>) createItems).FirstOrDefault<DownloadBatchItem>((Func<DownloadBatchItem, bool>) (x => x.DownloadObject.Uid == child.Key && x.DownloadObject.Language == createItem.DownloadObject.Language));
                if (downloadBatchItem3 != null)
                  entities3.Add(new DownloadObjectLink()
                  {
                    Object = downloadBatchItem3.DownloadObject,
                    Parent = downloadBatchItem2.DownloadObject
                  });
              }
            }
            downloadDataContext.DownloadObjectLinkTable.InsertAllOnSubmit<DownloadObjectLink>((IEnumerable<DownloadObjectLink>) entities3);
          }
        }
        foreach (DownloadBatchItem downloadBatchItem in ((IEnumerable<DownloadBatchItem>) items).Where<DownloadBatchItem>((Func<DownloadBatchItem, bool>) (x => x.Action == DownloadBatchItemAction.Update && x.DownloadObject != null && x.DownloadObject.Id > 0)).ToArray<DownloadBatchItem>())
        {
          DownloadBatchItem updateItem = downloadBatchItem;

          DownloadObject objectToUpdate = default;//downloadDataContext.DownloadObjectTable.FirstOrDefault<DownloadObject>((Expression<Func<DownloadObject, bool>>) (x => x.Id == updateItem.DownloadObject.Id));
          if (objectToUpdate != null)
          {
            DownloadBatchItem updateItemLocal = updateItem;
            objectToUpdate.Uid = updateItem.DownloadObject.Uid;
            objectToUpdate.Language = updateItem.DownloadObject.Language;
            objectToUpdate.Type = updateItem.DownloadObject.Type;
            objectToUpdate.Title = updateItem.DownloadObject.Title;
            objectToUpdate.Latitude = updateItem.DownloadObject.Latitude;
            objectToUpdate.Longitude = updateItem.DownloadObject.Longitude;
            objectToUpdate.RegionUid = updateItem.DownloadObject.RegionUid;
            objectToUpdate.Data = updateItem.DownloadObject.Data;
            objectToUpdate.Hash = updateItem.DownloadObject.Hash;
            objectToUpdate.Status = updateItem.DownloadObject.Status;
            
            //downloadDataContext.DownloadMediaTable.DeleteAllOnSubmit<DownloadMedia>((IEnumerable<DownloadMedia>) objectToUpdate.DownloadMediaItems);
            
            foreach (DownloadMedia downloadMedia in updateItem.DownloadMediaList)
            {
                downloadMedia.Object = objectToUpdate;
            }
            downloadDataContext.DownloadMediaTable.InsertAllOnSubmit<DownloadMedia>(
                (IEnumerable<DownloadMedia>) updateItem.DownloadMediaList);

                        IEnumerable<DownloadObjectLink> entities4 = default;
                            /* objectToUpdate.ChildLinks.Where<DownloadObjectLink>(
                                (Func<DownloadObjectLink, bool>)
                                (x => ((IEnumerable<DownloadBatchItem>) deleteItems)
                                .Any<DownloadBatchItem>((Func<DownloadBatchItem, bool>)
                                (d => d.DownloadObject.Id == x.ObjectId)) 
                                || !updateItemLocal.Children.ContainsValue(x.ObjectId)))
                            .Union<DownloadObjectLink>(
                                objectToUpdate.ParentLinks.Where<DownloadObjectLink>(
                                    (Func<DownloadObjectLink, bool>)
                                    (x => ((IEnumerable<DownloadBatchItem>) deleteItems)
                                    .Any<DownloadBatchItem>((Func<DownloadBatchItem, bool>) 
                                    (d => d.DownloadObject.Id == x.ParentId)))));*/

            downloadDataContext.DownloadObjectLinkTable.DeleteAllOnSubmit<DownloadObjectLink>
                            (entities4);
            IEnumerable<DownloadObjectLink> entities5 
                            = updateItem.Children.Where<KeyValuePair<string, int>>(
                                (Func<KeyValuePair<string, int>, bool>) (
                                x => ((IEnumerable<DownloadBatchItem>) createItems)
                                .Any<DownloadBatchItem>((Func<DownloadBatchItem, bool>)
                                (i => i.DownloadObject.Uid == x.Key))))
                            .Select<KeyValuePair<string, int>, DownloadBatchItem>(
                                (Func<KeyValuePair<string, int>, DownloadBatchItem>) 
                                (createdChild => ((IEnumerable<DownloadBatchItem>) createItems)
                                .FirstOrDefault<DownloadBatchItem>((Func<DownloadBatchItem, bool>) 
                                (x => x.DownloadObject.Uid == createdChild.Key))))
                            .Where<DownloadBatchItem>((Func<DownloadBatchItem, bool>) 
                            (childObject => childObject != null)).Select<DownloadBatchItem, 
                            DownloadObjectLink>((Func<DownloadBatchItem, DownloadObjectLink>) 
                            (childObject => new DownloadObjectLink()
            {
              Parent = objectToUpdate,
              Object = childObject.DownloadObject
            }));

            downloadDataContext.DownloadObjectLinkTable.InsertAllOnSubmit<DownloadObjectLink>
                            (entities5);
                        IEnumerable<KeyValuePair<string, int>> source = default;
                            /*updateItem.Children.Where<KeyValuePair<string, int>>(
                                (Func<KeyValuePair<string, int>, bool>) (x => x.Value > 0 
                                && objectToUpdate.ChildLinks.All<DownloadObjectLink>(
                                    (Func<DownloadObjectLink, bool>) (c => c.ObjectId != x.Value))));*/

            downloadDataContext.DownloadObjectLinkTable
                            .InsertAllOnSubmit<DownloadObjectLink>(
                source.Select<KeyValuePair<string, int>, DownloadObjectLink>(
                    (Func<KeyValuePair<string, int>, DownloadObjectLink>) (x => new DownloadObjectLink()
            {
              ParentId = objectToUpdate.Id,
              ObjectId = x.Value
            })));
          }
        }
        IEnumerable<int> objectToDeleteIdList = ((IEnumerable<DownloadBatchItem>) deleteItems).Select<DownloadBatchItem, int>((Func<DownloadBatchItem, int>) (x => x.DownloadObject.Id));
        
        DownloadObject[] array1 = default;//downloadDataContext.DownloadObjectTable
                                          //.Where<DownloadObject>((Expression<Func<DownloadObject,
                                          //bool>>) (x => objectToDeleteIdList.Contains<int>(x.Id)
                                          //&& x.ChildLinks.Count == 0 && x.ParentLinks.Count == 0))
                                          //.ToArray<DownloadObject>();

         DownloadMedia[] array2 = default;//((IEnumerable<DownloadObject>) array1)
                                          //.SelectMany<DownloadObject, DownloadMedia>(
                                          //(Func<DownloadObject, IEnumerable<DownloadMedia>>)
                                          //(x => (IEnumerable<DownloadMedia>) x.DownloadMediaItems))
                                          //.ToArray<DownloadMedia>();
        
        downloadDataContext.DownloadMediaTable.DeleteAllOnSubmit<DownloadMedia>(
            (IEnumerable<DownloadMedia>) array2);
        downloadDataContext.DownloadObjectTable.DeleteAllOnSubmit<DownloadObject>(
            (IEnumerable<DownloadObject>) array1);

        downloadDataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
      }
    }

        private class c__DisplayClass7_0
        {
            internal DownloadObjectListQuery query;
            internal string[] queryUidList;
            internal string queryParentUid;
            internal string[] queryLanguages;
            internal DownloadObjectType[] queryTypes;
            internal string queryRegionUid;
            internal DownloadStatus[] queryStatuses;
            internal string queryString;

            internal bool CGetDownloadObjectListb__4(DownloadObject @object)
            {
                throw new NotImplementedException();
            }
        }

        private class c__DisplayClass17_0
        {
            internal IEnumerable<DownloadObject> downloadObjects;
        }

        private class c__DisplayClass17_1
        {
            internal DownloadObject downloadObject;

            internal bool SaveEb__0(DownloadObject @object)
            {
                throw new NotImplementedException();
            }
        }

        private class f__AnonymousType0<T1, T2>
        {
        }

        private class c__DisplayClass9_0
        {
            internal string[] queryUidList;
            internal string queryParentUid;
            internal string[] queryLanguages;
            internal DownloadStatus[] queryStatuses;
            internal DownloadObjectType[] queryTypes;
        }
    }
}
