// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlCommentNode
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>Represents an HTML comment.</summary>
  public class HtmlCommentNode : HtmlNode
  {
    private string _comment;

    internal HtmlCommentNode(HtmlDocument ownerdocument, int index)
      : base(HtmlNodeType.Comment, ownerdocument, index)
    {
    }

    /// <summary>Gets or Sets the comment text of the node.</summary>
    public string Comment
    {
      get => this._comment == null ? base.InnerHtml : this._comment;
      set => this._comment = value;
    }

    /// <summary>
    /// Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
    /// </summary>
    public override string InnerHtml
    {
      get => this._comment == null ? base.InnerHtml : this._comment;
      set => this._comment = value;
    }

    /// <summary>Gets or Sets the object and its content in HTML.</summary>
    public override string OuterHtml
    {
      get => this._comment == null ? base.OuterHtml : "<!--" + this._comment + "-->";
    }
  }
}
