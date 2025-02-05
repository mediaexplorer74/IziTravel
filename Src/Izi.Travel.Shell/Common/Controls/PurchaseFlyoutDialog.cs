// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Controls.PurchaseFlyoutDialog
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Common.Controls
{
  public class PurchaseFlyoutDialog : UserControl
  {
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof (IsBusy), typeof (bool), typeof (PurchaseFlyoutDialog), new PropertyMetadata((PropertyChangedCallback) null));
    private readonly MtgObject _mtgObject;
    internal UserControl PartFlyoutDialog;
    internal Izi.Travel.Shell.Core.Controls.Flyout.Flyout PartFlyout;
    private bool _contentLoaded;

    public bool IsBusy
    {
      get => (bool) this.GetValue(PurchaseFlyoutDialog.IsBusyProperty);
      set => this.SetValue(PurchaseFlyoutDialog.IsBusyProperty, (object) value);
    }

    public string Title => string.Format(AppResources.PurchaseDialogTitle).ToUpper();

    public string Message
    {
      get
      {
        return string.Format(AppResources.PurchaseDialogMessage, (object) this.GetTitle(), (object) this.GetPriceString());
      }
    }

    private PurchaseFlyoutDialog(MtgObject mtgObject)
    {
      this._mtgObject = mtgObject;
      this.InitializeComponent();
      this.DataContext = (object) this;
    }

    private string GetTitle()
    {
      return this._mtgObject == null || this._mtgObject.MainContent == null ? (string) null : this._mtgObject.MainContent.Title;
    }

    private string GetPriceString()
    {
      return this._mtgObject == null || this._mtgObject.Purchase == null ? (string) null : this._mtgObject.Purchase.PriceString;
    }

    public void Show() => this.PartFlyout.Show();

    private async void OnPurchaseButtonClick(object sender, RoutedEventArgs e)
    {
      this.IsBusy = true;
      await PurchaseManager.Instance.Purchase(this._mtgObject);
      this.IsBusy = false;
      this.PartFlyout.Hide();
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e) => this.PartFlyout.Hide();

    public static bool ConditionalShow(MtgObject mtgObject)
    {
      if (PurchaseManager.Instance.IsPurchased(mtgObject))
        return true;
      new PurchaseFlyoutDialog(mtgObject).Show();
      return false;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Common/Controls/PurchaseFlyoutDialog.xaml", UriKind.Relative));
      this.PartFlyoutDialog = (UserControl) this.FindName("PartFlyoutDialog");
      this.PartFlyout = (Izi.Travel.Shell.Core.Controls.Flyout.Flyout) this.FindName("PartFlyout");
    }
  }
}
