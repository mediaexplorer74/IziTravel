// ********************************************************************
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
      if (uri1 == null || uri2 == null)
        return false;
        
      UriHelper.TryParse(uri1, out var path1, out var parameters1);
      UriHelper.TryParse(uri2, out var path2, out var parameters2);
      
      if (path1 == null || path2 == null || !string.Equals(path1, path2, StringComparison.OrdinalIgnoreCase))
        return false;
        
      // Compare case-insensitive parameter keys and values
      var dict1 = parameters1.ToDictionary(
        x => x.Key.ToLowerInvariant(), 
        x => x.Value.ToLowerInvariant());
        
      var dict2 = parameters2.ToDictionary(
        x => x.Key.ToLowerInvariant(), 
        x => x.Value.ToLowerInvariant());
      
      // Check if all keys in dict1 exist in dict2 with the same values
      return dict1.All(kv => 
        dict2.TryGetValue(kv.Key, out var value) && 
        string.Equals(kv.Value, value, StringComparison.Ordinal));
    }

    public static void TryParse(
      Uri uri,
      out string path,
      out Dictionary<string, string> parameters)
    {
      path = null;
      parameters = new Dictionary<string, string>();
      
      if (uri == null)
        return;
        
      // Split path and query
      var uriString = uri.OriginalString;
      var pathEnd = uriString.IndexOf('?');
      
      if (pathEnd == -1)
      {
        path = uriString;
        return;
      }
      
      path = uriString.Substring(0, pathEnd);
      var queryString = uriString.Substring(pathEnd + 1);
      
      // Remove fragment if present
      var fragmentIndex = queryString.IndexOf('#');
      if (fragmentIndex != -1)
      {
        queryString = queryString.Substring(0, fragmentIndex);
      }
      
      // Parse query parameters
      var paramPairs = queryString.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
      
      foreach (var pair in paramPairs)
      {
        var keyValue = pair.Split(new[] { '=' }, 2);
        if (keyValue.Length == 2)
        {
          var key = Uri.UnescapeDataString(keyValue[0]);
          var value = Uri.UnescapeDataString(keyValue[1]);
          parameters[key] = value;
        }
      }
    }
  }
}
