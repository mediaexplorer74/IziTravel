// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Geopoint
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using System;
using System.Globalization;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  public class Geopoint
  {
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public Geopoint()
    {
    }

    public Geopoint(double latitude, double longitude)
    {
      this.Latitude = latitude;
      this.Longitude = longitude;
    }

    public override string ToString()
    {
      double num = this.Latitude;
      string str1 = num.ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture);
      num = this.Longitude;
      string str2 = num.ToString("0.000", (IFormatProvider) CultureInfo.InvariantCulture);
      return string.Format("{0},{1}", (object) str1, (object) str2);
    }
  }
}
