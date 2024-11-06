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
using Izi.Travel.Geofencing.Primitives;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Business.Managers
{
  public class Geotracker : BaseGeotracker
  {
    private static Izi.Travel.Business.Managers.Geotracker _instance;
    private static readonly object SyncRoot = new object();
    private IGeotracker _geotracker;
    private Geolocation _position;
    private ILog _log;

    public static Izi.Travel.Business.Managers.Geotracker Instance
    {
      get
      {
        if (Izi.Travel.Business.Managers.Geotracker._instance == null)
        {
          lock (Izi.Travel.Business.Managers.Geotracker.SyncRoot)
          {
            if (Izi.Travel.Business.Managers.Geotracker._instance == null)
              Izi.Travel.Business.Managers.Geotracker._instance = new Izi.Travel.Business.Managers.Geotracker();
          }
        }
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

    public Geolocation Position => !this.IsEnabled ? (Geolocation) null : this._position;

    private Geotracker()
    {
      this._log = LogManager.GetLog(typeof (Izi.Travel.Business.Managers.Geotracker));
      this.SetGeotracker((IGeotracker) new GpsGeotracker());
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackStateChanged += new TypedEventHandler<TourPlaybackManager, EventArgs>((object) this, __methodptr(TourPlaybackManager_TourPlaybackStateChanged));
    }

    private void TourPlaybackManager_TourPlaybackStateChanged(
      TourPlaybackManager sender,
      EventArgs args)
    {
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      if (!appSettings.TourEmulationEnabled)
        return;
      if (TourPlaybackManager.Instance.TourPlaybackState == TourPlaybackState.Started && TourPlaybackManager.Instance.TourPlayback.Route != null)
        this.SetGeotracker((IGeotracker) new RouteGeotracker(((IEnumerable<GeoCoordinate>) TourPlaybackManager.Instance.TourPlayback.Route).Select<GeoCoordinate, Geolocation>((Func<GeoCoordinate, Geolocation>) (x => new Geolocation(x.Latitude, x.Longitude))).ToArray<Geolocation>(), appSettings.TourEmulationSpeed));
      if (TourPlaybackManager.Instance.TourPlaybackState != TourPlaybackState.Stopped)
        return;
      this.SetGeotracker((IGeotracker) new GpsGeotracker());
    }

    public override bool IsEnabled => this.IsEnabledInternal && this._geotracker.IsEnabled;

    public bool IsEnabledInternal => ServiceFacade.SettingsService.GetAppSettings().LocationEnabled;

    public override async Task<Geolocation> GetPosition()
    {
      if (!this.IsEnabled)
        return (Geolocation) null;
      Geolocation position = await this._geotracker.GetPosition();
      if (position != null)
        this._position = position;
      return position;
    }

    protected override void OnStart()
    {
      this._log.Info("Start");
      // ISSUE: method pointer
      this._geotracker.PositionChanged += new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(_geotracker_PositionChanged));
    }

    protected override void OnStop()
    {
      this._log.Info("Stop");
      // ISSUE: method pointer
      this._geotracker.PositionChanged -= new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(_geotracker_PositionChanged));
    }

    private void SetGeotracker(IGeotracker geotracker)
    {
      if (this._geotracker != null && this.IsStarted)
      {
        // ISSUE: method pointer
        this._geotracker.PositionChanged -= new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(_geotracker_PositionChanged));
      }
      this._geotracker = geotracker;
      if (!this.IsStarted)
        return;
      // ISSUE: method pointer
      this._geotracker.PositionChanged += new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(_geotracker_PositionChanged));
    }

    private void _geotracker_PositionChanged(IGeotracker sender, Geolocation position)
    {
      if (!this.IsEnabled)
        return;
      if (position != null)
        this._position = position;
      this.OnPositionChanged(position);
    }
  }
}
