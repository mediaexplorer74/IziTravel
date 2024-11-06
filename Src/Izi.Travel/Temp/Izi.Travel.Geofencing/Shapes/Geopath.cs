// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Shapes.Geopath
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Primitives;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Geofencing.Shapes
{
  public class Geopath : IGeoshape
  {
    public GeoshapeType GeoshapeType => GeoshapeType.Geopath;

    public IReadOnlyList<Geolocation> Positions { get; private set; }

    public Geopath(IEnumerable<Geolocation> positions)
    {
      this.Positions = (IReadOnlyList<Geolocation>) positions.ToList<Geolocation>();
    }

    public bool Contains(Geolocation geolocation)
    {
      IReadOnlyList<Geolocation> positions = this.Positions;
      Geolocation geolocation1 = geolocation;
      if (positions == null || geolocation1 == null)
        return false;
      Geolocation[] array = positions.ToArray<Geolocation>();
      bool flag = false;
      int index1 = 0;
      int index2 = array.Length - 1;
      for (; index1 < array.Length; index2 = index1++)
      {
        if ((array[index1].Latitude <= geolocation1.Latitude && geolocation1.Latitude < array[index2].Latitude || array[index2].Latitude <= geolocation1.Latitude && geolocation1.Latitude < array[index1].Latitude) && geolocation1.Longitude < (array[index2].Longitude - array[index1].Longitude) * (geolocation1.Latitude - array[index1].Latitude) / (array[index2].Latitude - array[index1].Latitude) + array[index1].Longitude)
          flag = !flag;
      }
      return flag;
    }
  }
}
