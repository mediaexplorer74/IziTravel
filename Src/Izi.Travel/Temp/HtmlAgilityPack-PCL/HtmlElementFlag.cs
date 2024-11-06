// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlElementFlag
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System;

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>Flags that describe the behavior of an Element node.</summary>
  [Flags]
  public enum HtmlElementFlag
  {
    /// <summary>The node is a CDATA node.</summary>
    CData = 1,
    /// <summary>
    /// The node is empty. META or IMG are example of such nodes.
    /// </summary>
    Empty = 2,
    /// <summary>The node will automatically be closed during parsing.</summary>
    Closed = 4,
    /// <summary>The node can overlap.</summary>
    CanOverlap = 8,
  }
}
