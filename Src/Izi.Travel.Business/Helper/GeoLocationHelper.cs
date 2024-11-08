// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Helper.GeoLocationHelper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Izi.Travel.Business.Helper
{
  public static class GeoLocationHelper
  {
    public static GeoLocation[] Parse(string data)
    {
      if (string.IsNullOrWhiteSpace(data))
        return (GeoLocation[]) null;
      string[] strArray = data.Split(',', ';');
      if (strArray.Length < 2)
        return (GeoLocation[]) null;
      List<GeoLocation> geoLocationList = new List<GeoLocation>();
      for (int index = 0; index < strArray.Length; index += 2)
      {
        string s1 = strArray[index];
        string s2 = index + 1 < strArray.Length ? strArray[index + 1] : (string) null;
        double result1;
        double result2;
        if (!string.IsNullOrWhiteSpace(s1) && !string.IsNullOrWhiteSpace(s2) && double.TryParse(s1, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result1) && double.TryParse(s2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          geoLocationList.Add(new GeoLocation()
          {
            Latitude = result1,
            Longitude = result2
          });
      }
      return geoLocationList.ToArray();
    }
  }
}
