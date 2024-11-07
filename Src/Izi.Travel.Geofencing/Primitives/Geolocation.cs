// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Primitives.Geolocation
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

#nullable disable
namespace Izi.Travel.Geofencing.Primitives
{
  public class Geolocation
  {
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double? Altitude { get; set; }

    public double Accuracy { get; set; }

    public Geolocation()
    {
    }

    public Geolocation(double latitude, double longitude)
      : this(latitude, longitude, new double?())
    {
    }

    public Geolocation(double latitude, double longitude, double? altitude)
    {
      this.Latitude = latitude;
      this.Longitude = longitude;
      this.Altitude = altitude;
    }

    public override string ToString()
    {
      return string.Format("{0}, {1}", (object) this.Latitude, (object) this.Longitude);
    }
  }
}
