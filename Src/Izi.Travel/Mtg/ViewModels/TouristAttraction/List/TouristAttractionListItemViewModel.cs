// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.List.TouristAttractionListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.List
{
  public class TouristAttractionListItemViewModel : ListItemViewModel
  {
    private RelayCommand _navigateCommand;

    public MtgObject Parent { get; private set; }

    public int Index { get; private set; }

    public string FullTitle => string.Format("{0} {1}", (object) this.Index, (object) this.Title);

    public TouristAttractionListItemViewModel(
      IListViewModel listViewModel,
      MtgObject parent,
      MtgObject attraction,
      int index)
      : base(listViewModel, attraction)
    {
      this.Parent = parent;
      this.Index = index;
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand)));
      }
    }

    private void ExecuteNavigateCommand(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), this.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), this.Language).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.ParentUid), this.Parent.Uid).Navigate();
    }
  }
}
