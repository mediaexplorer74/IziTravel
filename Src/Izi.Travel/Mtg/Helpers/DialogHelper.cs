// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.DialogHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using System;
using System.Threading.Tasks;
using System.Windows;
using Windows.System;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public class DialogHelper
  {
    public static Task<bool> CheckForLocationServices()
    {
      TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
      if (Geotracker.Instance.IsEnabled)
        taskCompletionSource.TrySetResult(true);
      else
        ShellServiceFacade.DialogService.Show(AppResources.DialogLocationServicesDisabledTitle, AppResources.DialogLocationServicesDisabledMessage, MessageBoxButtonContent.Ok, (Action<FlyoutDialog>) (x =>
        {
          x.IsHyperlinkVisible = true;
          x.HyperlinkContent = (object) AppResources.DialogLocationServicesDisabledLink;
          x.HyperlinkAction = (Action) (async () =>
          {
            if (!Geotracker.Instance.IsEnabledInternal)
            {
              ShellServiceFacade.NavigationService.Navigate(new Uri("/Views/Application/SettingsAppLocationView.xaml", UriKind.Relative));
            }
            else
            {
              int num = await Launcher.LaunchUriAsync(new Uri("ms-settings-location:")) ? 1 : 0;
            }
          });
        }), (Action<FlyoutDialog, MessageBoxResult>) ((x, y) => taskCompletionSource.TrySetResult(Geotracker.Instance.IsEnabled)));
      return taskCompletionSource.Task;
    }
  }
}
