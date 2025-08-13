// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.PhoneTextBoxBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using System.Windows.Input;
using Windows.System;

#nullable disable
namespace Izi.Travel.Core.Components.Behaviors
{
  public class PhoneTextBoxBehavior : Behavior<TextBox>
  {
    public static readonly DependencyProperty UnfocusOnSubmitProperty = DependencyProperty.Register(nameof (UnfocusOnSubmit), typeof (bool), typeof (PhoneTextBoxBehavior), new PropertyMetadata((object) false));
    public static readonly DependencyProperty SubmitCommandProperty = DependencyProperty.Register(nameof (SubmitCommand), typeof (System.Windows.Input.ICommand), typeof (PhoneTextBoxBehavior), new PropertyMetadata((PropertyChangedCallback) null));

    public bool UnfocusOnSubmit
    {
      get => (bool) this.GetValue(PhoneTextBoxBehavior.UnfocusOnSubmitProperty);
      set => this.SetValue(PhoneTextBoxBehavior.UnfocusOnSubmitProperty, (object) value);
    }

    public System.Windows.Input.ICommand SubmitCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(PhoneTextBoxBehavior.SubmitCommandProperty);
      set => this.SetValue(PhoneTextBoxBehavior.SubmitCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.KeyDown += this.AssociatedObject_KeyDown;
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.KeyDown -= this.AssociatedObject_KeyDown;
    }

    private void AssociatedObject_KeyDown(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != VirtualKey.Enter)
        return;
      if (this.UnfocusOnSubmit && Window.Current.Content is Control rootVisual)
        rootVisual.Focus(FocusState.Programmatic);
      if (this.SubmitCommand == null || !this.SubmitCommand.CanExecute((object) null))
        return;
      this.SubmitCommand.Execute((object) null);
    }
  }
}

