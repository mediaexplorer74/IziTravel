// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.ProfileTileViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Model.Profile;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public abstract class ProfileTileViewModel : Screen
  {
    private readonly ILog _logger;
    private bool _isDataLoading;
    private Task _task;
    private CancellationTokenSource _tokenSource;
    private RelayCommand _naviagateCommand;

    public abstract string Title { get; }

    public abstract ProfileType Type { get; }

    public virtual bool IsFrozen => true;

    public bool IsDataLoading
    {
      get => this._isDataLoading;
      set
      {
        this.SetProperty<bool>(ref this._isDataLoading, value, propertyName: nameof (IsDataLoading));
      }
    }

    protected ProfileTileViewModel() => this._logger = LogManager.GetLog(this.GetType());

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._naviagateCommand ?? (this._naviagateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand)));
      }
    }

    protected virtual void ExecuteNavigateCommand(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<ProfileDetailPartViewModel>().WithParam<ProfileType>((Expression<Func<ProfileDetailPartViewModel, ProfileType>>) (x => x.SelectedType), this.Type).Navigate();
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.LoadDataAsync();
    }

    protected override async void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      if (this._tokenSource == null)
        return;
      if (this._task == null)
        return;
      try
      {
        this._tokenSource.Cancel();
        await this._task;
      }
      catch
      {
      }
    }

    protected virtual Task LoadDataProcess(CancellationToken token)
    {
      return Task.Factory.StartNew((System.Action) (() => { }), token);
    }

    protected virtual void RefreshCommands()
    {
    }

    private async void LoadDataAsync()
    {
      this.IsDataLoading = true;
      try
      {
        if (this._tokenSource != null && this._task != null)
        {
          this._tokenSource.Cancel();
          await this._task;
        }
        this._tokenSource = new CancellationTokenSource();
        this._task = this.LoadDataProcess(this._tokenSource.Token);
        if (this._task == null)
          return;
        await this._task;
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
      }
      finally
      {
        this.IsDataLoading = false;
      }
    }
  }
}
