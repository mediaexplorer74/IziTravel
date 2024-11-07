// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.GeofenceStateChangeReport
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Primitives;

#nullable disable
namespace Izi.Travel.Geofencing
{
  public sealed class GeofenceStateChangeReport
  {
    public Geofence Geofence { get; private set; }

    public Geolocation Geoposition { get; private set; }

    public GeofenceState NewState { get; private set; }

    public GeofenceStateChangeReport(
      Geofence geofence,
      Geolocation geoposition,
      GeofenceState newState)
    {
      this.Geofence = geofence;
      this.Geoposition = geoposition;
      this.NewState = newState;
    }
  }
}
