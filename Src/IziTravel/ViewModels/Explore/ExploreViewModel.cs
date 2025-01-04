// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Explore.ExploreViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Geofencing.Geotracker;
using Izi.Travel.Geofencing.Primitives;
using Izi.Travel.Shell.Common.Commands;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Themes;
using Izi.Travel.Shell.Model.Explore;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.ViewModels.Explore.Flyouts;
using Izi.Travel.Shell.ViewModels.Featured;
using Izi.Travel.Shell.Views.Explore;
using Izi.Travel.Utility.Extensions;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Explore
{
  public sealed class ExploreViewModel : 
    Screen,
    IMainTabViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
  {
    private const int DataLoadLimit = 20;
    private const double MapDefaultZoomLevel = 10.0;
    private const double MapLocationThreshold = 200.0;
    private const double MapLocationRectangleThreshold = 200.0;
    private const int MapClusterPadding = 70;
    private const double MapClusterDistance = 35.0;
    private const double MaxClusterizationZoomLevel = 15.0;
    private static readonly double ScreenHeight = Application.Current.Host.Content.ActualHeight;
    private static readonly double ScreenWidth = Application.Current.Host.Content.ActualWidth;
    private AppSettings _appSettings;
    private readonly ExploreFlyoutLocationViewModel _flyoutLocationViewModel;
    private readonly ExploreFlyoutTypeViewModel _flyoutTypeViewModel;
    private MtgObjectType[] _selectedTypes;
    private readonly ObservableCollection<ExploreItemViewModel> _items;
    private readonly ObservableCollection<ExploreItemViewModel> _listItems;
    private ExploreItemViewModel _selectedFlipItem;
    private double _zoomLevel = 10.0;
    private double _viewZoomLevel;
    private GeoCoordinate _userLocation = GeoCoordinate.Unknown;
    private GeoCoordinate _center = GeoCoordinate.Unknown;
    private GeoCoordinate _mapLocationCenter = GeoCoordinate.Unknown;
    private GeoCoordinate _viewCenter = GeoCoordinate.Unknown;
    private LocationRectangle _viewBounds;
    private readonly DispatcherTimer _clusterTimer;
    private readonly double _clusterDistance;
    private int _clusterItemsCount;
    private double _clusterZoomLevel;
    private string _query;
    private string _currentQuery;
    private bool _isListHidden;
    private bool _isInitializing;
    private bool _isItemsDataRefreshing;
    private bool _isItemsDataLoading;
    private bool _isMapViewChanging;
    private bool? _geotrackerIsEnabled;
    private ExploreLoadResult _loadResult;
    private readonly ObservableCollection<MapElement> _selectedMapItemRouteElements;
    private ExploreItemViewModel _previousSelectedItem;
    private BaseCommand _navigateToNetworkSettingsCommand;
    private RelayCommand _openFeaturedGuidesCommand;
    private RelayCommand _locateUserCommand;
    private RelayCommand _zoomInCommand;
    private RelayCommand _zoomOutCommand;
    private RelayCommand _refreshCommand;
    private RelayCommand _loadListDataCommand;
    private RelayCommand _loadMapDataCommand;
    private RelayCommand _clearMapSelectionCommand;
    private RelayCommand _expandClusterCommand;
    private RelayCommand _clearSearchStringCommand;
    private RelayCommand _searchCommand;
    private RelayCommand _toggleExploreModeCommand;
    private RelayCommand _navigateCommand;

    public override string DisplayName
    {
      get => AppResources.LabelExplore;
      set => throw new NotImplementedException();
    }

    public string ImageUrl => "/Assets/Icons/tab.explore.png";

    public string SelectedImageUrl => "/Assets/Icons/tab.explore.selected.png";

    public ScreenProperties Properties { get; set; }

    public ExploreFlyoutLocationViewModel FlyoutLocationViewModel => this._flyoutLocationViewModel;

    public ExploreFlyoutTypeViewModel FlyoutTypeViewModel => this._flyoutTypeViewModel;

    public ObservableCollection<ExploreItemViewModel> Items => this._items;

    public ObservableCollection<ExploreItemViewModel> ListItems => this._listItems;

    public MtgObjectType[] SelectedTypes
    {
      get => this._selectedTypes;
      set
      {
        this.SetProperty<MtgObjectType[]>(ref this._selectedTypes, value, propertyName: nameof (SelectedTypes));
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
        if (Math.Abs(this._viewZoomLevel - value) <= double.Epsilon)
          return;
        this._viewZoomLevel = value;
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.ViewZoomLevel));
        this.ZoomInCommand.RaiseCanExecuteChanged();
        this.ZoomOutCommand.RaiseCanExecuteChanged();
      }
    }

    public GeoCoordinate UserLocation
    {
      get => this._userLocation;
      set
      {
        this.SetProperty<GeoCoordinate>(ref this._userLocation, value, (System.Action) (() =>
        {
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.\u003C\u003E4__this.HasUserLocation));
          if (!(value != (GeoCoordinate) null) || !(value != GeoCoordinate.Unknown))
            return;
          ExploreLocationItem.AroundMe.TrySetLocation(value);
        }), nameof (UserLocation));
      }
    }

    public bool HasUserLocation
    {
      get
      {
        return this.UserLocation != (GeoCoordinate) null && this.UserLocation != GeoCoordinate.Unknown;
      }
    }

    public GeoCoordinate Center
    {
      get => this._center;
      set
      {
        this.SetProperty<GeoCoordinate>(ref this._center, value, (System.Action) (() => ExploreLocationItem.MapLocation.TrySetLocation(this._center)), nameof (Center));
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

    public LocationRectangle ViewBounds
    {
      get => this._viewBounds;
      set
      {
        this.SetProperty<LocationRectangle>(ref this._viewBounds, value, propertyName: nameof (ViewBounds));
      }
    }

    public string Query
    {
      get => this._query;
      set
      {
        this.SetProperty<string>(ref this._query, value, (System.Action) (() =>
        {
          this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.QueryIcon));
          this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.MessageEmptyInfo));
          this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.MessageEmptyPrompt));
          this.ClearSearchStringCommand.RaiseCanExecuteChanged();
        }), nameof (Query));
      }
    }

    public string QueryIcon
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.Query) ? (string) null : "/Assets/Icons/action.delete.gray.png";
      }
    }

    public bool IsInitializing
    {
      get => this._isInitializing;
      set
      {
        this.SetProperty<bool>(ref this._isInitializing, value, (System.Action) (() =>
        {
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsBusy));
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsForegroundBusy));
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsFilterEnabled));
          this.RefreshCommand.RaiseCanExecuteChanged();
          this.LocateUserCommand.RaiseCanExecuteChanged();
        }), nameof (IsInitializing));
      }
    }

    public bool IsItemsDataRefreshing
    {
      get => this._isItemsDataRefreshing;
      set
      {
        this.SetProperty<bool>(ref this._isItemsDataRefreshing, value, (System.Action) (() =>
        {
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsForegroundBusy));
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsFilterEnabled));
          this.RefreshCommand.RaiseCanExecuteChanged();
          this.LocateUserCommand.RaiseCanExecuteChanged();
        }), nameof (IsItemsDataRefreshing));
      }
    }

    public bool IsItemsDataLoading
    {
      get => this._isItemsDataLoading;
      private set
      {
        this.SetProperty<bool>(ref this._isItemsDataLoading, value, (System.Action) (() =>
        {
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsBusy));
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsFilterEnabled));
          this.SearchCommand.RaiseCanExecuteChanged();
          this.ClearSearchStringCommand.RaiseCanExecuteChanged();
          this.RefreshCommand.RaiseCanExecuteChanged();
          this.LocateUserCommand.RaiseCanExecuteChanged();
        }), nameof (IsItemsDataLoading));
      }
    }

    public bool IsBusy
    {
      get => !this.IsInitializing && !this.IsItemsDataRefreshing && this.IsItemsDataLoading;
    }

    public string BusyTitle => AppResources.ActionLoading;

    public bool IsForegroundBusy => this.IsInitializing || this.IsItemsDataRefreshing;

    public bool IsFilterEnabled => !this.IsBusy && !this.IsForegroundBusy;

    public bool IsListHidden
    {
      get => this._isListHidden;
      set
      {
        this.SetProperty<bool>(ref this._isListHidden, value, propertyName: nameof (IsListHidden));
      }
    }

    public bool IsFilterApplied
    {
      get
      {
        return this.FlyoutLocationViewModel.SelectedLocationItem != null && this.FlyoutLocationViewModel.SelectedLocationItem.Uid != "ExploreLocationItemAroundMe" && this.FlyoutLocationViewModel.SelectedLocationItem.Uid != "ExploreLocationItemMapLocation" || this.SelectedTypes.Length < 2 || !string.IsNullOrWhiteSpace(this.Query);
      }
    }

    public ExploreItemViewModel SelectedItem
    {
      get
      {
        return this.Items.FirstOrDefault<ExploreItemViewModel>((Func<ExploreItemViewModel, bool>) (x => x.IsSelected));
      }
    }

    public ExploreItemViewModel SelectedMapItem
    {
      get => this.SelectedItem;
      set
      {
        ExploreItemViewModel selectedItem = this.SelectedItem;
        if (selectedItem != null)
        {
          selectedItem.IsSelected = false;
          this._selectedFlipItem = value;
          this.NotifyOfPropertyChange<ExploreItemViewModel>((Expression<Func<ExploreItemViewModel>>) (() => this.SelectedFlipItem));
        }
        if (value != null)
        {
          value.IsSelected = true;
          if (value.Location != (GeoCoordinate) null)
            this.ViewCenter = value.Location;
        }
        this.NotifyOfPropertyChange<ExploreItemViewModel>((Expression<Func<ExploreItemViewModel>>) (() => this.SelectedMapItem));
      }
    }

    public ObservableCollection<MapElement> SelectedMapItemRouteElements
    {
      get => this._selectedMapItemRouteElements;
    }

    public ExploreItemViewModel SelectedFlipItem
    {
      get => this._selectedFlipItem;
      set
      {
        this.SetProperty<ExploreItemViewModel>(ref this._selectedFlipItem, value, (System.Action) (() =>
        {
          if (this.SelectedMapItem == null)
            return;
          this.SelectedMapItem = value;
        }), nameof (SelectedFlipItem));
      }
    }

    public bool IsMapViewChanging
    {
      get => this._isMapViewChanging;
      set
      {
        this.SetProperty<bool>(ref this._isMapViewChanging, value, propertyName: nameof (IsMapViewChanging));
      }
    }

    public ExploreLoadResult LoadResult
    {
      get => this._loadResult;
      set
      {
        this.SetProperty<ExploreLoadResult>(ref this._loadResult, value, (System.Action) (() => this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsLoadResultSuccess))), nameof (LoadResult));
      }
    }

    public bool IsLoadResultSuccess => this.LoadResult == ExploreLoadResult.Success;

    public string MessageEmptyInfo
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.Query) ? AppResources.MessageExploreSearchResultIsEmptyFilterInfo : string.Format(AppResources.MessageExploreSearchResultIsEmptyQueryInfo, (object) this.Query.Truncate(30));
      }
    }

    public string MessageEmptyPrompt
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.Query) ? AppResources.MessageExploreSearchResultIsEmptyFilterPrompt : AppResources.MessageExploreSearchResultIsEmptyQueryPrompt;
      }
    }

    public ExploreViewModel()
    {
      ScreenProperties screenProperties = new ScreenProperties();
      screenProperties.AppBarButtons = (IEnumerable<ButtonInfo>) new ButtonInfo[3]
      {
        new ButtonInfo()
        {
          Key = "LocateUser",
          ImageUrl = "/Assets/Icons/appbar.featured.png",
          Text = AppResources.FlyoutFeaturedTitle,
          Command = (ICommand) this.OpenFeaturedGuidesCommand
        },
        new ButtonInfo()
        {
          Key = "Refresh",
          ImageUrl = "/Assets/Icons/appbar.refresh.png",
          Text = AppResources.CommandRefresh,
          Command = (ICommand) this.RefreshCommand
        },
        new ButtonInfo()
        {
          Key = "ExploreMode",
          ImageUrl = "/Assets/Icons/appbar.map.png",
          Text = AppResources.LabelMap,
          AlternativeImageUrl = "/Assets/Icons/appbar.list.png",
          AlternativeText = AppResources.LabelList,
          Command = (ICommand) this.ToggleExploreModeCommand
        }
      };
      this.Properties = screenProperties;
      this._flyoutLocationViewModel = new ExploreFlyoutLocationViewModel(this);
      this._flyoutTypeViewModel = new ExploreFlyoutTypeViewModel(this);
      this._items = new ObservableCollection<ExploreItemViewModel>();
      this._listItems = new ObservableCollection<ExploreItemViewModel>();
      this._clusterDistance = Math.Pow(35.0, 2.0);
      this._clusterTimer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromMilliseconds(300.0)
      };
      this._clusterTimer.Tick += new EventHandler(this.OnClusterTimerTick);
      this._appSettings = ServiceFacade.SettingsService.GetAppSettings();
      this._selectedMapItemRouteElements = new ObservableCollection<MapElement>();
    }

    public BaseCommand NavigateToNetworkSettingsCommand
    {
      get
      {
        return this._navigateToNetworkSettingsCommand ?? (this._navigateToNetworkSettingsCommand = (BaseCommand) new LaunchUriCommand(new Uri("ms-settings-wifi:")));
      }
    }

    public RelayCommand OpenFeaturedGuidesCommand
    {
      get
      {
        return this._openFeaturedGuidesCommand ?? (this._openFeaturedGuidesCommand = new RelayCommand(new Action<object>(this.ExecuteOpenFeaturedGuidesCommand)));
      }
    }

    private void ExecuteOpenFeaturedGuidesCommand(object parameter)
    {
      new FeaturedListTask().Show();
    }

    public RelayCommand LocateUserCommand
    {
      get
      {
        return this._locateUserCommand ?? (this._locateUserCommand = new RelayCommand(new Action<object>(this.ExecuteLocateUserCommand), new Func<object, bool>(this.CanExecuteLocateUserCommand)));
      }
    }

    private bool CanExecuteLocateUserCommand(object parameter)
    {
      return !this.IsForegroundBusy && !this.IsBusy;
    }

    private async void ExecuteLocateUserCommand(object parameter)
    {
      if (!await DialogHelper.CheckForLocationServices())
        return;
      this.FlyoutLocationViewModel.SelectedLocationItem = ExploreLocationItem.AroundMe;
      this.SetMapView(ExploreLocationItem.AroundMe);
      await this.RefreshItemsDataAsync();
    }

    public RelayCommand ZoomInCommand
    {
      get
      {
        return this._zoomInCommand ?? (this._zoomInCommand = new RelayCommand((Action<object>) (x => this.ViewZoomLevel = Math.Max(this.ViewZoomLevel - 1.0, 1.0)), (Func<object, bool>) (x => this.ViewZoomLevel > 1.0)));
      }
    }

    public RelayCommand ZoomOutCommand
    {
      get
      {
        return this._zoomOutCommand ?? (this._zoomOutCommand = new RelayCommand((Action<object>) (x => this.ViewZoomLevel = Math.Min(this.ViewZoomLevel + 1.0, 20.0)), (Func<object, bool>) (x => this.ViewZoomLevel < 20.0)));
      }
    }

    public RelayCommand RefreshCommand
    {
      get
      {
        return this._refreshCommand ?? (this._refreshCommand = new RelayCommand(new Action<object>(this.ExecuteRefreshCommand), new Func<object, bool>(this.CanExecuteRefreshCommand)));
      }
    }

    private bool CanExecuteRefreshCommand(object parameter)
    {
      return !this.IsForegroundBusy && !this.IsBusy;
    }

    private async void ExecuteRefreshCommand(object parameter)
    {
      await this.RefreshItemsDataAsync();
    }

    public RelayCommand LoadListDataCommand
    {
      get
      {
        return this._loadListDataCommand ?? (this._loadListDataCommand = new RelayCommand(new Action<object>(this.ExecuteLoadListDataCommand), new Func<object, bool>(this.CanExecuteLoadListDataCommand)));
      }
    }

    private bool CanExecuteLoadListDataCommand(object parameter)
    {
      return !this.IsItemsDataLoading && !this.IsListHidden;
    }

    private async void ExecuteLoadListDataCommand(object parameter)
    {
      await this.LoadItemsDataAsync(true);
    }

    public RelayCommand LoadMapDataCommand
    {
      get
      {
        return this._loadMapDataCommand ?? (this._loadMapDataCommand = new RelayCommand(new Action<object>(this.ExecuteLoadMapDataCommand), new Func<object, bool>(this.CanExecuteLoadMapDataCommand)));
      }
    }

    private bool CanExecuteLoadMapDataCommand(object parameter)
    {
      return !this.IsMapViewChanging && !this.IsItemsDataLoading && this.IsListHidden;
    }

    private async void ExecuteLoadMapDataCommand(object parameter)
    {
      if (this.LoadResult == ExploreLoadResult.ErrorNetwork || this.LoadResult == ExploreLoadResult.ErrorUnknown)
        return;
      if (this.FlyoutLocationViewModel.LocationSelected)
      {
        this.FlyoutLocationViewModel.LocationSelected = false;
      }
      else
      {
        ExploreLocationItem selectedLocationItem = this.FlyoutLocationViewModel.SelectedLocationItem;
        if (selectedLocationItem != ExploreLocationItem.MapLocation)
        {
          if (this.Center == GeoCoordinate.Unknown || selectedLocationItem.Location == GeoCoordinate.Unknown || selectedLocationItem.LocationRectangle == null || selectedLocationItem.LocationRectangle.Center == GeoCoordinate.Unknown || selectedLocationItem.LocationRectangle.Center.GetDistanceTo(this.Center) < 200.0)
            return;
          this.FlyoutLocationViewModel.SelectedLocationItem = ExploreLocationItem.MapLocation;
          this._mapLocationCenter = this.Center;
        }
        else if (this._mapLocationCenter != GeoCoordinate.Unknown && selectedLocationItem.Location != GeoCoordinate.Unknown)
        {
          if (this._mapLocationCenter.GetDistanceTo(selectedLocationItem.Location) < 200.0)
            return;
          this._mapLocationCenter = this.Center;
        }
        await this.LoadItemsDataAsync();
      }
    }

    public RelayCommand ClearMapSelectionCommand
    {
      get
      {
        return this._clearMapSelectionCommand ?? (this._clearMapSelectionCommand = new RelayCommand(new Action<object>(this.ExecuteClearMapSelectionCommand)));
      }
    }

    private void ExecuteClearMapSelectionCommand(object parameter)
    {
      this.SelectedMapItem = (ExploreItemViewModel) null;
    }

    public RelayCommand ExpandClusterCommand
    {
      get
      {
        return this._expandClusterCommand ?? (this._expandClusterCommand = new RelayCommand(new Action<object>(this.ExecuteExpandClusterCommand)));
      }
    }

    private void ExecuteExpandClusterCommand(object parameter)
    {
      if (!(parameter is ExploreItemViewModel exploreItemViewModel) || !exploreItemViewModel.IsCluster)
        return;
      this.ViewBounds = exploreItemViewModel.ClusterBounds;
    }

    public RelayCommand ClearSearchStringCommand
    {
      get
      {
        return this._clearSearchStringCommand ?? (this._clearSearchStringCommand = new RelayCommand(new Action<object>(this.ExecuteClearSearchStringCommand), new Func<object, bool>(this.CanExecuteClearSearchStringCommand)));
      }
    }

    private bool CanExecuteClearSearchStringCommand(object parameter)
    {
      return !string.IsNullOrEmpty(this.Query) && !this.IsItemsDataLoading;
    }

    private async void ExecuteClearSearchStringCommand(object parameter)
    {
      this.Query = string.Empty;
      await this.RefreshItemsDataAsync();
      this.SetExploreMode(false);
    }

    public RelayCommand SearchCommand
    {
      get
      {
        return this._searchCommand ?? (this._searchCommand = new RelayCommand(new Action<object>(this.ExecuteSearchCommand), new Func<object, bool>(this.CanExecuteSearchCommand)));
      }
    }

    private bool CanExecuteSearchCommand(object parameter) => !this.IsItemsDataLoading;

    private async void ExecuteSearchCommand(object parameter)
    {
      this.FlyoutLocationViewModel.SelectedLocationItem = ExploreLocationItem.MapLocation;
      this.FlyoutTypeViewModel.SelectedItem = this.FlyoutTypeViewModel.Items.First<KeyValueModel>();
      AnalyticsHelper.SendSearch(this.Query);
      await this.RefreshItemsDataAsync();
      this.SetExploreMode(false);
    }

    public RelayCommand ToggleExploreModeCommand
    {
      get
      {
        return this._toggleExploreModeCommand ?? (this._toggleExploreModeCommand = new RelayCommand(new Action<object>(this.ExecuteToggleExploreModeCommand)));
      }
    }

    private void ExecuteToggleExploreModeCommand(object parameter)
    {
      this.SetExploreMode(!this.IsListHidden);
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand)));
      }
    }

    private void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is ExploreItemViewModel exploreItemViewModel))
        return;
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), exploreItemViewModel.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), exploreItemViewModel.Language).Navigate();
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.InitializeAsync();
      this.FlyoutLocationViewModel.InitializeAsync();
    }

    protected override async void OnActivate()
    {
      base.OnActivate();
      string[] settingsChangeKeys = ServiceFacade.SettingsService.GetAppSettingsChangeKeys(this._appSettings);
      if (((settingsChangeKeys == null ? 0 : (((IEnumerable<string>) settingsChangeKeys).Contains<string>("AppSettingsServerEnvironment") || ((IEnumerable<string>) settingsChangeKeys).Contains<string>("AppSettingsLanguages") || ((IEnumerable<string>) settingsChangeKeys).Contains<string>("AppSettingsLocationEnabled") ? 1 : (((IEnumerable<string>) settingsChangeKeys).Contains<string>("AppSettingsCodeNames") ? 1 : 0))) | (!this._geotrackerIsEnabled.HasValue ? (false ? 1 : 0) : (this._geotrackerIsEnabled.Value != Izi.Travel.Business.Managers.Geotracker.Instance.IsEnabled ? 1 : 0))) != 0)
      {
        this.InitializeAsync();
        this.FlyoutLocationViewModel.InitializeAsync();
        this._appSettings = ServiceFacade.SettingsService.GetAppSettings();
      }
      else
        await this.RefreshExploreItemStatusAsync();
      this._clusterTimer.Start();
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadStateChanged));
      // ISSUE: method pointer
      PurchaseManager.Instance.IsPurchasedChanged += new TypedEventHandler<string, bool>((object) this, __methodptr(OnIsPurchasedChanged));
      // ISSUE: method pointer
      Izi.Travel.Business.Managers.Geotracker.Instance.PositionChanged += new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(OnGeoTrackerPositionChanged));
      this.UserLocation = (await Izi.Travel.Business.Managers.Geotracker.Instance.GetPosition() ?? Izi.Travel.Business.Managers.Geotracker.Instance.DefaultPosition).ToGeoCoordinate();
    }

    protected override void OnDeactivate(bool close)
    {
      this._geotrackerIsEnabled = new bool?(Izi.Travel.Business.Managers.Geotracker.Instance.IsEnabled);
      // ISSUE: method pointer
      Izi.Travel.Business.Managers.Geotracker.Instance.PositionChanged -= new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(OnGeoTrackerPositionChanged));
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged -= new TypedEventHandler<DownloadManager, DownloadProcess>((object) this, __methodptr(OnDownloadStateChanged));
      // ISSUE: method pointer
      PurchaseManager.Instance.IsPurchasedChanged -= new TypedEventHandler<string, bool>((object) this, __methodptr(OnIsPurchasedChanged));
      this._clusterTimer.Stop();
      base.OnDeactivate(close);
    }

    public void SetMapView(ExploreLocationItem location)
    {
      if (location == null || location.LocationRectangle == null)
        return;
      this.ViewBounds = location.LocationRectangle;
    }

    public async Task RefreshItemsDataAsync()
    {
      this.IsItemsDataRefreshing = true;
      this.LoadResult = ExploreLoadResult.None;
      try
      {
        this.SelectedMapItem = (ExploreItemViewModel) null;
        this._currentQuery = this.Query;
        this.Items.Clear();
        this.NotifyOfPropertyChange<ExploreItemViewModel>((Expression<Func<ExploreItemViewModel>>) (() => this.SelectedMapItem));
        await this.LoadItemsDataAsync();
      }
      finally
      {
        this.IsItemsDataRefreshing = false;
        if (this.LoadResult != ExploreLoadResult.Success)
          this.SetExploreMode(false);
      }
    }

    internal void UpdateSelectedMapItemRoute(ExploreItemViewModel exploreItemViewModel)
    {
      this.SelectedMapItemRouteElements.Clear();
      if (exploreItemViewModel == null || exploreItemViewModel.MtgObject == null || exploreItemViewModel.MtgObject.Map == null || exploreItemViewModel.MtgObject.Map.Route == null)
        return;
      Color themeColor = ThemeHelper.GetThemeColor("IziTravelBlueColor");
      this.SelectedMapItemRouteElements.Add((MapElement) MapHelper.CreatePolyline(((IEnumerable<GeoLocation>) exploreItemViewModel.MtgObject.Map.Route).Select<GeoLocation, GeoCoordinate>((Func<GeoLocation, GeoCoordinate>) (x => x.ToGeoCoordinate())), themeColor, 3.0));
    }

    private async void InitializeAsync()
    {
      this.IsInitializing = true;
      try
      {
        await this.InitializeLocationAsync();
        await this.RefreshItemsDataAsync();
      }
      finally
      {
        this.IsInitializing = false;
      }
    }

    private async Task InitializeLocationAsync()
    {
      Geolocation geolocation = await Izi.Travel.Business.Managers.Geotracker.Instance.GetPosition() ?? Izi.Travel.Business.Managers.Geotracker.Instance.DefaultPosition;
      if (geolocation == null)
        return;
      GeoCoordinate geoCoordinate = geolocation.ToGeoCoordinate();
      ExploreLocationItem.AroundMe.TrySetLocation(geoCoordinate);
      ExploreLocationItem.MapLocation.TrySetLocation(geoCoordinate);
      this.SetMapView(ExploreLocationItem.AroundMe);
    }

    private async Task LoadItemsDataAsync(bool isListLoad = false)
    {
      this.IsItemsDataLoading = true;
      this._clusterItemsCount = 0;
      MtgObjectListFilter objectListFilter = new MtgObjectListFilter();
      objectListFilter.Offset = new int?(isListLoad ? this.ListItems.Count : 0);
      objectListFilter.Types = this.SelectedTypes;
      objectListFilter.Includes = ContentSection.None;
      objectListFilter.Excludes = ContentSection.All;
      objectListFilter.Languages = ServiceFacade.SettingsService.GetAppSettings().Languages;
      objectListFilter.Query = this._currentQuery;
      MtgObjectListFilter filter = objectListFilter;
      ExploreLocationItem location = this.FlyoutLocationViewModel.SelectedLocationItem;
      GeoCoordinate coordinate = !(location.Location != (GeoCoordinate) null) || !(location.Location != GeoCoordinate.Unknown) ? Izi.Travel.Business.Managers.Geotracker.Instance.DefaultPosition.ToGeoCoordinate() : location.Location;
      bool hasRegion = location != ExploreLocationItem.AroundMe && location != ExploreLocationItem.MapLocation;
      if (!hasRegion)
      {
        filter.Limit = new int?(20);
        filter.Location = GeoLocation.FromGeoCoordinate(coordinate);
        filter.Radius = new uint?((uint) int.MaxValue);
      }
      else
      {
        filter.Limit = new int?(int.MaxValue);
        filter.Region = location.Uid;
      }
      MtgListResult mtgObjectListAsync = await MtgObjectServiceHelper.GetMtgObjectListAsync(filter);
      if (!isListLoad)
      {
        ExploreItemViewModel[] array = mtgObjectListAsync.Data.Where<MtgObject>((Func<MtgObject, bool>) (x => this.Items.All<ExploreItemViewModel>((Func<ExploreItemViewModel, bool>) (y => y.Uid != x.Uid)))).Select<MtgObject, ExploreItemViewModel>((Func<MtgObject, ExploreItemViewModel>) (x => new ExploreItemViewModel(this, x)
        {
          ClusterIsHidden = true
        })).ToArray<ExploreItemViewModel>();
        if (hasRegion && array.Length != 0 && location.LocationRectangle != null)
        {
          LocationRectangle boundingRectangle = LocationRectangle.CreateBoundingRectangle(((IEnumerable<ExploreItemViewModel>) array).Select<ExploreItemViewModel, GeoCoordinate>((Func<ExploreItemViewModel, GeoCoordinate>) (x => x.Location)));
          if (!boundingRectangle.Intersects(location.LocationRectangle))
          {
            ExploreLocationItem location1 = new ExploreLocationItem();
            location1.TrySetLocation((GeoCoordinate) null, boundingRectangle);
            this.SetMapView(location1);
          }
        }
        this.Items.AddRange<ExploreItemViewModel>((IEnumerable<ExploreItemViewModel>) array);
        if (filter.Location != null)
        {
          foreach (ExploreItemViewModel exploreItemViewModel in (Collection<ExploreItemViewModel>) this.Items)
            exploreItemViewModel.RefreshMapDistance(filter.Location.ToGeoCoordinate());
        }
        this.ListItems.Clear();
        this.ListItems.AddRange<ExploreItemViewModel>(this.Items.OrderBy<ExploreItemViewModel, double>((Func<ExploreItemViewModel, double>) (x => x.MapCenterDistance)).Take<ExploreItemViewModel>(filter.Limit.Value));
      }
      else
      {
        ExploreItemViewModel[] array = mtgObjectListAsync.Data.Where<MtgObject>((Func<MtgObject, bool>) (x => this.ListItems.All<ExploreItemViewModel>((Func<ExploreItemViewModel, bool>) (y => y.Uid != x.Uid)))).Select<MtgObject, ExploreItemViewModel>((Func<MtgObject, ExploreItemViewModel>) (x => new ExploreItemViewModel(this, x))).OrderBy<ExploreItemViewModel, double>((Func<ExploreItemViewModel, double>) (x => x.Distance)).Take<ExploreItemViewModel>(filter.Limit.Value).ToArray<ExploreItemViewModel>();
        if (filter.Location != null)
        {
          foreach (ExploreItemViewModel exploreItemViewModel in array)
            exploreItemViewModel.RefreshMapDistance(filter.Location.ToGeoCoordinate());
        }
        this.ListItems.AddRange<ExploreItemViewModel>((IEnumerable<ExploreItemViewModel>) array);
      }
      this.LoadResult = this.Items.Count <= 0 ? (!mtgObjectListAsync.RemoteSuccess ? ExploreLoadResult.NoContentOffline : (this.IsFilterApplied ? ExploreLoadResult.Empty : ExploreLoadResult.NoContent)) : ExploreLoadResult.Success;
      this.IsItemsDataLoading = false;
    }

    private void SetExploreMode(bool isMapMode)
    {
      this.IsListHidden = isMapMode;
      if (isMapMode)
      {
        this._clusterTimer.Start();
      }
      else
      {
        this._clusterTimer.Stop();
        this.SelectedMapItem = (ExploreItemViewModel) null;
      }
      ButtonInfo buttonInfo = this.Properties.AppBarButtons.FirstOrDefault<ButtonInfo>((Func<ButtonInfo, bool>) (x => x.Key == "ExploreMode"));
      if (buttonInfo == null)
        return;
      buttonInfo.ShowAlternative = this.IsListHidden;
    }

    private Task RefreshExploreItemStatusAsync()
    {
      List<ExploreItemViewModel> items = this.ListItems.ToList<ExploreItemViewModel>();
      return Task.Factory.StartNew((System.Action) (() =>
      {
        foreach (ExploreItemViewModel exploreItemViewModel in items)
        {
          Tuple<DownloadProcessState, double> objectDownloadInfo = DownloadManager.Instance.GetMtgObjectDownloadInfo(exploreItemViewModel.MtgObject);
          exploreItemViewModel.IsDownloaded = objectDownloadInfo.Item1 == DownloadProcessState.Downloaded || objectDownloadInfo.Item1 == DownloadProcessState.Updated;
          exploreItemViewModel.IsPurchased = PurchaseManager.Instance.IsPurchased(exploreItemViewModel.MtgObject);
          exploreItemViewModel.RefreshStatus();
        }
      }));
    }

    private void OnClusterTimerTick(object sender, EventArgs eventArgs) => this.CreateClusters();

    private void CreateClusters()
    {
      if (this._previousSelectedItem == this.SelectedItem && this.Items.Count == this._clusterItemsCount && Math.Abs(this.ZoomLevel - this._clusterZoomLevel) < 1.0)
        return;
      this._previousSelectedItem = this.SelectedItem;
      if (!(this.Views.FirstOrDefault<KeyValuePair<object, object>>().Value is ExploreView exploreView))
        return;
      Map partExploreMap = exploreView.PartExploreMap;
      if (partExploreMap == null)
        return;
      foreach (ExploreItemViewModel exploreItemViewModel in (Collection<ExploreItemViewModel>) this.Items)
      {
        exploreItemViewModel.ClusterViewportPoint = partExploreMap.ConvertGeoCoordinateToViewportPoint(exploreItemViewModel.Location);
        exploreItemViewModel.ClusterIsVisited = !ExploreViewModel.Contains(exploreItemViewModel.ClusterViewportPoint);
      }
      for (int index1 = 0; index1 < this.Items.Count; ++index1)
      {
        ExploreItemViewModel exploreItemViewModel1 = this.Items[index1];
        if (!exploreItemViewModel1.ClusterIsVisited)
        {
          int num = 1;
          List<GeoCoordinate> locations = new List<GeoCoordinate>()
          {
            exploreItemViewModel1.Location
          };
          if (!exploreItemViewModel1.IsSelected && this.ZoomLevel < 15.0)
          {
            for (int index2 = index1 + 1; index2 < this.Items.Count; ++index2)
            {
              ExploreItemViewModel exploreItemViewModel2 = this.Items[index2];
              if (!exploreItemViewModel2.IsSelected && !exploreItemViewModel2.ClusterIsVisited && ExploreViewModel.GetDistanceSquared(exploreItemViewModel1.ClusterViewportPoint, exploreItemViewModel2.ClusterViewportPoint) < this._clusterDistance)
              {
                locations.Add(exploreItemViewModel2.Location);
                ++num;
                exploreItemViewModel2.ClusterIsVisited = true;
                exploreItemViewModel2.ClusterIsHidden = true;
              }
            }
          }
          if (num > 1)
            exploreItemViewModel1.ClusterBounds = LocationRectangle.CreateBoundingRectangle((IEnumerable<GeoCoordinate>) locations);
          exploreItemViewModel1.ClusterCount = num;
          exploreItemViewModel1.ClusterIsHidden = false;
          exploreItemViewModel1.ClusterIsVisited = true;
        }
      }
      this._clusterItemsCount = this.Items.Count;
      this._clusterZoomLevel = (double) (int) this.ZoomLevel;
    }

    private static double GetDistanceSquared(System.Windows.Point source, System.Windows.Point target)
    {
      return Math.Pow(target.X - source.X, 2.0) + Math.Pow(target.Y - source.Y, 2.0);
    }

    private static bool Contains(System.Windows.Point point, int padding = 70)
    {
      return point.X >= (double) -padding && point.X <= ExploreViewModel.ScreenWidth + (double) padding && point.Y >= (double) -padding && point.Y <= ExploreViewModel.ScreenHeight + (double) padding;
    }

    private void OnGeoTrackerPositionChanged(IGeotracker tracker, Geolocation location)
    {
      if (location == null)
        return;
      this.UserLocation = new GeoCoordinate(location.Latitude, location.Longitude);
      foreach (ExploreItemViewModel exploreItemViewModel in (Collection<ExploreItemViewModel>) this.Items)
        exploreItemViewModel.RefreshUserLocationDistance();
    }

    private void OnDownloadStateChanged(DownloadManager manager, DownloadProcess downloadProcess)
    {
      ExploreItemViewModel exploreItemViewModel = this.ListItems.FirstOrDefault<ExploreItemViewModel>((Func<ExploreItemViewModel, bool>) (x => x.Equals(downloadProcess.Uid, downloadProcess.Language)));
      if (exploreItemViewModel == null)
        return;
      exploreItemViewModel.IsDownloaded = downloadProcess.State == DownloadProcessState.Downloaded || downloadProcess.State == DownloadProcessState.Updated;
      exploreItemViewModel.RefreshStatus();
    }

    private void OnIsPurchasedChanged(string uid, bool isPurchased)
    {
      if (string.IsNullOrWhiteSpace(uid))
        return;
      ExploreItemViewModel exploreItemViewModel = this.ListItems.FirstOrDefault<ExploreItemViewModel>((Func<ExploreItemViewModel, bool>) (x => x.Equals(uid)));
      if (exploreItemViewModel == null)
        return;
      exploreItemViewModel.IsPurchased = isPurchased;
      exploreItemViewModel.RefreshStatus();
    }
  }
}
