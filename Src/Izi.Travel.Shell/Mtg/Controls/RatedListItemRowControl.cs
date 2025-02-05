// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.RatedListItemRowControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Controls
{
  public class RatedListItemRowControl : ListItemControl
  {
    public static readonly DependencyProperty ShowNumberProperty = DependencyProperty.Register(nameof (ShowNumber), typeof (bool), typeof (RatedListItemRowControl), new PropertyMetadata((object) false));
    public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(nameof (Number), typeof (string), typeof (RatedListItemRowControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ShowRatingProperty = DependencyProperty.Register(nameof (ShowRating), typeof (bool), typeof (RatedListItemRowControl), new PropertyMetadata((object) false));
    public static readonly DependencyProperty RatingProperty = DependencyProperty.Register(nameof (Rating), typeof (double), typeof (RatedListItemRowControl), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty RatingLabelProperty = DependencyProperty.Register(nameof (RatingLabel), typeof (string), typeof (RatedListItemRowControl), new PropertyMetadata((object) null));

    public bool ShowNumber
    {
      get => (bool) this.GetValue(RatedListItemRowControl.ShowNumberProperty);
      set => this.SetValue(RatedListItemRowControl.ShowNumberProperty, (object) value);
    }

    public string Number
    {
      get => (string) this.GetValue(RatedListItemRowControl.NumberProperty);
      set => this.SetValue(RatedListItemRowControl.NumberProperty, (object) value);
    }

    public bool ShowRating
    {
      get => (bool) this.GetValue(RatedListItemRowControl.ShowRatingProperty);
      set => this.SetValue(RatedListItemRowControl.ShowRatingProperty, (object) value);
    }

    public double Rating
    {
      get => (double) this.GetValue(RatedListItemRowControl.RatingProperty);
      set => this.SetValue(RatedListItemRowControl.RatingProperty, (object) value);
    }

    public string RatingLabel
    {
      get => (string) this.GetValue(RatedListItemRowControl.RatingLabelProperty);
      set => this.SetValue(RatedListItemRowControl.RatingLabelProperty, (object) value);
    }

    public RatedListItemRowControl()
    {
      this.DefaultStyleKey = (object) typeof (RatedListItemRowControl);
    }
  }
}
