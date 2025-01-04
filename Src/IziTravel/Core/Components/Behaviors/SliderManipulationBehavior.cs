// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.SliderManipulationBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class SliderManipulationBehavior : Behavior<Slider>
  {
    public static readonly DependencyProperty StartCommandProperty = DependencyProperty.Register(nameof (StartCommand), typeof (ICommand), typeof (SliderManipulationBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty EndCommandProperty = DependencyProperty.Register(nameof (EndCommand), typeof (ICommand), typeof (SliderManipulationBehavior), new PropertyMetadata((object) null));

    public ICommand StartCommand
    {
      get => (ICommand) this.GetValue(SliderManipulationBehavior.StartCommandProperty);
      set => this.SetValue(SliderManipulationBehavior.StartCommandProperty, (object) value);
    }

    public ICommand EndCommand
    {
      get => (ICommand) this.GetValue(SliderManipulationBehavior.EndCommandProperty);
      set => this.SetValue(SliderManipulationBehavior.EndCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnSliderManipulationStarted);
      this.AssociatedObject.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnSliderManipulationCompleted);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnSliderManipulationStarted);
      this.AssociatedObject.ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnSliderManipulationCompleted);
      base.OnDetaching();
    }

    private void OnSliderManipulationStarted(object sender, ManipulationStartedEventArgs args)
    {
      if (this.StartCommand == null || !this.StartCommand.CanExecute((object) this.AssociatedObject.Value))
        return;
      this.StartCommand.Execute((object) this.AssociatedObject.Value);
    }

    private void OnSliderManipulationCompleted(object sender, ManipulationCompletedEventArgs args)
    {
      if (this.EndCommand == null || !this.EndCommand.CanExecute((object) this.AssociatedObject.Value))
        return;
      this.EndCommand.Execute((object) this.AssociatedObject.Value);
    }
  }
}
