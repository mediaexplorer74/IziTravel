// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Geofence
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Shapes;
using System;

#nullable disable
namespace Izi.Travel.Geofencing
{
  public class Geofence
  {
    public string Id { get; private set; }

    public IGeoshape Geoshape { get; private set; }

    public object Tag { get; set; }

    public Geofence(string id, IGeoshape geoshape)
    {
      if (string.IsNullOrWhiteSpace(id))
        throw new ArgumentNullException(nameof (id));
      if (geoshape == null)
        throw new ArgumentNullException(nameof (geoshape));
      this.Id = id;
      this.Geoshape = geoshape;
    }
  }
}
