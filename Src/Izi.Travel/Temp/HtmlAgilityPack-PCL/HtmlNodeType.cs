// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlNodeType
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>Represents the type of a node.</summary>
  public enum HtmlNodeType
  {
    /// <summary>The root of a document.</summary>
    Document,
    /// <summary>An HTML element.</summary>
    Element,
    /// <summary>An HTML comment.</summary>
    Comment,
    /// <summary>
    /// A text node is always the child of an element or a document node.
    /// </summary>
    Text,
  }
}
