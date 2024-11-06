// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Settings.ProfileSettingsTileViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Model.Profile;
using Izi.Travel.Shell.Settings.ViewModels;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Settings
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
