// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Download.ProfileDownloadListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Download
{
  public class ProfileDownloadListItemViewModel : ProfileListItemViewModel
  {
    private bool _isUpdateAvailable;
    private RelayCommand _deleteCommand;
    private RelayCommand _updateCommand;

    public bool IsUpdateAvailable
    {
      get => this._isUpdateAvailable;
      set
      {
        this.SetProperty<bool>(ref this._isUpdateAvailable, value, (Action) (() =>
        {
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsUpdating));
          this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.StateDownloadedString));
          this.UpdateCommand.RaiseCanExecuteChanged();
        }), nameof (IsUpdateAvailable));
      }
    }

    public override string StateDownloadedString
    {
      get
      {
        return !this.IsUpdateAvailable ? DownloadHelper.GetDownloadStateString(this.State) : AppResources.LabelUpdate;
      }
    }

    public override bool IsUpdating => base.IsUpdating || this.IsUpdateAvailable;

    public bool IsRunning
    {
      get
      {
        return this.State == DownloadProcessState.Downloading || this.State == DownloadProcessState.Updating || this.State == DownloadProcessState.Removing;
      }
    }

    public bool IsError => this.State == DownloadProcessState.Error;

    public bool ProgressIsIndeterminate => Math.Abs(this.Progress) < double.Epsilon;

    public ProfileDownloadListItemViewModel(IListViewModel listViewModel, MtgObject entity)
      : base(listViewModel, entity)
    {
    }

    public RelayCommand DeleteCommand
    {
      get
      {
        return this._deleteCommand ?? (this._deleteCommand = new RelayCommand(new Action<object>(this.ExecuteDeleteCommand), new Func<object, bool>(this.CanExecuteDeleteCommand)));
      }
    }

    private bool CanExecuteDeleteCommand(object parameter)
    {
      return this.ListViewModel != null && !this.ListViewModel.IsDataLoading && this.State != DownloadProcessState.Removing;
    }

    private void ExecuteDeleteCommand(object parameter)
    {
      DownloadManager.Instance.RemoveAsync(this.MtgObject);
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
      return this.ListViewModel != null && !this.ListViewModel.IsDataLoading && this.IsUpdateAvailable && !this.IsRunning;
    }

    private void ExecuteUpdateCommand(object parameter)
    {
      this.IsUpdateAvailable = false;
      DownloadManager.Instance.DownloadAsync(this.MtgObject);
    }

    protected override void OnStateChanged()
    {
      base.OnStateChanged();
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsRunning));
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsError));
      this.DeleteCommand.RaiseCanExecuteChanged();
      this.UpdateCommand.RaiseCanExecuteChanged();
    }

    protected override void OnProgressChanged()
    {
      base.OnProgressChanged();
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.ProgressIsIndeterminate));
    }
  }
}
