// ********************************************************************
// Type: Izi.Travel.Shell.Commands.RateApplicationCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Command;
using Windows.System;
using Windows.ApplicationModel;

#nullable disable
namespace Izi.Travel.Shell.Commands
{
  public class RateApplicationCommand : BaseCommand
  {
    public override bool CanExecute(object parameter) => true;

    public override void Execute(object parameter)
    {
      // UWP: open Microsoft Store review page for this app
      string pfn = Package.Current.Id.FamilyName;
      var _ = Launcher.LaunchUriAsync(new System.Uri($"ms-windows-store://review/?PFN={pfn}"));
    }
  }
}
