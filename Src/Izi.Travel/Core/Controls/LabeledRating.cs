// ********************************************************************
// Type: Izi.Travel.Core.Controls.LabeledRating
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll


using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

#nullable disable
namespace Izi.Travel.Core.Controls
{
  [TemplatePart(Name = "PartRating", Type = typeof (Setter))]
  public class LabeledRating : Control
  {
    private const string PartRating = "PartRating";
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (double), typeof (LabeledRating), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof (Label), typeof (string), typeof (LabeledRating), new PropertyMetadata((object) null));
    public static readonly DependencyProperty FilledItemBackgroundProperty = DependencyProperty.Register(nameof (FilledItemBackground), typeof (Brush), typeof (LabeledRating), new PropertyMetadata((object) null));
    public static readonly DependencyProperty UnfilledItemBackgroundProperty = DependencyProperty.Register(nameof (UnfilledItemBackground), typeof (Brush), typeof (LabeledRating), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ItemMarginProperty = DependencyProperty.Register(nameof (ItemMargin), typeof (Thickness), typeof (LabeledRating), new PropertyMetadata((object) new Thickness()));
    public static readonly DependencyProperty ItemsWidthProperty = DependencyProperty.Register(nameof (ItemsWidth), typeof (double), typeof (LabeledRating), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty ItemsHeightProperty = DependencyProperty.Register(nameof (ItemsHeight), typeof (double), typeof (LabeledRating), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty LabelMarginProperty = DependencyProperty.Register(nameof (LabelMargin), typeof (Thickness), typeof (LabeledRating), new PropertyMetadata((object) new Thickness()));

    public double Value
    {
      get => (double) this.GetValue(LabeledRating.ValueProperty);
      set => this.SetValue(LabeledRating.ValueProperty, (object) value);
    }

    public string Label
    {
      get => (string) this.GetValue(LabeledRating.LabelProperty);
      set => this.SetValue(LabeledRating.LabelProperty, (object) value);
    }

    public Brush FilledItemBackground
    {
      get => (Brush) this.GetValue(LabeledRating.FilledItemBackgroundProperty);
      set => this.SetValue(LabeledRating.FilledItemBackgroundProperty, (object) value);
    }

    public Brush UnfilledItemBackground
    {
      get => (Brush) this.GetValue(LabeledRating.UnfilledItemBackgroundProperty);
      set => this.SetValue(LabeledRating.UnfilledItemBackgroundProperty, (object) value);
    }

    public Thickness ItemMargin
    {
      get => (Thickness) this.GetValue(LabeledRating.ItemMarginProperty);
      set => this.SetValue(LabeledRating.ItemMarginProperty, (object) value);
    }

    public double ItemsWidth
    {
      get => (double) this.GetValue(LabeledRating.ItemsWidthProperty);
      set => this.SetValue(LabeledRating.ItemsWidthProperty, (object) value);
    }

    public double ItemsHeight
    {
      get => (double) this.GetValue(LabeledRating.ItemsHeightProperty);
      set => this.SetValue(LabeledRating.ItemsHeightProperty, (object) value);
    }

    public Thickness LabelMargin
    {
      get => (Thickness) this.GetValue(LabeledRating.LabelMarginProperty);
      set => this.SetValue(LabeledRating.LabelMarginProperty, (object) value);
    }

    public LabeledRating() => this.DefaultStyleKey = (object) typeof (LabeledRating);

    protected override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      
      // The RatingControl is already configured in XAML with the necessary bindings
      // No need to set styles programmatically as they are handled by the control
      
      // If we need to do any additional setup, we can get the RatingControl like this:
      // var ratingControl = GetTemplateChild("PartRating") as RatingControl;
      // if (ratingControl != null)
      // {
      //     // Additional setup if needed
      // }
    }
  }
}

