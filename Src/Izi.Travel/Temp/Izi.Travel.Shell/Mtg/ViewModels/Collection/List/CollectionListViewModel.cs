// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Collection.List.CollectionListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player;
using Izi.Travel.Shell.Mtg.Views.Common.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Collection.List
{
  [View(typeof (ChildrenListView))]
  public class CollectionListViewModel : ChildrenListViewModel<CollectionListItemViewModel>
  {
    protected override async Task<IEnumerable<CollectionListItemViewModel>> GetDataAsync()
    {
      int num;
      if (num != 0 && (this.MtgObject == null || this.MtgObject.MainContent == null))
        return (IEnumerable<CollectionListItemViewModel>) null;
      try
      {
        MtgObjectChildrenFilter filter = new MtgObjectChildrenFilter();
        filter.Uid = this.MtgObject.Uid;
        filter.Languages = new string[1]
        {
          this.MtgObject.MainContent.Language
        };
        filter.Types = new MtgObjectType[1]
        {
          MtgObjectType.Collection
        };
        filter.Includes = ContentSection.None;
        filter.Excludes = ContentSection.All;
        filter.ShowHidden = false;
        filter.Form = MtgObjectForm.Compact;
        filter.Limit = new int?(int.MaxValue);
        MtgObject[] objectChildrenAsync = await MtgObjectServiceHelper.GetMtgObjectChildrenAsync(filter);
        return objectChildrenAsync == null || objectChildrenAsync.Length == 0 ? (IEnumerable<CollectionListItemViewModel>) null : (IEnumerable<CollectionListItemViewModel>) ((IEnumerable<MtgObject>) objectChildrenAsync).Select<MtgObject, CollectionListItemViewModel>((Func<MtgObject, CollectionListItemViewModel>) (x => new CollectionListItemViewModel((IListViewModel) this, x))).OrderBy<CollectionListItemViewModel, string>((Func<CollectionListItemViewModel, string>) (x => x.Title));
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        return (IEnumerable<CollectionListItemViewModel>) null;
      }
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is CollectionListItemViewModel listItemViewModel))
        return;
      ShellServiceFacade.NavigationService.UriFor<PlayerPartViewModel>().WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.ParentUid), listItemViewModel.Uid).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Language), listItemViewModel.Language).Navigate();
    }

    protected override string GetItemTitle(CollectionListItemViewModel item) => item.Title;

    protected override bool GetItemIsHidden(CollectionListItemViewModel item) => item.IsHidden;

    protected override void SetItemIsHidden(CollectionListItemViewModel item, bool value)
    {
      item.IsHidden = value;
    }
  }
}
