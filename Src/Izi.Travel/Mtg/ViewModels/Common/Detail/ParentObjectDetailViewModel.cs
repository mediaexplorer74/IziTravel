// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.ParentObjectDetailViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Managers;
using Izi.Travel.Common.Controls;
using Izi.Travel.Common.Model;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Controls.Flyout;
using Izi.Travel.Core.Extensions;
using Izi.Travel.Core.Helpers;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Core.Services.Entities;
using Izi.Travel.Mtg.Commands;
using Izi.Travel.Mtg.Components.Enums;
using Izi.Travel.Mtg.Components.Tasks;
using Izi.Travel.Mtg.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail
{
  public abstract class ParentObjectDetailViewModel : 
    DetailViewModel,
    IHandle<RefreshCommandMessage>,
    IHandle,
    INotifyPropertyChanged
  {
    private double _downloadProgress;
    private bool _isDownloadRunning;
    private bool _isDownloadUpdate;
    private bool _isDownloadRemoving;
    private bool _hasDownload;
    private bool _isUpdateChecked;
    private RelayCommand _toggleDownloadCommand;
    private RelayCommand _showNumpadCommand;
    private readonly CoreDispatcher _dispatcher;

    protected ParentObjectDetailViewModel()
    {
        _dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        // Initialize any properties or commands here
    }

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

        private RelayCommand ToggleDownloadCommand =>
      _toggleDownloadCommand ??= new RelayCommand(
          async (param) => await ToggleDownloadAsync(),
          (param) => !IsDownloadRunning && !IsDownloadRemoving);

        private async Task ToggleDownloadAsync()
    {
        try
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                try
                {
                    IsDownloadRunning = true;
                    
                    // Your existing download logic here
                    // Example:
                    // if (HasDownload)
                    // {
                    //     await RemoveDownloadAsync();
                    // }
                    // else
                    // {
                    //     await StartDownloadAsync();
                    // }
                    
                    // Notify UI of changes
                    NotifyOfPropertyChange(nameof(HasDownload));
                    NotifyOfPropertyChange(nameof(IsDownloadRunning));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error in ToggleDownload: {ex.Message}");
                    // Consider showing an error message to the user
                }
                finally
                {
                    IsDownloadRunning = false;
                }
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in ToggleDownload (dispatcher): {ex.Message}");
        }
    }

        public RelayCommand ShowNumpadCommand =>
         _showNumpadCommand ??= new RelayCommand(async (param) => await ShowNumpadAsync());

        private async Task ShowNumpadAsync()
    {
        try
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                try
                {
                    // Your existing numpad logic here
                    // Example:
                    // var numpadDialog = new NumpadDialog();
                    // var result = await numpadDialog.ShowAsync();
                    // if (result == ContentDialogResult.Primary)
                    // {
                    //     // Handle the result
                    // }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error in ShowNumpad: {ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in ShowNumpad (dispatcher): {ex.Message}");
        }
    }

    protected virtual bool CanExecuteToggleDownloadCommand(object parameter)
    {
      return !this.DetailPartViewModel.IsDataLoading && this.MtgObject != null && !this.IsDownloadRemoving;
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      Tuple<DownloadProcessState, double> objectDownloadInfo = DownloadManager.Instance.GetMtgObjectDownloadInfo(this.MtgObject);
      this.RefreshDownloadInfo(objectDownloadInfo.Item1, objectDownloadInfo.Item2);
      DownloadManager.Instance.DownloadProcessStateChanged += OnDownloadProcessStateChanged;
      DownloadManager.Instance.DownloadProcessProgressChanged += OnDownloadProcessProgressChanged;
      this.CheckUpdateAsync();
    }

    protected override void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      DownloadManager.Instance.DownloadProcessStateChanged -= OnDownloadProcessStateChanged;
      DownloadManager.Instance.DownloadProcessProgressChanged -= OnDownloadProcessProgressChanged;
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
            Caliburn.Micro.Execute.OnUIThread(() => this.DetailPartViewModel.RefreshCommand.Execute((object) null));
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

