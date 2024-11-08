// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.Extensions.UriExtensions
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Utility.Extensions
{
  public static class UriExtensions
  {
    public static Dictionary<string, string> GetQueryParameters(this Uri uri)
    {
      if (uri == (Uri) null || string.IsNullOrWhiteSpace(uri.Query))
        return (Dictionary<string, string>) null;
      string str1 = uri.Query.Trim().TrimStart('?');
      if (string.IsNullOrWhiteSpace(str1))
        return (Dictionary<string, string>) null;
      string[] strArray1 = str1.Split('&');
      if (strArray1.Length == 0)
        return (Dictionary<string, string>) null;
      Dictionary<string, string> queryParameters = new Dictionary<string, string>();
      foreach (string str2 in strArray1)
      {
        char[] chArray = new char[1]{ '=' };
        string[] strArray2 = str2.Split(chArray);
        if (strArray2.Length >= 2)
        {
          string key = strArray2[0];
          string str3 = strArray2[1];
          if (!queryParameters.ContainsKey(key))
            queryParameters.Add(key, str3);
        }
      }
      return queryParameters;
    }
  }
}
