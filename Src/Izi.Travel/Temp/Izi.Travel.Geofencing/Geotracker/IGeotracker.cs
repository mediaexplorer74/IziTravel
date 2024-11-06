// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Geotracker.IGeotracker
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Izi.Travel.Geofencing.Primitives;
using System.Threading.Tasks;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Geofencing.Geotracker
{
  public interface IGeotracker
  {
    event TypedEventHandler<IGeotracker, Geolocation> PositionChanged;

    bool IsStarted { get; }

    bool IsEnabled { get; }

    Task<Geolocation> GetPosition();
  }
}
