// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail.PublisherDetailContentListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail
{
  public class PublisherDetailContentListViewModel : 
    BaseListViewModel<PublisherDetailContentListItemViewModel>
  {
    protected IMtgObjectProvider MtgObjectProvider => this.Parent as IMtgObjectProvider;

    protected MtgObject MtgObject
    {
      get => this.MtgObjectProvider == null ? (MtgObject) null : this.MtgObjectProvider.MtgObject;
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is PublisherDetailContentListItemViewModel listItemViewModel))
        return;
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), listItemViewModel.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), listItemViewModel.Language).Navigate();
    }

    protected override async Task<IEnumerable<PublisherDetailContentListItemViewModel>> GetDataAsync()
    {
      try
      {
        MtgPublisherChildrenFilter filter = new MtgPublisherChildrenFilter();
        filter.Uid = this.MtgObject.Uid;
        filter.Languages = ServiceFacade.SettingsService.GetAppSettings().Languages;
        filter.Includes = ContentSection.None;
        filter.Excludes = ContentSection.All;
        filter.Form = MtgObjectForm.Compact;
        filter.Limit = new int?(10);
        filter.Offset = new int?(this.ItemsCount);
        MtgObject[] publisherChildrenAsync = await ServiceFacade.MtgObjectService.GetPublisherChildrenAsync(filter);
        return publisherChildrenAsync == null || publisherChildrenAsync.Length == 0 ? (IEnumerable<PublisherDetailContentListItemViewModel>) null : ((IEnumerable<MtgObject>) publisherChildrenAsync).Select<MtgObject, PublisherDetailContentListItemViewModel>((Func<MtgObject, PublisherDetailContentListItemViewModel>) (x => new PublisherDetailContentListItemViewModel((IListViewModel) this, x)));
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        return (IEnumerable<PublisherDetailContentListItemViewModel>) null;
      }
    }
  }
}
