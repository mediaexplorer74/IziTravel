// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Helpers.MtgLinkHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Core.Helpers
{
  public static class MtgLinkHelper
  {
    private const string MtgLinkHostName = "izi.travel";
    private const string MtgLinkStageHostName = "stage.izi.travel";
    private const string MtgLinkDevHostName = "dev.izi.travel";
    private const string MtgLinkSegmentBrowseName = "browse";
    private const string MtgLinkParameterBrowseLanguageName = "lang";
    private const string MtgLinkParameterBrowsePasscodeName = "passcode";
    private const string MtgLinkSegmentMtgSiteName = "mtgsite";
    private const string MtgLinkSegmentNumberName = "number";
    private const string MtgLinkExtensionMtgSiteName = ".mtg";
    private static readonly string[] MtgLinkHosts = new string[3]
    {
      "izi.travel",
      "stage.izi.travel",
      "dev.izi.travel"
    };

    public static MtgLinkInfo Parse(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
        return (MtgLinkInfo) null;
      Uri result1;
      if (!Uri.TryCreate(url, UriKind.Absolute, out result1))
        return (MtgLinkInfo) null;
      string lower = result1.Host.ToLower(CultureInfo.InvariantCulture);
      if (string.IsNullOrWhiteSpace(lower) || !((IEnumerable<string>) MtgLinkHelper.MtgLinkHosts).Contains<string>(lower))
        return (MtgLinkInfo) null;
      MtgLinkInfo linkInfo = new MtgLinkInfo()
      {
        Type = MtgLinkType.Unknown
      };
      if (result1.Segments == null)
        return (MtgLinkInfo) null;
      string[] array = ((IEnumerable<string>) result1.Segments).Select<string, string>((Func<string, string>) (x => x.Trim('/').ToLower())).ToArray<string>();
      if (array.Length < 2)
        return (MtgLinkInfo) null;
      if (array.Length > 2)
      {
        if (array[1] == "browse")
        {
          linkInfo.Type = MtgLinkType.Browse;
          Guid result2;
          if (!Guid.TryParse(array[2], out result2))
            return (MtgLinkInfo) null;
          string str = result2.ToString();
          if (array.Length > 3 && Guid.TryParse(array[3], out result2))
          {
            linkInfo.Uid = result2.ToString();
            linkInfo.ParentUid = str;
          }
          else
            linkInfo.Uid = str;
          MtgLinkHelper.ReadParameters(linkInfo, result1);
          return linkInfo;
        }
        if (!(array[1] == "mtgsite") || !(Path.GetExtension(url) == ".mtg"))
          return (MtgLinkInfo) null;
        linkInfo.Type = MtgLinkType.MtgSite;
        Guid result3;
        if (!Guid.TryParse(array[2].Replace(".mtg", string.Empty), out result3))
          return (MtgLinkInfo) null;
        linkInfo.Uid = result3.ToString();
        if (array.Length > 4 && array[3] == "number")
          linkInfo.Number = array[4].Replace(".mtg", string.Empty);
        MtgLinkHelper.ReadParameters(linkInfo, result1);
        return linkInfo;
      }
      Guid result4;
      if (!Guid.TryParse(array[1], out result4))
        return (MtgLinkInfo) null;
      linkInfo.Uid = result4.ToString();
      linkInfo.Type = MtgLinkType.Mobile;
      MtgLinkHelper.ReadParameters(linkInfo, result1);
      return linkInfo;
    }

    public static Uri CreateUri(MtgLinkInfo linkInfo)
    {
      if (linkInfo == null || string.IsNullOrWhiteSpace(linkInfo.Uid))
        return (Uri) null;
      string uriString = string.Format("http://{0}/{1}/{2}", (object) "izi.travel", (object) "browse", (object) linkInfo.Uid);
      if (!string.IsNullOrWhiteSpace(linkInfo.Language))
        uriString += string.Format("?{0}={1}", (object) "lang", (object) linkInfo.Language);
      return new Uri(uriString, UriKind.Absolute);
    }

    private static void ReadParameters(MtgLinkInfo linkInfo, Uri uri)
    {
      Dictionary<string, string> queryParameters = uri.GetQueryParameters();
      if (queryParameters == null || queryParameters.Count <= 0)
        return;
      if (queryParameters.ContainsKey("lang"))
        linkInfo.Language = queryParameters["lang"];
      if (!queryParameters.ContainsKey("passcode"))
        return;
      linkInfo.Passcode = queryParameters["passcode"];
    }
  }
}
