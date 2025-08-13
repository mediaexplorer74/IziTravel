// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.ControlSetFocusBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

#nullable disable
namespace Izi.Travel.Core.Components.Behaviors
{
  public class ControlSetFocusBehavior : Behavior<Control>
  {
    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.Loaded += new RoutedEventHandler(this.OnAssociatedObjectLoaded);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
      base.OnDetaching();
    }

    private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      this.AssociatedObject.Focus(FocusState.Programmatic);
      if (!(this.AssociatedObject is TextBox associatedObject))
        return;
      associatedObject.SelectAll();
    }
  }
}

