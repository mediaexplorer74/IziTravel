// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Shapes.IGeoshape
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Primitives;

#nullable disable
namespace Izi.Travel.Geofencing.Shapes
{
  public interface IGeoshape
  {
    GeoshapeType GeoshapeType { get; }

    bool Contains(Geolocation geolocation);
  }
}
