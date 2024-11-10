// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.PhoneTextBoxBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class PhoneTextBoxBehavior : Behavior<PhoneTextBox>
  {
    public static readonly DependencyProperty UnfocusOnSubmitProperty = DependencyProperty.Register(nameof (UnfocusOnSubmit), typeof (bool), typeof (PhoneTextBoxBehavior), new PropertyMetadata((object) false));
    public static readonly DependencyProperty SubmitCommandProperty = DependencyProperty.Register(nameof (SubmitCommand), typeof (ICommand), typeof (PhoneTextBoxBehavior), new PropertyMetadata((PropertyChangedCallback) null));

    public bool UnfocusOnSubmit
    {
      get => (bool) this.GetValue(PhoneTextBoxBehavior.UnfocusOnSubmitProperty);
      set => this.SetValue(PhoneTextBoxBehavior.UnfocusOnSubmitProperty, (object) value);
    }

    public ICommand SubmitCommand
    {
      get => (ICommand) this.GetValue(PhoneTextBoxBehavior.SubmitCommandProperty);
      set => this.SetValue(PhoneTextBoxBehavior.SubmitCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.KeyDown += new KeyEventHandler(this.AssociatedObject_KeyDown);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.KeyDown -= new KeyEventHandler(this.AssociatedObject_KeyDown);
    }

    private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key != Key.Enter)
        return;
      if (this.UnfocusOnSubmit && Application.Current.RootVisual is Control rootVisual)
        rootVisual.Focus();
      if (this.SubmitCommand == null || !this.SubmitCommand.CanExecute((object) null))
        return;
      this.SubmitCommand.Execute((object) null);
    }
  }
}
