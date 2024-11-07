// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.ProfileListViewModel`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public class ProfileListViewModel<TItemViewModel> : 
    BaseListViewModel<TItemViewModel>,
    IProfileListViewModel
    where TItemViewModel : class
  {
    private RelayCommand _clearCommand;

    public RelayCommand ClearCommand
    {
      get
      {
        return this._clearCommand ?? (this._clearCommand = new RelayCommand(new Action<object>(this.ExecuteClearCommand), new Func<object, bool>(this.CanExecuteClearCommand)));
      }
    }

    protected virtual bool CanExecuteClearCommand(object parameter)
    {
      return !this.IsDataLoading && !this.IsListEmpty;
    }

    protected virtual void ExecuteClearCommand(object parameter)
    {
      ShellServiceFacade.DialogService.Show(this.DisplayName, this.GetClearPrompt(), MessageBoxButtonContent.YesNo, (Action<FlyoutDialog, MessageBoxResult>) ((d, x) =>
      {
        if (x != MessageBoxResult.Yes)
          return;
        this.ClearAsync();
      }));
    }

    protected override void OnActivate()
    {
      this.Items.Clear();
      base.OnActivate();
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessStateChanged));
    }

    protected override void OnDeactivate(bool close)
    {
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged -= new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessStateChanged));
      base.OnDeactivate(close);
    }

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is ProfileListItemViewModel listItemViewModel))
        return;
      MtgObject mtgObject = listItemViewModel.MtgObject;
      NavigationHelper.NavigateToDetails(mtgObject.Type, mtgObject.Uid, mtgObject.Language, mtgObject.ParentUid);
    }

    protected override void RefreshCommands()
    {
      base.RefreshCommands();
      this.ClearCommand.RaiseCanExecuteChanged();
    }

    protected virtual string GetClearPrompt() => string.Empty;

    protected virtual Task ClearProcess() => Task.Factory.StartNew((Action) (() => { }));

    protected virtual IEnumerable<ProfileListItemViewModel> GetProfileListItems()
    {
      return this.Items.OfType<ProfileListItemViewModel>();
    }

    private async void ClearAsync()
    {
      try
      {
        this.IsDataLoading = true;
        await this.ClearProcess();
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
      }
      finally
      {
        this.IsDataLoading = false;
        this.RefreshCommand.Execute((object) null);
      }
    }

    private void OnDownloadProcessStateChanged(DownloadManager manager, DownloadProcess process)
    {
      if (process == null || string.IsNullOrWhiteSpace(process.Uid) || string.IsNullOrWhiteSpace(process.Language))
        return;
      ProfileListItemViewModel listItemViewModel = this.GetProfileListItems().FirstOrDefault<ProfileListItemViewModel>((Func<ProfileListItemViewModel, bool>) (x => process.Uid.Equals(x.Uid, StringComparison.InvariantCultureIgnoreCase) && process.Language.Equals(x.Language, StringComparison.InvariantCultureIgnoreCase)));
      if (listItemViewModel == null)
        return;
      listItemViewModel.State = process.State;
      if (listItemViewModel.State != DownloadProcessState.Updated)
        return;
      listItemViewModel.RefreshData(process.MtgObject);
    }
  }
}
