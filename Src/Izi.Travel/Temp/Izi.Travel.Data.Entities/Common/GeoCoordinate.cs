// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Common.GeoCoordinate
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using System;
using System.Globalization;

#nullable disable
namespace Izi.Travel.Data.Entities.Common
{
  public class GeoCoordinate
  {
    public double Latitude { get; set; }

    public double Longitude { get; set; }

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
