// ********************************************************************
// Type: Izi.Travel.Core.Controls.ExpandableTextBlock
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;
using Windows.UI.Xaml.Markup;
using Izi.Travel.Core.Extensions; //?

#nullable disable
namespace Izi.Travel.Core.Controls
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

    protected override void OnApplyTemplate()
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
        
      // Clear existing content and event handlers
      foreach (var element in this._stackPanel.Children.OfType<FrameworkElement>())
      {
        if (element is RichTextBlock rtb)
        {
          rtb.SizeChanged -= OnRichTextBlockSizeChanged;
        }
      }
      this._stackPanel.Children.Clear();
      
      if (string.IsNullOrWhiteSpace(this.Text))
        return;
        
      // Process HTML content
      var htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(HtmlEntity.DeEntitize(this.Text));
      this.ProcessHyperlinks(htmlDocument);
      
      // Create a single RichTextBlock for all content
      var richTextBlock = new RichTextBlock 
      { 
        TextWrapping = TextWrapping.Wrap,
        IsTextSelectionEnabled = true,
        Margin = new Thickness(-12, 0, -12, 0)
      };
      
      richTextBlock.SizeChanged += OnRichTextBlockSizeChanged;
      
      var paragraph = new Paragraph();
      
      foreach (var textNode in htmlDocument.DocumentNode.Descendants("#text").ToList())
      {
        var ancestors = textNode.Ancestors().ToList();
        var linkNode = ancestors.FirstOrDefault(x => x.Name == "a");
        bool isLink = linkNode != null;
        string href = linkNode?.GetAttributeValue("href", string.Empty);
        
        bool isBold = ancestors.Any(x => x.Name == "b" || x.Name == "strong");
        bool isItalic = ancestors.Any(x => x.Name == "i" || x.Name == "em");
        bool isUnderline = ancestors.Any(x => x.Name == "u");
        
        var textParts = this.GetTextParts(textNode.InnerText);
        
        foreach (var text in textParts.Where(t => !string.IsNullOrEmpty(t)))
        {
          Inline inline;
          
          if (isLink && !string.IsNullOrEmpty(href))
          {
            inline = this.GetHyperlinkInline(text, href);
          }
          else
          {
            inline = this.GetTextInline(text);
          }
          
          // Apply formatting
          if (isBold) inline.FontWeight = FontWeights.Bold;
          if (isItalic) inline.FontStyle = FontStyle.Italic;
          if (isUnderline) inline.TextDecorations = Windows.UI.Text.TextDecorations.Underline;
          
          paragraph.Inlines.Add(inline);
        }
      }
      
      richTextBlock.Blocks.Add(paragraph);
      this._stackPanel.Children.Add(richTextBlock);
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
      this._linkButton.Visibility = this._linkButton.Content != null ? 
          Izi.Travel.Core.Extensions.UiExtensions.ToVisibility(this._collapsedHeight < this._expandedHeight) : 
          Visibility.Collapsed;
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

    private Inline GetTextInline(string text) => (Inline) new Run()
    {
      Text = text ?? string.Empty
    };

    private Inline GetHyperlinkInline(string text, string url)
    {
      if (string.IsNullOrEmpty(url))
        return GetTextInline(text);
        
      try
      {
        var hyperlink = new Hyperlink();
        hyperlink.Inlines.Add(new Run { Text = text ?? string.Empty });
        hyperlink.NavigateUri = new Uri(url, UriKind.RelativeOrAbsolute);
        return hyperlink;
      }
      catch (UriFormatException)
      {
        return GetTextInline(text);
      }
    }

    private void OnLinkButtonClick(object sender, RoutedEventArgs e)
    {
      this.IsExpanded = !this.IsExpanded;
    }

    private void OnRichTextBlockSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (!(sender is RichTextBlock richTextBlock))
        return;
        
      if (this.IsExpanded)
      {
        this._expandedHeight = richTextBlock.ActualHeight;
      }
      else
      {
        this._collapsedHeight = this.GetCollapsedHeight();
        this.ApplyExpandedState();
      }
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

