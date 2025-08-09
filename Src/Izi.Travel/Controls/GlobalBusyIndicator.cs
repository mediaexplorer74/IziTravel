// IziTravel.Controls.GlobalBusyIndicator


using IziTravel.AppServices;
using IziTravel.Core;

using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Shell.Controls
{
  public class GlobalBusyIndicator : DependencyObject, IBusyIndicator
  {
    public static readonly DependencyProperty BusyIndicatorProperty = DependencyProperty.RegisterAttached("BusyIndicator", typeof (IBusyIndicator), typeof (GlobalBusyIndicator), new PropertyMetadata((PropertyChangedCallback) null));

    private readonly Page _page;
    private int _isBusyCounter;

    public static IBusyIndicator Create()
    {
      Page content = default;//((ContentControl) Application.Current.RootVisual).Content as Page;
      if (!(((DependencyObject) content).GetValue(GlobalBusyIndicator.BusyIndicatorProperty) is IBusyIndicator busyIndicator))
      {
        busyIndicator = (IBusyIndicator) new GlobalBusyIndicator(content);
        ((DependencyObject) content).SetValue(GlobalBusyIndicator.BusyIndicatorProperty, (object) busyIndicator);
      }
      return busyIndicator;
    }

    private GlobalBusyIndicator(Page page)
    {
      this._page = page;

    }

    public bool IsBusy => this._isBusyCounter > 0;

    public IDisposable StartJob()
    {
      ++this._isBusyCounter;
      this.UpdateIndicatorVisibility();
      return (IDisposable) new DisposableSource((Action) (() => this.EndJob()));
    }

    public void EndJob()
    {
      if (this._isBusyCounter > 0)
        --this._isBusyCounter;
      this.UpdateIndicatorVisibility();
    }

    private void UpdateIndicatorVisibility()
    {
      bool flag = this._isBusyCounter > 0;

    }
  }
}
