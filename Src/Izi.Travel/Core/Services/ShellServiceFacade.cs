// ********************************************************************
// Type: Izi.Travel.Shell.Core.Services.ShellServiceFacade
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.AppServices.Navigation;
using Izi.Travel.Shell.Core.Services.Contract;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Shell.Core.Services
{
  public class ShellServiceFacade
  {
    private static INavigationService _navigationService;
    private static IDialogService _dialogService;
    private static Frame _rootFrame;

    /// <summary>
    /// Initializes the navigation service with the root frame.
    /// This must be called during app startup with the main application frame.
    /// </summary>
    /// <param name="rootFrame">The root frame of the application.</param>
    public static void Initialize(Frame rootFrame)
    {
      _rootFrame = rootFrame ?? throw new System.ArgumentNullException(nameof(rootFrame));
      //_navigationService = new UwpNavigationService(rootFrame);

    }

    public static INavigationService NavigationService
    {
      get
      {
        if (_navigationService == null)
        {
          if (_rootFrame == null)
          {
            throw new System.InvalidOperationException("ShellServiceFacade has not been initialized. Call Initialize() first.");
          }
         // _navigationService = new UwpNavigationService(_rootFrame);
        }
        return _navigationService;
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
