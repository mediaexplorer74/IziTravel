// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Flyout.FlyoutBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class FlyoutBehavior : Behavior<FlyoutBase>
  {
    public static readonly DependencyProperty OpeningCommandProperty = DependencyProperty.Register(nameof (OpeningCommand), typeof (ICommand), typeof (FlyoutBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty OpenedCommandProperty = DependencyProperty.Register(nameof (OpenedCommand), typeof (ICommand), typeof (FlyoutBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ClosingCommandProperty = DependencyProperty.Register(nameof (ClosingCommand), typeof (ICommand), typeof (FlyoutBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ClosedCommandProperty = DependencyProperty.Register(nameof (ClosedCommand), typeof (ICommand), typeof (FlyoutBehavior), new PropertyMetadata((object) null));

    public ICommand OpeningCommand
    {
      get => (ICommand) this.GetValue(FlyoutBehavior.OpeningCommandProperty);
      set => this.SetValue(FlyoutBehavior.OpeningCommandProperty, (object) value);
    }

    public ICommand OpenedCommand
    {
      get => (ICommand) this.GetValue(FlyoutBehavior.OpenedCommandProperty);
      set => this.SetValue(FlyoutBehavior.OpenedCommandProperty, (object) value);
    }

    public ICommand ClosingCommand
    {
      get => (ICommand) this.GetValue(FlyoutBehavior.ClosingCommandProperty);
      set => this.SetValue(FlyoutBehavior.ClosingCommandProperty, (object) value);
    }

    public ICommand ClosedCommand
    {
      get => (ICommand) this.GetValue(FlyoutBehavior.ClosedCommandProperty);
      set => this.SetValue(FlyoutBehavior.ClosedCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.Opening += new EventHandler<object>(this.AssociatedObject_Opening);
      this.AssociatedObject.Opened += new EventHandler<object>(this.AssociatedObject_Opened);
      this.AssociatedObject.Closing += new EventHandler<object>(this.AssociatedObject_Closing);
      this.AssociatedObject.Closed += new EventHandler<object>(this.AssociatedObject_Closed);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.Opening -= new EventHandler<object>(this.AssociatedObject_Opening);
      this.AssociatedObject.Opened -= new EventHandler<object>(this.AssociatedObject_Opened);
      this.AssociatedObject.Closing -= new EventHandler<object>(this.AssociatedObject_Closing);
      this.AssociatedObject.Closed -= new EventHandler<object>(this.AssociatedObject_Closed);
    }

    private void AssociatedObject_Opening(object sender, object e)
    {
      if (this.OpeningCommand == null || !this.OpeningCommand.CanExecute((object) null))
        return;
      this.OpeningCommand.Execute((object) null);
    }

    private void AssociatedObject_Opened(object sender, object e)
    {
      if (this.OpenedCommand == null || !this.OpenedCommand.CanExecute((object) null))
        return;
      this.OpenedCommand.Execute((object) null);
    }

    private void AssociatedObject_Closing(object sender, object e)
    {
      if (this.ClosingCommand == null || !this.ClosingCommand.CanExecute((object) null))
        return;
      this.ClosingCommand.Execute((object) null);
    }

    private void AssociatedObject_Closed(object sender, object e)
    {
      if (this.ClosedCommand == null || !this.ClosedCommand.CanExecute((object) null))
        return;
      this.ClosedCommand.Execute((object) null);
    }
  }
}
