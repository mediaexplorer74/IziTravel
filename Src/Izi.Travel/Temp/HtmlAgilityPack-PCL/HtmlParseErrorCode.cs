// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlParseErrorCode
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>Represents the type of parsing error.</summary>
  public enum HtmlParseErrorCode
  {
    /// <summary>A tag was not closed.</summary>
    TagNotClosed,
    /// <summary>A tag was not opened.</summary>
    TagNotOpened,
    /// <summary>
    /// There is a charset mismatch between stream and declared (META) encoding.
    /// </summary>
    CharsetMismatch,
    /// <summary>An end tag was not required.</summary>
    EndTagNotRequired,
    /// <summary>An end tag is invalid at this position.</summary>
    EndTagInvalidHere,
  }
}
