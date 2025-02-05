// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsListItemCodeNameViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Settings.ViewModels.Items;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsListItemCodeNameViewModel : SettingsListItemBaseViewModel
  {
    private SettingsViewModel SettingsViewModel { get; set; }

    public SettingsListItemCodeNameViewModel(
      SettingsViewModel settingsViewModel,
      string name,
      string info)
      : base(name, info)
    {
      this.SettingsViewModel = settingsViewModel;
    }

    protected override void ExecuteSelectCommand(object parameter)
    {
      this.SettingsViewModel.PasscodeFlyoutViewModel.OpenCommand.Execute((object) null);
    }
  }
}
