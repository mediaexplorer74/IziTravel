// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Geotracker.GpsGeotracker
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Caliburn.Micro;
using Izi.Travel.Geofencing.Helpers;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Geofencing.Geotracker
{
  public sealed class GpsGeotracker : BaseGeotracker
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (GpsGeotracker));
    private Geolocator _geolocator;
    private Geolocator _locationStatusGeolocator;
    private Izi.Travel.Geofencing.Primitives.Geolocation _actualPosition;

    public override bool IsEnabled
    {
      get
      {
        return (this._locationStatusGeolocator = this._locationStatusGeolocator ?? new Geolocator()).LocationStatus != 3;
      }
    }

    public override async Task<Izi.Travel.Geofencing.Primitives.Geolocation> GetPosition()
    {
      if (!this.IsEnabled)
        return (Izi.Travel.Geofencing.Primitives.Geolocation) null;
      if (this._actualPosition != null)
        return this._actualPosition;
      IAsyncOperation<Geoposition> getGeopositionAsyncTask = (IAsyncOperation<Geoposition>) null;
      try
      {
        getGeopositionAsyncTask = new Geolocator().GetGeopositionAsync(TimeSpan.FromSeconds(3.0), TimeSpan.FromSeconds(10.0));
        return (await getGeopositionAsyncTask).ToGeolocation();
      }
      catch (Exception ex)
      {
        GpsGeotracker.Log.Error(ex);
        return (Izi.Travel.Geofencing.Primitives.Geolocation) null;
      }
      finally
      {
        if (getGeopositionAsyncTask != null)
        {
          if (((IAsyncInfo) getGeopositionAsyncTask).Status == null)
            ((IAsyncInfo) getGeopositionAsyncTask).Cancel();
          ((IAsyncInfo) getGeopositionAsyncTask).Close();
        }
      }
    }

    protected override void OnStart()
    {
      if (!this.IsEnabled)
        return;
      Geolocator geolocator1 = new Geolocator();
      geolocator1.put_DesiredAccuracy((PositionAccuracy) 1);
      geolocator1.put_MovementThreshold(1.0);
      this._geolocator = geolocator1;
      Geolocator geolocator2 = this._geolocator;
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Geolocator, StatusChangedEventArgs>>(new Func<TypedEventHandler<Geolocator, StatusChangedEventArgs>, EventRegistrationToken>(geolocator2.add_StatusChanged), new Action<EventRegistrationToken>(geolocator2.remove_StatusChanged), new TypedEventHandler<Geolocator, StatusChangedEventArgs>((object) this, __methodptr(Geolocator_StatusChanged)));
      Geolocator geolocator3 = this._geolocator;
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Geolocator, PositionChangedEventArgs>>(new Func<TypedEventHandler<Geolocator, PositionChangedEventArgs>, EventRegistrationToken>(geolocator3.add_PositionChanged), new Action<EventRegistrationToken>(geolocator3.remove_PositionChanged), new TypedEventHandler<Geolocator, PositionChangedEventArgs>((object) this, __methodptr(Geolocator_PositionChanged)));
    }

    protected override void OnStop()
    {
      if (this._geolocator != null)
      {
        // ISSUE: method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<Geolocator, StatusChangedEventArgs>>(new Action<EventRegistrationToken>(this._geolocator.remove_StatusChanged), new TypedEventHandler<Geolocator, StatusChangedEventArgs>((object) this, __methodptr(Geolocator_StatusChanged)));
        // ISSUE: method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<Geolocator, PositionChangedEventArgs>>(new Action<EventRegistrationToken>(this._geolocator.remove_PositionChanged), new TypedEventHandler<Geolocator, PositionChangedEventArgs>((object) this, __methodptr(Geolocator_PositionChanged)));
      }
      this._geolocator = (Geolocator) null;
      this._actualPosition = (Izi.Travel.Geofencing.Primitives.Geolocation) null;
    }

    private void Geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
    {
      GpsGeotracker.Log.Info("Status changed to {0}", (object) args.Status);
    }

    private void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs e)
    {
      this.OnPositionChanged(this._actualPosition = e.Position.ToGeolocation());
    }
  }
}
