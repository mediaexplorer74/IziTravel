// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Featured.FeaturedListFlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Common.ViewModels.Flyout;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Featured
{
  public sealed class FeaturedListFlyoutViewModel : FlyoutViewModel
  {
    private readonly FeaturedListViewModel _listViewModel;

    public FeaturedListViewModel ListViewModel => this._listViewModel;

    public FeaturedListFlyoutViewModel()
    {
      this._listViewModel = IoC.Get<FeaturedListViewModel>();
      this._listViewModel.ExploreCommand = this.CloseCommand;
    }

    protected override void OnOpening()
    {
      base.OnOpening();
      if (!this._listViewModel.RefreshCommand.CanExecute((object) null))
        return;
      this._listViewModel.RefreshCommand.Execute((object) null);
    }
  }
}
