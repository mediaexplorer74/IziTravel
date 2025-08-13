// ********************************************************************
// Type: Izi.Travel.Core.Controls.LinkButton
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

#nullable disable
namespace Izi.Travel.Core.Controls
{
  public class LinkButton : Button
  {
    public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register(nameof (TextTrimming), typeof (TextTrimming), typeof (LinkButton), new PropertyMetadata((object) TextTrimming.None));
    public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register(nameof (PressedForeground), typeof (Brush), typeof (LinkButton), new PropertyMetadata((object) null));

    public TextTrimming TextTrimming
    {
      get => (TextTrimming) this.GetValue(LinkButton.TextTrimmingProperty);
      set => this.SetValue(LinkButton.TextTrimmingProperty, (object) value);
    }

    public Brush PressedForeground
    {
      get => (Brush) this.GetValue(LinkButton.PressedForegroundProperty);
      set => this.SetValue(LinkButton.PressedForegroundProperty, (object) value);
    }

    public LinkButton() => this.DefaultStyleKey = (object) typeof (LinkButton);
  }
}

