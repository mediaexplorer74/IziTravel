// ********************************************************************
// Type: Izi.Travel.ViewModels.Explore.ExploreLocationItem
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Extensions;
using Izi.Travel.Core.Resources;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Izi.Travel.Business.Entities.Data;

#nullable disable
namespace Izi.Travel.ViewModels.Explore
{
  public class ExploreLocationItem : PropertyChangedBase
  {
    public const string AroundMeUid = "ExploreLocationItemAroundMe";
    public const string MapLocationUid = "ExploreLocationItemMapLocation";
    public static readonly ExploreLocationItem AroundMe = new ExploreLocationItem()
    {
      Uid = "ExploreLocationItemAroundMe",
      CityName = AppResources.LabelAroundMe
    };
    public static readonly ExploreLocationItem MapLocation = new ExploreLocationItem()
    {
      Uid = "ExploreLocationItemMapLocation",
      CityName = AppResources.LabelMapLocation
    };
    private string _uid;
    private string _cityName;
    private string _countryCode;
    private string _countryName;
    private Windows.Devices.Geolocation.Geopoint _location = null;
    private LocationRectangle _locationRectangle;

    public string Uid
    {
      get => this._uid;
      set => this.SetProperty<string>(ref this._uid, value, propertyName: nameof (Uid));
    }

    public string CityName
    {
      get => this._cityName;
      set => this.SetProperty<string>(ref this._cityName, value, propertyName: nameof (CityName));
    }

    public string CountryCode
    {
      get => this._countryCode;
      set
      {
        this.SetProperty<string>(ref this._countryCode, value, propertyName: nameof (CountryCode));
      }
    }

    public string CountryName
    {
      get => this._countryName;
      set
      {
        this.SetProperty<string>(ref this._countryName, value, propertyName: nameof (CountryName));
      }
    }

    public Windows.Devices.Geolocation.Geopoint Location
    {
      get => this._location;
      private set
      {
        this.SetProperty<Windows.Devices.Geolocation.Geopoint>(ref this._location, value, propertyName: nameof (Location));
      }
    }

    public LocationRectangle LocationRectangle
    {
      get => this._locationRectangle;
      private set
      {
        this.SetProperty<LocationRectangle>(ref this._locationRectangle, value, propertyName: nameof (LocationRectangle));
      }
    }

    public void TrySetLocation(Windows.Devices.Geolocation.Geopoint location, LocationRectangle locationRectangle = null)
    {
      if (location != null)
      {
        this.Location = location;
        this.LocationRectangle = locationRectangle ?? new LocationRectangle(location, 0.25, 0.25);
      }
      else
      {
        if (locationRectangle == null)
          return;
        this.Location = locationRectangle.Center;
        this.LocationRectangle = locationRectangle;
      }
    }
  }
}
