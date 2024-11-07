// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.MtgObjectService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Components;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Mapping.Entity;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Client;
using Izi.Travel.Client.Entities;
using Izi.Travel.Client.Queries;
using Izi.Travel.Data.Entities.Local;
using Izi.Travel.Data.Entities.Local.Query;
using Izi.Travel.Data.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  internal sealed class MtgObjectService : IMtgObjectService
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (MtgObjectService));
    private readonly IziTravelClient _iziTravelClient;
    private readonly ILocalDataService _localDataService;
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper = IoC.Get<MtgObjectTypeMapper>();
    private readonly ContentSectionMapper _contentSectionMapper = IoC.Get<ContentSectionMapper>();
    private readonly MtgObjectFeaturedContentMapper _mtgObjectFeaturedContentMapper = IoC.Get<MtgObjectFeaturedContentMapper>();
    private readonly MtgObjectFullMapper _mtgObjectFullMapper = IoC.Get<MtgObjectFullMapper>();
    private readonly MtgObjectCompactMapper _mtgObjectCompactMapper = IoC.Get<MtgObjectCompactMapper>();
    private readonly MtgChildrenListResultFullMapper _mtgChildrenListResultFullMapper = IoC.Get<MtgChildrenListResultFullMapper>();
    private readonly MtgChildrenListResultCompactMapper _mtgChildrenListResultCompactMapper = IoC.Get<MtgChildrenListResultCompactMapper>();
    private readonly MtgObjectPublisherFullMapper _mtgObjectPublisherFullMapper = IoC.Get<MtgObjectPublisherFullMapper>();
    private readonly MtgObjectPublisherCompactMapper _mtgObjectPublisherCompactMapper = IoC.Get<MtgObjectPublisherCompactMapper>();
    private readonly MtgObjectPurchaseMapper _mtgObjectPurchaseMapper = IoC.Get<MtgObjectPurchaseMapper>();
    private readonly ReviewMapper _reviewMapper = IoC.Get<ReviewMapper>();
    private readonly MtgObjectBookmarkMapper _mtgObjectBookmarkMapper = IoC.Get<MtgObjectBookmarkMapper>();
    private readonly MtgObjectHistoryMapper _mtgObjectHistoryMapper = IoC.Get<MtgObjectHistoryMapper>();

    public MtgObjectService(IziTravelClient iziTravelClient, ILocalDataService localDataService)
    {
      this._iziTravelClient = iziTravelClient;
      this._localDataService = localDataService;
    }

    public async Task<MtgObject[]> GetFeaturedListAsync(string[] languages, CancellationToken ct = default (CancellationToken))
    {
      MtgObject[] array;
      try
      {
        List<FeaturedContent> featuredContentAsync = await this._iziTravelClient.GetFeaturedContentAsync(new FeaturedContentQuery()
        {
          Languages = languages
        });
        array = featuredContentAsync == null || featuredContentAsync.Count <= 0 ? (MtgObject[]) null : featuredContentAsync.Select<FeaturedContent, MtgObject>((Func<FeaturedContent, MtgObject>) (x => this._mtgObjectFeaturedContentMapper.ConvertBack(x))).ToArray<MtgObject>();
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return array;
    }

    public async Task<MtgObject[]> GetMtgObjectListAsync(
      MtgObjectListFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        MtgObjectSearchQuery objectSearchQuery = new MtgObjectSearchQuery();
        objectSearchQuery.Languages = filter.Languages;
        objectSearchQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        objectSearchQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        objectSearchQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        objectSearchQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        objectSearchQuery.Limit = filter.Limit;
        objectSearchQuery.Offset = filter.Offset;
        objectSearchQuery.Radius = filter.Radius;
        objectSearchQuery.ExclusionRadius = filter.ExclusionRadius;
        objectSearchQuery.Query = filter.Query;
        objectSearchQuery.RegionUid = filter.Region;
        MtgObjectSearchQuery query = objectSearchQuery;
        if (filter.Location != null)
          query.Location = new Geopoint(filter.Location.Latitude, filter.Location.Longitude);
        if (filter.ExclusionLocation != null)
          query.ExclusionLocation = new Geopoint(filter.ExclusionLocation.Latitude, filter.ExclusionLocation.Longitude);
        if (filter.Types != null)
          query.Types = ((IEnumerable<Izi.Travel.Business.Entities.Data.MtgObjectType>) filter.Types).Select<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>((Func<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>) (x => this._mtgObjectTypeMapper.Convert(x))).ToArray<Izi.Travel.Client.Entities.MtgObjectType>();
        if (filter.Form == MtgObjectForm.Full)
        {
          List<MtgObjectFull> source = await this._iziTravelClient.SearchMtgObjectAsync<MtgObjectFull>(query);
          if (source != null && source.Count > 0)
            return source.Select<MtgObjectFull, MtgObject>((Func<MtgObjectFull, MtgObject>) (x => this._mtgObjectFullMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        else
        {
          List<MtgObjectCompact> source = await this._iziTravelClient.SearchMtgObjectAsync<MtgObjectCompact>(query);
          if (source != null)
            return source.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        return (MtgObject[]) null;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<MtgObject[]> GetMtgObjectChildrenAsync(
      MtgObjectChildrenFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        MtgObjectChildrenQuery objectChildrenQuery = new MtgObjectChildrenQuery();
        objectChildrenQuery.Uid = filter.Uid;
        objectChildrenQuery.Offset = filter.Offset;
        objectChildrenQuery.Limit = filter.Limit;
        objectChildrenQuery.Languages = filter.Languages;
        objectChildrenQuery.Types = filter.Types != null ? ((IEnumerable<Izi.Travel.Business.Entities.Data.MtgObjectType>) filter.Types).Select<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>((Func<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>) (x => this._mtgObjectTypeMapper.Convert(x))).ToArray<Izi.Travel.Client.Entities.MtgObjectType>() : (Izi.Travel.Client.Entities.MtgObjectType[]) null;
        objectChildrenQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        objectChildrenQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        objectChildrenQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        objectChildrenQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        objectChildrenQuery.IncludeHidden = filter.ShowHidden;
        MtgObjectChildrenQuery query = objectChildrenQuery;
        if (filter.Form == MtgObjectForm.Full)
        {
          List<MtgObjectFull> objectChildrenAsync = await this._iziTravelClient.GetMtgObjectChildrenAsync<MtgObjectFull>(query);
          if (objectChildrenAsync != null && objectChildrenAsync.Count > 0)
            return objectChildrenAsync.Select<MtgObjectFull, MtgObject>((Func<MtgObjectFull, MtgObject>) (x => this._mtgObjectFullMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        else
        {
          List<MtgObjectCompact> objectChildrenAsync = await this._iziTravelClient.GetMtgObjectChildrenAsync<MtgObjectCompact>(query);
          if (objectChildrenAsync != null && objectChildrenAsync.Count > 0)
            return objectChildrenAsync.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        return (MtgObject[]) null;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<MtgChildrenListResult> GetMtgObjectChildrenExtendedAsync(
      MtgObjectChildrenExtendedFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        MtgObjectChildrenListQuery childrenListQuery = new MtgObjectChildrenListQuery();
        childrenListQuery.Offset = filter.Offset;
        childrenListQuery.Limit = filter.Limit;
        childrenListQuery.Uid = filter.Uid;
        childrenListQuery.PageUid = filter.PageUid;
        childrenListQuery.PageExhibitNumber = filter.PageExhibitNumber;
        childrenListQuery.Languages = filter.Languages;
        childrenListQuery.SortExhibits = filter.SortExhibits;
        childrenListQuery.IncludeHidden = filter.ShowHidden;
        childrenListQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        childrenListQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        childrenListQuery.Types = filter.Types != null ? ((IEnumerable<Izi.Travel.Business.Entities.Data.MtgObjectType>) filter.Types).Select<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>((Func<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>) (x => this._mtgObjectTypeMapper.Convert(x))).ToArray<Izi.Travel.Client.Entities.MtgObjectType>() : (Izi.Travel.Client.Entities.MtgObjectType[]) null;
        childrenListQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        childrenListQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        MtgObjectChildrenListQuery query = childrenListQuery;
        return filter.Form == MtgObjectForm.Full ? this._mtgChildrenListResultFullMapper.ConvertBack(await this._iziTravelClient.GetMtgObjectChildrenList<MtgObjectFull>(query)) : this._mtgChildrenListResultCompactMapper.ConvertBack(await this._iziTravelClient.GetMtgObjectChildrenList<MtgObjectCompact>(query));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public Task<int> GetMtgObjectChildrenCountAsync(
      MtgObjectChildrenCountFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        return this._iziTravelClient.GetMtgObjectChildrenCountAsync(new MtgObjectChildrenCountQuery()
        {
          Uid = filter.Uid,
          Languages = filter.Languages,
          Types = filter.Types != null ? ((IEnumerable<Izi.Travel.Business.Entities.Data.MtgObjectType>) filter.Types).Select<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>((Func<Izi.Travel.Business.Entities.Data.MtgObjectType, Izi.Travel.Client.Entities.MtgObjectType>) (x => this._mtgObjectTypeMapper.Convert(x))).ToArray<Izi.Travel.Client.Entities.MtgObjectType>() : (Izi.Travel.Client.Entities.MtgObjectType[]) null
        });
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<MtgObject> GetMtgObjectAsync(MtgObjectFilter filter, CancellationToken ct = default (CancellationToken))
    {
      MtgObject mtgObjectAsync;
      try
      {
        MtgObjectQuery mtgObjectQuery = new MtgObjectQuery();
        mtgObjectQuery.Uid = filter.Uid;
        mtgObjectQuery.Languages = filter.Languages;
        mtgObjectQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        mtgObjectQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        mtgObjectQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        mtgObjectQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        MtgObjectQuery query = mtgObjectQuery;
        MtgObject mtgObject;
        if (filter.Form == MtgObjectForm.Full)
          mtgObject = this._mtgObjectFullMapper.ConvertBack(await this._iziTravelClient.GetMtgObjectAsync<MtgObjectFull>(query));
        else
          mtgObject = this._mtgObjectCompactMapper.ConvertBack(await this._iziTravelClient.GetMtgObjectAsync<MtgObjectCompact>(query));
        mtgObjectAsync = mtgObject;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return mtgObjectAsync;
    }

    public async Task<MtgObject[]> GetPublisherChildrenAsync(
      MtgPublisherChildrenFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        PublisherChildrenQuery publisherChildrenQuery = new PublisherChildrenQuery();
        publisherChildrenQuery.Uid = filter.Uid;
        publisherChildrenQuery.Offset = filter.Offset;
        publisherChildrenQuery.Limit = filter.Limit;
        publisherChildrenQuery.Languages = filter.Languages;
        publisherChildrenQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        publisherChildrenQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        publisherChildrenQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        publisherChildrenQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        PublisherChildrenQuery query = publisherChildrenQuery;
        if (filter.Form == MtgObjectForm.Full)
        {
          List<MtgObjectFull> publisherChildrenAsync = await this._iziTravelClient.GetPublisherChildrenAsync<MtgObjectFull>(query);
          if (publisherChildrenAsync != null && publisherChildrenAsync.Count > 0)
            return publisherChildrenAsync.Select<MtgObjectFull, MtgObject>((Func<MtgObjectFull, MtgObject>) (x => this._mtgObjectFullMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        else
        {
          List<MtgObjectCompact> publisherChildrenAsync = await this._iziTravelClient.GetPublisherChildrenAsync<MtgObjectCompact>(query);
          if (publisherChildrenAsync != null && publisherChildrenAsync.Count > 0)
            return publisherChildrenAsync.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        return (MtgObject[]) null;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public Task<int> GetPublisherChildrenCountAsync(
      MtgPublisherChildrenCountFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        return this._iziTravelClient.GetPublisherChildrenCountAsync(new PublisherChildrenCountQuery()
        {
          Uid = filter.Uid,
          Languages = filter.Languages
        });
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<MtgObject> GetPublisherAsync(MtgObjectFilter filter, CancellationToken ct = default (CancellationToken))
    {
      MtgObject publisherAsync;
      try
      {
        PublisherQuery publisherQuery = new PublisherQuery();
        publisherQuery.Uid = filter.Uid;
        publisherQuery.Languages = filter.Languages;
        publisherQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        publisherQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        publisherQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        publisherQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        PublisherQuery query = publisherQuery;
        MtgObject mtgObject;
        if (filter.Form == MtgObjectForm.Full)
          mtgObject = this._mtgObjectPublisherFullMapper.ConvertBack(await this._iziTravelClient.GetPublisherAsync<PublisherFull>(query));
        else
          mtgObject = this._mtgObjectPublisherCompactMapper.ConvertBack(await this._iziTravelClient.GetPublisherAsync<PublisherCompact>(query));
        publisherAsync = mtgObject;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return publisherAsync;
    }

    public async Task<string> GetProductIdAsync(ProductIdFilter filter, CancellationToken ct = default (CancellationToken))
    {
      string productId;
      try
      {
        productId = (await this._iziTravelClient.GetMtgObjectProductAsync(new MtgObjectProductQuery()
        {
          Uid = filter.Uid
        }))?.ProductId;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return productId;
    }

    public async Task<MtgObject[]> GetPaidMtgObjectListAsync(
      PaidMtgObjectListFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      try
      {
        MtgObjectPaidListQuery objectPaidListQuery = new MtgObjectPaidListQuery();
        objectPaidListQuery.Offset = filter.Offset;
        objectPaidListQuery.Limit = filter.Limit;
        objectPaidListQuery.Languages = filter.Languages;
        objectPaidListQuery.ProductIds = filter.ProductIds;
        objectPaidListQuery.IncludeAudioDuration = filter.IncludeAudioDuration;
        objectPaidListQuery.IncludeChildrenCount = filter.IncludeChildrenCountInFullForm;
        objectPaidListQuery.Includes = this._contentSectionMapper.Convert(filter.Includes);
        objectPaidListQuery.Excludes = this._contentSectionMapper.Convert(filter.Excludes);
        MtgObjectPaidListQuery query = objectPaidListQuery;
        if (filter.Form == MtgObjectForm.Full)
        {
          List<MtgObjectFull> objectPaidListAsync = await this._iziTravelClient.GetMtgObjectPaidListAsync<MtgObjectFull>(query);
          if (objectPaidListAsync != null && objectPaidListAsync.Count > 0)
            return objectPaidListAsync.Select<MtgObjectFull, MtgObject>((Func<MtgObjectFull, MtgObject>) (x => this._mtgObjectFullMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        else
        {
          List<MtgObjectCompact> objectPaidListAsync = await this._iziTravelClient.GetMtgObjectPaidListAsync<MtgObjectCompact>(query);
          if (objectPaidListAsync != null && objectPaidListAsync.Count > 0)
            return objectPaidListAsync.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
        return (MtgObject[]) null;
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public Task CreatePurchaseAsync(MtgObject mtgObject)
    {
      try
      {
        return Task.Factory.StartNew((Action) (() => this._localDataService.CreatePurchase(this._mtgObjectPurchaseMapper.Convert(mtgObject))));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public Task<MtgObject[]> GetPurchaseListAsync(ListFilter filter)
    {
      try
      {
        PurchaseLocalListQuery purchaseLocalListQuery = new PurchaseLocalListQuery();
        purchaseLocalListQuery.Offset = filter.Offset;
        purchaseLocalListQuery.Limit = filter.Limit;
        PurchaseLocalListQuery query = purchaseLocalListQuery;
        return Task.Factory.StartNew<MtgObject[]>((Func<MtgObject[]>) (() =>
        {
          Izi.Travel.Data.Entities.Local.Purchase[] purchaseList = this._localDataService.GetPurchaseList(query);
          return purchaseList == null || purchaseList.Length == 0 ? (MtgObject[]) null : ((IEnumerable<Izi.Travel.Data.Entities.Local.Purchase>) purchaseList).Select<Izi.Travel.Data.Entities.Local.Purchase, MtgObject>((Func<Izi.Travel.Data.Entities.Local.Purchase, MtgObject>) (x => this._mtgObjectPurchaseMapper.ConvertBack(x))).ToArray<MtgObject>();
        }));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<Izi.Travel.Business.Entities.Data.Review[]> GetReviewsAsync(
      GetReviewsFilter filter,
      CancellationToken ct = default (CancellationToken))
    {
      Izi.Travel.Business.Entities.Data.Review[] array;
      try
      {
        ReviewListResult reviewListAsync = await this._iziTravelClient.GetReviewListAsync(new ReviewListQuery()
        {
          Offset = filter.Offset,
          Limit = filter.Limit,
          Uid = filter.Uid
        });
        array = reviewListAsync == null || reviewListAsync.Data == null ? (Izi.Travel.Business.Entities.Data.Review[]) null : ((IEnumerable<Izi.Travel.Client.Entities.Review>) reviewListAsync.Data).Select<Izi.Travel.Client.Entities.Review, Izi.Travel.Business.Entities.Data.Review>((Func<Izi.Travel.Client.Entities.Review, Izi.Travel.Business.Entities.Data.Review>) (x => this._reviewMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.Review>();
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return array;
    }

    public async Task PostReviewAsync(PostReviewFilter filter, CancellationToken ct = default (CancellationToken))
    {
      try
      {
        ReviewListResult reviewListResult = await this._iziTravelClient.PostReviewAsync(new ReviewPostQuery()
        {
          Uid = filter.Uid,
          Hash = filter.Hash,
          Language = filter.Language,
          Rating = filter.Rating,
          ReviewerName = filter.ReviewerName,
          Review = filter.Text
        });
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public Task CreateBookmarkAsync(MtgObject mtgObject, string parentUid = null)
    {
      return Task.Factory.StartNew((Action) (() =>
      {
        try
        {
          Bookmark bookmark = this._mtgObjectBookmarkMapper.Convert(mtgObject);
          if (bookmark == null)
            return;
          bookmark.ParentUid = parentUid;
          this._localDataService.CreateBookmark(bookmark);
        }
        catch (Exception ex)
        {
          MtgObjectService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public async Task RemoveBookmarkAsync(MtgObjectFilter mtgObjectFilter)
    {
      try
      {
        if (mtgObjectFilter == null || mtgObjectFilter.Languages == null || mtgObjectFilter.Languages.Length == 0)
          return;
        string uid = mtgObjectFilter.Uid;
        string language = mtgObjectFilter.Languages[0];
        await Task.Factory.StartNew((Action) (() => this._localDataService.DeleteBookmark(uid, language)));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<bool> IsBookmarkExistsForMtgObjectAsync(MtgObjectFilter mtgObjectFilter)
    {
      try
      {
        if (mtgObjectFilter == null || mtgObjectFilter.Languages == null || mtgObjectFilter.Languages.Length == 0)
          return false;
        string uid = mtgObjectFilter.Uid;
        string language = mtgObjectFilter.Languages[0];
        return await Task.Factory.StartNew<bool>((Func<bool>) (() => this._localDataService.IsBookmarkExists(uid, language)));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<MtgObject[]> GetBookmarkListAsync(MtgObjectListFilter filter)
    {
      try
      {
        BookmarkLocalListQuery bookmarkLocalListQuery = new BookmarkLocalListQuery();
        bookmarkLocalListQuery.Languages = filter.Languages;
        bookmarkLocalListQuery.Types = filter.Types != null ? ((IEnumerable<Izi.Travel.Business.Entities.Data.MtgObjectType>) filter.Types).Select<Izi.Travel.Business.Entities.Data.MtgObjectType, string>((Func<Izi.Travel.Business.Entities.Data.MtgObjectType, string>) (x => x.ToString())).ToArray<string>() : (string[]) null;
        bookmarkLocalListQuery.Limit = filter.Limit;
        bookmarkLocalListQuery.Offset = filter.Offset;
        BookmarkLocalListQuery query = bookmarkLocalListQuery;
        Bookmark[] source = await Task<Bookmark[]>.Factory.StartNew((Func<Bookmark[]>) (() => this._localDataService.GetBookmarkList(query)));
        if (source != null)
        {
          if (source.Length != 0)
            return ((IEnumerable<Bookmark>) source).Select<Bookmark, MtgObject>((Func<Bookmark, MtgObject>) (x => this._mtgObjectBookmarkMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return (MtgObject[]) null;
    }

    public async Task ClearBookmarkListAsync()
    {
      try
      {
        await Task.Factory.StartNew((Action) (() => this._localDataService.ClearBookmarkList()));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task CreateOrUpdateHistoryAsync(MtgObject mtgObject, string parentUid = null)
    {
      if (mtgObject == null)
        return;
      if (mtgObject.Hidden)
        return;
      try
      {
        await Task.Factory.StartNew((Action) (() =>
        {
          History history = this._localDataService.GetHistory(mtgObject.Uid, mtgObject.MainContent != null ? mtgObject.MainContent.Language : string.Empty) ?? this._mtgObjectHistoryMapper.Convert(mtgObject);
          history.DateTime = DateTime.Now;
          history.ParentUid = parentUid;
          if (history.Id > 0)
            this._localDataService.UpdateHistory(history);
          else
            this._localDataService.CreateHistory(history);
        }));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }

    public async Task<MtgObject[]> GetHistoryListAsync(HistoryListFilter filter)
    {
      try
      {
        HistoryLocalListQuery historyLocalListQuery = new HistoryLocalListQuery();
        historyLocalListQuery.Languages = filter.Languages;
        historyLocalListQuery.Offset = filter.Offset;
        historyLocalListQuery.Limit = filter.Limit;
        historyLocalListQuery.From = filter.From;
        historyLocalListQuery.To = filter.To;
        historyLocalListQuery.Types = filter.Types != null ? ((IEnumerable<Izi.Travel.Business.Entities.Data.MtgObjectType>) filter.Types).Select<Izi.Travel.Business.Entities.Data.MtgObjectType, string>((Func<Izi.Travel.Business.Entities.Data.MtgObjectType, string>) (x => x.ToString())).ToArray<string>() : (string[]) null;
        HistoryLocalListQuery query = historyLocalListQuery;
        History[] source = await Task<History[]>.Factory.StartNew((Func<History[]>) (() => this._localDataService.GetHistoryList(query)));
        if (source != null)
        {
          if (source.Length != 0)
            return ((IEnumerable<History>) source).Select<History, MtgObject>((Func<History, MtgObject>) (x => this._mtgObjectHistoryMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
      return (MtgObject[]) null;
    }

    public async Task ClearHistoryListAsync()
    {
      try
      {
        await Task.Factory.StartNew((Action) (() => this._localDataService.ClearHistoryList()));
      }
      catch (Exception ex)
      {
        MtgObjectService.Logger.Error(ex);
        throw ExceptionTranslator.Translate(ex);
      }
    }
  }
}
