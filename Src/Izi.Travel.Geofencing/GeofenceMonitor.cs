// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.GeofenceMonitor
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

//RnD
//using Caliburn.Micro;
using Izi.Travel.Geofencing.Geotracker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Geofencing
{
  public sealed class GeofenceMonitor
  {
    private static volatile GeofenceMonitor _current;
    private static readonly object SyncRoot = new object();
    private readonly GeofenceCollection _geofences;
    private static List<GeofenceStateChangeReport> _report;
    private IGeotracker _geotracker;
    //private ILog _log;

    public static GeofenceMonitor Current
    {
      get
      {
        if (GeofenceMonitor._current == null)
        {
          lock (GeofenceMonitor.SyncRoot)
          {
            if (GeofenceMonitor._current == null)
              GeofenceMonitor._current = new GeofenceMonitor();
          }
        }
        return GeofenceMonitor._current;
      }
    }

    public static Func<IGeotracker> GetGeotracker { get; set; }

    public Izi.Travel.Geofencing.Primitives.Geolocation Position { get; private set; }

    public PositionStatus Status { get; private set; }

    public bool IsStarted { get; private set; }

    public IList<Geofence> Geofences => (IList<Geofence>) this._geofences;

    //RnD
    public event TypedEventHandler<GeofenceMonitor, object> GeofenceStateChanged
    {
      add
      {
            TypedEventHandler<GeofenceMonitor, object> typedEventHandler1 = default;// += GeofenceStateChanged;
            TypedEventHandler<GeofenceMonitor, object> typedEventHandler2;
            do
            {
                typedEventHandler2 = typedEventHandler1;
                /*typedEventHandler1 = Interlocked.CompareExchange(
                    ref GeofenceStateChanged,
                    (TypedEventHandler<GeofenceMonitor, object>)Delegate.Combine(
                        typedEventHandler2, value),
                    typedEventHandler2);*/
            }
            while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
                TypedEventHandler<GeofenceMonitor, object> typedEventHandler1 = default;
                //    = this.GeofenceStateChanged;
        TypedEventHandler<GeofenceMonitor, object> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
         // typedEventHandler1 = 
         //               Interlocked.CompareExchange<TypedEventHandler<GeofenceMonitor, object>>
         //               (ref this.GeofenceStateChanged, (TypedEventHandler<GeofenceMonitor, object>) 
         //               Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value),
         //               typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

        //RnD
        public event TypedEventHandler<GeofenceMonitor, PositionStatus> StatusChanged
        {
            add
            {
                TypedEventHandler<GeofenceMonitor, PositionStatus> typedEventHandler1 = default;
                //    = this.StatusChanged;
                TypedEventHandler<GeofenceMonitor, PositionStatus> typedEventHandler2;
                do
                {
                    typedEventHandler2 = typedEventHandler1;
                    //typedEventHandler1 = Interlocked.CompareExchange(ref this.StatusChanged,
                    //    (TypedEventHandler<GeofenceMonitor, PositionStatus>)
                    //    Delegate.Combine(typedEventHandler2, value), typedEventHandler2);
                }
                while (typedEventHandler1 != typedEventHandler2);
            }
            remove
            {
                TypedEventHandler<GeofenceMonitor, PositionStatus> typedEventHandler1 = default;
                //    = this.StatusChanged;
                TypedEventHandler<GeofenceMonitor, PositionStatus> typedEventHandler2;
                do
                {
                    typedEventHandler2 = typedEventHandler1;
                    //typedEventHandler1 = Interlocked.CompareExchange(ref this.StatusChanged,
                    //    (TypedEventHandler<GeofenceMonitor, PositionStatus>)
                    //    Delegate.Remove(typedEventHandler2, value), typedEventHandler2);
                }
                while (typedEventHandler1 != typedEventHandler2);
            }
        }

        public event TypedEventHandler<GeofenceMonitor, 
            Izi.Travel.Geofencing.Primitives.Geolocation> PositionChanged
        {
            add
            {
                TypedEventHandler<GeofenceMonitor,
                    Izi.Travel.Geofencing.Primitives.Geolocation>
                 typedEventHandler1 = default;// this.PositionChanged;

                TypedEventHandler<GeofenceMonitor, 
                    Izi.Travel.Geofencing.Primitives.Geolocation> typedEventHandler2;
                do
                {
                    typedEventHandler2 = typedEventHandler1;
                    //typedEventHandler1 = Interlocked.CompareExchange(ref this.PositionChanged,
                    //    (TypedEventHandler<GeofenceMonitor, 
                    //    Izi.Travel.Geofencing.Primitives.Geolocation>)
                    //    Delegate.Combine(typedEventHandler2, value), typedEventHandler2);
                }
                while (typedEventHandler1 != typedEventHandler2);
            }
            remove
            {
                TypedEventHandler<GeofenceMonitor,
                    Izi.Travel.Geofencing.Primitives.Geolocation> typedEventHandler1 = default;
                //    = this.PositionChanged;

                TypedEventHandler<GeofenceMonitor, 
                    Izi.Travel.Geofencing.Primitives.Geolocation> typedEventHandler2;
                do
                {
                    typedEventHandler2 = typedEventHandler1;
                    //typedEventHandler1 = Interlocked.CompareExchange(ref this.PositionChanged,
                    //    (TypedEventHandler<GeofenceMonitor, 
                    //    Izi.Travel.Geofencing.Primitives.Geolocation>)
                    //    Delegate.Remove(typedEventHandler2, value), typedEventHandler2);
                }
                while (typedEventHandler1 != typedEventHandler2);
            }
        }

    private GeofenceMonitor()
    {
      //this._log = LogManager.GetLog(typeof (GeofenceMonitor));
      GeofenceMonitor._report = new List<GeofenceStateChangeReport>();
      this._geofences = new GeofenceCollection();
      this._geofences.CollectionChanged += new NotifyCollectionChangedEventHandler(
          this.OnGeofencesCollectionChanged);
    }

    public async void Start()
    {
            if (!this.IsStarted)
            {
                this._geotracker = this._geotracker ?? GeofenceMonitor.GetGeotracker();
                // ISSUE: method pointer
                //this._geotracker.PositionChanged += 
                //            new TypedEventHandler<IGeotracker,
                //            Izi.Travel.Geofencing.Primitives.Geolocation>((object) this, 
                //            __methodptr(OnGeotrackerPositionChanged));
                this._geotracker.PositionChanged += OnGeotrackerPositionChanged;

                this.IsStarted = true;
                //this._log.Info(nameof (Start));
                IGeotracker sender = this._geotracker;
                Izi.Travel.Geofencing.Primitives.Geolocation position
                            = await this._geotracker.GetPosition();
                this.OnGeotrackerPositionChanged(sender, position);
                sender = (IGeotracker)null;
            }
            else
            {
                //this._log.Info("Start when started");
            }
    }

    public void Stop()
    {
            if (this.IsStarted)
            {
                GeofenceMonitor._report.Clear();
                this._geotracker.PositionChanged -= OnGeotrackerPositionChanged;
                this.IsStarted = false;
                //this._log.Info(nameof (Stop));
            }
            else
            {
                //this._log.Info("Stop when stopped");
            }
    }

    public IReadOnlyList<GeofenceStateChangeReport> ReadReport()
    {
      return (IReadOnlyList<GeofenceStateChangeReport>) GeofenceMonitor._report;
    }

    private void TrackGeofences(Izi.Travel.Geofencing.Primitives.Geolocation position)
    {
      foreach (Geofence geofence in (Collection<Geofence>) this._geofences)
      {
        GeofenceState geofenceState = GeofenceState.None;
        GeofenceStateChangeReport lastReport = this.GetLastReport(geofence.Id);
        if (lastReport != null)
          geofenceState = lastReport.NewState;
        GeofenceState newState = geofenceState;
        bool flag = geofence.Geoshape.Contains(position);
        if (geofenceState == GeofenceState.Entered && !flag)
          newState = GeofenceState.Exited;
        else if (((geofenceState == GeofenceState.Exited 
                    ? 1 
                    : (geofenceState == GeofenceState.None ? 1 : 0)) & (flag ? 1 : 0)) != 0)
          newState = GeofenceState.Entered;
        if (newState != geofenceState)
        {
          this.AddReport(new GeofenceStateChangeReport(geofence, position, newState));
          this.OnGeofenceStateChanged();
        }
      }
    }

    private GeofenceStateChangeReport GetLastReport(string geofenceId)
    {
      lock (GeofenceMonitor._report)
        return GeofenceMonitor._report.LastOrDefault<GeofenceStateChangeReport>(
            (Func<GeofenceStateChangeReport, bool>) (x => x.Geofence.Id == geofenceId));
    }

    private void AddReport(GeofenceStateChangeReport report)
    {
      lock (GeofenceMonitor._report)
        GeofenceMonitor._report.Add(report);
    }

        private void OnGeofenceStateChanged()
        {
            //this.GeofenceStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnStatusChanged()
        {
           //this.StatusChanged?.Invoke(this, this.Status);// EventArgs.Empty);
        }

    private void OnPositionChanged()
    {
      //this.PositionChanged?.Invoke(this, this.Position);
    }

    private void OnGeotrackerStatusChanged(IGeotracker sender, PositionStatus status)
    {
      this.Status = status;
      this.OnStatusChanged();
    }

    private void OnGeotrackerPositionChanged(IGeotracker sender, 
        Izi.Travel.Geofencing.Primitives.Geolocation position)
    {
      this.Position = position;
      this.OnPositionChanged();
      this.TrackGeofences(this.Position);
    }

    private void OnGeofencesCollectionChanged(object sender, 
        NotifyCollectionChangedEventArgs e)
    {
       //
    }
  }
}
