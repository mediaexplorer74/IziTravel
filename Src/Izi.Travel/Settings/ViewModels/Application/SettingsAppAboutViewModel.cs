// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.Application.SettingsAppAboutViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Resources;
using Windows.ApplicationModel;

#nullable disable
namespace Izi.Travel.Settings.ViewModels.Application
{
  public class SettingsAppAboutViewModel : Screen
  {
    public string Version
    {
      get
      {
        var version = Package.Current.Id.Version;
        return $"{AppResources.LabelVersion} {version.Major}.{version.Minor}.{version.Build}";
      }
    }
  }
}
