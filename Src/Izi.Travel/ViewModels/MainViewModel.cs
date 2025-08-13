// ********************************************************************
// Type: Izi.Travel.ViewModels.MainViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.ViewModels.Explore;
using Izi.Travel.ViewModels.Profile;
using Izi.Travel.ViewModels.QuickAccess;

#nullable disable
namespace Izi.Travel.ViewModels
{
  public sealed class MainViewModel : Conductor<IScreen>.Collection.OneActive
  {
    public ExploreViewModel ExploreViewModel { get; private set; }

    public ProfileViewModel ProfileViewModel { get; private set; }

    public QuickAccessViewModel QuickAccessViewModel { get; private set; }

    public MainViewModel(
      ExploreViewModel exploreViewModel,
      ProfileViewModel profileViewModel,
      QuickAccessViewModel quickAccessViewModel)
    {
      this.ExploreViewModel = exploreViewModel;
      this.QuickAccessViewModel = quickAccessViewModel;
      this.ProfileViewModel = profileViewModel;
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.Add((IScreen) this.ExploreViewModel);
      this.Items.Add((IScreen) this.ProfileViewModel);
      this.Items.Add((IScreen) this.QuickAccessViewModel);
    }
  }
}
