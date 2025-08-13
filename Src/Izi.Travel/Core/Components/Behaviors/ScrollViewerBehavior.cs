// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.ScrollViewerBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

#nullable disable
namespace Izi.Travel.Core.Components.Behaviors
{
  public class ScrollViewerBehavior : Behavior<ScrollViewer>
  {
    public static readonly DependencyProperty ResetVerticalOffsetTriggerProperty = DependencyProperty.Register(nameof (ResetVerticalOffsetTrigger), typeof (int), typeof (ScrollViewerBehavior), new PropertyMetadata((object) 0, (PropertyChangedCallback) ((x, y) => ((Behavior<ScrollViewer>) x).AssociatedObject.ScrollToVerticalOffset(0.0))));

    public int ResetVerticalOffsetTrigger
    {
      get => (int) this.GetValue(ScrollViewerBehavior.ResetVerticalOffsetTriggerProperty);
      set => this.SetValue(ScrollViewerBehavior.ResetVerticalOffsetTriggerProperty, (object) value);
    }
  }
}

