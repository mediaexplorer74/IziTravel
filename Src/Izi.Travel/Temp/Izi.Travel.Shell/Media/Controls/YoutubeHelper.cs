// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Controls.YoutubeHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Izi.Travel.Shell.Media.Controls
{
  public static class YoutubeHelper
  {
    private const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";

    public static string GetVideoId(string input)
    {
      foreach (Match match in new Regex("(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+", RegexOptions.Compiled).Matches(input))
      {
        using (IEnumerator<Group> enumerator = match.Groups.Cast<Group>().Where<Group>((Func<Group, bool>) (groupdata => !groupdata.ToString().StartsWith("http://") && !groupdata.ToString().StartsWith("https://") && !groupdata.ToString().StartsWith("youtu") && !groupdata.ToString().StartsWith("www."))).GetEnumerator())
        {
          if (enumerator.MoveNext())
            return enumerator.Current.ToString();
        }
      }
      return string.Empty;
    }
  }
}
