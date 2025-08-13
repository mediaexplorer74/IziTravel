// ********************************************************************
// Type: Izi.Travel.ViewModels.Explore.Flyouts.ExploreFlyoutViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Common.ViewModels.Flyout;

#nullable disable
namespace Izi.Travel.ViewModels.Explore.Flyouts
{
  public abstract class ExploreFlyoutViewModel : FlyoutViewModel
  {
    public ExploreViewModel ExploreViewModel { get; private set; }

    protected ExploreFlyoutViewModel(ExploreViewModel exploreViewModel)
    {
      this.ExploreViewModel = exploreViewModel;
    }
  }
}
