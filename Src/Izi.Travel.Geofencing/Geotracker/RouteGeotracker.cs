// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Geotracker.RouteGeotracker
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Helpers;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Geofencing.Geotracker
{
  public sealed class RouteGeotracker : BaseGeotracker
  {
    private const long DefaultEmulationUpdatePeriod = 500;
    private const long MinEmulationUpdatePeriod = 100;
    private const long MaxEmulationUpdatePeriod = 5000;
    private Geolocator _geolocator;
    private Geolocator _locationStatusGeolocator;
    private Izi.Travel.Geofencing.Primitives.Geolocation _actualPosition;
    private Timer _timer;
    private readonly long _period;
    private readonly double _speed;
    private readonly Izi.Travel.Geofencing.Primitives.Geolocation[] _route;
    private readonly double _routeDistance;
    private long _startTime;
    private int _lastRouteIndex;
    private double _lastRouteDistance;

    public RouteGeotracker(Izi.Travel.Geofencing.Primitives.Geolocation[] route, 
        double speed, long period = 500)
    {
      this._startTime = 0L;
      this._routeDistance = 0.0;
      this._lastRouteIndex = 1;
      this._lastRouteDistance = 0.0;
      this._period = this._period < 100L || this._period > 5000L ? 500L : period;
      if (route == null || route.Length <= 1 || speed <= 0.0)
        return;
      this._route = route;
      this._speed = speed / 3.6;
      this._routeDistance = GeoHelper.CalculateDistance(route);
    }

    public override bool IsEnabled
    {
      get
      {
        return (this._locationStatusGeolocator = this._locationStatusGeolocator 
                    ?? new Geolocator()).LocationStatus != PositionStatus.Disabled;
      }
    }

    public override async Task<Izi.Travel.Geofencing.Primitives.Geolocation> GetPosition()
    {
      if (!this.IsEnabled || this._route == null || this._route.Length == 0)
        return (Izi.Travel.Geofencing.Primitives.Geolocation) null;

      return this._actualPosition != null 
                ? this._actualPosition
                : await Task.FromResult<Izi.Travel.Geofencing.Primitives.Geolocation>(
                    this._route[0]);
    }

    protected override void OnStart()
    {
      if (!this.IsEnabled)
        return;
      Geolocator geolocator1 = new Geolocator();
      geolocator1.ReportInterval = (uint) this._period;
      this._geolocator = geolocator1;
      Geolocator geolocator2 = this._geolocator;

      // RnD / TODO
      //WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Geolocator, 
      //    PositionChangedEventArgs>>(new Func<TypedEventHandler<Geolocator,
      //    PositionChangedEventArgs>, EventRegistrationToken>(geolocator2.add_PositionChanged),
      //    new Action<EventRegistrationToken>(geolocator2.remove_PositionChanged), 
      //    new TypedEventHandler<Geolocator, PositionChangedEventArgs>((object) this, 
      //    __methodptr(OnGeolocatorPositionChanged)));

      this._timer = new Timer(new TimerCallback(this.RouteTimerCallback), 
          (object) null, 0, (int)this._period);
    }

    protected override void OnStop()
    {
      if (this._timer != null)
      {
        this._timer.Dispose();
        this._timer = (Timer) null;
      }
      if (this._geolocator != null)
      {
        //RnD / TODO
        // ISSUE: method pointer
        //WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<Geolocator, 
        //    PositionChangedEventArgs>>(new Action<EventRegistrationToken>(
        //        this._geolocator.remove_PositionChanged), 
        //        new TypedEventHandler<Geolocator, PositionChangedEventArgs>(
        //            (object) this, __methodptr(OnGeolocatorPositionChanged)));
      }
      this._geolocator = (Geolocator) null;
      this._actualPosition = (Izi.Travel.Geofencing.Primitives.Geolocation) null;
    }

    private void RouteTimerCallback(object state)
    {
      if (this._route == null || this._route.Length == 0)
        return;
      this._actualPosition = (Izi.Travel.Geofencing.Primitives.Geolocation) null;
      long num1 = DateTime.Now.Ticks / 10000L;
      if (this._startTime == 0L)
      {
        this._actualPosition = this._route[0];
        this._startTime = num1;
      }
      else
      {
        double num2 = this._speed * (double) ((num1 - this._startTime) / 1000L);
        if (num2 >= this._routeDistance)
        {
          this._actualPosition = this._route[this._route.Length - 1];
        }
        else
        {
          for (; this._lastRouteIndex < this._route.Length; ++this._lastRouteIndex)
          {
            Izi.Travel.Geofencing.Primitives.Geolocation source
                            = this._route[this._lastRouteIndex - 1];

            Izi.Travel.Geofencing.Primitives.Geolocation target
                            = this._route[this._lastRouteIndex];

            double distance = GeoHelper.CalculateDistance(source, target);
            if (Math.Abs(num2 - (this._lastRouteDistance + distance)) < double.Epsilon)
            {
              this._actualPosition = target;
              break;
            }
            if (num2 < this._lastRouteDistance + distance)
            {
              double num3 = (num2 - this._lastRouteDistance) / distance;
              this._actualPosition = 
                                new Izi.Travel.Geofencing.Primitives.Geolocation(
                                    source.Latitude + (target.Latitude - source.Latitude)
                                    * num3, source.Longitude + (
                                    target.Longitude - source.Longitude) * num3);
              break;
            }
            this._lastRouteDistance += distance;
          }
        }
      }
      if (this._actualPosition == null)
        return;
      this.OnPositionChanged(this._actualPosition);
    }

    private void OnGeolocatorPositionChanged(Geolocator sender, PositionChangedEventArgs e)
    {
    }
  }
}
