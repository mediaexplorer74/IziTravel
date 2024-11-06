// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Shapes.Geocircle
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Helpers;
using Izi.Travel.Geofencing.Primitives;

#nullable disable
namespace Izi.Travel.Geofencing.Shapes
{
  public class Geocircle : IGeoshape
  {
    public GeoshapeType GeoshapeType => GeoshapeType.Geocircle;

    public Geolocation Center { get; private set; }

    public double Radius { get; private set; }

    public Geocircle(Geolocation center, double radius)
    {
      this.Center = center;
      this.Radius = radius;
    }

    public bool Contains(Geolocation geolocation)
    {
      Geolocation center = this.Center;
      double radius = this.Radius;
      Geolocation target = geolocation;
      return center != null && radius >= double.Epsilon && target != null && GeoHelper.CalculateDistance(center, target) < radius;
    }
  }
}
