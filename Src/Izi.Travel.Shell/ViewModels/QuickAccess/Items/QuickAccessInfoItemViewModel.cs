// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.QuickAccess.Items.QuickAccessInfoItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using System;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.QuickAccess.Items
{
  public sealed class QuickAccessInfoItemViewModel : QuickAccessBaseItemViewModel
  {
    private RelayCommand _navigateToExploreCommand;

    public QuickAccessInfoItemViewModel(QuickAccessViewModel quickAccessViewModel)
      : base(quickAccessViewModel)
    {
    }

    public RelayCommand NavigateToExploreCommand
    {
      get
      {
        return this._navigateToExploreCommand ?? (this._navigateToExploreCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateToExploreCommand)));
      }
    }

    private void ExecuteNavigateToExploreCommand(object parameter)
    {
      if (this.QuickAccessViewModel == null || !(this.QuickAccessViewModel.Parent is MainViewModel parent))
        return;
      parent.ActiveItem = (IScreen) parent.ExploreViewModel;
    }
  }
}
