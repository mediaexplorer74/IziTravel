// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.UriHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public class UriHelper
  {
    public static bool EqualsByCommonParameters(Uri uri1, Uri uri2)
    {
      if (uri1 == (Uri) null || uri2 == (Uri) null)
        return false;
      string path1 = (string) null;
      Dictionary<string, string> parameters = (Dictionary<string, string>) null;
      UriHelper.TryParse(uri1, out path1, out parameters);
      string path2 = (string) null;
      Dictionary<string, string> parameters2 = (Dictionary<string, string>) null;
      UriHelper.TryParse(uri2, out path2, out parameters2);
      if (path1 == null || path2 == null || !string.Equals(path1, path2, StringComparison.InvariantCultureIgnoreCase))
        return false;
      Dictionary<string, string> dictionary = parameters.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key.ToLower()), (Func<KeyValuePair<string, string>, string>) (x => x.Value.ToLower()));
      parameters2 = parameters2.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key.ToLower()), (Func<KeyValuePair<string, string>, string>) (x => x.Value.ToLower()));
      return dictionary.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => parameters2.ContainsKey(x.Key))).All<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Value == parameters2[x.Key]));
    }

    public static void TryParse(
      Uri uri,
      out string path,
      out Dictionary<string, string> parameters)
    {
      path = (string) null;
      parameters = new Dictionary<string, string>();
      if (uri == (Uri) null)
        return;
      string source1 = (string) null;
      uri.OriginalString.Split('?', out path, out source1);
      if (source1 == null)
        return;
      string str1 = (string) null;
      string str2 = (string) null;
      source1.Split('#', out str1, out str2);
      string str3 = str1;
      char[] chArray = new char[1]{ '&' };
      foreach (string source2 in str3.Split(chArray))
      {
        string key = (string) null;
        string str4 = (string) null;
        ref string local1 = ref key;
        ref string local2 = ref str4;
        source2.Split('=', out local1, out local2);
        if (key != null && str4 != null)
          parameters.Set<string, string>(key, str4);
      }
    }
  }
}
