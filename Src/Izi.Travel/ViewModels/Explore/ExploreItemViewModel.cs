// ********************************************************************
// Type: Izi.Travel.Shell.ViewModels.Explore.ExploreItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Geofencing.Primitives;
using Windows.Devices.Geolocation;
using Izi.Travel.Shell.Common.Helpers;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Explore;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Map;
using Windows.UI.Xaml.Controls.Maps;
using System;
using System.Device.Location;
using System.Linq.Expressions;
using Windows.UI.Xaml;
using Windows.Foundation;
#nullable disable
namespace Izi.Travel.Shell.ViewModels.Explore
{
  public class ExploreItemViewModel : BaseMapItemViewModel
  {
    private const int ClusterCountSmall = 2;
    private const int ClusterCountMedium = 5;
    private const int ClusterCountLarge = 11;
    private string _imageUrl;
    private string _contentProvider;
    private double _mapCenterDistance;
    private LocationRectangle _clusterBounds;
    private Point _clusterViewportPoint;
    private bool _clusterIsHidden;
    private bool _clusterIsVisited;
    private int _clusterCount;

    public ExploreViewModel ExploreViewModel { get; private set; }

    public override bool IsSelected
    {
      get => base.IsSelected;
      set
      {
        base.IsSelected = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.MapImageUrl));
        if (this.ExploreViewModel == null)
          return;
        if (value && this.ExploreViewModel != null)
        {
          if (this.ExploreViewModel.SelectedFlipItem != this)
            this.ExploreViewModel.SelectedFlipItem = this;
          else
            this.ExploreViewModel.NotifyOfPropertyChange<ExploreItemViewModel>((Expression<Func<ExploreItemViewModel>>) (() => this.ExploreViewModel.SelectedMapItem));
        }
        this.ExploreViewModel.UpdateSelectedMapItemRoute(value ? this : (ExploreItemViewModel) null);
      }
    }

    public string ImageUrl
    {
      get => this._imageUrl;
      set => this.SetProperty<string>(ref this._imageUrl, value, propertyName: nameof (ImageUrl));
    }

    public string MapImageUrl => MtgObjectHelper.GetMapImageUrl(this.Type, this.IsSelected);

    public string ContentProvider
    {
      get => this._contentProvider;
      set
      {
        this.SetProperty<string>(ref this._contentProvider, value, propertyName: nameof (ContentProvider));
      }
    }

    public double MapCenterDistance
    {
      get => this._mapCenterDistance;
      set
      {
        this.SetProperty<double>(ref this._mapCenterDistance, value, propertyName: nameof (MapCenterDistance));
      }
    }

    public LocationRectangle ClusterBounds
    {
      get => this._clusterBounds;
      set
      {
        this.SetProperty<LocationRectangle>(ref this._clusterBounds, value, propertyName: nameof (ClusterBounds));
      }
    }

    public Point ClusterViewportPoint
    {
      get => this._clusterViewportPoint;
      set
      {
        this.SetProperty<Point>(ref this._clusterViewportPoint, value, propertyName: nameof (ClusterViewportPoint));
      }
    }

    public bool ClusterIsHidden
    {
      get => this._clusterIsHidden;
      set
      {
        this.SetProperty<bool>(ref this._clusterIsHidden, value, propertyName: nameof (ClusterIsHidden));
      }
    }

    public bool ClusterIsVisited
    {
      get => this._clusterIsVisited;
      set
      {
        this.SetProperty<bool>(ref this._clusterIsVisited, value, propertyName: nameof (ClusterIsVisited));
      }
    }

    public int ClusterCount
    {
      get => this._clusterCount;
      set
      {
        this.SetProperty<int>(ref this._clusterCount, value, (Action) (() =>
        {
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsCluster));
          this.NotifyOfPropertyChange<ExploreMapClusterType>((Expression<Func<ExploreMapClusterType>>) (() => this.ClusterType));
        }), nameof (ClusterCount));
      }
    }

    public bool IsDownloaded { get; set; }

    public bool IsPurchased { get; set; }

    public string Price
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Purchase == null || !(this.MtgObject.Purchase.Price > 0M) ? (string) null : this.MtgObject.Purchase.PriceString;
      }
    }

    public ExploreMapClusterType ClusterType
    {
      get
      {
        if (this.ClusterCount < 2)
          return ExploreMapClusterType.None;
        if (this.ClusterCount < 5)
          return ExploreMapClusterType.Small;
        return this.ClusterCount < 11 ? ExploreMapClusterType.Medium : ExploreMapClusterType.Large;
      }
    }

    public bool IsCluster => this.ClusterCount > 1;

    public bool IsRated
    {
      get
      {
        return this.MtgObject != null && this.MtgObject.Rating != null && this.MtgObject.Rating.Average > 0.0;
      }
    }

    public double Rating
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Rating == null ? 0.0 : this.MtgObject.Rating.Average / 2.0;
      }
    }

    public string RatingLabel
    {
      get
      {
        return string.Format(AppResources.LabelShortRatingCount, (object) (this.MtgObject == null || this.MtgObject.Rating == null ? 0 : this.MtgObject.Rating.Count));
      }
    }

    public ExploreItemStatus Status { get; private set; }

    public string TypeString
    {
      get
      {
        if (this.MtgObject == null)
          return (string) null;
        return this.MtgObject.Type != MtgObjectType.Tour ? MtgObjectHelper.GetTypeName(this.MtgObject.Type) : TourHelper.GetCategoryName(this.MtgObject.Category);
      }
    }

    public ExploreItemViewModel(ExploreViewModel exploreViewModel, MtgObject mtgObject)
      : base(mtgObject)
    {
      this.ExploreViewModel = exploreViewModel;
      this.Initialize();
    }

    private void Initialize()
    {
      if (this.MtgObject == null)
        return;
      this.IsDownloaded = this.MtgObject.AccessType == MtgObjectAccessType.Offline;
      this.IsPurchased = PurchaseManager.Instance.IsPurchased(this.MtgObject);
      this.Status = new ExploreItemStatus(this);
      this.ImageUrl = ServiceFacade.MediaService.GetImageOrPlaceholderUrl(this.MtgObject, ImageFormat.Low120X90);
      if (this.MtgObject.Publisher != null)
        this.ContentProvider = this.MtgObject.Publisher.Title;
      this.RefreshUserLocationDistance();
    }

    private double CalculateDistance(Windows.Devices.Geolocation.Geopoint point1, Windows.Devices.Geolocation.Geopoint point2)
    {
        if (point1 == null || point2 == null)
            return double.MaxValue;
            
        var lat1 = point1.Position.Latitude * (Math.PI / 180.0);
        var lon1 = point1.Position.Longitude * (Math.PI / 180.0);
        var lat2 = point2.Position.Latitude * (Math.PI / 180.0);
        var lon2 = point2.Position.Longitude * (Math.PI / 180.0);

        // Haversine formula
        var dlon = lon2 - lon1;
        var dlat = lat2 - lat1;
        var a = Math.Pow(Math.Sin(dlat / 2), 2) + 
                Math.Cos(lat1) * Math.Cos(lat2) * 
                Math.Pow(Math.Sin(dlon / 2), 2);
                
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = 6371 * c * 1000; // Convert to meters
        
        return distance;
    }

    public void RefreshMapDistance(Windows.Devices.Geolocation.Geopoint mapLocation)
    {
        if (!this.HasLocation || mapLocation == null)
        {
            this.MapCenterDistance = short.MaxValue;
            return;
        }
        
        var location = new Windows.Devices.Geolocation.Geopoint(new BasicGeoposition
        {
            Latitude = default,//this.Location.Latitude,
            Longitude = default//this.Location.Longitude
        });
        
        this.MapCenterDistance = CalculateDistance(mapLocation, location);
    }

    public void RefreshUserLocationDistance()
    {
        if (!this.HasLocation)
        {
            this.Distance = short.MaxValue;
            return;
        }

        var position = Izi.Travel.Business.Managers.Geotracker.Instance.Position;
        if (position == null)
        {
            this.Distance = short.MaxValue;
            return;
        }

        var userLocation = new Windows.Devices.Geolocation.Geopoint(new BasicGeoposition
        {
            Latitude = position.Latitude,
            Longitude = position.Longitude
        });
        
        var targetLocation = new Windows.Devices.Geolocation.Geopoint(new BasicGeoposition
        {
            Latitude = default,//this.Location.Latitude,
            Longitude = default//this.Location.Longitude
        });
        
        this.Distance = CalculateDistance(userLocation, targetLocation);
    }

    public void RefreshStatus()
    {
      this.NotifyOfPropertyChange<ExploreItemStatus>((Expression<Func<ExploreItemStatus>>) (() => this.Status));
    }

    public bool Equals(string uid)
    {
      return this.Uid != null && this.Uid.Equals(uid, StringComparison.OrdinalIgnoreCase);
    }

    public bool Equals(string uid, string language)
    {
      return this.Uid != null && this.Language != null && this.Uid.Equals(uid, StringComparison.OrdinalIgnoreCase) && this.Language.Equals(language, StringComparison.OrdinalIgnoreCase);
    }
  }
}

