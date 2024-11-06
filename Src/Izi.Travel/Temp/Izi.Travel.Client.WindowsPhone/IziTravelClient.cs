// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.IziTravelClient
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;
using Izi.Travel.Client.Exceptions;
using Izi.Travel.Client.Helpers;
using Izi.Travel.Client.Queries;
using Izi.Travel.Client.Queries.Base;
using Izi.Travel.Client.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Client
{
  public class IziTravelClient
  {
    private const string ApiUrlDevelopment = "http://api.dev.izi.travel/";
    private const string ApiUrlMediaDevelopment = "http://media.dev.izi.travel/";
    private const string ApiUrlStage = "http://api.stage.izi.travel/";
    private const string ApiUrlMediaStage = "http://media.stage.izi.travel/";
    private const string ApiUrlProduction = "http://api.izi.travel/";
    private const string ApiUrlMediaProduction = "http://media.izi.travel/";
    private readonly string _apiKey;
    private string _apiVersion;
    private IziTravelEnvironment _apiEnvironment;
    private string _apiPassword;
    private IRestClient _restClient;

    public string ApiKey => this._apiKey;

    public string ApiVersion
    {
      get => this._apiVersion;
      set
      {
        if (!(this._apiVersion != value))
          return;
        this._apiVersion = value;
        this.UpdateRestClient();
      }
    }

    public string Password
    {
      get => this._apiPassword;
      set
      {
        if (!(this._apiPassword != value))
          return;
        this._apiPassword = value;
        this.UpdateRestClient();
      }
    }

    public IziTravelEnvironment Environment
    {
      get => this._apiEnvironment;
      set
      {
        if (this._apiEnvironment == value)
          return;
        this._apiEnvironment = value;
        this.UpdateRestClient();
      }
    }

    public IziTravelClient(string apiKey)
    {
      this._apiKey = apiKey;
      this._apiVersion = "1.2.3";
      this._apiPassword = (string) null;
      this._apiEnvironment = IziTravelEnvironment.Production;
      this.UpdateRestClient();
    }

    public Task<Izi.Travel.Client.Entities.Version> GetVersionAsync()
    {
      return this.ExecuteAsync<Izi.Travel.Client.Entities.Version>((IRestRequest) new RestRequest("version"));
    }

    public async Task<T> GetMtgObjectAsync<T>(MtgObjectQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("mtgobjects/{url_parameter}");
      request.AddParameter("url_parameter", !string.IsNullOrWhiteSpace(query.Uid) ? (object) query.Uid : (object) query.Ip, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      List<T> objList = await this.ExecuteAsync<List<T>>((IRestRequest) request);
      return objList == null || objList.Count <= 0 ? default (T) : objList[0];
    }

    public Task<MtgObjectProduct> GetMtgObjectProductAsync(MtgObjectProductQuery query)
    {
      RestRequest request = new RestRequest("mtgobjects/{uuid}/product_id");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      return this.ExecuteAsync<MtgObjectProduct>((IRestRequest) request);
    }

    public Task<List<T>> GetMtgObjectBatchAsync<T>(MtgObjectBatchQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("mtgobjects/batch/{uuid_list}");
      request.AddParameter("uuid_list", (object) string.Join(",", query.Uids), ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<List<T>> GetMtgObjectChildrenAsync<T>(MtgObjectChildrenQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("mtgobjects/{uuid}/children");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      if (query.Types != null && query.Types.Length != 0)
        request.AddParameter("type", (object) ((IEnumerable<MtgObjectType>) query.Types).Select<MtgObjectType, string>(new Func<MtgObjectType, string>(EntityEnumHelper.ConvertMtgObjectType)).ToArray<string>());
      request.AddParameter("show_hidden", (object) query.IncludeHidden.ToString().ToLower());
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<int> GetMtgObjectChildrenCountAsync(MtgObjectChildrenCountQuery query)
    {
      RestRequest request = new RestRequest("mtgobjects/{uuid}/children/count");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      request.AddParameter("languages", (object) query.Languages);
      if (query.Types != null && query.Types.Length != 0)
        request.AddParameter("type", (object) ((IEnumerable<MtgObjectType>) query.Types).Select<MtgObjectType, string>(new Func<MtgObjectType, string>(EntityEnumHelper.ConvertMtgObjectType)).ToArray<string>());
      return this.ExecuteAsync<int>((IRestRequest) request);
    }

    public Task<MtgObjectChildrenListResult<T>> GetMtgObjectChildrenList<T>(
      MtgObjectChildrenListQuery query)
      where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("/mtgobjects/{uuid}/children/android");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      if (query.Types != null && query.Types.Length != 0)
        request.AddParameter("type", (object) ((IEnumerable<MtgObjectType>) query.Types).Select<MtgObjectType, string>(new Func<MtgObjectType, string>(EntityEnumHelper.ConvertMtgObjectType)).ToArray<string>());
      if (!string.IsNullOrWhiteSpace(query.PageUid))
        request.AddParameter("page_uuid", (object) query.PageUid);
      if (!string.IsNullOrWhiteSpace(query.PageExhibitNumber))
        request.AddParameter("page_exhibit_number", (object) query.PageExhibitNumber);
      if (!string.IsNullOrWhiteSpace(query.SortExhibits))
        request.AddParameter("sort_exhibits", (object) query.SortExhibits);
      request.AddParameter("show_hidden", (object) query.IncludeHidden.ToString().ToLower());
      return this.ExecuteAsync<MtgObjectChildrenListResult<T>>((IRestRequest) request);
    }

    public Task<List<T>> GetMtgObjectPaidListAsync<T>(MtgObjectPaidListQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("mtgobjects/search/paid");
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      if (query.ProductIds != null && query.ProductIds.Length != 0)
        request.AddParameter("product_ids", (object) query.ProductIds);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<List<T>> SearchMtgObjectAsync<T>(MtgObjectSearchQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("mtg/objects/search");
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      if (query.Types != null && query.Types.Length != 0)
        request.AddParameter("type", (object) ((IEnumerable<MtgObjectType>) query.Types).Select<MtgObjectType, string>(new Func<MtgObjectType, string>(EntityEnumHelper.ConvertMtgObjectType)).ToArray<string>());
      if (query.Location != null)
        request.AddParameter("lat_lon", (object) query.Location);
      if (query.Radius.HasValue)
        request.AddParameter("radius", (object) query.Radius);
      if (query.ExclusionLocation != null)
        request.AddParameter("ex_lat_lon", (object) query.ExclusionLocation);
      if (query.ExclusionRadius.HasValue)
        request.AddParameter("ex_radius", (object) query.ExclusionRadius);
      if (!string.IsNullOrWhiteSpace(query.Query))
        request.AddParameter(nameof (query), (object) query.Query);
      if (!string.IsNullOrWhiteSpace(query.RegionUid))
        request.AddParameter("region", (object) query.RegionUid);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<T> GetPublisherAsync<T>(PublisherQuery query) where T : PublisherBase, new()
    {
      RestRequest request = new RestRequest("mtg/publishers/{url_parameter}");
      request.AddParameter("url_parameter", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      return this.ExecuteAsync<T>((IRestRequest) request);
    }

    public Task<List<T>> GetPublisherChildrenAsync<T>(PublisherChildrenQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("mtg/publishers/{uuid}/children");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<int> GetPublisherChildrenCountAsync(PublisherChildrenCountQuery query)
    {
      RestRequest request = new RestRequest("mtg/publishers/{uuid}/children/count");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      request.AddParameter("languages", (object) query.Languages);
      return this.ExecuteAsync<int>((IRestRequest) request);
    }

    public Task<List<string>> GetPublisherChildrenLanguageListAsync(
      PublisherChildrenLanguageListQuery query)
    {
      RestRequest request = new RestRequest("mtg/publishers/{uuid}/children/languages");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      request.AddParameter("languages", (object) query.Languages);
      return this.ExecuteAsync<List<string>>((IRestRequest) request);
    }

    public Task<List<T>> GetCountryListAsync<T>(CountryListQuery query) where T : CountryBase, new()
    {
      RestRequest request = new RestRequest("countries");
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<T> GetCountryAsync<T>(CountryQuery query) where T : CountryBase, new()
    {
      RestRequest request = new RestRequest("countries/{url_parameter}");
      request.AddParameter("url_parameter", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      return this.ExecuteAsync<T>((IRestRequest) request);
    }

    public Task<List<T>> GetCountryChildrenAsync<T>(CountryChildrenQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("countries/{uuid}/children");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      if (query.Types != null && query.Types.Length != 0)
        request.AddParameter("type", (object) ((IEnumerable<MtgObjectType>) query.Types).Select<MtgObjectType, string>(new Func<MtgObjectType, string>(EntityEnumHelper.ConvertMtgObjectType)).ToArray<string>());
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<List<T>> GetCountryCityList<T>(CountryCityQuery query) where T : CityBase, new()
    {
      RestRequest request = new RestRequest("countries/{uuid}/cities");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<List<T>> GetCityListAsync<T>(CityListQuery query) where T : CityBase, new()
    {
      RestRequest request = new RestRequest("cities");
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<T> GetCityAsync<T>(CityQuery query) where T : CityBase, new()
    {
      RestRequest request = new RestRequest("cities/{url_parameter}");
      request.AddParameter("url_parameter", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      return this.ExecuteAsync<T>((IRestRequest) request);
    }

    public Task<List<T>> GetCityChildrenAsync<T>(CityChildrenQuery query) where T : MtgObjectBase, new()
    {
      RestRequest request = new RestRequest("cities/{uuid}/children");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectListQueryParameters<T>(request, (MtgObjectListQueryBase) query);
      if (query.Types != null && query.Types.Length != 0)
        request.AddParameter("type", (object) ((IEnumerable<MtgObjectType>) query.Types).Select<MtgObjectType, string>(new Func<MtgObjectType, string>(EntityEnumHelper.ConvertMtgObjectType)).ToArray<string>());
      return this.ExecuteAsync<List<T>>((IRestRequest) request);
    }

    public Task<int> GetCityChildrenCountAsync(CityChildrenCountQuery query)
    {
      RestRequest request = new RestRequest("cities/{uuid}/children/count");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      request.AddParameter("languages", (object) query.Languages);
      return this.ExecuteAsync<int>((IRestRequest) request);
    }

    public Task<T> GetCityCountryAsync<T>(CityCountryQuery query) where T : CountryBase, new()
    {
      RestRequest request = new RestRequest("cities/{uuid}/country");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      return this.ExecuteAsync<T>((IRestRequest) request);
    }

    public Task<List<FeaturedContent>> GetFeaturedContentAsync(FeaturedContentQuery query)
    {
      RestRequest request = new RestRequest("featured/mobile");
      request.AddParameter("languages", (object) query.Languages);
      return this.ExecuteAsync<List<FeaturedContent>>((IRestRequest) request);
    }

    public Uri GetFeaturedContentImageUri(Media media)
    {
      return media == null ? (Uri) null : this.GetFeaturedContentImageUri(media.Uid);
    }

    public Uri GetFeaturedContentImageUri(string mediaUid)
    {
      return string.IsNullOrWhiteSpace(mediaUid) ? (Uri) null : new Uri(this.GetBaseMediaUri(), string.Format("featured/{0}", (object) mediaUid));
    }

    public Task<ReviewListResult> GetReviewListAsync(ReviewListQuery query)
    {
      RestRequest request = new RestRequest("mtgobjects/{uuid}/reviews");
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      if (!string.IsNullOrWhiteSpace(query.Language))
        request.AddParameter("lang", (object) query.Language);
      int? nullable = query.Offset;
      if (nullable.HasValue)
        request.AddParameter("offset", (object) query.Offset);
      nullable = query.Limit;
      if (nullable.HasValue)
        request.AddParameter("limit", (object) query.Limit);
      return this.ExecuteAsync<ReviewListResult>((IRestRequest) request);
    }

    public Task<ReviewListResult> PostReviewAsync(ReviewPostQuery query)
    {
      RestRequest request = new RestRequest("mtgobjects/{uuid}/reviews", Method.Post)
      {
        Content = (object) query
      };
      request.AddParameter("uuid", (object) query.Uid, ParameterType.UrlSegment);
      return this.ExecuteAsync<ReviewListResult>((IRestRequest) request);
    }

    public Uri GetMediaImageUri(
      Media media,
      ContentProvider contentProvider,
      ImageFormat imageFormat,
      ImageExtension imageExtension = ImageExtension.Jpg)
    {
      return media == null || contentProvider == null ? (Uri) null : this.GetMediaImageUri(media.Uid, contentProvider.Uid, imageFormat, imageExtension);
    }

    public Uri GetMediaImageUri(
      string mediaUid,
      string contentProviderUid,
      ImageFormat imageFormat,
      ImageExtension imageExtension = ImageExtension.Jpg)
    {
      if (string.IsNullOrWhiteSpace(mediaUid) || string.IsNullOrWhiteSpace(contentProviderUid))
        return (Uri) null;
      string str = string.Empty;
      if (imageFormat != ImageFormat.Undefined)
        str = "_" + imageFormat.ToString().ToLower().Replace("high", string.Empty).Replace("low", string.Empty);
      string relativeUri = string.Format("{0}/{1}{2}.{3}", (object) contentProviderUid, (object) mediaUid, (object) str, (object) imageExtension.ToString().ToLower());
      return new Uri(this.GetBaseMediaUri(), relativeUri);
    }

    public Uri GetMediaAudioUri(Media media, ContentProvider contentProvider)
    {
      return media == null || contentProvider == null ? (Uri) null : this.GetMediaAudioUri(media.Uid, contentProvider.Uid);
    }

    public Uri GetMediaAudioUri(string mediaUid, string contentProviderUid)
    {
      return string.IsNullOrWhiteSpace(mediaUid) || string.IsNullOrWhiteSpace(contentProviderUid) ? (Uri) null : new Uri(this.GetBaseMediaUri(), string.Format("{0}/{1}.m4a", (object) contentProviderUid, (object) mediaUid));
    }

    public Uri GetMediaVideoUri(Media media, ContentProvider contentProvider)
    {
      return media == null || contentProvider == null ? (Uri) null : this.GetMediaVideoUri(media.Uid, contentProvider.Uid);
    }

    public Uri GetMediaVideoUri(string mediaUid, string contentProviderUid)
    {
      return string.IsNullOrWhiteSpace(mediaUid) || string.IsNullOrWhiteSpace(contentProviderUid) ? (Uri) null : new Uri(this.GetBaseMediaUri(), string.Format("{0}/{1}.mp4", (object) contentProviderUid, (object) mediaUid));
    }

    private void UpdateRestClient()
    {
      if (this._restClient == null)
        this._restClient = (IRestClient) new RestClient();
      this._restClient.BaseUri = this.GetBaseUri();
      this._restClient.DefaultParameters.Clear();
      this._restClient.DefaultParameters.Add(new Parameter()
      {
        Name = "X-IZI-API-KEY",
        Value = (object) this._apiKey,
        Type = ParameterType.HttpHeader
      });
      if (!string.IsNullOrWhiteSpace(this._apiVersion))
        this._restClient.DefaultParameters.Add(new Parameter()
        {
          Name = "Accept",
          Value = (object) string.Format("application/izi-api-v{0}+json", (object) this._apiVersion),
          Type = ParameterType.HttpHeader
        });
      if (string.IsNullOrWhiteSpace(this._apiPassword))
        return;
      this._restClient.DefaultParameters.Add(new Parameter()
      {
        Name = "X-IZI-API-PASSWORD",
        Value = (object) this._apiPassword,
        Type = ParameterType.HttpHeader
      });
    }

    private static void InitializeMtgObjectListQueryParameters<T>(
      RestRequest request,
      MtgObjectListQueryBase query)
      where T : new()
    {
      IziTravelClient.InitializeMtgObjectQueryParameters<T>(request, (MtgObjectQueryBase) query);
      if (query.Offset.HasValue)
        request.AddParameter("offset", (object) query.Offset);
      if (!query.Limit.HasValue)
        return;
      request.AddParameter("limit", (object) query.Limit);
    }

    private static void InitializeMtgObjectQueryParameters<T>(
      RestRequest request,
      MtgObjectQueryBase query)
      where T : new()
    {
      if (request == null)
        return;
      request.AddParameter("languages", (object) query.Languages);
      Type type = typeof (T);
      if (type == typeof (MtgObjectFull))
      {
        request.AddParameter("form", (object) "full");
        request.AddParameter("children_count_in_full_form", (object) query.IncludeChildrenCount.ToString().ToLower());
      }
      else if (type == typeof (PublisherFull) || type == typeof (CountryFull) || type == typeof (CityFull))
        request.AddParameter("form", (object) "full");
      else
        request.AddParameter("form", (object) "compact");
      request.AddParameter("audio_duration", (object) query.IncludeAudioDuration.ToString().ToLower());
      string[] strArray1 = EntityEnumHelper.ConvertContentSection(query.Includes);
      if (strArray1 != null && strArray1.Length != 0)
        request.AddParameter("includes", (object) strArray1);
      string[] strArray2 = EntityEnumHelper.ConvertContentSection(query.Excludes);
      if (strArray2 == null || strArray2.Length == 0)
        return;
      request.AddParameter("except", (object) strArray2);
    }

    private async Task<T> ExecuteAsync<T>(IRestRequest request) where T : new()
    {
      if (request == null)
        return default (T);
      request.Parameters.Add(new Parameter("nocache", (object) DateTime.Now.Ticks));
      IRestResponse<T> restResponse = await this._restClient.ExecuteTaskAsync<T>(request);
      if (restResponse.ErrorException != null)
        throw restResponse.ErrorException;
      if (restResponse.StatusCode == HttpStatusCode.NotFound)
        throw new IziTravelNotFoundException();
      return restResponse.StatusCode == HttpStatusCode.OK ? restResponse.Data : throw new IziTravelException(restResponse.StatusCode.ToString());
    }

    private Uri GetBaseUri()
    {
      switch (this._apiEnvironment)
      {
        case IziTravelEnvironment.Development:
          return new Uri("http://api.dev.izi.travel/");
        case IziTravelEnvironment.Stage:
          return new Uri("http://api.stage.izi.travel/");
        default:
          return new Uri("http://api.izi.travel/");
      }
    }

    private Uri GetBaseMediaUri()
    {
      switch (this._apiEnvironment)
      {
        case IziTravelEnvironment.Development:
          return new Uri("http://media.dev.izi.travel/");
        case IziTravelEnvironment.Stage:
          return new Uri("http://media.stage.izi.travel/");
        default:
          return new Uri("http://media.izi.travel/");
      }
    }
  }
}
