// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.SliderManipulationBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Core.Components.Behaviors
{
  public class SliderManipulationBehavior : Behavior<Slider>
  {
    public static readonly DependencyProperty StartCommandProperty = DependencyProperty.Register(nameof (StartCommand), typeof (System.Windows.Input.ICommand), typeof (SliderManipulationBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty EndCommandProperty = DependencyProperty.Register(nameof (EndCommand), typeof (System.Windows.Input.ICommand), typeof (SliderManipulationBehavior), new PropertyMetadata((object) null));

    public System.Windows.Input.ICommand StartCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(SliderManipulationBehavior.StartCommandProperty);
      set => this.SetValue(SliderManipulationBehavior.StartCommandProperty, (object) value);
    }

    public System.Windows.Input.ICommand EndCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(SliderManipulationBehavior.EndCommandProperty);
      set => this.SetValue(SliderManipulationBehavior.EndCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.ManipulationStarted += this.OnSliderManipulationStarted;
      this.AssociatedObject.ManipulationCompleted += this.OnSliderManipulationCompleted;
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.ManipulationStarted -= this.OnSliderManipulationStarted;
      this.AssociatedObject.ManipulationCompleted -= this.OnSliderManipulationCompleted;
      base.OnDetaching();
    }

    private void OnSliderManipulationStarted(object sender, ManipulationStartedRoutedEventArgs args)
    {
      if (this.StartCommand == null || !this.StartCommand.CanExecute((object) this.AssociatedObject.Value))
        return;
      this.StartCommand.Execute((object) this.AssociatedObject.Value);
    }

    private void OnSliderManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs args)
    {
      if (this.EndCommand == null || !this.EndCommand.CanExecute((object) this.AssociatedObject.Value))
        return;
      this.EndCommand.Execute((object) this.AssociatedObject.Value);
    }
  }
}

