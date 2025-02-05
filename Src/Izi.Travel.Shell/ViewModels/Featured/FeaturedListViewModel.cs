// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Featured.FeaturedListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Featured
{
  public class FeaturedListViewModel : BaseListViewModel<FeaturedListItemViewModel>
  {
    public bool IsHeaderVisible { get; set; }

    public bool IsFooterVisible { get; set; }

    public RelayCommand ExploreCommand { get; set; }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.LoadDataAsync();
    }

    protected override void OnActivate()
    {
    }

    protected override async Task<IEnumerable<FeaturedListItemViewModel>> GetDataAsync()
    {
      MtgObject[] featuredListAsync = await ServiceFacade.MtgObjectService.GetFeaturedListAsync(ServiceFacade.SettingsService.GetAppSettings().Languages);
      return featuredListAsync != null ? ((IEnumerable<MtgObject>) featuredListAsync).Select<MtgObject, FeaturedListItemViewModel>((Func<MtgObject, FeaturedListItemViewModel>) (x => new FeaturedListItemViewModel((IListViewModel) this, x))) : (IEnumerable<FeaturedListItemViewModel>) null;
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is FeaturedListItemViewModel listItemViewModel) || listItemViewModel.MtgObject == null)
        return;
      NavigationHelper.NavigateToDetails(listItemViewModel.MtgObject.Type, listItemViewModel.MtgObject.Uid, listItemViewModel.MtgObject.Language, (string) null);
    }
  }
}
