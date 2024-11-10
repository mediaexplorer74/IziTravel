// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ViewboxTextBlock
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplatePart(Name = "TextBlock", Type = typeof (TextBlock))]
  public class ViewboxTextBlock : Control
  {
    private const string TextBlockTemplatePart = "TextBlock";
    private TextBlock _textBlock;
    private double _actualWidth;
    private double _parentActualHeight;
    private string _text;
    private double _owerflowedHeight;
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (ViewboxTextBlock), new PropertyMetadata((object) null, new System.Windows.PropertyChangedCallback(ViewboxTextBlock.PropertyChangedCallback)));
    public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(nameof (TextAlignment), typeof (TextAlignment), typeof (ViewboxTextBlock), new PropertyMetadata((object) TextAlignment.Center));
    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(nameof (TextWrapping), typeof (TextWrapping), typeof (ViewboxTextBlock), new PropertyMetadata((object) (TextWrapping) 0));
    public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register("TextTrimming", typeof (TextTrimming), typeof (ViewboxTextBlock), new PropertyMetadata((object) TextTrimming.None));
    public static readonly DependencyProperty MinFontSizeProperty = DependencyProperty.Register(nameof (MinFontSize), typeof (double), typeof (ViewboxTextBlock), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty MaxFontSizeProperty = DependencyProperty.Register(nameof (MaxFontSize), typeof (double), typeof (ViewboxTextBlock), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty IsOverflowedProperty = DependencyProperty.Register(nameof (IsOverflowed), typeof (bool), typeof (ViewboxTextBlock), new PropertyMetadata((object) false));
    public static readonly DependencyProperty TrimProperty = DependencyProperty.Register(nameof (Trim), typeof (bool), typeof (ViewboxTextBlock), new PropertyMetadata((object) false));

    public string Text
    {
      get => (string) this.GetValue(ViewboxTextBlock.TextProperty);
      set => this.SetValue(ViewboxTextBlock.TextProperty, (object) value);
    }

    public TextAlignment TextAlignment
    {
      get => (TextAlignment) this.GetValue(ViewboxTextBlock.TextAlignmentProperty);
      set => this.SetValue(ViewboxTextBlock.TextAlignmentProperty, (object) value);
    }

    public TextWrapping TextWrapping
    {
      get => (TextWrapping) this.GetValue(ViewboxTextBlock.TextWrappingProperty);
      set => this.SetValue(ViewboxTextBlock.TextWrappingProperty, (object) value);
    }

    public double MinFontSize
    {
      get => (double) this.GetValue(ViewboxTextBlock.MinFontSizeProperty);
      set => this.SetValue(ViewboxTextBlock.MinFontSizeProperty, (object) value);
    }

    public double MaxFontSize
    {
      get => (double) this.GetValue(ViewboxTextBlock.MaxFontSizeProperty);
      set => this.SetValue(ViewboxTextBlock.MaxFontSizeProperty, (object) value);
    }

    public bool IsOverflowed
    {
      get => (bool) this.GetValue(ViewboxTextBlock.IsOverflowedProperty);
      set => this.SetValue(ViewboxTextBlock.IsOverflowedProperty, (object) value);
    }

    public bool Trim
    {
      get => (bool) this.GetValue(ViewboxTextBlock.TrimProperty);
      set => this.SetValue(ViewboxTextBlock.TrimProperty, (object) value);
    }

    public double OverflowedHeight => this._owerflowedHeight;

    public ViewboxTextBlock()
    {
      this.DefaultStyleKey = (object) typeof (ViewboxTextBlock);
      this.LayoutUpdated += (EventHandler) ((s, e) => this.Update());
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._textBlock = this.GetTemplateChild("TextBlock") as TextBlock;
      this.Update();
    }

    private void Update()
    {
      if (!(this.Parent is FrameworkElement parent) || Math.Abs(parent.ActualHeight) < double.Epsilon || this._textBlock == null || Math.Abs(this.ActualWidth) < double.Epsilon || Math.Abs(this._parentActualHeight - parent.ActualHeight) < double.Epsilon && Math.Abs(this._actualWidth - this.ActualWidth) < double.Epsilon && this._text == this.Text)
        return;
      this._parentActualHeight = parent.ActualHeight;
      this._actualWidth = this.ActualWidth;
      this._text = this.Text;
      double heightToFit = this._parentActualHeight - this.Margin.Top - this.Margin.Bottom;
      TextBlock textBlock1 = new TextBlock();
      textBlock1.Width = this._actualWidth;
      textBlock1.Text = this.Text;
      textBlock1.FontFamily = this.FontFamily;
      textBlock1.FontWeight = this.FontWeight;
      textBlock1.TextWrapping = this.TextWrapping;
      TextBlock textBlock2 = textBlock1;
      double maxFontSize;
      for (maxFontSize = this.MaxFontSize; maxFontSize > this.MinFontSize; --maxFontSize)
      {
        textBlock2.FontSize = maxFontSize;
        textBlock2.UpdateLayout();
        if (textBlock2.ActualHeight < heightToFit)
          break;
      }
      this._owerflowedHeight = textBlock2.ActualHeight;
      this.IsOverflowed = textBlock2.ActualHeight > heightToFit;
      this._textBlock.FontSize = maxFontSize;
      if (!this.Trim)
        return;
      this._textBlock.TextTrimming = TextTrimming.WordEllipsis;
      this._textBlock.MaxHeight = ViewboxTextBlock.GetMaxHeight(this._textBlock, heightToFit);
    }

    private static double GetMaxHeight(TextBlock originalTextBlock, double heightToFit)
    {
      TextBlock textBlock = new TextBlock()
      {
        FontSize = originalTextBlock.FontSize,
        FontFamily = originalTextBlock.FontFamily,
        FontWeight = originalTextBlock.FontWeight,
        TextWrapping = TextWrapping.Wrap
      };
      double a = 0.0;
      while (true)
      {
        textBlock.Text += Environment.NewLine;
        textBlock.UpdateLayout();
        if (textBlock.ActualHeight <= heightToFit)
          a = textBlock.ActualHeight;
        else
          break;
      }
      return Math.Ceiling(a) + 1.0;
    }

    private static void PropertyChangedCallback(
      DependencyObject x,
      DependencyPropertyChangedEventArgs y)
    {
      ((ViewboxTextBlock) x).Update();
    }
  }
}
