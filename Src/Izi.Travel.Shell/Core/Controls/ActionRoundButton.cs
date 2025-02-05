// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ActionRoundButton
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplatePart(Name = "PartBorder", Type = typeof (Border))]
  [TemplatePart(Name = "PartEllipse", Type = typeof (Ellipse))]
  public class ActionRoundButton : Button
  {
    private const string PartBorder = "PartBorder";
    private const string PartEllipse = "PartEllipse";
    private Border _border;
    private Ellipse _ellipse;
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ActionRoundButton), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(nameof (ImageWidth), typeof (double), typeof (ActionRoundButton), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(nameof (ImageHeight), typeof (double), typeof (ActionRoundButton), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register(nameof (PressedBackground), typeof (Brush), typeof (ActionRoundButton), new PropertyMetadata((object) null));
    public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register(nameof (PressedForeground), typeof (Brush), typeof (ActionRoundButton), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ButtonOverlayContentProperty = DependencyProperty.Register(nameof (ButtonOverlayContent), typeof (object), typeof (ActionRoundButton), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ButtonOverlayContentTemplateProperty = DependencyProperty.Register(nameof (ButtonOverlayContentTemplate), typeof (DataTemplate), typeof (ActionRoundButton), new PropertyMetadata((object) null));

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ActionRoundButton.ImageSourceProperty);
      set => this.SetValue(ActionRoundButton.ImageSourceProperty, (object) value);
    }

    public double ImageWidth
    {
      get => (double) this.GetValue(ActionRoundButton.ImageWidthProperty);
      set => this.SetValue(ActionRoundButton.ImageWidthProperty, (object) value);
    }

    public double ImageHeight
    {
      get => (double) this.GetValue(ActionRoundButton.ImageHeightProperty);
      set => this.SetValue(ActionRoundButton.ImageHeightProperty, (object) value);
    }

    public Brush PressedBackground
    {
      get => (Brush) this.GetValue(ActionRoundButton.PressedBackgroundProperty);
      set => this.SetValue(ActionRoundButton.PressedBackgroundProperty, (object) value);
    }

    public Brush PressedForeground
    {
      get => (Brush) this.GetValue(ActionRoundButton.PressedForegroundProperty);
      set => this.SetValue(ActionRoundButton.PressedForegroundProperty, (object) value);
    }

    public object ButtonOverlayContent
    {
      get => this.GetValue(ActionRoundButton.ButtonOverlayContentProperty);
      set => this.SetValue(ActionRoundButton.ButtonOverlayContentProperty, value);
    }

    public DataTemplate ButtonOverlayContentTemplate
    {
      get => (DataTemplate) this.GetValue(ActionRoundButton.ButtonOverlayContentTemplateProperty);
      set => this.SetValue(ActionRoundButton.ButtonOverlayContentTemplateProperty, (object) value);
    }

    public ActionRoundButton() => this.DefaultStyleKey = (object) typeof (ActionRoundButton);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._border = this.GetTemplateChild("PartBorder") as Border;
      this._ellipse = this.GetTemplateChild("PartEllipse") as Ellipse;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this._border != null)
      {
        this._border.Width = finalSize.Height;
        this._border.Height = finalSize.Height;
        this._border.CornerRadius = new CornerRadius(this._border.Width / 2.0);
        if (this._ellipse != null)
        {
          this._ellipse.Width = !double.IsNaN(this.ImageWidth) ? this.ImageWidth : this._border.Width;
          this._ellipse.Height = !double.IsNaN(this.ImageHeight) ? this.ImageHeight : this._border.Height;
        }
      }
      return base.ArrangeOverride(finalSize);
    }
  }
}
