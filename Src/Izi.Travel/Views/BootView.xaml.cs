// ********************************************************************
// Type: Izi.Travel.Shell.Views.BootView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.ViewModels;
using Izi.Travel.Shell.ViewModels.Featured;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#nullable disable
namespace Izi.Travel.Shell.Views
{
    public sealed partial class BootView : Page
    {
        
        public BootView()
        {
            this.InitializeComponent();
        }

        //TODO
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (ServiceFacade.SettingsService.GetAppSettings().FirstLaunch)
              ShellServiceFacade.NavigationService.UriFor<FeaturedPartViewModel>().Navigate();
            else
              ShellServiceFacade.NavigationService.UriFor<MainViewModel>().Navigate();

            await Task.Factory.StartNew((System.Action)(()
    => CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
        new DispatchedHandler(() =>
        {
            /*ShellServiceFacade.NavigationService.RemoveBackEntry();*/
        }))));

        }
    }
}

