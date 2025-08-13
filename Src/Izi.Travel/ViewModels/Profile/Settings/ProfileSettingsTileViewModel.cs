// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.Settings.ProfileSettingsTileViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Model.Profile;
using Izi.Travel.Settings.ViewModels;

#nullable disable
namespace Izi.Travel.ViewModels.Profile.Settings
{
  public class ProfileSettingsTileViewModel : ProfileTileViewModel
  {
    public override string Title => AppResources.LabelSettings;

    public override ProfileType Type => ProfileType.Custom;

    protected override void ExecuteNavigateCommand(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<SettingsViewModel>().Navigate();
    }
  }
}
