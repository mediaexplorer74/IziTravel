// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Flyout.FlyoutBase
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Linq;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public abstract class FlyoutBase : DependencyObject
  {
    private bool _silentIsOpen;
    public static readonly DependencyProperty AttachedFlyoutsProperty = DependencyProperty.RegisterAttached("AttachedFlyouts", typeof (FlyoutCollection), typeof (FlyoutBase), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.RegisterAttached("IsOpen", typeof (bool), typeof (FlyoutBase), new PropertyMetadata((object) false, new PropertyChangedCallback(FlyoutBase.OnIsOpenPropertyChanged)));

    public static FlyoutCollection GetAttachedFlyouts(FrameworkElement element)
    {
      FlyoutCollection attachedFlyouts = element != null ? (FlyoutCollection) element.GetValue(FlyoutBase.AttachedFlyoutsProperty) : throw new ArgumentNullException(nameof (element));
      if (attachedFlyouts == null)
      {
        attachedFlyouts = new FlyoutCollection(element);
        element.SetValue(FlyoutBase.AttachedFlyoutsProperty, (object) attachedFlyouts);
      }
      return attachedFlyouts;
    }

    public static void SetIsOpen(FlyoutBase element, bool value)
    {
      element.SetValue(FlyoutBase.IsOpenProperty, (object) value);
    }

    public static bool GetIsOpen(FlyoutBase element)
    {
      return (bool) element.GetValue(FlyoutBase.IsOpenProperty);
    }

    private static void OnIsOpenPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is FlyoutBase flyoutBase) || flyoutBase._silentIsOpen || !(e.NewValue is bool))
        return;
      if ((bool) e.NewValue)
        flyoutBase.Show();
      else
        flyoutBase.Hide();
    }

    public event EventHandler<object> Opening;

    public event EventHandler<object> Opened;

    public event EventHandler<object> Closing;

    public event EventHandler<object> Closed;

    public string Key { get; set; }

    protected FrameworkElement Owner { get; private set; }

    public static void ShowAttachedFlyout(FrameworkElement owner, string flyoutKey)
    {
      FlyoutCollection attachedFlyouts = FlyoutBase.GetAttachedFlyouts(owner);
      if (attachedFlyouts.Count == 0)
        return;
      (string.IsNullOrWhiteSpace(flyoutKey) ? attachedFlyouts[0] : attachedFlyouts.FirstOrDefault<FlyoutBase>((Func<FlyoutBase, bool>) (x => x.Key == flyoutKey)))?.Show();
    }

    public void Show()
    {
      this.ShowImpl();
      this.SetIsOpenSilent(true);
    }

    public void Hide()
    {
      this.HideImpl();
      this.SetIsOpenSilent(false);
    }

    internal void SetOwner(FrameworkElement owner) => this.Owner = owner;

    protected abstract void ShowImpl();

    protected abstract void HideImpl();

    protected virtual void OnOpening()
    {
      EventHandler<object> opening = this.Opening;
      if (opening == null)
        return;
      opening((object) this, (object) this.Owner);
    }

    protected virtual void OnOpened()
    {
      EventHandler<object> opened = this.Opened;
      if (opened == null)
        return;
      opened((object) this, (object) this.Owner);
    }

    protected virtual void OnClosing()
    {
      EventHandler<object> closing = this.Closing;
      if (closing == null)
        return;
      closing((object) this, (object) this.Owner);
    }

    protected virtual void OnClosed()
    {
      EventHandler<object> closed = this.Closed;
      if (closed == null)
        return;
      closed((object) this, (object) this.Owner);
    }

    private void SetIsOpenSilent(bool value)
    {
      this._silentIsOpen = true;
      FlyoutBase.SetIsOpen(this, value);
      this._silentIsOpen = false;
    }
  }
}
