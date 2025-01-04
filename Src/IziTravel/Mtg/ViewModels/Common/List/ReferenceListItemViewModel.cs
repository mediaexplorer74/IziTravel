// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.ReferenceListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Mtg.Helpers;
using System;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public class ReferenceListItemViewModel : ListItemViewModel
  {
    private RelayCommand _navigateCommand;

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.Navigate)));
      }
    }

    private void Navigate(object parameter)
    {
      if (this.MtgObject == null)
        return;
      NavigationHelper.NavigateToDetails(this.MtgObject.Type, this.MtgObject.Uid, this.MtgObject.Language, this.MtgObject.ParentUid);
    }

    public ReferenceListItemViewModel(IListViewModel listViewModel, MtgObject mtgObject)
      : base(listViewModel, mtgObject)
    {
    }
  }
}
