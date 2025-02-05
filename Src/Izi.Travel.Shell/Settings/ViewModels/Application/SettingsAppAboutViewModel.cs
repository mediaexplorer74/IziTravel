// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppAboutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Resources;
using System.Reflection;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppAboutViewModel : Screen
  {
    public string Version
    {
      get
      {
        return string.Format("{0} {1}", (object) AppResources.LabelVersion, (object) Assembly.GetExecutingAssembly().GetName().Version);
      }
    }
  }
}
