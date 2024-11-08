// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.TourPlaybackManager
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Data.Entities.Local;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Geofencing;
using System;
using System.Collections.Generic;
using Izi.Travel.Data.Entities.Common; //using System.Device.Location;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using System.Windows.Threading;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml;
using System.Diagnostics;


#nullable disable
namespace Izi.Travel.Business.Managers
{
  public sealed class TourPlaybackManager
  {
    private static volatile TourPlaybackManager _instance;
    private static readonly object SyncRoot = new object();
    //private readonly ILog _log;
    private readonly ILocalDataService _dataService;
    private readonly TourPlaybackAction[] _tourActions;
    private Task _task;

    public static TourPlaybackManager Instance
    {
      get
      {
        if (TourPlaybackManager._instance == null)
        {
          lock (TourPlaybackManager.SyncRoot)
          {
            if (TourPlaybackManager._instance == null)
              TourPlaybackManager._instance = new TourPlaybackManager();
          }
        }
        return TourPlaybackManager._instance;
      }
    }

    public Izi.Travel.Business.Entities.TourPlayback.TourPlayback 
            TourPlayback { get; private set; }

    public string TourPlaybackUid
    {
       get => (string)null;//this.TourPlayback == null ? (string) null : this.TourPlayback.Uid;
    }

    public string TourPlaybackLanguage
    {
            get => (string)null;//this.TourPlayback == null ? (string) null : this.TourPlayback.Language;
    }

    public TourPlaybackState TourPlaybackState { get; private set; }

    public static System.Action<string> NotifyTouristAttractionReached { get; set; }

    public static System.Action NotifyTourStopped { get; set; }

