// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Exhibit.List.ExhibitListViewModel
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
using Izi.Travel.Shell.Mtg.Messages;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player;
using Izi.Travel.Shell.Mtg.Views.Common.List;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Exhibit.List
{
  [View(typeof (ChildrenListView))]
  public class ExhibitListViewModel : ChildrenListViewModel<ExhibitListItemViewModel>
  {
    protected override async Task<IEnumerable<ExhibitListItemViewModel>> GetDataAsync()
    {
      int num;
      if (num != 0 && (this.MtgObject == null || this.MtgObject.MainContent == null))
        return (IEnumerable<ExhibitListItemViewModel>) null;
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
          MtgObjectType.Exhibit
        };
        filter.Includes = ContentSection.None;
        filter.Excludes = ContentSection.All;
        filter.ShowHidden = false;
        filter.Form = MtgObjectForm.Compact;
        filter.Limit = new int?(int.MaxValue);
        MtgObject[] objectChildrenAsync = await MtgObjectServiceHelper.GetMtgObjectChildrenAsync(filter);
        return objectChildrenAsync == null || objectChildrenAsync.Length == 0 ? (IEnumerable<ExhibitListItemViewModel>) null : (IEnumerable<ExhibitListItemViewModel>) ((IEnumerable<MtgObject>) objectChildrenAsync).Select<MtgObject, ExhibitListItemViewModel>((Func<MtgObject, ExhibitListItemViewModel>) (x => new ExhibitListItemViewModel((IListViewModel) this, x))).OrderBy<ExhibitListItemViewModel, string>((Func<ExhibitListItemViewModel, string>) (x => x.Number), (IComparer<string>) new StringNumberComparer());
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        return (IEnumerable<ExhibitListItemViewModel>) null;
      }
    }

    protected override void OnLoadDataCompleted()
    {
      base.OnLoadDataCompleted();
      IoC.Get<IEventAggregator>().PublishOnUIThread((object) RefreshCommandMessage.RefreshNumpadCommandMessage);
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is ExhibitListItemViewModel listItemViewModel))
        return;
      ShellServiceFacade.NavigationService.UriFor<PlayerPartViewModel>().WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Uid), listItemViewModel.Uid).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.Language), this.MtgObject.Language).WithParam<string>((Expression<Func<PlayerPartViewModel, string>>) (x => x.ParentUid), this.MtgObject.Uid).Navigate();
    }

    protected override string GetItemTitle(ExhibitListItemViewModel item) => item.FullTitle;

    protected override bool GetItemIsHidden(ExhibitListItemViewModel item) => item.IsHidden;

    protected override void SetItemIsHidden(ExhibitListItemViewModel item, bool value)
    {
      item.IsHidden = value;
    }
  }
}
