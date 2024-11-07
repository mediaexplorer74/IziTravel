// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Flyout.FlyoutDialog
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class FlyoutDialog : UserControl
  {
    private FlyoutDialogResult _result;
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (FlyoutDialog), new PropertyMetadata((object) null));
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof (Message), typeof (string), typeof (FlyoutDialog), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsLeftButtonEnabledProperty = DependencyProperty.Register(nameof (IsLeftButtonEnabled), typeof (bool), typeof (FlyoutDialog), new PropertyMetadata((object) true));
    public static readonly DependencyProperty LeftButtonContentProperty = DependencyProperty.Register(nameof (LeftButtonContent), typeof (object), typeof (FlyoutDialog), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsRightButtonEnabledProperty = DependencyProperty.Register(nameof (IsRightButtonEnabled), typeof (bool), typeof (FlyoutDialog), new PropertyMetadata((object) false));
    public static readonly DependencyProperty RightButtonContentProperty = DependencyProperty.Register(nameof (RightButtonContent), typeof (object), typeof (FlyoutDialog), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsDontShowVisibleProperty = DependencyProperty.Register(nameof (IsDontShowVisible), typeof (bool), typeof (FlyoutDialog), new PropertyMetadata((object) false));
    public static readonly DependencyProperty IsDontShowEnabledProperty = DependencyProperty.Register(nameof (IsDontShowEnabled), typeof (bool), typeof (FlyoutDialog), new PropertyMetadata((object) false));
    public static readonly DependencyProperty HyperlinkContentProperty = DependencyProperty.Register(nameof (HyperlinkContent), typeof (object), typeof (FlyoutDialog), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty IsHyperlinkVisibleProperty = DependencyProperty.Register(nameof (IsHyperlinkVisible), typeof (bool), typeof (FlyoutDialog), new PropertyMetadata((object) false));
    internal UserControl PartFlyoutDialog;
    internal Izi.Travel.Shell.Core.Controls.Flyout.Flyout PartFlyout;
    private bool _contentLoaded;

    public event TypedEventHandler<FlyoutDialog, FlyoutDialogResult> Closed
    {
      add
      {
        TypedEventHandler<FlyoutDialog, FlyoutDialogResult> typedEventHandler1 = this.Closed;
        TypedEventHandler<FlyoutDialog, FlyoutDialogResult> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<FlyoutDialog, FlyoutDialogResult>>(ref this.Closed, (TypedEventHandler<FlyoutDialog, FlyoutDialogResult>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<FlyoutDialog, FlyoutDialogResult> typedEventHandler1 = this.Closed;
        TypedEventHandler<FlyoutDialog, FlyoutDialogResult> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<FlyoutDialog, FlyoutDialogResult>>(ref this.Closed, (TypedEventHandler<FlyoutDialog, FlyoutDialogResult>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public string Title
    {
      get => (string) this.GetValue(FlyoutDialog.TitleProperty);
      set => this.SetValue(FlyoutDialog.TitleProperty, (object) value);
    }

    public string Message
    {
      get => (string) this.GetValue(FlyoutDialog.MessageProperty);
      set => this.SetValue(FlyoutDialog.MessageProperty, (object) value);
    }

    public bool IsLeftButtonEnabled
    {
      get => (bool) this.GetValue(FlyoutDialog.IsLeftButtonEnabledProperty);
      set => this.SetValue(FlyoutDialog.IsLeftButtonEnabledProperty, (object) value);
    }

    public object LeftButtonContent
    {
      get => this.GetValue(FlyoutDialog.LeftButtonContentProperty);
      set => this.SetValue(FlyoutDialog.LeftButtonContentProperty, value);
    }

    public bool IsRightButtonEnabled
    {
      get => (bool) this.GetValue(FlyoutDialog.IsRightButtonEnabledProperty);
      set => this.SetValue(FlyoutDialog.IsRightButtonEnabledProperty, (object) value);
    }

    public object RightButtonContent
    {
      get => this.GetValue(FlyoutDialog.RightButtonContentProperty);
      set => this.SetValue(FlyoutDialog.RightButtonContentProperty, value);
    }

    public bool IsDontShowVisible
    {
      get => (bool) this.GetValue(FlyoutDialog.IsDontShowVisibleProperty);
      set => this.SetValue(FlyoutDialog.IsDontShowVisibleProperty, (object) value);
    }

    public bool IsDontShowEnabled
    {
      get => (bool) this.GetValue(FlyoutDialog.IsDontShowEnabledProperty);
      set => this.SetValue(FlyoutDialog.IsDontShowEnabledProperty, (object) value);
    }

    public object HyperlinkContent
    {
      get => this.GetValue(FlyoutDialog.HyperlinkContentProperty);
      set => this.SetValue(FlyoutDialog.HyperlinkContentProperty, value);
    }

    public bool IsHyperlinkVisible
    {
      get => (bool) this.GetValue(FlyoutDialog.IsHyperlinkVisibleProperty);
      set => this.SetValue(FlyoutDialog.IsHyperlinkVisibleProperty, (object) value);
    }

    public Action HyperlinkAction { get; set; }

    public FlyoutDialog()
    {
      this.InitializeComponent();
      this.DataContext = (object) this;
    }

    public void Show() => this.PartFlyout.Show();

    private void OnLeftButtonClick(object sender, RoutedEventArgs e)
    {
      this._result = FlyoutDialogResult.LeftButton;
      this.PartFlyout.Hide();
    }

    private void OnRightButtonClick(object sender, RoutedEventArgs e)
    {
      this._result = FlyoutDialogResult.RightButton;
      this.PartFlyout.Hide();
    }

    private void OnPartFlyoutClosed(object sender, object e)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.Closed == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.Closed.Invoke(this, this._result);
    }

    private void HyperlinkButton_OnClick(object sender, RoutedEventArgs e)
    {
      Action hyperlinkAction = this.HyperlinkAction;
      if (hyperlinkAction == null)
        return;
      hyperlinkAction();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Core/Controls/Flyout/FlyoutDialog.xaml", UriKind.Relative));
      this.PartFlyoutDialog = (UserControl) this.FindName("PartFlyoutDialog");
      this.PartFlyout = (Izi.Travel.Shell.Core.Controls.Flyout.Flyout) this.FindName("PartFlyout");
    }
  }
}