    public event TypedEventHandler<TourPlaybackManager, PositionStatus> PositionStatusChanged
    {
      add
      {
        TypedEventHandler<TourPlaybackManager, PositionStatus> typedEventHandler1 = default;//this.PositionStatusChanged;
        TypedEventHandler<TourPlaybackManager, PositionStatus> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;
                       // Interlocked.CompareExchange<TypedEventHandler<TourPlaybackManager,
                       // PositionStatus>>(ref this.PositionStatusChanged, 
                       // (TypedEventHandler<TourPlaybackManager, PositionStatus>) 
                       // Delegate.Combine((Delegate) typedEventHandler2, 
                       // (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<TourPlaybackManager, PositionStatus> typedEventHandler1 = default;//this.PositionStatusChanged;
        TypedEventHandler<TourPlaybackManager, PositionStatus> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<TourPlaybackManager,
              //PositionStatus>>(ref this.PositionStatusChanged, 
              //(TypedEventHandler<TourPlaybackManager, PositionStatus>) 
              //Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

  
    public event TypedEventHandler<TourPlaybackManager, Geolocation> PositionChanged
    {
           
        add
        {
            TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler1 = default;
               // = this.PositionChanged;
          

            TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler2;
            do
            {
                typedEventHandler2 = typedEventHandler1;
                typedEventHandler1 = default;
                    //Interlocked.CompareExchange<TypedEventHandler<TourPlaybackManager, 
                    //Geolocation>>(ref this.PositionChanged, (TypedEventHandler<TourPlaybackManager, 
                    //Geolocation>)Delegate.Combine((Delegate)typedEventHandler2, (Delegate)value), 
                    //typedEventHandler2);
            }
            while (typedEventHandler1 != typedEventHandler2);
        }
        remove
        {
            TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler1 = default;
                // this.PositionChanged;

            TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler2;
            do
            {
                typedEventHandler2 = typedEventHandler1;
                typedEventHandler1 = default;
                    //Interlocked.CompareExchange<TypedEventHandler<TourPlaybackManager, 
                    //Geolocation>>(ref this.PositionChanged, (TypedEventHandler<TourPlaybackManager, 
                    //Geolocation>)Delegate.Remove((Delegate)typedEventHandler2, 
                    //(Delegate)value), typedEventHandler2);
            }
            while (typedEventHandler1 != typedEventHandler2);
        }
            

            //RnD
            /*add
            {
                TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler1
                    += (sender, e) => this.PositionChanged(sender, e);
            }
            remove
            {
                TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler2
                    -= (sender, e) => this.PositionChanged(sender, e);
            }*/
    }

    public event TypedEventHandler<TourPlaybackManager, EventArgs> TourPlaybackStateChanged
    {
            
          add
          {
            TypedEventHandler<TourPlaybackManager, EventArgs> typedEventHandler1
                   = default;//this.TourPlaybackStateChanged;
            TypedEventHandler<TourPlaybackManager, EventArgs> typedEventHandler2;
            do
            {
              typedEventHandler2 = typedEventHandler1;
              typedEventHandler1 = default;
                        //Interlocked.CompareExchange<TypedEventHandler<TourPlaybackManager, 
                        //EventArgs>>(ref this.TourPlaybackStateChanged,
                        //(TypedEventHandler<TourPlaybackManager, EventArgs>) 
                        //Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), 
                        //typedEventHandler2);
            }
            while (typedEventHandler1 != typedEventHandler2);
          }
          remove
          {
                TypedEventHandler<TourPlaybackManager, EventArgs> typedEventHandler1
                        = default;//this.TourPlaybackStateChanged;
            TypedEventHandler<TourPlaybackManager, EventArgs> typedEventHandler2;
            do
            {
              typedEventHandler2 = typedEventHandler1;
                    typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<TourPlaybackManager,
                  //EventArgs>>(ref this.TourPlaybackStateChanged, (TypedEventHandler<TourPlaybackManager, EventArgs>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
            }
            while (typedEventHandler1 != typedEventHandler2);
          }
            
            /*add
            {
                TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler1
                    += (sender, e) => this.TourPlaybackStateChanged(sender, e);
            }
            remove
            {
                TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler2
                    -= (sender, e) => this.TourPlaybackStateChanged(sender, e);
            }*/
        }

    public event TypedEventHandler<TourPlaybackAttraction, EventArgs> 
            TourPlaybackAttractionIsPlayingChanged
    {
            
      add
      {
                TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler1 = default;//this.TourPlaybackAttractionIsPlayingChanged;
        TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<TourPlaybackAttraction,
              //EventArgs>>(ref this.TourPlaybackAttractionIsPlayingChanged,
              //(TypedEventHandler<TourPlaybackAttraction, EventArgs>)
              //Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
                TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler1 = default;//this.TourPlaybackAttractionIsPlayingChanged;
        TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<TourPlaybackAttraction, 
              //EventArgs>>(ref this.TourPlaybackAttractionIsPlayingChanged, (TypedEventHandler<TourPlaybackAttraction, EventArgs>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
            /*
            add
            {
                TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler1
                    += (sender, e) => this.TourPlaybackAttractionIsPlayingChanged(sender, e);
            }
            remove
            {
                TypedEventHandler<TourPlaybackManager, Geolocation> typedEventHandler2
                    -= (sender, e) => this.TourPlaybackAttractionIsPlayingChanged(sender, e);
            }*/
        }

    public event TypedEventHandler<TourPlaybackAttraction, EventArgs> TourPlaybackAttractionIsVisitedChanged
    {
      add
      {
        TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler1 = default;//this.TourPlaybackAttractionIsVisitedChanged;
        TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;
                        //Interlocked.CompareExchange<TypedEventHandler<TourPlaybackAttraction, EventArgs>>(ref this.TourPlaybackAttractionIsVisitedChanged, (TypedEventHandler<TourPlaybackAttraction, EventArgs>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler1 = default;//this.TourPlaybackAttractionIsVisitedChanged;
        TypedEventHandler<TourPlaybackAttraction, EventArgs> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<TourPlaybackAttraction, EventArgs>>
                        //(ref this.TourPlaybackAttractionIsVisitedChanged, (TypedEventHandler<TourPlaybackAttraction, EventArgs>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public event TypedEventHandler<TourPlaybackTriggerZone, EventArgs> TourPlaybackTriggerZoneStateChanged
    {
      add
      {
        TypedEventHandler<TourPlaybackTriggerZone, EventArgs> typedEventHandler1 = default;//this.TourPlaybackTriggerZoneStateChanged;
        TypedEventHandler<TourPlaybackTriggerZone, EventArgs> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<TourPlaybackTriggerZone, EventArgs>>(
             // ref this.TourPlaybackTriggerZoneStateChanged, (TypedEventHandler<TourPlaybackTriggerZone, EventArgs>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
                TypedEventHandler<TourPlaybackTriggerZone, EventArgs> typedEventHandler1 = default;
                //    = this.TourPlaybackTriggerZoneStateChanged;
        TypedEventHandler<TourPlaybackTriggerZone, EventArgs> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
                    typedEventHandler1 = default;
                    //Interlocked.CompareExchange<TypedEventHandler<TourPlaybackTriggerZone, EventArgs>>(ref this.TourPlaybackTriggerZoneStateChanged, (TypedEventHandler<TourPlaybackTriggerZone, EventArgs>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    private TourPlaybackManager()
    {
      TourPlaybackManager.NotifyTouristAttractionReached 
                = TourPlaybackManager.NotifyTouristAttractionReached ?? (Action<string>) delegate { };

      TourPlaybackManager.NotifyTourStopped = TourPlaybackManager.NotifyTourStopped 
                ?? (System.Action) (() => { });
           
      //RnD
      this._dataService = IoC.Get<ILocalDataService>();
      //this._log = LogManager.GetLog(typeof (TourPlaybackManager));
      
            this._tourActions = new TourPlaybackAction[3]
      {
        TourPlaybackAction.TourStarted,
        TourPlaybackAction.TourPaused,
        TourPlaybackAction.TourStopped
      };
      // ISSUE: method pointer
      GeofenceMonitor.Current.StatusChanged 
                += new TypedEventHandler<GeofenceMonitor, PositionStatus>(OnGeofenceMonitorStatusChanged);
      // ISSUE: method pointer
      GeofenceMonitor.Current.PositionChanged
                += new TypedEventHandler<GeofenceMonitor, 
                Izi.Travel.Geofencing.Primitives.Geolocation>(OnGeofenceMonitorPositionChanged);
      // ISSUE: method pointer
      GeofenceMonitor.Current.GeofenceStateChanged 
                += new TypedEventHandler<GeofenceMonitor, object>(OnGeofenceMonitorGeofenceStateChanged);
      // ISSUE: method pointer
      ServiceFacade.AudioService.StateChanged 
                += new TypedEventHandler<IAudioService, AudioServiceState>(OnAudioPlayerStateChanged);
      this.SetTourPlaybackState(TourPlaybackState.Idle);
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Interval = TimeSpan.FromMinutes(1.0);
      
      //dispatcherTimer.Tick += this.DispatcherTimerTick;
      dispatcherTimer.Tick += (sender, e) => this.DispatcherTimerTick((object) null, (EventArgs) null);
      dispatcherTimer.Start();
    }

    public void DispatcherTimerTick(object sender, EventArgs e)
    {
      if (this.TourPlaybackState != TourPlaybackState.Started)
        return;
      TourPlaybackItem[] playbackItemList = this._dataService.GetLastTourPlaybackItemList();
      if (playbackItemList == null || playbackItemList.Length == 0 || !(DateTime.Now - ((IEnumerable<TourPlaybackItem>) playbackItemList).Last<TourPlaybackItem>().DateTime > TimeSpan.FromMinutes(30.0)))
        return;
      TourPlaybackManager.NotifyTourStopped();
      this.Stop();
    }

    public void Restore()
    {
      if (this.TourPlaybackState != TourPlaybackState.Idle)
        return;
      this.RestoreTourPlaybackAsync();
    }

    public bool Initialize(MtgObject mtgObject)
    {
      try
      {
        if (mtgObject == null || mtgObject.Type != MtgObjectType.Tour || mtgObject.MainContent == null)
          return false;
        if (this.TourPlayback != null && this.Equals(mtgObject.Uid, this.TourPlaybackUid, mtgObject.Language, this.TourPlaybackLanguage))
          return true;
        this.Stop();
        this.SetTourPlaybackState(TourPlaybackState.Initializing);
        this.TourPlayback = TourPlaybackManager.CreateTourPlayback(mtgObject);
        TourPlaybackManager.InitializeGeofences(this.TourPlayback);
        this.SetTourPlaybackState(TourPlaybackState.Initialized);
        return true;
      }
      catch (Exception ex)
      {
        //this._log.Error(ex);
        Debug.WriteLine("[ex] TourPlaybackManager error: " + ex.Message);
        this.SetTourPlaybackState(TourPlaybackState.Error);
        return false;
      }
    }

    public void Start()
    {
      //this._log.Info(nameof (Start));
      if (this.TourPlayback == null 
                || this.TourPlaybackState == TourPlaybackState.Started
                || this.TourPlaybackState != TourPlaybackState.Initialized 
                && this.TourPlaybackState != TourPlaybackState.Paused)
        return;
      ServiceFacade.AudioService.Stop();
      GeofenceMonitor.Current.Start();
      this.SetTourPlaybackState(TourPlaybackState.Started);
    }

    public void Pause(bool stopTourAudio = true)
    {
      //this._log.Info(nameof (Pause));
      if (this.TourPlayback == null)
        return;
      if (this.TourPlaybackState != TourPlaybackState.Started)
        return;
      try
      {
        GeofenceMonitor.Current.Stop();
        if (!stopTourAudio)
          return;
        this.StopTourAudio();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] TourPlaybackManager error: " + ex.Message);
        //this._log.Error(ex);
      }
      finally
      {
        this.SetTourPlaybackState(TourPlaybackState.Paused);
      }
    }

    public void Stop()
    {
      //this._log.Info(nameof (Stop));
      if (this.TourPlayback == null)
        return;
      if (this.TourPlaybackState == TourPlaybackState.Stopped)
        return;
      try
      {
        GeofenceMonitor.Current.Geofences.Clear();
        GeofenceMonitor.Current.Stop();
        this.StopTourAudio();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] TourPlaybackManager error: " + ex.Message);
        //this._log.Error(ex);
      }
      finally
      {
        this._dataService.ClearTourPlaybackItemList(this.TourPlaybackUid);
        this.SetTourPlaybackState(TourPlaybackState.Stopped);
        this.TourPlayback = (Izi.Travel.Business.Entities.TourPlayback.TourPlayback) null;
        this.SetTourPlaybackState(TourPlaybackState.Idle);
      }
    }

    public static bool IsTourAttached(string tourUid, string tourLanguage)
    {
      return !string.IsNullOrWhiteSpace(tourUid) 
                && !string.IsNullOrWhiteSpace(TourPlaybackManager.Instance.TourPlaybackUid)
                && string.Equals(TourPlaybackManager.Instance.TourPlaybackUid, tourUid,
                StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(TourPlaybackManager.Instance.TourPlaybackLanguage,
                tourLanguage, StringComparison.CurrentCultureIgnoreCase);
    }

    public static TourPlaybackAttraction GetAttraction(
      string tourUid,
      string tourLanguage,
      string attractionUid)
    {
      if (!TourPlaybackManager.IsTourAttached(tourUid, tourLanguage))
        return (TourPlaybackAttraction) null;
      return string.IsNullOrWhiteSpace(attractionUid) ? (TourPlaybackAttraction) null : TourPlaybackManager.Instance.TourPlayback.Attractions.FirstOrDefault<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => string.Equals(x.Uid, attractionUid, StringComparison.CurrentCultureIgnoreCase)));
    }

    private static Izi.Travel.Business.Entities.TourPlayback.TourPlayback CreateTourPlayback(
      MtgObject mtgObject)
    {
      if (mtgObject == null || mtgObject.Type != MtgObjectType.Tour)
        return (Izi.Travel.Business.Entities.TourPlayback.TourPlayback) null;

      Izi.Travel.Business.Entities.TourPlayback.TourPlayback tourPlayback 
       = new Izi.Travel.Business.Entities.TourPlayback.TourPlayback()
      {
        Uid = mtgObject.Uid,
        ContentProviderUid = mtgObject.ContentProvider.Uid
      };

      Content mainContent = mtgObject.MainContent;
      if (mainContent != null)
      {
        tourPlayback.Title = mainContent.Title;
        tourPlayback.Language = mainContent.Language;
        if (mainContent.Children != null)
        {
          List<string> order = mainContent.Playback != null
                        ? ((IEnumerable<string>) mainContent.Playback.Order).ToList<string>() 
                        : new List<string>();

          IEnumerable<MtgObject> source = ((IEnumerable<MtgObject>) mainContent.Children)
                        .Where<MtgObject>((Func<MtgObject, bool>)
                        (x => x.Type == MtgObjectType.TouristAttraction));

          foreach (MtgObject attraction in (IEnumerable<MtgObject>) source
                        .OrderBy<MtgObject, int>((Func<MtgObject, int>) (x => order.IndexOf(x.Uid))))
          {
            TourPlaybackAttraction tourAttraction = new TourPlaybackAttraction(tourPlayback)
            {
              Uid = attraction.Uid,
              Order = order.IndexOf(attraction.Uid),
              IsHidden = attraction.Hidden
            };
            if (attraction.Location != null)
              tourAttraction.Location = attraction.Location.ToGeoCoordinate();
            if (attraction.MainContent != null)
              tourAttraction.Title = attraction.MainContent.Title;
            tourAttraction.TriggerZones.AddRange(
                TourPlaybackManager.CreateTourPlaybackTriggerZones(tourAttraction, attraction));
            tourPlayback.Attractions.Add(tourAttraction);
          }
        }
      }
      if (mtgObject.Map != null && mtgObject.Map.Route != null)
        tourPlayback.Route = ((IEnumerable<GeoLocation>) mtgObject.Map.Route).Select<GeoLocation, GeoCoordinate>((Func<GeoLocation, GeoCoordinate>) (x => x.ToGeoCoordinate())).ToArray<GeoCoordinate>();
      return tourPlayback;
    }

    private static void InitializeGeofences(
        Izi.Travel.Business.Entities.TourPlayback.TourPlayback tourPlayback)
    {
      if (tourPlayback == null || tourPlayback.Attractions == null)
        return;
      GeofenceMonitor.Current.Geofences.Clear();
      foreach (Geofence geofence in tourPlayback.Attractions
                .SelectMany<TourPlaybackAttraction, Geofence>(
          new Func<TourPlaybackAttraction, IEnumerable<Geofence>>(
              TourPlaybackManager.CreateTourPlaybackGeofences)))
        GeofenceMonitor.Current.Geofences.Add(geofence);
    }

    private static IEnumerable<TourPlaybackTriggerZone> CreateTourPlaybackTriggerZones(
      TourPlaybackAttraction tourAttraction,
      MtgObject attraction)
    {
      return attraction == null || attraction.TriggerZones == null 
                || attraction.TriggerZones.Length == 0 
                ? (IEnumerable<TourPlaybackTriggerZone>) new List<TourPlaybackTriggerZone>()
                : (IEnumerable<TourPlaybackTriggerZone>) ((IEnumerable<TriggerZone>) attraction.TriggerZones).Where<TriggerZone>((Func<TriggerZone, bool>) (x => x.Geoshape != null)).Select<TriggerZone, TourPlaybackTriggerZone>((Func<TriggerZone, int, TourPlaybackTriggerZone>) ((x, i) => new TourPlaybackTriggerZone(tourAttraction)
      {
        Uid = string.Format("{0}_{1}", (object) attraction.Uid, (object) (i + 1)),
        Geoshape = x.Geoshape
      })).ToList<TourPlaybackTriggerZone>();
    }

    private static IEnumerable<Geofence> CreateTourPlaybackGeofences(
      TourPlaybackAttraction attraction)
    {
      return attraction == null 
                || attraction.TriggerZones == null 
                || attraction.TriggerZones.Count == 0
                ? (IEnumerable<Geofence>) new List<Geofence>() 
                : (IEnumerable<Geofence>) attraction.TriggerZones.Select<TourPlaybackTriggerZone, 
                Geofence>((Func<TourPlaybackTriggerZone, Geofence>) (x => new Geofence(x.Uid, x.Geoshape)
      {
        Tag = (object) attraction.Uid
      })).ToList<Geofence>();
    }

    private async void RestoreTourPlaybackAsync()
    {
      try
      {
        this.SetTourPlaybackState(TourPlaybackState.Initializing);
        TourPlaybackItem[] items = this._dataService.GetLastTourPlaybackItemList();
        if (items == null || items.Length == 0)
        {
          this.SetTourPlaybackState(TourPlaybackState.Idle);
        }
        else
        {
          TourPlaybackItem actualItem = items[items.Length - 1];
          if (actualItem.Action == TourPlaybackAction.TourStopped)
          {
            this.SetTourPlaybackState(TourPlaybackState.Idle);
          }
          else
          {
            MtgObjectFilter filter = new MtgObjectFilter(actualItem.TourUid, new string[1]
            {
              actualItem.Language
            });
            filter.Includes = ContentSection.Children;
            MtgObject mtgObjectAsync = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
            if (mtgObjectAsync == null)
            {
              this.SetTourPlaybackState(TourPlaybackState.Idle);
            }
            else
            {
              this.TourPlayback = TourPlaybackManager.CreateTourPlayback(mtgObjectAsync);
              if (this.TourPlayback == null)
              {
                this.SetTourPlaybackState(TourPlaybackState.Idle);
              }
              else
              {
                TourPlaybackManager.InitializeGeofences(this.TourPlayback);
                this.SetTourPlaybackState(TourPlaybackState.Initialized);
                foreach (IGrouping<string, TourPlaybackItem> grouping in ((IEnumerable<TourPlaybackItem>) items).Where<TourPlaybackItem>((Func<TourPlaybackItem, bool>) (x => !string.IsNullOrWhiteSpace(x.ChildUid))).GroupBy<TourPlaybackItem, string>((Func<TourPlaybackItem, string>) (x => x.ChildUid)).ToList<IGrouping<string, TourPlaybackItem>>())
                {
                  IGrouping<string, TourPlaybackItem> itemGroup = grouping;
                  TourPlaybackAttraction playbackAttraction = this.TourPlayback.Attractions.FirstOrDefault<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => string.Equals(x.Uid, itemGroup.Key, StringComparison.CurrentCultureIgnoreCase)));
                  if (playbackAttraction != null && itemGroup.OrderByDescending<TourPlaybackItem, DateTime>((Func<TourPlaybackItem, DateTime>) (x => x.DateTime)).FirstOrDefault<TourPlaybackItem>() != null && itemGroup.Any<TourPlaybackItem>((Func<TourPlaybackItem, bool>) (x => x.Action == TourPlaybackAction.AttractionVisited)))
                    playbackAttraction.IsVisited = true;
                }
                if (actualItem.Action != TourPlaybackAction.TourPaused)
                  this.SetTourPlaybackState(TourPlaybackState.Paused);
                else
                  this.TourPlaybackState = TourPlaybackState.Paused;
                items = (TourPlaybackItem[]) null;
                actualItem = (TourPlaybackItem) null;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] TourPlaybackManager - RestoreTourPlaybackAsync error: " + ex.Message);
        //this._log.Error(ex);
        this.SetTourPlaybackState(TourPlaybackState.Error);
      }
    }

    private void ProcessGeofence(GeofenceStateChangeReport reportItem)
    {
      if (reportItem == null || this.TourPlayback == null || this.TourPlayback.Attractions == null)
        return;
      string attractionId = reportItem.Geofence.Tag as string;
      if (string.IsNullOrWhiteSpace(attractionId))
        return;
      TourPlaybackAttraction attraction = this.TourPlayback.Attractions.FirstOrDefault<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => x.Uid == attractionId));
      if (attraction == null)
        return;
      TourPlaybackTriggerZone triggerZone = attraction.TriggerZones.FirstOrDefault<TourPlaybackTriggerZone>((Func<TourPlaybackTriggerZone, bool>) (x => x.Uid == reportItem.Geofence.Id));
      if (triggerZone == null)
        return;

      //this._log.Info("{0} zone state switched to {1}", (object) triggerZone.Uid, (object) reportItem.NewState);
      Debug.WriteLine("[i] " + "{0} zone state switched to {1}", (object)triggerZone.Uid,
          (object)reportItem.NewState);
            
      if (reportItem.NewState == GeofenceState.Entered)
      {
        this.SetTourPlaybackTriggerZoneState(triggerZone, TourPlaybackTriggerZoneState.Entered);
        if (attraction.IsPlaying)
        {
          this.SetTourPlaybackAttractionIsVisited(attraction, true);
        }
        else
        {
          if (this._task != null && !this._task.IsCompleted)
            return;
          this._task = this.PlayAsync(attraction, new List<AudioServiceState>()
          {
            AudioServiceState.Playing
          });
        }
      }
      else
      {
        if (reportItem.NewState != GeofenceState.Exited)
          return;
        this.SetTourPlaybackTriggerZoneState(triggerZone, TourPlaybackTriggerZoneState.Leaved);
      }
    }

    private void StopTourAudio()
    {
      try
      {
        AudioTrackInfo trackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
        if (trackInfo == null || this.TourPlayback == null 
                    || this.TourPlayback.Attractions == null 
                    || !this.TourPlayback.Attractions.Any<TourPlaybackAttraction>(
                        (Func<TourPlaybackAttraction, bool>) 
                        (x => this.Equals(trackInfo.MtgObjectUid, x.Uid, trackInfo.Language,
                        this.TourPlayback.Language))))
          return;

        ServiceFacade.AudioService.Stop();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] StopTourAudio error: " + ex.Message);
        //this._log.Error(ex);
      }
    }

    private async Task PlayAsync(
      TourPlaybackAttraction attraction,
      List<AudioServiceState> cancelPlayStates)
    {
      if (attraction == null || attraction.TourPlayback == null || attraction.IsVisited)
        return;

      //this._log.Info("Play {0}", (object) attraction.Uid);
      
      if (attraction.AudioTrackInfo == null)
      {
        MtgObjectFilter filter = new MtgObjectFilter(attraction.Uid, new string[1]
        {
          attraction.TourPlayback.Language
        });
        filter.Includes = ContentSection.None;
        filter.Excludes = ContentSection.All;
        MtgObject mtgObjectAsync = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
        if (mtgObjectAsync == null || mtgObjectAsync.MainAudioMedia == null)
          return;
        attraction.AudioTrackInfo = AudioTrackInfoHelper.FromMtgObject(mtgObjectAsync, 
            this.TourPlaybackUid, MtgObjectType.Tour, ActivationTypeParameter.Gps);
      }
      if (!cancelPlayStates.Contains(ServiceFacade.AudioService.State))
      {
        ServiceFacade.AudioService.Play(attraction.AudioTrackInfo);
      }
      else
      {
        AudioTrackInfo currentTrackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
        if (currentTrackInfo != null && currentTrackInfo.MtgObjectUid == attraction.Uid)
          return;
        TourPlaybackManager.NotifyTouristAttractionReached(attraction.Uid);
      }
    }

    private void SetTourPlaybackState(TourPlaybackState state)
    {
      if (this.TourPlaybackState == state)
        return;
      this.TourPlaybackState = state;
      this.AddTourPlaybackAction(this.TourPlayback, (TourPlaybackAttraction) null, 
          TourPlaybackManager.GetTourPlaybackAction(this.TourPlaybackState));

      // RnD
      //this.TourPlaybackStateChanged?.Invoke(this, EventArgs.Empty); 
    }

    private void SetTourPlaybackTriggerZoneState(
      TourPlaybackTriggerZone triggerZone,
      TourPlaybackTriggerZoneState state)
    {
      if (triggerZone == null || triggerZone.State == state)
        return;
      triggerZone.State = state;
      if (triggerZone.TourAttraction != null && triggerZone.TourAttraction.TourPlayback != null)
        this.AddTourPlaybackAction(triggerZone.TourAttraction.TourPlayback, triggerZone.TourAttraction, TourPlaybackManager.GetTourPlaybackTriggerZoneAction(state));
      // RnD
      //this.TourPlaybackTriggerZoneStateChanged?.Invoke(triggerZone, EventArgs.Empty);
    }

    private void SetTourPlaybackAttractionIsPlaying(
      TourPlaybackAttraction attraction,
      bool isPlaying)
    {
      if (attraction == null || attraction.IsPlaying == isPlaying)
        return;
      attraction.IsPlaying = isPlaying;
      if (attraction.TourPlayback != null & isPlaying)
        this.AddTourPlaybackAction(attraction.TourPlayback, attraction, 
            TourPlaybackAction.AttractionPlaying);

      // RnD
      //this.TourPlaybackAttractionIsPlayingChanged?.Invoke(attraction, EventArgs.Empty);
    }

    private void SetTourPlaybackAttractionIsVisited(
      TourPlaybackAttraction attraction,
      bool isVisited)
    {
      if (attraction == null || attraction.IsVisited == isVisited)
        return;
      attraction.IsVisited = isVisited;
      if (attraction.TourPlayback != null & isVisited)
        this.AddTourPlaybackAction(attraction.TourPlayback, attraction, TourPlaybackAction.AttractionVisited);
      // ISSUE: reference to a compiler-generated field
      //this.TourPlaybackAttractionIsVisitedChanged?.Invoke(attraction, EventArgs.Empty);
    }

    private void AddTourPlaybackAction(
      Izi.Travel.Business.Entities.TourPlayback.TourPlayback tourPlayback,
      TourPlaybackAttraction attraction,
      TourPlaybackAction action)
    {
      if (action == TourPlaybackAction.Unknown || tourPlayback == null || attraction == null && !((IEnumerable<TourPlaybackAction>) this._tourActions).Contains<TourPlaybackAction>(action))
        return;
      TourPlaybackItem tourPlaybackItem = new TourPlaybackItem()
      {
        TourUid = tourPlayback.Uid,
        Language = tourPlayback.Language,
        DateTime = DateTime.Now,
        Action = action
      };
      if (attraction != null)
        tourPlaybackItem.ChildUid = attraction.Uid;
      this._dataService.CreateTourPlaybackItem(tourPlaybackItem);
    }

    private static TourPlaybackAction GetTourPlaybackAction(TourPlaybackState state)
    {
      switch (state)
      {
        case TourPlaybackState.Started:
          return TourPlaybackAction.TourStarted;
        case TourPlaybackState.Paused:
          return TourPlaybackAction.TourPaused;
        case TourPlaybackState.Stopped:
          return TourPlaybackAction.TourStopped;
        default:
          return TourPlaybackAction.Unknown;
      }
    }

    private static TourPlaybackAction GetTourPlaybackTriggerZoneAction(
      TourPlaybackTriggerZoneState state)
    {
      if (state == TourPlaybackTriggerZoneState.Entered)
        return TourPlaybackAction.AttractionEntered;
      return state == TourPlaybackTriggerZoneState.Leaved 
                ? TourPlaybackAction.AttractionLeaved
                : TourPlaybackAction.Unknown;
    }

    private bool IsTourTrack(AudioTrackInfo trackInfo)
    {
      return trackInfo != null 
                && this.TourPlayback != null && this.TourPlayback.Attractions != null 
                && this.TourPlayback.Attractions.Any<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => this.Equals(trackInfo.MtgObjectUid, x.Uid, trackInfo.Language, this.TourPlayback.Language)));
    }

    private bool Equals(string uid1, string uid2, string language1, string language2)
    {
      return uid1 != null && language1 != null && string.Equals(uid1, uid2,
          StringComparison.CurrentCultureIgnoreCase) && string.Equals(language1, language2, 
          StringComparison.CurrentCultureIgnoreCase);
    }

    private void OnGeofenceMonitorStatusChanged(GeofenceMonitor monitor, PositionStatus status)
    {
      // RnD
      //this.PositionStatusChanged?.Invoke(this, status);
    }

    private void OnGeofenceMonitorPositionChanged(GeofenceMonitor monitor,
        Izi.Travel.Geofencing.Primitives.Geolocation position)
    {
      // RnD
      //this.PositionChanged?.Invoke(this, new GeoCoordinate(position.Latitude,
      //position.Longitude, position.Latitude));
    }

    private void OnGeofenceMonitorGeofenceStateChanged(GeofenceMonitor monitor, object args)
    {
      IReadOnlyList<GeofenceStateChangeReport> stateChangeReportList1 = monitor.ReadReport();
      if (stateChangeReportList1.Count == 0)
        return;
      IReadOnlyList<GeofenceStateChangeReport> stateChangeReportList2 = stateChangeReportList1;
      this.ProcessGeofence(stateChangeReportList2[stateChangeReportList2.Count - 1]);
    }

    private void OnAudioPlayerStateChanged(object sender, AudioServiceState state)
    {
      if (this.TourPlayback == null)
        return;
      AudioTrackInfo trackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
      if (trackInfo != null && this.IsTourTrack(trackInfo))
      {
        TourPlaybackAttraction attraction = this.TourPlayback.Attractions.FirstOrDefault<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => string.Equals(x.Uid, trackInfo.MtgObjectUid, StringComparison.CurrentCultureIgnoreCase)));
        if (attraction != null)
        {
          if (state == AudioServiceState.Playing)
          {
            this.SetTourPlaybackAttractionIsPlaying(attraction, true);
            if (attraction.TriggerZones.Any<TourPlaybackTriggerZone>((Func<TourPlaybackTriggerZone, bool>) (x => x.State == TourPlaybackTriggerZoneState.Entered)))
              this.SetTourPlaybackAttractionIsVisited(attraction, true);
          }
          else
            this.SetTourPlaybackAttractionIsPlaying(attraction, false);
        }
      }
      else if (state != AudioServiceState.Unknown)
        this.Pause();
      if (state == AudioServiceState.Stopped)
        ((System.Action) (async () =>
        {
          await Task.Delay(1000);
          if (this.TourPlaybackState != TourPlaybackState.Started)
            return;
          TourPlaybackAttraction attraction = this.TourPlayback.Attractions.OrderBy<TourPlaybackAttraction, int>((Func<TourPlaybackAttraction, int>) (x => x.Order)).Where<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => !x.IsVisited)).FirstOrDefault<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => x.TriggerZones.Any<TourPlaybackTriggerZone>((Func<TourPlaybackTriggerZone, bool>) (y => y.State == TourPlaybackTriggerZoneState.Entered))));
          if (attraction == null)
            return;
          this.PlayAsync(attraction, new List<AudioServiceState>()
          {
            AudioServiceState.Playing,
            AudioServiceState.Paused
          });
        }))();
      if (this.TourPlaybackState != TourPlaybackState.Started || state == AudioServiceState.Playing || !this.TourPlayback.Attractions.All<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => !x.IsHidden && x.IsVisited)))
        return;
      this.Pause(false);
    }
  }
}
