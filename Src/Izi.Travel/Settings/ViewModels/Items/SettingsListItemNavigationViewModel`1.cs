// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.Items.SettingsListItemNavigationViewModel`1
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;

#nullable disable
namespace Izi.Travel.Settings.ViewModels.Items
{
  public class SettingsListItemNavigationViewModel<TTargetViewModel> : SettingsListItemBaseViewModel where TTargetViewModel : IScreen
  {
    private readonly INavigationService _navigationService = IoC.Get<INavigationService>();

    protected INavigationService NavigationService => this._navigationService;

    public SettingsListItemNavigationViewModel(string name, string info)
      : base(name, info)
    {
    }

    protected override void ExecuteSelectCommand(object parameter)
    {
      this.NavigationService.UriFor<TTargetViewModel>().Navigate();
    }
  }
}
