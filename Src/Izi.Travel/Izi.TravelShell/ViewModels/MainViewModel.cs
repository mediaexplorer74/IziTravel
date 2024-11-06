// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.MainViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.ViewModels.Explore;
using Izi.Travel.Shell.ViewModels.Profile;
using Izi.Travel.Shell.ViewModels.QuickAccess;

#nullable disable
namespace Izi.Travel.Shell.ViewModels
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
