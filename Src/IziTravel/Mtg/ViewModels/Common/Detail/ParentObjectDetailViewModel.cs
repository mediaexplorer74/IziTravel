// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.ParentObjectDetailViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Commands;
using Izi.Travel.Shell.Mtg.Components.Enums;
using Izi.Travel.Shell.Mtg.Components.Tasks;
using Izi.Travel.Shell.Mtg.Messages;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public abstract class ParentObjectDetailViewModel : 
    DetailViewModel,
    IHandle<RefreshCommandMessage>,
    IHandle
  {
    private double _downloadProgress;
    private bool _isDownloadRunning;
    private bool _isDownloadUpdate;
    private bool _isDownloadRemoving;
    private bool _hasDownload;
    private bool _isUpdateChecked;
    private RelayCommand _toggleDownloadCommand;
    private RelayCommand _showNumpadCommand;

    public double DownloadProgress
    {
      get => this._downloadProgress;
      private set
      {
        if (Math.Abs(this._downloadProgress - value) <= double.Epsilon)
          return;
        this._downloadProgress = value;
        this.NotifyOfPropertyChange(nameof (DownloadProgress));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.HasDownloadProgress));
      }
    }

    public bool HasDownloadProgress => this.DownloadProgress > 0.0;

    public bool IsDownloadUpdate
    {
      get => this._isDownloadUpdate;
      set
      {
        this.SetProperty<bool>(ref this._isDownloadUpdate, value, propertyName: nameof (IsDownloadUpdate));
      }
    }

    public bool IsDownloadRunning
    {
      get => this._isDownloadRunning;
      set
      {
        if (this._isDownloadRunning == value)
          return;
        this._isDownloadRunning = value;
        this.NotifyOfPropertyChange(nameof (IsDownloadRunning));
      }
    }

    public bool IsDownloadRemoving
    {
      get => this._isDownloadRemoving;
      set
      {
        if (this._isDownloadRemoving == value)
          return;
        this._isDownloadRemoving = value;
        this.NotifyOfPropertyChange(nameof (IsDownloadRemoving));
        this.ToggleDownloadCommand.RaiseCanExecuteChanged();
      }
    }

    public bool HasDownload
    {
      get => this._hasDownload;
      set
      {
        if (this._hasDownload == value)
          return;
        this._hasDownload = value;
        this.NotifyOfPropertyChange(nameof (HasDownload));
      }
    }

    public RelayCommand ToggleDownloadCommand
    {
      get
      {
        return this._toggleDownloadCommand ?? (this._toggleDownloadCommand = new RelayCommand(new Action<object>(this.ExecuteToggleDownloadCommand), new Func<object, bool>(this.CanExecuteToggleDownloadCommand)));
      }
    }

    protected virtual bool CanExecuteToggleDownloadCommand(object parameter)
    {
      return !this.DetailPartViewModel.IsDataLoading && this.MtgObject != null && !this.IsDownloadRemoving;
    }

    protected virtual void ExecuteToggleDownloadCommand(object parameter)
    {
      if (this.HasDownload)
      {
        ShellServiceFacade.DialogService.Show(AppResources.PromptDownloadRemoveTitle, AppResources.PromptDownloadRemove, MessageBoxButtonContent.YesNo, (Action<FlyoutDialog, MessageBoxResult>) ((d, x) =>
        {
          if (x != MessageBoxResult.Yes)
            return;
          try
          {
            DownloadManager.Instance.RemoveAsync(this.MtgObject);
            if (this.MtgObject.AccessType != MtgObjectAccessType.Offline || !ShellServiceFacade.NavigationService.CanGoBack)
              return;
            ShellServiceFacade.NavigationService.GoBack();
          }
          catch (Exception ex)
          {
            this.Logger.Error(ex);
          }
        }));
      }
      else
      {
        if (!PurchaseFlyoutDialog.ConditionalShow(this.MtgObject))
          return;
        ShellServiceFacade.DialogService.Show(AppResources.LabelDownload, string.Format(AppResources.PromptDownloadStart, (object) Math.Max(this.MtgObject.SizeInMegabytes, 1)), MessageBoxButtonContent.YesNo, (Action<FlyoutDialog>) (d =>
        {
          d.IsHyperlinkVisible = true;
          d.HyperlinkContent = (object) AppResources.MessageGoToMapDownload;
          d.HyperlinkAction = (System.Action) (() =>
          {
            try
            {
              new MapDownloaderTask().Show();
            }
            catch (Exception ex)
            {
              this.Logger.Error(ex);
            }
          });
        }), (Action<FlyoutDialog, MessageBoxResult>) ((d, x) =>
        {
          if (x != MessageBoxResult.Yes)
            return;
          try
          {
            DownloadManager.Instance.DownloadAsync(this.MtgObject);
          }
          catch (Exception ex)
          {
            this.Logger.Error(ex);
            ShellServiceFacade.DialogService.Show(AppResources.LabelDownload, AppResources.ErrorDownload, MessageBoxButtonContent.Ok, (Action<FlyoutDialog>) null, (Action<FlyoutDialog, MessageBoxResult>) null);
          }
        }));
      }
    }

    public RelayCommand ShowNumpadCommand
    {
      get
      {
        return this._showNumpadCommand ?? (this._showNumpadCommand = new RelayCommand(new Action<object>(this.ShowNumpad)));
      }
    }

    private void ShowNumpad(object parameter)
    {
      if (this.MtgObject == null)
        return;
      NumpadSearchTask numpadSearchTask = new NumpadSearchTask();
      numpadSearchTask.ParentScreen = (IScreen) this;
      numpadSearchTask.ParentUid = this.MtgObject.Uid;
      numpadSearchTask.ParentType = this.MtgObject.Type;
      numpadSearchTask.ParentLanguage = this.MtgObject.Language;
      numpadSearchTask.ActivationMode = FlyoutSearchActivationMode.None;
      numpadSearchTask.NavigationMode = FlyoutSearchNavigationMode.Player;
      numpadSearchTask.CloseMode = FlyoutSearchCloseMode.Silent;
      numpadSearchTask.Show();
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      Tuple<DownloadProcessState, double> objectDownloadInfo = DownloadManager.Instance.GetMtgObjectDownloadInfo(this.MtgObject);
      this.RefreshDownloadInfo(objectDownloadInfo.Item1, objectDownloadInfo.Item2);
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessStateChanged));
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessProgressChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessProgressChanged));
      this.CheckUpdateAsync();
    }

    protected override void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged -= new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessStateChanged));
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessProgressChanged -= new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadProcessProgressChanged));
    }

    protected override void RefreshCommands()
    {
      base.RefreshCommands();
      this.ToggleDownloadCommand.RaiseCanExecuteChanged();
      this.ShowNumpadCommand.RaiseCanExecuteChanged();
    }

    protected override IEnumerable<ButtonInfo> GetAvailableAppBarButtons()
    {
      ButtonInfo[] second = new ButtonInfo[3]
      {
        new ButtonInfo()
        {
          Order = 6,
          Key = "Download",
          Text = AppResources.CommandDownload,
          AlternativeText = AppResources.CommandDelete,
          ImageUrl = "/Assets/Icons/appbar.download.png",
          AlternativeImageUrl = "/Assets/Icons/appbar.delete.png",
          Command = (ICommand) this.ToggleDownloadCommand
        },
        new ButtonInfo()
        {
          Order = 25,
          Key = "Numpad",
          Text = AppResources.LabelNumpad,
          ImageUrl = "/Assets/Icons/appbar.numpad.png",
          Command = (ICommand) this.ShowNumpadCommand
        },
        new ButtonInfo()
        {
          Order = 30,
          Key = "QrCode",
          Text = AppResources.LabelQrScanner,
          ImageUrl = "/Assets/Icons/appbar.qrcode.png",
          Command = (ICommand) new OpenQrCodeScannerCommand((IScreen) this, this.MtgObject.Uid, this.MtgObject.Language, this.MtgObject.Type)
        }
      };
      IEnumerable<ButtonInfo> availableAppBarButtons = base.GetAvailableAppBarButtons();
      return availableAppBarButtons == null ? (IEnumerable<ButtonInfo>) second : availableAppBarButtons.Union<ButtonInfo>((IEnumerable<ButtonInfo>) second);
    }

    protected override IEnumerable<MenuItemInfo> GetAvailableAppBarMenuItems()
    {
      MenuItemInfo[] second = new MenuItemInfo[1]
      {
        new MenuItemInfo()
        {
          Order = 5,
          Key = "QrCode",
          Text = AppResources.LabelQrScanner,
          Command = (ICommand) new OpenQrCodeScannerCommand((IScreen) this, this.MtgObject.Uid, this.MtgObject.Language, this.MtgObject.Type)
        }
      };
      IEnumerable<MenuItemInfo> availableAppBarMenuItems = base.GetAvailableAppBarMenuItems();
      return availableAppBarMenuItems == null ? (IEnumerable<MenuItemInfo>) second : availableAppBarMenuItems.Union<MenuItemInfo>((IEnumerable<MenuItemInfo>) second);
    }

    public void Handle(RefreshCommandMessage message)
    {
      if (message != RefreshCommandMessage.RefreshNumpadCommandMessage)
        return;
      this.ShowNumpadCommand.RaiseCanExecuteChanged();
    }

    private async void CheckUpdateAsync()
    {
      if (this._isUpdateChecked || this.MtgObject == null)
        return;
      List<string> suspendedUpdates = PhoneStateHelper.GetParameter<List<string>>("UpdateSuspendList") ?? new List<string>();
      if (suspendedUpdates.Contains(this.MtgObject.Key))
        return;
      this._isUpdateChecked = true;
      if (!await DownloadManager.Instance.CheckUpdateAsync(this.MtgObject) || !this.IsActive)
        return;
      ShellServiceFacade.DialogService.Show(AppResources.PromptDownloadUpdateStartTitle, string.Format(AppResources.PromptDownloadUpdateStartInfo, (object) Math.Max(this.MtgObject.SizeInMegabytes, 1)), MessageBoxButtonContent.YesNo, (Action<FlyoutDialog>) (d =>
      {
        d.LeftButtonContent = (object) AppResources.CommandUpdate;
        d.RightButtonContent = (object) AppResources.LabelLater;
      }), (Action<FlyoutDialog, MessageBoxResult>) ((d, e) =>
      {
        if (e == MessageBoxResult.Yes)
        {
          DownloadManager.Instance.DownloadAsync(this.MtgObject);
        }
        else
        {
          if (e != MessageBoxResult.No)
            return;
          suspendedUpdates.Add(this.MtgObject.Key);
          PhoneStateHelper.SetParameter<List<string>>("UpdateSuspendList", suspendedUpdates);
        }
      }));
    }

    private void RefreshDownloadInfo(DownloadProcessState state, double progress)
    {
      this.HasDownload = state == DownloadProcessState.Downloaded || state == DownloadProcessState.Downloading || state == DownloadProcessState.Updated || state == DownloadProcessState.Updating;
      this.IsDownloadRunning = state == DownloadProcessState.Downloading || state == DownloadProcessState.Updating || state == DownloadProcessState.Removing;
      this.IsDownloadUpdate = state == DownloadProcessState.Updated || state == DownloadProcessState.Updating;
      this.IsDownloadRemoving = state == DownloadProcessState.Removing;
      this.DownloadProgress = progress;
      ButtonInfo buttonInfo = this.AvailableAppBarButtons.FirstOrDefault<ButtonInfo>((Func<ButtonInfo, bool>) (x => x.Key == "Download"));
      if (buttonInfo != null)
        buttonInfo.ShowAlternative = this.HasDownload;
      this.ToggleDownloadCommand.RaiseCanExecuteChanged();
    }

    private void OnDownloadProcessStateChanged(DownloadManager manager, DownloadProcess process)
    {
      ((System.Action) (() =>
      {
        if (!manager.CheckMtgObjectDownloadProcess(this.MtgObject, process))
          return;
        if (process.State == DownloadProcessState.Error && process.Error != DownloadProcessError.ProcessCanceled)
        {
          this.RefreshDownloadInfo(process.State, process.Progress);
          ShellServiceFacade.DialogService.Show(AppResources.PromptDownloadErrorTitle, AppResources.PromptDownloadErrorCommon, MessageBoxButtonContent.OkCancel, (Action<FlyoutDialog>) (x => x.LeftButtonContent = (object) AppResources.CommandRetry), (Action<FlyoutDialog, MessageBoxResult>) ((x, e) =>
          {
            if (e != MessageBoxResult.OK)
              return;
            DownloadManager.Instance.DownloadAsync(this.MtgObject);
          }));
        }
        else
        {
          if (this.MtgObject != null && this.MtgObject.AccessType == MtgObjectAccessType.Offline && (process.State == DownloadProcessState.Removing || process.State == DownloadProcessState.Removed))
            this.MtgObject.AccessType = MtgObjectAccessType.Online;
          if ((process.State == DownloadProcessState.Downloaded || process.State == DownloadProcessState.Updated) && this.DetailPartViewModel != null && this.DetailPartViewModel.RefreshCommand.CanExecute((object) null))
            Deployment.Current.Dispatcher.BeginInvoke((System.Action) (() => this.DetailPartViewModel.RefreshCommand.Execute((object) null)));
          else
            this.RefreshDownloadInfo(process.State, process.Progress);
        }
      })).OnUIThread();
    }

    private void OnDownloadProcessProgressChanged(DownloadManager manager, DownloadProcess process)
    {
      if (!manager.CheckMtgObjectDownloadProcess(this.MtgObject, process))
        return;
      ((System.Action) (() => this.RefreshDownloadInfo(process.State, process.Progress))).OnUIThread();
    }
  }
}
