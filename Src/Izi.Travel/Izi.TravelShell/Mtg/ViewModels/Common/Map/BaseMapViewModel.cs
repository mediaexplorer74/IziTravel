// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Map.BaseMapViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Managers;
using Izi.Travel.Geofencing.Primitives;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Mtg.Helpers;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Map
{
  public abstract class BaseMapViewModel : Screen
  {
    private const double DefaultZoomLevel = 14.0;
    protected readonly ILog Logger;
    private bool _isDataLoading;
    private bool _isInitializing;
    private GeoCoordinate _center = GeoCoordinate.Unknown;
    private GeoCoordinate _viewCenter = GeoCoordinate.Unknown;
    private GeoCoordinate _userLocation = GeoCoordinate.Unknown;
    private LocationRectangle _locationRect;
    private double _zoomLevel = 14.0;
    private double _viewZoomLevel;
    private readonly ObservableCollection<BaseMapItemViewModel> _items;
    private RelayCommand _loadDataCommand;
    private RelayCommand _locateUserCommand;
    private RelayCommand _zoomInCommand;
    private RelayCommand _zoomOutCommand;

    public bool IsDataLoading
    {
      get => this._isDataLoading;
      protected set
      {
        this.SetProperty<bool, bool>(ref this._isDataLoading, value, (Expression<Func<bool>>) (() => this.IsBusy), propertyName: nameof (IsDataLoading));
      }
    }

    public bool IsInitializing
    {
      get => this._isInitializing;
      protected set
      {
        this.SetProperty<bool, bool>(ref this._isInitializing, value, (Expression<Func<bool>>) (() => this.IsBusy), propertyName: nameof (IsInitializing));
      }
    }

    public bool IsBusy => this.IsInitializing || this.IsDataLoading;

    public virtual GeoCoordinate Center
    {
      get => this._center;
      set
      {
        this.SetProperty<GeoCoordinate>(ref this._center, value, propertyName: nameof (Center));
      }
    }

    public GeoCoordinate ViewCenter
    {
      get => this._viewCenter;
      set
      {
        this.SetProperty<GeoCoordinate>(ref this._viewCenter, value, propertyName: nameof (ViewCenter));
      }
    }

    public virtual GeoCoordinate UserLocation
    {
      get => this._userLocation;
      set
      {
        this.SetProperty<GeoCoordinate>(ref this._userLocation, value, propertyName: nameof (UserLocation));
      }
    }

    public LocationRectangle LocationRect
    {
      get => this._locationRect;
      set
      {
        this.SetProperty<LocationRectangle>(ref this._locationRect, value, propertyName: nameof (LocationRect));
      }
    }

    public double ZoomLevel
    {
      get => this._zoomLevel;
      set => this.SetProperty<double>(ref this._zoomLevel, value, propertyName: nameof (ZoomLevel));
    }

    public double ViewZoomLevel
    {
      get => this._viewZoomLevel;
      set
      {
        this.SetProperty<double>(ref this._viewZoomLevel, value, (System.Action) (() =>
        {
          this.ZoomInCommand.RaiseCanExecuteChanged();
          this.ZoomOutCommand.RaiseCanExecuteChanged();
        }), nameof (ViewZoomLevel));
      }
    }

    public ObservableCollection<BaseMapItemViewModel> Items => this._items;

    protected BaseMapViewModel()
    {
      this.Logger = LogManager.GetLog(this.GetType());
      this._items = new ObservableCollection<BaseMapItemViewModel>();
    }

    public RelayCommand LoadDataCommand
    {
      get
      {
        return this._loadDataCommand ?? (this._loadDataCommand = new RelayCommand(new Action<object>(this.ExecuteLoadDataCommand), new Func<object, bool>(this.CanExecuteLoadDataCommand)));
      }
    }

    protected virtual bool CanExecuteLoadDataCommand(object parameter)
    {
      return !this.IsInitializing && !this.IsDataLoading;
    }

    protected virtual void ExecuteLoadDataCommand(object parameter) => this.LoadDataAsync();

    public RelayCommand LocateUserCommand
    {
      get
      {
        return this._locateUserCommand ?? (this._locateUserCommand = new RelayCommand(new Action<object>(this.ExecuteLocateUserCommand), new Func<object, bool>(this.CanExecuteLocateUserCommand)));
      }
    }

    protected virtual bool CanExecuteLocateUserCommand(object parameter) => !this.IsDataLoading;

    protected virtual async void ExecuteLocateUserCommand(object parameter)
    {
      if (!await DialogHelper.CheckForLocationServices())
        return;
      Geolocation position = await Geotracker.Instance.GetPosition();
      if (position != null)
        this.UserLocation = position.ToGeoCoordinate();
      if (this.UserLocation == (GeoCoordinate) null || this.UserLocation == GeoCoordinate.Unknown)
        return;
      this.ViewCenter = this.UserLocation;
    }

    public RelayCommand ZoomInCommand
    {
      get
      {
        return this._zoomInCommand ?? (this._zoomInCommand = new RelayCommand((Action<object>) (x => --this.ViewZoomLevel), (Func<object, bool>) (x => this.ViewZoomLevel > 1.0)));
      }
    }

    public RelayCommand ZoomOutCommand
    {
      get
      {
        return this._zoomOutCommand ?? (this._zoomOutCommand = new RelayCommand((Action<object>) (x => ++this.ViewZoomLevel), (Func<object, bool>) (x => this.ViewZoomLevel < 20.0)));
      }
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.InitializeAsync();
    }

    protected virtual void RefreshCommands() => this.LocateUserCommand.RaiseCanExecuteChanged();

    protected virtual void OnInitializeBegin() => this.RefreshCommands();

    protected virtual Task OnInitializeProcessAsync()
    {
      return Task.Factory.StartNew((System.Action) (() => { }));
    }

    protected virtual void OnInitializeComplete() => this.RefreshCommands();

    protected virtual void OnInitializeError(Exception ex)
    {
    }

    private async void InitializeAsync()
    {
      this.IsInitializing = true;
      try
      {
        this.OnInitializeBegin();
        await this.OnInitializeProcessAsync();
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.OnInitializeError(ex);
      }
      finally
      {
        this.IsInitializing = false;
        this.OnInitializeComplete();
      }
    }

    protected virtual void OnLoadDataBegin() => this.RefreshCommands();

    protected virtual Task OnLoadDataProcess() => Task.Factory.StartNew((System.Action) (() => { }));

    protected virtual void OnLoadDataComplete() => this.RefreshCommands();

    protected virtual void OnLoadDataError(Exception ex)
    {
    }

    private async void LoadDataAsync()
    {
      try
      {
        this.IsDataLoading = true;
        this.OnLoadDataBegin();
        await this.OnLoadDataProcess();
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.OnLoadDataError(ex);
      }
      finally
      {
        this.IsDataLoading = false;
        this.OnLoadDataComplete();
      }
    }
  }
}
