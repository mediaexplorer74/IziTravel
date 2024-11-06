// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.Utilities
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System.Collections.Generic;

#nullable disable
namespace HtmlAgilityPack
{
  internal static class Utilities
  {
    public static TValue GetDictionaryValueOrNull<TKey, TValue>(
      Dictionary<TKey, TValue> dict,
      TKey key)
      where TKey : class
    {
      return !dict.ContainsKey(key) ? default (TValue) : dict[key];
    }
  }
}
