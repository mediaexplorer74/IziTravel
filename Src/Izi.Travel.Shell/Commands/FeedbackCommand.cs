// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Commands.FeedbackCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Settings.ViewModels.Application;

#nullable disable
namespace Izi.Travel.Shell.Commands
{
  public class FeedbackCommand : BaseCommand
  {
    public override bool CanExecute(object parameter) => true;

    public override void Execute(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<SettingsAppFeedbackMessageViewModel>().Navigate();
    }
  }
}
