// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.Application.SettingsListItemAboutViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Settings.ViewModels.Items;

#nullable disable
namespace Izi.Travel.Settings.ViewModels.Application
{
  public class SettingsListItemAboutViewModel : 
    SettingsListItemNavigationViewModel<SettingsAppAboutViewModel>
  {
    public SettingsListItemAboutViewModel(string name, string info)
      : base(name, info)
    {
    }
  }
}
