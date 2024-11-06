// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.StringExtension
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace HtmlAgilityPack
{
  internal static class StringExtension
  {
    public static string[] Split(this string Subject, char[] Separator, int Count)
    {
      string[] source = Subject.Split(Separator);
      if (source.Length > 2)
        source[1] = string.Join(((IEnumerable<char>) Separator).First<char>().ToString(), ((IEnumerable<string>) source).Skip<string>(1).ToArray<string>());
      return source;
    }
  }
}
