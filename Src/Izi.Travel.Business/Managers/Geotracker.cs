// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Geotracker
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Services;
using Izi.Travel.Geofencing.Geotracker;
using System;
using System.Collections.Generic;
using Izi.Travel.Data.Entities.Common; //using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Geolocation = Izi.Travel.Geofencing.Primitives.Geolocation;


#nullable disable
namespace Izi.Travel.Business.Managers
{
  public class Geotracker : BaseGeotracker
  {
    private static Izi.Travel.Business.Managers.Geotracker _instance;
    private static readonly object SyncRoot = new object();
    private IGeotracker _geotracker;
    private Izi.Travel.Geofencing.Primitives.Geolocation _position;
    //private ILog _log;

    public static Izi.Travel.Business.Managers.Geotracker Instance
    {
      get
      {
        //if (Izi.Travel.Business.Managers.Geotracker._instance == null)
        //{
        //  lock (Izi.Travel.Business.Managers.Geotracker.SyncRoot)
        //  {
        //    if (Izi.Travel.Business.Managers.Geotracker._instance == null)
              Izi.Travel.Business.Managers.Geotracker._instance 
                                = new Izi.Travel.Business.Managers.Geotracker();
        //  }
        //}
        return Izi.Travel.Business.Managers.Geotracker._instance;
      }
    }

    public Geolocation DefaultPosition
    {
      get
      {
        return new Geolocation()
        {
          Latitude = 52.3728528,
          Longitude = 4.8930825
        };
      }
    }

    public Izi.Travel.Geofencing.Primitives.Geolocation Position 
            => !this.IsEnabled ? (Izi.Travel.Geofencing.Primitives.Geolocation) null : this._position;

    private Geotracker()
    {
      //this._log = LogManager.GetLog(typeof (Izi.Travel.Business.Managers.Geotracker));
      this.SetGeotracker(new GpsGeotracker());
     
      TourPlaybackManager.Instance.TourPlaybackStateChanged += 
                new TypedEventHandler<TourPlaybackManager, EventArgs>(
                    this.TourPlaybackManager_TourPlaybackStateChanged);
     }

    private void TourPlaybackManager_TourPlaybackStateChanged(
      TourPlaybackManager sender,
      EventArgs args)
    {
            //TODO : del Caliburn.Micro settings refs
      AppSettings appSettings = default;//ServiceFacade.SettingsService.GetAppSettings();
      if (!appSettings.TourEmulationEnabled)
        return;
            if (TourPlaybackManager.Instance.TourPlaybackState == TourPlaybackState.Started
                      && TourPlaybackManager.Instance.TourPlayback.Route != null)
            {
                this.SetGeotracker((IGeotracker)
                    new RouteGeotracker((
                    (IEnumerable<GeoCoordinate>)TourPlaybackManager.Instance.TourPlayback.Route)
                    .Select<GeoCoordinate, Izi.Travel.Geofencing.Primitives.Geolocation>(
                    (Func<GeoCoordinate, Izi.Travel.Geofencing.Primitives.Geolocation>)(
                    x => new Izi.Travel.Geofencing.Primitives.Geolocation(x.Latitude, x.Longitude)))
                    .ToArray<Izi.Travel.Geofencing.Primitives.Geolocation>(), appSettings.TourEmulationSpeed));
            }
      if (TourPlaybackManager.Instance.TourPlaybackState != TourPlaybackState.Stopped)
        return;
      this.SetGeotracker((IGeotracker) new GpsGeotracker());
    }

        public override bool IsEnabled
        {
            get
            {
                return this.IsEnabledInternal && this._geotracker.IsEnabled;
            }
        }

        public bool IsEnabledInternal
        {
            get
            {
                return true;//ServiceFacade.SettingsService.GetAppSettings().LocationEnabled;
            }
        }

        public override async Task<Izi.Travel.Geofencing.Primitives.Geolocation> GetPosition()
    {
      if (!this.IsEnabled)
        return (Izi.Travel.Geofencing.Primitives.Geolocation) null;
      
      Izi.Travel.Geofencing.Primitives.Geolocation position = await this._geotracker.GetPosition();
      if (position != null)
        this._position = position;
      return position;
    }

    protected override void OnStart()
    {
      //this._log.Info("Start");
  
      this._geotracker.PositionChanged += (sender, e) 
                =>  this._geotracker_PositionChanged(this._geotracker, this._position);
    }

        protected override void OnStop()
    {
      //this._log.Info("Stop");
 
      //this._geotracker.PositionChanged -= new TypedEventHandler<IGeotracker,
      //                                Geolocation>(this._geotracker_PositionChanged);
    }

        //-------------
        private TypedEventHandler<IGeotracker, Geolocation> _geotrackerPositionChangedHandler;
        private void SetGeotracker(IGeotracker geotracker)
    {
          

        // ...

        _geotrackerPositionChangedHandler = (sender, e) =>  
        this._geotracker_PositionChanged(this._geotracker, this._position);

        // this._geotracker.PositionChanged += _geotrackerPositionChangedHandler;

        // ...
       
        // this._geotracker.PositionChanged -= _geotrackerPositionChangedHandler;
            //----------------

      if (this._geotracker != null && this.IsStarted)
      {
   
        this._geotracker.PositionChanged -= this._geotracker_PositionChanged;
      }
      this._geotracker = geotracker;
      if (!this.IsStarted)
        return;
   
      this._geotracker.PositionChanged += this._geotracker_PositionChanged;
    }

    private void _geotracker_PositionChanged(IGeotracker sender, 
        Izi.Travel.Geofencing.Primitives.Geolocation position)
    {
      if (!this.IsEnabled)
        return;
      if (position != null)
        this._position = position;
      //this.OnPositionChanged(position);//RnD
    }
  }
}
