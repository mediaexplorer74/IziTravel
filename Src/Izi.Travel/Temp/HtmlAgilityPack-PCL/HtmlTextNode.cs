// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlTextNode
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

#nullable disable
namespace HtmlAgilityPack
{
  /// <summary>Represents an HTML text node.</summary>
  public class HtmlTextNode : HtmlNode
  {
    private string _text;

    internal HtmlTextNode(HtmlDocument ownerdocument, int index)
      : base(HtmlNodeType.Text, ownerdocument, index)
    {
    }

    /// <summary>
    /// Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
    /// </summary>
    public override string InnerHtml
    {
      get => this.OuterHtml;
      set => this._text = value;
    }

    /// <summary>Gets or Sets the object and its content in HTML.</summary>
    public override string OuterHtml => this._text == null ? base.OuterHtml : this._text;

    /// <summary>Gets or Sets the text of the node.</summary>
    public string Text
    {
      get => this._text == null ? base.OuterHtml : this._text;
      set => this._text = value;
    }
  }
}
