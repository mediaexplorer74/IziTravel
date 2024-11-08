// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.TourPlaybackManager
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

namespace Izi.Travel.Business.Managers
{
    public class Geolocation
    {
        public double Latitude;
        public double Longitude;

        public Geolocation()
        {
            this.Latitude = default;
            this.Longitude = default;
        }

        public Geolocation(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

       
    }
}