// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.MtgObjectRegionService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Components;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Mapping.Entity;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Client;
using Izi.Travel.Client.Entities;
using Izi.Travel.Client.Queries;
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
  internal class MtgObjectRegionService : IMtgObjectRegionService
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (MtgObjectRegionService));
    private readonly IziTravelClient _iziTravelClient;
    private readonly IDownloadDataService _downloadDataService;
    private readonly ContentSectionMapper _contentSectionMapper = IoC.Get<ContentSectionMapper>();
    private readonly MtgObjectCountryCompactMapper _mtgObjectCountryCompactMapper = IoC.Get<MtgObjectCountryCompactMapper>();
    private readonly MtgObjectCityCompactMapper _mtgObjectCityCompactMapper = IoC.Get<MtgObjectCityCompactMapper>();

    public MtgObjectRegionService(
      IziTravelClient iziTravelClient,
      IDownloadDataService downloadDataService)
    {
      this._iziTravelClient = iziTravelClient;
      this._downloadDataService = downloadDataService;
    }

    public async Task<MtgObject[]> GetCountryListAsync(string[] languages)
    {
      MtgObject[] result = (MtgObject[]) null;
      Exception remoteException = (Exception) null;
      try
      {
        IziTravelClient iziTravelClient = this._iziTravelClient;
        CountryListQuery query = new CountryListQuery();
        query.Offset = new int?(0);
        query.Limit = new int?(int.MaxValue);
        query.Languages = languages;
        query.Includes = this._contentSectionMapper.Convert(Izi.Travel.Business.Entities.Data.ContentSection.None);
        query.Excludes = this._contentSectionMapper.Convert(Izi.Travel.Business.Entities.Data.ContentSection.All);
        List<CountryCompact> countryListAsync = await iziTravelClient.GetCountryListAsync<CountryCompact>(query);
        if (countryListAsync != null)
        {
          if (countryListAsync.Count > 0)
            result = countryListAsync.Select<CountryCompact, MtgObject>((Func<CountryCompact, MtgObject>) (x => this._mtgObjectCountryCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
      }
      catch (Exception ex)
      {
        remoteException = ex;
        MtgObjectRegionService.Logger.Error(ex);
      }
      if (result != null)
      {
        await Task.Factory.StartNew((Action) (() =>
        {
          try
          {
            this._downloadDataService.DeleteDownloadObjectList(new DownloadObjectDeleteListQuery()
            {
              Types = new DownloadObjectType[1]
              {
                DownloadObjectType.Country
              }
            });
            this._downloadDataService.Save(((IEnumerable<MtgObject>) result).Where<MtgObject>((Func<MtgObject, bool>) (mtgObject => mtgObject.MainContent != null)).Select<MtgObject, DownloadObject>((Func<MtgObject, DownloadObject>) (x => new DownloadObject()
            {
              Uid = x.Uid,
              Language = x.MainContent.Language,
              Title = x.MainContent.Title,
              Data = JsonSerializerHelper.SerializeToByteArray<MtgObject>(x),
              Hash = x.Hash,
              Type = DownloadObjectType.Country,
              Status = DownloadStatus.Completed
            })), (IEnumerable<DownloadObjectLink>) null, (IEnumerable<DownloadMedia>) null);
          }
          catch (Exception ex)
          {
            MtgObjectRegionService.Logger.Error(ex);
          }
        }));
      }
      else
      {
        try
        {
          MtgObject[] mtgObjectArray = result;
          result = await Task<MtgObject[]>.Factory.StartNew((Func<MtgObject[]>) (() =>
          {
            DownloadObject[] downloadObjectList = this._downloadDataService.GetDownloadObjectList(new DownloadObjectListQuery()
            {
              Limit = new int?(int.MaxValue),
              Types = new DownloadObjectType[1]
              {
                DownloadObjectType.Country
              }
            });
            return downloadObjectList != null && downloadObjectList.Length != 0 ? ((IEnumerable<DownloadObject>) downloadObjectList).Where<DownloadObject>((Func<DownloadObject, bool>) (x => x.Data != null)).Select<DownloadObject, MtgObject>((Func<DownloadObject, MtgObject>) (x => JsonSerializerHelper.DeserializeFromByteArray<MtgObject>(x.Data))).ToArray<MtgObject>() : (MtgObject[]) null;
          }));
        }
        catch (Exception ex)
        {
          MtgObjectRegionService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
        if (result == null && remoteException != null)
          throw ExceptionTranslator.Translate(remoteException);
      }
      return result;
    }

    public async Task<MtgObject[]> GetCityListAsync(string[] languages)
    {
      MtgObject[] result = (MtgObject[]) null;
      Exception remoteException = (Exception) null;
      try
      {
        IziTravelClient iziTravelClient = this._iziTravelClient;
        CityListQuery query = new CityListQuery();
        query.Offset = new int?(0);
        query.Limit = new int?(int.MaxValue);
        query.Languages = languages;
        query.Includes = this._contentSectionMapper.Convert(Izi.Travel.Business.Entities.Data.ContentSection.None);
        query.Excludes = this._contentSectionMapper.Convert(Izi.Travel.Business.Entities.Data.ContentSection.All);
        List<CityCompact> cityListAsync = await iziTravelClient.GetCityListAsync<CityCompact>(query);
        if (cityListAsync != null)
        {
          if (cityListAsync.Count > 0)
            result = cityListAsync.Select<CityCompact, MtgObject>((Func<CityCompact, MtgObject>) (x => this._mtgObjectCityCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
        }
      }
      catch (Exception ex)
      {
        remoteException = ex;
        MtgObjectRegionService.Logger.Error(ex);
      }
      if (result != null)
      {
        await Task.Factory.StartNew((Action) (() =>
        {
          try
          {
            this._downloadDataService.DeleteDownloadObjectList(new DownloadObjectDeleteListQuery()
            {
              Types = new DownloadObjectType[1]
              {
                DownloadObjectType.City
              }
            });
            this._downloadDataService.Save(((IEnumerable<MtgObject>) result).Where<MtgObject>((Func<MtgObject, bool>) (mtgObject => mtgObject.MainContent != null)).Select<MtgObject, DownloadObject>((Func<MtgObject, DownloadObject>) (x => new DownloadObject()
            {
              Uid = x.Uid,
              Language = x.MainContent.Language,
              Title = x.MainContent.Title,
              Data = JsonSerializerHelper.SerializeToByteArray<MtgObject>(x),
              Hash = x.Hash,
              Type = DownloadObjectType.City,
              Status = DownloadStatus.Completed
            })), (IEnumerable<DownloadObjectLink>) null, (IEnumerable<DownloadMedia>) null);
          }
          catch (Exception ex)
          {
            MtgObjectRegionService.Logger.Error(ex);
          }
        }));
      }
      else
      {
        try
        {
          MtgObject[] mtgObjectArray = result;
          result = await Task<MtgObject[]>.Factory.StartNew((Func<MtgObject[]>) (() =>
          {
            DownloadObject[] downloadObjectList = this._downloadDataService.GetDownloadObjectList(new DownloadObjectListQuery()
            {
              Limit = new int?(int.MaxValue),
              Types = new DownloadObjectType[1]
              {
                DownloadObjectType.City
              }
            });
            return downloadObjectList != null && downloadObjectList.Length != 0 ? ((IEnumerable<DownloadObject>) downloadObjectList).Where<DownloadObject>((Func<DownloadObject, bool>) (x => x.Data != null)).Select<DownloadObject, MtgObject>((Func<DownloadObject, MtgObject>) (x => JsonSerializerHelper.DeserializeFromByteArray<MtgObject>(x.Data))).ToArray<MtgObject>() : (MtgObject[]) null;
          }));
        }
        catch (Exception ex)
        {
          MtgObjectRegionService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
        if (result == null && remoteException != null)
          throw ExceptionTranslator.Translate(remoteException);
      }
      return result;
    }
  }
}
