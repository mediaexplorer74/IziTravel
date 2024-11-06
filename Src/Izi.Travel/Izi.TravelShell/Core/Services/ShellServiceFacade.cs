// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Services.ShellServiceFacade
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Services.Contract;

#nullable disable
namespace Izi.Travel.Shell.Core.Services
{
  public class ShellServiceFacade
  {
    private static INavigationService _navigationService;
    private static IDialogService _dialogService;

    public static INavigationService NavigationService
    {
      get
      {
        return ShellServiceFacade._navigationService ?? (ShellServiceFacade._navigationService = IoC.Get<INavigationService>());
      }
    }

    public static IDialogService DialogService
    {
      get
      {
        return ShellServiceFacade._dialogService ?? (ShellServiceFacade._dialogService = IoC.Get<IDialogService>());
      }
    }
  }
}
