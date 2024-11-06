// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ActionRoundRadioButton
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplatePart(Name = "PartBorder", Type = typeof (Border))]
  public class ActionRoundRadioButton : RadioButton
  {
    private const string PartBorder = "PartBorder";
    private Border _border;
    public static readonly DependencyProperty CheckedBrushProperty = DependencyProperty.Register(nameof (CheckedBrush), typeof (Brush), typeof (ActionRoundRadioButton), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ActionRoundRadioButton), new PropertyMetadata((object) null));

    public Brush CheckedBrush
    {
      get => (Brush) this.GetValue(ActionRoundRadioButton.CheckedBrushProperty);
      set => this.SetValue(ActionRoundRadioButton.CheckedBrushProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ActionRoundRadioButton.ImageSourceProperty);
      set => this.SetValue(ActionRoundRadioButton.ImageSourceProperty, (object) value);
    }

    public ActionRoundRadioButton()
    {
      this.DefaultStyleKey = (object) typeof (ActionRoundRadioButton);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._border = this.GetTemplateChild("PartBorder") as Border;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this._border != null)
      {
        this._border.Width = finalSize.Height;
        this._border.Height = finalSize.Height;
        this._border.CornerRadius = new CornerRadius(this._border.Width / 2.0);
      }
      return base.ArrangeOverride(finalSize);
    }
  }
}
