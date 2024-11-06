// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Bookmark.ProfileBookmarkListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Bookmark
{
  public class ProfileBookmarkListViewModel : ProfileListViewModel<ProfileBookmarkListItemViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelBookmarks;
      set => throw new NotImplementedException();
    }

    protected override string GetClearPrompt() => AppResources.PromptClearBookmarks;

    protected override Task ClearProcess()
    {
      return ServiceFacade.MtgObjectService.ClearBookmarkListAsync();
    }

    protected override bool CanExecuteDeleteItemCommand(object parameter)
    {
      return !this.IsDataLoading && parameter != null;
    }

    protected override async void ExecuteDeleteItemCommand(object parameter)
    {
      ProfileBookmarkListItemViewModel listItemViewModel = parameter as ProfileBookmarkListItemViewModel;
      if (listItemViewModel == null)
        return;
      await ServiceFacade.MtgObjectService.RemoveBookmarkAsync(new MtgObjectFilter(listItemViewModel.Uid, new string[1]
      {
        listItemViewModel.Language
      }));
      Deployment.Current.Dispatcher.BeginInvoke((Action) (() =>
      {
        this.Items.Remove(listItemViewModel);
        this.OnLoadDataCompleted();
      }));
    }

    protected override async Task<IEnumerable<ProfileBookmarkListItemViewModel>> GetDataAsync()
    {
      MtgObjectListFilter filter = new MtgObjectListFilter();
      filter.Types = new MtgObjectType[5]
      {
        MtgObjectType.Tour,
        MtgObjectType.TouristAttraction,
        MtgObjectType.Museum,
        MtgObjectType.Collection,
        MtgObjectType.Exhibit
      };
      filter.Limit = new int?(25);
      filter.Offset = new int?(this.Items.Count);
      MtgObject[] bookmarkListAsync = await ServiceFacade.MtgObjectService.GetBookmarkListAsync(filter);
      return bookmarkListAsync == null || bookmarkListAsync.Length == 0 ? (IEnumerable<ProfileBookmarkListItemViewModel>) null : ((IEnumerable<MtgObject>) bookmarkListAsync).Select<MtgObject, ProfileBookmarkListItemViewModel>((Func<MtgObject, ProfileBookmarkListItemViewModel>) (x => new ProfileBookmarkListItemViewModel((IListViewModel) this, x)));
    }
  }
}
