// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.Purchase.ProfilePurchaseListViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.ViewModels.Profile.Purchase
{
  public class ProfilePurchaseListViewModel : BaseListViewModel<ProfilePurchaseListItemViewModel>
  {
    private RelayCommand _restoreCommand;

    public override string DisplayName
    {
      get => AppResources.LabelPurchases;
      set => throw new NotImplementedException();
    }

    public bool CanRestorePurchases => PurchaseManager.Instance.CanRestorePurchases;

    public RelayCommand RestoreCommand
    {
      get
      {
        return this._restoreCommand ?? (this._restoreCommand = new RelayCommand(new Action<object>(this.Restore)));
      }
    }

    private async void Restore(object o)
    {
      this.IsDataLoading = true;
      await PurchaseManager.Instance.RestorePurchases();
      this.IsDataLoading = false;
      if (this.RefreshCommand.CanExecute((object) null))
        this.RefreshCommand.Execute((object) null);
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.CanRestorePurchases));
    }

    protected override void OnActivate()
    {
      this.Items.Clear();
      base.OnActivate();
    }

    protected override async Task<IEnumerable<ProfilePurchaseListItemViewModel>> GetDataAsync()
    {
      return (IEnumerable<ProfilePurchaseListItemViewModel>) ((IEnumerable<MtgObject>) await ServiceFacade.MtgObjectService.GetPurchaseListAsync(new ListFilter()
      {
        Limit = new int?(25),
        Offset = new int?(this.Items.Count)
      })).Select<MtgObject, ProfilePurchaseListItemViewModel>((Func<MtgObject, ProfilePurchaseListItemViewModel>) (x => new ProfilePurchaseListItemViewModel((IListViewModel) this, x))).ToList<ProfilePurchaseListItemViewModel>();
    }

    protected override bool CanExecuteLoadDataCommand(object parameter) => false;

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is ProfilePurchaseListItemViewModel listItemViewModel))
        return;
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), listItemViewModel.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), listItemViewModel.Language).Navigate();
    }
  }
}
