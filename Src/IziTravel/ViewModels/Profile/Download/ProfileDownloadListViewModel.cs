// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Download.ProfileDownloadListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Download
{
  public class ProfileDownloadListViewModel : BaseListViewModel<ProfileDownloadListItemViewModel>
  {
    private CancellationTokenSource _tokenSourceCheckUpdate;
    private RelayCommand _clearCommand;
    private RelayCommand _updateCommand;

    public override string DisplayName
    {
      get => AppResources.LabelDownloads;
      set => throw new NotImplementedException();
    }

    public RelayCommand ClearCommand
    {
      get
      {
        return this._clearCommand ?? (this._clearCommand = new RelayCommand(new Action<object>(this.ExecuteClearCommand), new Func<object, bool>(this.CanExecuteClearCommand)));
      }
    }

    private bool CanExecuteClearCommand(object parameter)
    {
      return !this.IsDataLoading && this.Items.Count > 0 && this.Items.Any<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (x => x.DeleteCommand.CanExecute((object) null)));
    }

    private void ExecuteClearCommand(object parameter)
    {
      ShellServiceFacade.DialogService.Show(AppResources.PromptDownloadDeleteAllTitle, string.Format(AppResources.PromptDownloadDeleteAllInfo, (object) Math.Max(this.Items.Where<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (x => x.State != DownloadProcessState.Removing)).Sum<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, int>) (x => x.MtgObject.SizeInMegabytes)), 1)), MessageBoxButtonContent.YesNo, (Action<FlyoutDialog>) (d =>
      {
        d.LeftButtonContent = (object) AppResources.CommandDeleteAll;
        d.RightButtonContent = (object) AppResources.CommandCancel;
      }), (Action<FlyoutDialog, MessageBoxResult>) ((d, e) =>
      {
        if (e != MessageBoxResult.Yes)
          return;
        foreach (ProfileDownloadListItemViewModel listItemViewModel in (Collection<ProfileDownloadListItemViewModel>) this.Items)
        {
          if (listItemViewModel.DeleteCommand.CanExecute((object) null))
            listItemViewModel.DeleteCommand.Execute((object) null);
        }
      }));
    }

    public RelayCommand UpdateCommand
    {
      get
      {
        return this._updateCommand ?? (this._updateCommand = new RelayCommand(new Action<object>(this.ExecuteUpdateCommand), new Func<object, bool>(this.CanExecuteUpdateCommand)));
      }
    }

    private bool CanExecuteUpdateCommand(object parameter)
    {
      return this.Items.Count > 0 && this.Items.Any<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (x => x.UpdateCommand.CanExecute((object) null)));
    }

    private void ExecuteUpdateCommand(object parameter)
    {
      ProfileDownloadListItemViewModel[] updateItems = this.Items.Where<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (x => x.IsUpdateAvailable)).ToArray<ProfileDownloadListItemViewModel>();
      ShellServiceFacade.DialogService.Show(AppResources.CommandUpdateAll, string.Format(AppResources.PromptDownloadUpdateAllInfo, (object) Math.Max(((IEnumerable<ProfileDownloadListItemViewModel>) updateItems).Sum<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, int>) (x => x.MtgObject.SizeInMegabytes)), 1)), MessageBoxButtonContent.YesNo, (Action<FlyoutDialog>) null, (Action<FlyoutDialog, MessageBoxResult>) ((d, e) =>
      {
        if (e != MessageBoxResult.Yes)
          return;
        foreach (ProfileDownloadListItemViewModel listItemViewModel in ((IEnumerable<ProfileDownloadListItemViewModel>) updateItems).Where<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (item => item.UpdateCommand.CanExecute((object) null))))
          listItemViewModel.UpdateCommand.Execute((object) null);
      }));
    }

    protected override void OnActivate()
    {
      this.Items.Clear();
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessStateChanged));
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessProgressChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessProgressChanged));
      this._tokenSourceCheckUpdate = new CancellationTokenSource();
      base.OnActivate();
    }

    protected override void OnDeactivate(bool close)
    {
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged -= new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessStateChanged));
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessProgressChanged -= new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessProgressChanged));
      if (this._tokenSourceCheckUpdate != null)
        this._tokenSourceCheckUpdate.Cancel();
      base.OnDeactivate(close);
    }

    protected override async Task<IEnumerable<ProfileDownloadListItemViewModel>> GetDataAsync()
    {
      System.Collections.Generic.List<ProfileDownloadListItemViewModel> result = new System.Collections.Generic.List<ProfileDownloadListItemViewModel>();
      IMtgObjectDownloadService objectDownloadService = ServiceFacade.MtgObjectDownloadService;
      MtgObjectListFilter objectListFilter = new MtgObjectListFilter();
      objectListFilter.Includes = ContentSection.None;
      objectListFilter.Excludes = ContentSection.All;
      objectListFilter.Form = MtgObjectForm.Compact;
      objectListFilter.Languages = ServiceFacade.CultureService.GetNeutralLanguageCodes();
      objectListFilter.Types = new MtgObjectType[2]
      {
        MtgObjectType.Tour,
        MtgObjectType.Museum
      };
      MtgObjectListFilter filter = objectListFilter;
      MtgObject[] mtgObjectListAsync = await objectDownloadService.GetMtgObjectListAsync(filter);
      if (mtgObjectListAsync != null)
        result.AddRange(((IEnumerable<MtgObject>) mtgObjectListAsync).Select<MtgObject, ProfileDownloadListItemViewModel>((Func<MtgObject, ProfileDownloadListItemViewModel>) (x => new ProfileDownloadListItemViewModel((IListViewModel) this, x))));
      result.AddRange(((IEnumerable<DownloadProcess>) DownloadManager.Instance.GetDownloadProcessList()).Where<DownloadProcess>((Func<DownloadProcess, bool>) (x => !result.Any<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (r => r.MtgObject.Uid.Equals(x.Uid, StringComparison.InvariantCultureIgnoreCase) && r.MtgObject.Language.Equals(x.Language, StringComparison.InvariantCultureIgnoreCase))))).Select<DownloadProcess, ProfileDownloadListItemViewModel>((Func<DownloadProcess, ProfileDownloadListItemViewModel>) (process => new ProfileDownloadListItemViewModel((IListViewModel) this, process.MtgObject))));
      return (IEnumerable<ProfileDownloadListItemViewModel>) result.OrderBy<ProfileDownloadListItemViewModel, string>((Func<ProfileDownloadListItemViewModel, string>) (x => x.Title));
    }

    protected override void OnLoadDataCompleted()
    {
      base.OnLoadDataCompleted();
      this.CheckUpdateAsync(this._tokenSourceCheckUpdate.Token);
    }

    protected override bool CanExecuteLoadDataCommand(object parameter) => false;

    protected override void ExecuteNavigateCommand(object parameter)
    {
      ProfileDownloadListItemViewModel listItemViewModel = parameter as ProfileDownloadListItemViewModel;
      if (listItemViewModel == null)
        return;
      if (listItemViewModel.IsError)
        ShellServiceFacade.DialogService.Show(AppResources.PromptDownloadErrorTitle, AppResources.PromptDownloadErrorCommon, MessageBoxButtonContent.OkCancel, (Action<FlyoutDialog>) (x => x.LeftButtonContent = (object) AppResources.CommandRetry), (Action<FlyoutDialog, MessageBoxResult>) ((x, e) =>
        {
          if (e != MessageBoxResult.OK)
            return;
          DownloadManager.Instance.DownloadAsync(listItemViewModel.MtgObject);
        }));
      else
        ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), listItemViewModel.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), listItemViewModel.Language).Navigate();
    }

    protected override void RefreshCommands()
    {
      base.RefreshCommands();
      this.UpdateCommand.RaiseCanExecuteChanged();
      this.ClearCommand.RaiseCanExecuteChanged();
    }

    private ProfileDownloadListItemViewModel GetListItemViewModel(DownloadProcess downloadProcess)
    {
      return downloadProcess == null || string.IsNullOrWhiteSpace(downloadProcess.Uid) || string.IsNullOrWhiteSpace(downloadProcess.Language) ? (ProfileDownloadListItemViewModel) null : this.Items.FirstOrDefault<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (x => x.Uid == downloadProcess.Uid && x.Language == downloadProcess.Language));
    }

    private async void CheckUpdateAsync(CancellationToken token)
    {
      if (token.IsCancellationRequested)
        return;
      try
      {
        ProfileDownloadListItemViewModel[] listItemViewModelArray = this.Items.Where<ProfileDownloadListItemViewModel>((Func<ProfileDownloadListItemViewModel, bool>) (x => !x.IsUpdateAvailable && !x.IsRunning && x.State == DownloadProcessState.Downloaded)).ToArray<ProfileDownloadListItemViewModel>();
        for (int index = 0; index < listItemViewModelArray.Length; ++index)
        {
          ProfileDownloadListItemViewModel listItemViewModel1 = listItemViewModelArray[index];
          if (token.IsCancellationRequested)
            return;
          ProfileDownloadListItemViewModel listItemViewModel = listItemViewModel1;
          int num = await DownloadManager.Instance.CheckUpdateAsync(listItemViewModel1.MtgObject) ? 1 : 0;
          listItemViewModel.IsUpdateAvailable = num != 0;
          listItemViewModel = (ProfileDownloadListItemViewModel) null;
        }
        listItemViewModelArray = (ProfileDownloadListItemViewModel[]) null;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
      }
      finally
      {
        this.RefreshCommands();
      }
    }

    private void OnDownloadProcessStateChanged(DownloadManager manager, DownloadProcess process)
    {
      ProfileDownloadListItemViewModel listItemViewModel = this.GetListItemViewModel(process);
      if (listItemViewModel == null)
        return;
      if (process.State == DownloadProcessState.Removed)
      {
        this.Items.Remove(listItemViewModel);
        this.IsListEmpty = this.Items.Count == 0;
      }
      else
      {
        listItemViewModel.Progress = process.State != DownloadProcessState.Removing ? process.Progress : 0.0;
        if (process.State == DownloadProcessState.Error && process.Error == DownloadProcessError.ProcessCanceled)
          listItemViewModel.State = DownloadProcessState.Removing;
        else
          listItemViewModel.State = process.State;
        if (listItemViewModel.State == DownloadProcessState.Updated)
          listItemViewModel.RefreshData(process.MtgObject);
      }
      new System.Action(((BaseListViewModel<ProfileDownloadListItemViewModel>) this).RefreshCommands).OnUIThread();
    }

    private void OnDownloadProcessProgressChanged(DownloadManager manager, DownloadProcess process)
    {
      ProfileDownloadListItemViewModel listItemViewModel = this.GetListItemViewModel(process);
      if (listItemViewModel == null)
        return;
      listItemViewModel.Progress = process.State != DownloadProcessState.Removing ? process.Progress : 0.0;
    }
  }
}
