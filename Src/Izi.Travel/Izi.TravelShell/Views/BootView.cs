// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Views.BootView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.ViewModels;
using Izi.Travel.Shell.ViewModels.Featured;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell.Views
{
  public class BootView : PhoneApplicationPage
  {
    private bool _contentLoaded;

    public BootView() => this.InitializeComponent();

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
      if (ServiceFacade.SettingsService.GetAppSettings().FirstLaunch)
        ShellServiceFacade.NavigationService.UriFor<FeaturedPartViewModel>().Navigate();
      else
        ShellServiceFacade.NavigationService.UriFor<MainViewModel>().Navigate();
      await Task.Factory.StartNew((System.Action) (() => Deployment.Current.Dispatcher.BeginInvoke((System.Action) (() => ShellServiceFacade.NavigationService.RemoveBackEntry()))));
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Views/BootView.xaml", UriKind.Relative));
    }
  }
}
