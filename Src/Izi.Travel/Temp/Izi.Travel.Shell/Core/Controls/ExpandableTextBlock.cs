// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ExpandableTextBlock
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplatePart(Name = "PartStackPanel", Type = typeof (StackPanel))]
  [TemplatePart(Name = "PartButton", Type = typeof (LinkButton))]
  public class ExpandableTextBlock : Control
  {
    private const string PartStackPanel = "PartStackPanel";
    private const string PartButton = "PartButton";
    private static readonly Regex HyperlinkRegex = new Regex("((ht|f)tp(s?)\\:\\/\\/|www\\.)[0-9a-zA-Zа-яА-Я]([-.\\w]*[0-9a-zA-Zа-яА-Я])*(:(0-9)*)*(\\/?)([a-zA-Zа-яА-Я0-9\\-\\=\\.\\?\\,\\'\\/\\\\\\+&amp;%\\$#_]*)?", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    private StackPanel _stackPanel;
    private LinkButton _linkButton;
    private double _collapsedHeight;
    private double _expandedHeight;
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (ExpandableTextBlock), new PropertyMetadata((object) null, new PropertyChangedCallback(ExpandableTextBlock.OnTextPropertyChanged)));
    public static readonly DependencyProperty CollapsedLineCountProperty = DependencyProperty.Register(nameof (CollapsedLineCount), typeof (int), typeof (ExpandableTextBlock), new PropertyMetadata((object) 0, new PropertyChangedCallback(ExpandableTextBlock.OnCollapsedLineCountChanged)));
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof (IsExpanded), typeof (bool), typeof (ExpandableTextBlock), new PropertyMetadata((object) false, new PropertyChangedCallback(ExpandableTextBlock.OnIsExpandedPropertyChanged)));
    public static readonly DependencyProperty LinkTextCollapsedProperty = DependencyProperty.Register(nameof (LinkTextCollapsed), typeof (string), typeof (ExpandableTextBlock), new PropertyMetadata((object) null));
    public static readonly DependencyProperty LinkTextExpandedProperty = DependencyProperty.Register(nameof (LinkTextExpanded), typeof (string), typeof (ExpandableTextBlock), new PropertyMetadata((object) null));
    public static readonly DependencyProperty LinkForegroundProperty = DependencyProperty.Register(nameof (LinkForeground), typeof (Brush), typeof (ExpandableTextBlock), new PropertyMetadata((object) null));
    public static readonly DependencyProperty LinkPressedForegroundProperty = DependencyProperty.Register(nameof (LinkPressedForeground), typeof (Brush), typeof (ExpandableTextBlock), new PropertyMetadata((object) null));

    public string Text
    {
      get => (string) this.GetValue(ExpandableTextBlock.TextProperty);
      set => this.SetValue(ExpandableTextBlock.TextProperty, (object) value);
    }

    public int CollapsedLineCount
    {
      get => (int) this.GetValue(ExpandableTextBlock.CollapsedLineCountProperty);
      set => this.SetValue(ExpandableTextBlock.CollapsedLineCountProperty, (object) value);
    }

    public bool IsExpanded
    {
      get => (bool) this.GetValue(ExpandableTextBlock.IsExpandedProperty);
      set => this.SetValue(ExpandableTextBlock.IsExpandedProperty, (object) value);
    }

    public string LinkTextCollapsed
    {
      get => (string) this.GetValue(ExpandableTextBlock.LinkTextCollapsedProperty);
      set => this.SetValue(ExpandableTextBlock.LinkTextCollapsedProperty, (object) value);
    }

    public string LinkTextExpanded
    {
      get => (string) this.GetValue(ExpandableTextBlock.LinkTextExpandedProperty);
      set => this.SetValue(ExpandableTextBlock.LinkTextExpandedProperty, (object) value);
    }

    public Brush LinkForeground
    {
      get => (Brush) this.GetValue(ExpandableTextBlock.LinkForegroundProperty);
      set => this.SetValue(ExpandableTextBlock.LinkForegroundProperty, (object) value);
    }

    public Brush LinkPressedForeground
    {
      get => (Brush) this.GetValue(ExpandableTextBlock.LinkPressedForegroundProperty);
      set => this.SetValue(ExpandableTextBlock.LinkPressedForegroundProperty, (object) value);
    }

    public ExpandableTextBlock() => this.DefaultStyleKey = (object) typeof (ExpandableTextBlock);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._stackPanel = this.GetTemplateChild("PartStackPanel") as StackPanel;
      this._linkButton = this.GetTemplateChild("PartButton") as LinkButton;
      if (this._linkButton != null)
        this._linkButton.Click += new RoutedEventHandler(this.OnLinkButtonClick);
      this.ApplyText();
    }

    private void ApplyText()
    {
      if (this._stackPanel == null)
        return;
      foreach (FrameworkElement frameworkElement in this._stackPanel.Children.OfType<RichTextBox>())
        frameworkElement.SizeChanged -= new SizeChangedEventHandler(this.OnRichTextBoxSizeChanged);
      this._stackPanel.Children.Clear();
      if (string.IsNullOrWhiteSpace(this.Text))
        return;
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(HtmlEntity.DeEntitize(this.Text));
      this.ProcessHyperlinks(htmlDocument);
      RichTextBox richTextBox1 = (RichTextBox) null;
      Paragraph paragraph = (Paragraph) null;
      foreach (HtmlNode htmlNode1 in htmlDocument.DocumentNode.Descendants("#text").ToList<HtmlNode>())
      {
        List<HtmlNode> list = htmlNode1.Ancestors().ToList<HtmlNode>();
        HtmlNode htmlNode2 = list.FirstOrDefault<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name == "a"));
        bool flag1 = htmlNode2 != null;
        string attributeValue = htmlNode2?.GetAttributeValue("href", (string) null);
        bool flag2 = list.Any<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name == "b" || x.Name == "strong"));
        bool flag3 = list.Any<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name == "i" || x.Name == "em"));
        bool flag4 = list.Any<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name == "u"));
        List<string> textParts = this.GetTextParts(htmlNode1.InnerText);
        for (int index = 0; index < textParts.Count; ++index)
        {
          string text = textParts[index];
          Inline inline = flag1 ? this.GetHyperlinkInline(text, attributeValue) : this.GetTextInline(text);
          if (inline != null)
          {
            if (flag2)
              inline.FontWeight = FontWeights.Bold;
            if (flag3)
              inline.FontStyle = FontStyles.Italic;
            if (flag4)
              inline.TextDecorations = TextDecorations.Underline;
            if (richTextBox1 == null || index > 0)
            {
              RichTextBox richTextBox2 = new RichTextBox();
              richTextBox2.Margin = new Thickness(-12.0, 0.0, -12.0, 0.0);
              richTextBox1 = richTextBox2;
              richTextBox1.SizeChanged += new SizeChangedEventHandler(this.OnRichTextBoxSizeChanged);
              richTextBox1.Blocks.Add((Block) (paragraph = new Paragraph()));
              this._stackPanel.Children.Add((UIElement) richTextBox1);
            }
            paragraph.Inlines.Add(inline);
          }
        }
      }
    }

    private void ProcessHyperlinks(HtmlDocument htmlDocument)
    {
      foreach (HtmlNode oldChild in htmlDocument.DocumentNode.Descendants("#text").Where<HtmlNode>((Func<HtmlNode, bool>) (x => x.Ancestors().All<HtmlNode>((Func<HtmlNode, bool>) (y => y.Name != "a")))).ToList<HtmlNode>())
      {
        string str = ExpandableTextBlock.HyperlinkRegex.Replace(oldChild.InnerText, (MatchEvaluator) (x => string.Format("<a href=\"{0}\">{0}</a>", (object) x.Value)));
        if (str != oldChild.InnerText)
          oldChild.ParentNode.ReplaceChild(HtmlNode.CreateNode(string.Format("<span>{0}</span>", (object) str)), oldChild);
      }
    }

    private List<string> GetTextParts(string text)
    {
      List<string> textParts = new List<string>();
      if (text != null)
      {
        string str = text;
        string[] separator = new string[1]
        {
          Environment.NewLine
        };
        foreach (string source in str.Split(separator, StringSplitOptions.None))
        {
          if (string.IsNullOrEmpty(source))
            textParts.Add(source);
          else
            textParts.AddRange((IEnumerable<string>) source.SplitBy(1700));
        }
      }
      return textParts;
    }

    private void ApplyExpandedState()
    {
      if (this._stackPanel == null || this._linkButton == null)
        return;
      this._collapsedHeight = this.GetCollapsedHeight();
      this._stackPanel.MaxHeight = this.IsExpanded ? this._expandedHeight : this._collapsedHeight;
      this._linkButton.Content = this.IsExpanded ? (object) this.LinkTextExpanded : (object) this.LinkTextCollapsed;
      this._linkButton.Visibility = this._linkButton.Content != null ? (this._collapsedHeight < this._expandedHeight).ToVisibility() : Visibility.Collapsed;
    }

    private double GetCollapsedHeight()
    {
      if (this.CollapsedLineCount <= 0)
        return 0.0;
      TextBlock textBlock = new TextBlock()
      {
        FontFamily = this.FontFamily,
        FontSize = this.FontSize,
        FontWeight = this.FontWeight,
        Padding = this.Padding
      };
      for (int index = 1; index < this.CollapsedLineCount; ++index)
        textBlock.Text += Environment.NewLine;
      textBlock.UpdateLayout();
      return Math.Ceiling(textBlock.ActualHeight);
    }

    private Inline GetTextInline(string text)
    {
      Run textInline = new Run();
      textInline.Text = text;
      textInline.Foreground = this.Foreground;
      textInline.FontFamily = this.FontFamily;
      textInline.FontSize = this.FontSize;
      textInline.FontWeight = this.FontWeight;
      return (Inline) textInline;
    }

    private Inline GetHyperlinkInline(string text, string uriString)
    {
      if (uriString.StartsWith("www.", StringComparison.InvariantCultureIgnoreCase))
        uriString = "http://" + uriString;
      Uri result = (Uri) null;
      Uri.TryCreate(uriString, UriKind.Absolute, out result);
      Hyperlink hyperlinkInline = new Hyperlink();
      hyperlinkInline.TargetName = "_blank";
      hyperlinkInline.NavigateUri = result;
      hyperlinkInline.MouseOverForeground = this.LinkPressedForeground;
      hyperlinkInline.Foreground = this.LinkForeground;
      hyperlinkInline.FontFamily = this.FontFamily;
      hyperlinkInline.FontSize = this.FontSize;
      hyperlinkInline.FontWeight = this.FontWeight;
      hyperlinkInline.Inlines.Add((Inline) new Run()
      {
        Text = text
      });
      return (Inline) hyperlinkInline;
    }

    private void OnLinkButtonClick(object sender, RoutedEventArgs e)
    {
      this.IsExpanded = !this.IsExpanded;
    }

    private void OnRichTextBoxSizeChanged(object sender, SizeChangedEventArgs e)
    {
      this._expandedHeight = this._stackPanel.Children.OfType<RichTextBox>().Sum<RichTextBox>((Func<RichTextBox, double>) (x => x.ActualHeight));
      this.ApplyExpandedState();
    }

    private static void OnTextPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ExpandableTextBlock expandableTextBlock))
        return;
      expandableTextBlock.ApplyText();
      expandableTextBlock.ApplyExpandedState();
    }

    private static void OnCollapsedLineCountChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ExpandableTextBlock expandableTextBlock))
        return;
      expandableTextBlock.ApplyExpandedState();
    }

    private static void OnIsExpandedPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ExpandableTextBlock expandableTextBlock))
        return;
      expandableTextBlock.ApplyExpandedState();
    }
  }
}
