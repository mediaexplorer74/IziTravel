using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
// Alias the Windows.UI.Xaml.Controls.Maps.Map to avoid conflict with our custom Map class
using WindowsMap = Windows.UI.Xaml.Controls.Maps;
using Izi.Travel.Shell.Toolkit.Controls.Maps;
using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Commands;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.Interfaces;
using Izi.Travel.Shell.Mtg.Messages;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Map;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.Flyouts;
using Izi.Travel.Utility.Extensions;
using Windows.UI.Xaml.Controls.Maps;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Tour.Map
{
    public class TourMapPartViewModel : Screen, IHandle<MapItemSelectedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMapService _mapService;
        private readonly CoreDispatcher _dispatcher;
        private MtgObject _mtgObject;
        private bool _isBusy;
        private Geopoint _center;
        private double _zoomLevel = 12;
        //private Windows.Phone.Map _map;
        private ObservableCollection<BaseMapItemViewModel> _items = new ObservableCollection<BaseMapItemViewModel>();
        private BaseMapItemViewModel _selectedItem;
        private RelayCommand _mapTappedCommand;
        private RelayCommand _mapItemClickCommand;
        private RelayCommand _getDirectionsCommand;
        private NowPlayingCommand _nowPlayingCommand;
        private RelayCommand _startTourCommand;
        private RelayCommand _pauseTourCommand;
        private RelayCommand _stopTourCommand;
        private RelayCommand _toggleTriggerZoneModeCommand;
        private RelayCommand _openListCommand;
        public object Map;
        public BaseCommand MapZoomLevelChangedCommand;
        public BaseCommand MapCenterChangedCommand;
        internal Geopoint ViewCenter;
        private readonly List<MapElement> _triggerZones = new List<MapElement>();
        private readonly ObservableCollection<MapElement> _elements = new ObservableCollection<MapElement>();

        // Commands
        public ICommand MapTappedCommand => _mapTappedCommand ??= new RelayCommand(OnMapTapped);
        public ICommand MapItemClickCommand => _mapItemClickCommand ??= new RelayCommand(OnMapItemClicked);

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public Geopoint Center
        {
            get => _center;
            set => Set(ref _center, value);
        }

        public double ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                //if (Set(ref _zoomLevel, value) && _map != null)
                //{
                //    _map.ZoomLevel = value;
                //}
            }
        }

        //public Map Map => _map;

        public ObservableCollection<BaseMapItemViewModel> Items => _items;

        public BaseMapItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Set(ref _selectedItem, value))
                {
                    if (value != null)
                    {
                        Center = value.Location;
                        // Notify other parts of the app about the selection
                        _eventAggregator?.PublishOnUIThreadAsync(new MapItemSelectedMessage(value));
                    }
                }
            }
        }

        public TourMapPartViewModel(
            IEventAggregator eventAggregator,
            IMapService mapService,
            CoreDispatcher dispatcher)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

            // Initialize the map with default values
            var defaultLocation = new Geopoint(new BasicGeoposition
            {
                Latitude = 51.5074, // Default to London
                Longitude = -0.1278
            });
            
            _center = defaultLocation;
            //_map = new Map(null); // Will be initialized with actual MapControl in InitializeMap
            //_map.ZoomLevel = _zoomLevel;
            //_map.Center = _center;

            // Subscribe to messages
            _eventAggregator.Subscribe(this);
        }

        // Design-time constructor
        public TourMapPartViewModel() : this(null, null, null)
        {
            // This constructor is only for design-time support
#if DEBUG
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                throw new InvalidOperationException("This constructor is for design-time use only");
            }
            
            // Initialize with sample data for design-time
            _items = new ObservableCollection<BaseMapItemViewModel>
            {
                // Add sample items for design-time
            };
            
            // Initialize map for design-time
            var defaultLocation = new Geopoint(new BasicGeoposition { Latitude = 51.5074, Longitude = -0.1278 });
            _center = defaultLocation;
            //_map = new Map(null)
            //{
            //    ZoomLevel = _zoomLevel,
            //    Center = _center
            //};
#endif
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            
            // Initialize data if we have a valid service
            if (_mapService != null && !string.IsNullOrEmpty(Uid) && !string.IsNullOrEmpty(Language))
            {
                try
                {
                    await LoadMapDataAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error in OnActivate: {ex.Message}");
                }
            }
            
            // Initialize tour playback and other components
            //this.RefreshCommands();
            this.PerformItemsAction((Action<TourMapItemViewModel>)(x => x.Activate()));
            TourPlaybackManager.Instance.PositionStatusChanged += OnTourPlaybackPositionStatusChanged;
            TourPlaybackManager.Instance.PositionChanged += OnTourPlaybackPositionChanged;
            TourPlaybackManager.Instance.TourPlaybackStateChanged += OnTourPlaybackStateChanged;
            TourPlaybackManager.Instance.TourPlaybackTriggerZoneStateChanged += OnTourPlaybackTriggerZoneStateChanged;
            TourPlaybackManager.Instance.TourPlaybackAttractionIsPlayingChanged += OnTourPlaybackAttractionIsPlayingChanged;
            TourPlaybackManager.Instance.TourPlaybackAttractionIsVisitedChanged += OnTourPlaybackAttractionIsVisitedChanged;
            PurchaseManager.Instance.IsPurchasedChanged += PurchaseManagerIsPurchasedChanged;
            this.TrySelectTouristAttraction();
            this.OpenListCommand.RaiseCanExecuteChanged();
            
            // Ensure map is properly initialized
            //if (_map != null && _center != null)
            //{
            //    _map.Center = _center;
            //    _map.ZoomLevel = _zoomLevel;
            //}
        }

        protected override void OnDeactivate(bool close)
        {
            // Clean up resources
            //if (_map != null)
            //{
            //    _map.ZoomLevelChanged -= OnMapZoomLevelChanged;
            //}
            
            // Unsubscribe from events
            if (TourPlaybackManager.Instance != null)
            {
                TourPlaybackManager.Instance.PositionStatusChanged -= OnTourPlaybackPositionStatusChanged;
                TourPlaybackManager.Instance.PositionChanged -= OnTourPlaybackPositionChanged;
                TourPlaybackManager.Instance.TourPlaybackStateChanged -= OnTourPlaybackStateChanged;
                TourPlaybackManager.Instance.TourPlaybackTriggerZoneStateChanged -= OnTourPlaybackTriggerZoneStateChanged;
                TourPlaybackManager.Instance.TourPlaybackAttractionIsPlayingChanged -= OnTourPlaybackAttractionIsPlayingChanged;
                TourPlaybackManager.Instance.TourPlaybackAttractionIsVisitedChanged -= OnTourPlaybackAttractionIsVisitedChanged;
            }
            
            if (PurchaseManager.Instance != null)
            {
                PurchaseManager.Instance.IsPurchasedChanged -= PurchaseManagerIsPurchasedChanged;
            }
            
            base.OnDeactivate(close);
        }

        #region Map Initialization
        
        public void InitializeMap(Toolkit.Controls.Maps.MapControl mapControl)
        {
            if (mapControl == null) return;
            
            // Initialize the map with the actual MapControl
            //_map = new Map(mapControl)
            //{
            //    ZoomLevel = _zoomLevel,
            //    Center = _center
            //};

            // Subscribe to map events
            //_map.ZoomLevelChanged += OnMapZoomLevelChanged;
            
            // Initialize map with any existing items
            if (_items != null && _items.Count > 0)
            {
                foreach (var item in _items)
                {
                    // TODO: Add items to the map
                    // This will depend on how your map items are structured
                }
            }
        }
        
        private void OnMapZoomLevelChanged(object sender, MapZoomLevelChangedEventArgs e)
        {
            // Update the view model's ZoomLevel when the map's zoom changes
            if (Math.Abs(_zoomLevel - e.NewZoomLevel) > 0.1) // Small threshold to avoid unnecessary updates
            {
                _zoomLevel = e.NewZoomLevel;
                NotifyOfPropertyChange(() => ZoomLevel);
            }
        }
        
        #endregion

        #region IHandle<MapItemSelectedMessage> Implementation

        public void Handle(MapItemSelectedMessage message)
        {
            if (message?.MapItem != null)
            {
                // Handle the map item selection
                var selectedItem = Items?.FirstOrDefault(i => i.Uid == message.MapItem.Uid);
                if (selectedItem != null)
                {
                    // Handle the selected item
                    SelectedItem = selectedItem;
                    
                    // Center the map on the selected item
                    //if (_map != null && selectedItem.Location != null)
                    //{
                    //    _map.Center = selectedItem.Location;
                    //}
                }
            }
        }

        #endregion

        private async Task LoadMapDataAsync()
        {
            try
            {
                await SetBusyAsync(true);

                if (_mapService == null)
                {
                    Debug.WriteLine("Map service is not available");
                    return;
                }

                // Load map items asynchronously
                var items = await _mapService.GetMapItemsForTourAsync(Uid, Language);

                // Update the collection on the UI thread
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try
                    {
                        _items.Clear();
                        foreach (var item in items)
                        {
                            _items.Add(item);
                        }

                        // Set initial view if we have items
                        if (items.Any())
                        {
                            Center = items.First().Location;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Debug.WriteLine($"Error updating map items: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., show message to user)
                Debug.WriteLine($"Error loading map data: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SetBusyAsync(bool isBusy)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IsBusy = isBusy;
            });
        }

        private async void OnMapTapped(object parameter)
        {
            if (parameter is Geopoint location)
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    // Handle map tap (e.g., clear selection)
                    SelectedItem = null;
                });
            }
        }

        private async void OnMapItemClicked(object parameter)
        {
            if (parameter is BaseMapItemViewModel item)
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    SelectedItem = item;

                    // Optional: Add animation or navigation logic here
                    // For example, zoom to the selected item
                    if (item.Location != null)
                    {
                        Center = item.Location;
                    }
                });
            }
        }

        public string Uid { get; set; }

        public string Language { get; set; }

        // Helper method to convert GeoCoordinate to BasicGeoposition
        protected BasicGeoposition ConvertToBasicGeoposition(double latitude, double longitude)
        {
            return new BasicGeoposition
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }
        
        // Helper method to create Geopoint from latitude and longitude
        protected Geopoint CreateGeopoint(double latitude, double longitude)
        {
            var position = new BasicGeoposition { Latitude = latitude, Longitude = longitude };
            return new Geopoint(position);
        }

        public MtgObject MtgObject
        {
            get => this._mtgObject;
            set
            {
                this.SetProperty<MtgObject, string>(ref this._mtgObject, value, (Expression<Func<string>>) (() => this.StartTourLabel), propertyName: nameof (MtgObject));
            }
        }

        public ObservableCollection<MapElement> Elements => _elements;

        // SelectedItem is already defined above with proper property change notification

        public bool IsStartTourButtonVisible
        {
            get
            {
                return !TourPlaybackManager.IsTourAttached(this.Uid, this.Language) || TourPlaybackManager.Instance.TourPlaybackState != TourPlaybackState.Started;
            }
        }

        public bool IsPauseTourButtonVisible
        {
            get
            {
                return TourPlaybackManager.IsTourAttached(this.Uid, this.Language) && TourPlaybackManager.Instance.TourPlaybackState == TourPlaybackState.Started;
            }
        }

        public bool IsStopTourButtonVisible
        {
            get
            {
                return TourPlaybackManager.IsTourAttached(this.Uid, this.Language) && TourPlaybackManager.Instance.TourPlayback.Attractions.Any<TourPlaybackAttraction>((Func<TourPlaybackAttraction, bool>) (x => x.IsVisited)) && TourPlaybackManager.Instance.TourPlaybackState == TourPlaybackState.Paused;
            }
        }

        public string StartTourLabel
        {
            get
            {
                return !PurchaseManager.Instance.IsPurchased(this.MtgObject) ? string.Format(AppResources.CommandBuyForToStart, (object) this.MtgObject.Purchase.PriceString) : AppResources.CommandStart;
            }
        }

        // Constructor moved up

        public RelayCommand GetDirectionsCommand
        {
            get
            {
                return this._getDirectionsCommand ?? (this._getDirectionsCommand = new RelayCommand(new Action<object>(this.GetDirections)));
            }
        }

        private void GetDirections(object o)
        {
            MtgObject mtgObject = this.SelectedItem == null || this.SelectedItem.MtgObject == null ? this.MtgObject : this.SelectedItem.MtgObject;
            if (mtgObject == null)
                return;
            mtgObject.ShowMapDirectionsTask();
        }

        public NowPlayingCommand NowPlayingCommand
        {
            get
            {
                return this._nowPlayingCommand ?? (this._nowPlayingCommand = new NowPlayingCommand((IScreen) this));
            }
        }

        public RelayCommand StartTourCommand
        {
            get
            {
                return this._startTourCommand ?? (this._startTourCommand = new RelayCommand(new Action<object>(this.ExecuteStartTourCommand), new Func<object, bool>(this.CanExecuteStartTourCommand)));
            }
        }

        private bool CanExecuteStartTourCommand(object parameter) => this.IsStartTourButtonVisible;

        private async void ExecuteStartTourCommand(object parameter)
        {
            if (!PurchaseFlyoutDialog.ConditionalShow(this.MtgObject))
                return;
            if (!await DialogHelper.CheckForLocationServices())
                return;
            //if (this.FlyoutStartViewModel.IsEnabled && this.FlyoutStartViewModel.OpenCommand.CanExecute((object) null) && !TourPlaybackManager.IsTourAttached(this.Uid, this.Language))
            //    this.FlyoutStartViewModel.OpenCommand.Execute((object) null);
            //else
                this.StartTour();
        }

        public RelayCommand PauseTourCommand
        {
            get
            {
                return this._pauseTourCommand ?? (this._pauseTourCommand = new RelayCommand(new Action<object>(this.ExecutePauseTourCommand), new Func<object, bool>(this.CanExecutePauseTourCommand)));
            }
        }

        private bool CanExecutePauseTourCommand(object parameter) => this.IsPauseTourButtonVisible;

        private void ExecutePauseTourCommand(object parameter)
        {
            ShellServiceFacade.DialogService.Show(AppResources.LabelTour, AppResources.PromptTourStop, MessageBoxButtonContent.YesNo, (Action<FlyoutDialog, MessageBoxResult>) ((d, x) =>
            {
                if (x != MessageBoxResult.Yes)
                    return;
                TourPlaybackManager.Instance.Pause();
            }));
        }

        public RelayCommand StopTourCommand
        {
            get
            {
                return this._stopTourCommand ?? (this._stopTourCommand = new RelayCommand(new Action<object>(this.ExecuteStopTourCommand), new Func<object, bool>(this.CanExecuteStopTourCommand)));
            }
        }

        private bool CanExecuteStopTourCommand(object parameter) => this.IsStopTourButtonVisible;

        private void ExecuteStopTourCommand(object parameter)
        {
            ShellServiceFacade.DialogService.Show(AppResources.LabelTour, AppResources.PromptTourReset, MessageBoxButtonContent.YesNo, (Action<FlyoutDialog, MessageBoxResult>) ((d, x) =>
            {
                if (x != MessageBoxResult.Yes)
                    return;
                TourPlaybackManager.Instance.Stop();
            }));
        }

        public RelayCommand ToggleTriggerZoneModeCommand
        {
            get
            {
                return this._toggleTriggerZoneModeCommand ?? (this._toggleTriggerZoneModeCommand = new RelayCommand(new Action<object>(this.ExecuteToggleTriggerZoneModeCommand)));
            }
        }

        private void ExecuteToggleTriggerZoneModeCommand(object parameter)
        {
            if (this._triggerZones == null)
                return;
            foreach (MapElement triggerZone in this._triggerZones)
            {
                //if (!this._isTriggerZoneVisible)
                //    this.Elements.Add(triggerZone);
                //else
                    this.Elements.Remove(triggerZone);
            }
            //this._isTriggerZoneVisible = !this._isTriggerZoneVisible;
            this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.ToggleTriggerZoneModeLabel));
        }

        public string ToggleTriggerZoneModeLabel
        {
            get
            {
                return default;//!this._isTriggerZoneVisible ? AppResources.CommandShowTriggerZones : AppResources.CommandHideTriggerZones;
            }
        }

        public RelayCommand OpenListCommand
        {
            get
            {
                return this._openListCommand ?? (this._openListCommand = new RelayCommand(new Action<object>(this.ExecuteOpenListCommand), new Func<object, bool>(this.CanExecuteOpenListCommand)));
            }
        }

        private bool CanExecuteOpenListCommand(object parameter) => !this.IsBusy;

        private void ExecuteOpenListCommand(object parameter)
        {
            //this.FlyoutAttractionListViewModel.IsOpen = true;
        }

        // OnActivate implementation is already defined above with additional logic

    private void TrySelectTouristAttraction()
    {
      /*if (TourMapPartViewModel._selectedTouristAttractionUid == null || this.Items.Count == 0)
        return;
      List<TourMapItemViewModel> list = this.Items.OfType<TourMapItemViewModel>().ToList<TourMapItemViewModel>();
      TourMapItemViewModel selectedItem = list.FirstOrDefault<TourMapItemViewModel>((Func<TourMapItemViewModel, bool>) (x => x.Uid == TourMapPartViewModel._selectedTouristAttractionUid));
      if (selectedItem == null)
        return;
      list.ForEach((Action<TourMapItemViewModel>) (x =>
      {
        TourMapItemViewModel mapItemViewModel = x;
        mapItemViewModel.IsSelected = mapItemViewModel == selectedItem;
      }));
      TourMapPartViewModel._selectedTouristAttractionUid = (string) null;*/
    }

    public void StartTour()
    {
      AnalyticsHelper.SetStartTour(this.MtgObject);
      if (!TourPlaybackManager.Instance.Initialize(this.MtgObject))
        return;
      TourPlaybackManager.Instance.Start();
    }

    public void StopTour()
    {
      if (!TourPlaybackManager.IsTourAttached(this.Uid, this.Language))
        return;
      TourPlaybackManager.Instance.Stop();
    }

    private void PerformItemsAction(Action<TourMapItemViewModel> action)
    {
      if (action == null)
        return;
      foreach (TourMapItemViewModel mapItemViewModel in this.Items.OfType<TourMapItemViewModel>())
        action(mapItemViewModel);
    }

    private void RefreshMapAttractionItem(TourPlaybackAttraction attraction)
    {
      this.GetTourMapAttractionItem(attraction.Uid)?.RefreshAttractionState();
    }

    private TourMapItemViewModel GetTourMapAttractionItem(string uid)
    {
      return string.IsNullOrWhiteSpace(uid) ? (TourMapItemViewModel) null : this.Items.FirstOrDefault<BaseMapItemViewModel>((Func<BaseMapItemViewModel, bool>) (x => string.Equals(x.Uid, uid, StringComparison.CurrentCultureIgnoreCase))) as TourMapItemViewModel;
    }

    private void PurchaseManagerIsPurchasedChanged(string sender, bool args)
    {
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.StartTourLabel));
    }

    private async void OnTourPlaybackPositionStatusChanged(
      TourPlaybackManager sender,
      PositionStatus status)
    {
      if (status != PositionStatus.Disabled && status != PositionStatus.NotAvailable)
        return;
      if (await DialogHelper.CheckForLocationServices())
        return;
      this.StopTour();
    }

    //private void OnTourPlaybackPositionChanged(TourPlaybackManager manager, BasicGeoposition position)
    private void OnTourPlaybackPositionChanged(TourPlaybackManager sender, Geolocation args)
    {
      //this.UserLocation = position;
    }

    private void OnTourPlaybackStateChanged(TourPlaybackManager manager, EventArgs args)
    {
      //this.RefreshCommands();
      this.PerformItemsAction((Action<TourMapItemViewModel>) (x => x.RefreshAttractionState()));
    }

    private void OnTourPlaybackTriggerZoneStateChanged(
      TourPlaybackTriggerZone triggerZone,
      EventArgs args)
    {
      if (!TourPlaybackManager.IsTourAttached(this.Uid, this.Language) || triggerZone.State != TourPlaybackTriggerZoneState.Entered)
        return;
      BaseMapItemViewModel mapItemViewModel = this.Items.FirstOrDefault<BaseMapItemViewModel>((Func<BaseMapItemViewModel, bool>) (x => x.Uid == triggerZone.TourAttraction.Uid));
      if (mapItemViewModel == null)
        return;
      mapItemViewModel.IsSelected = true;
    }

    private void OnTourPlaybackAttractionIsPlayingChanged(
      TourPlaybackAttraction attraction,
      EventArgs args)
    {
      this.RefreshMapAttractionItem(attraction);
    }

    private void OnTourPlaybackAttractionIsVisitedChanged(
      TourPlaybackAttraction attraction,
      EventArgs args)
    {
      this.RefreshMapAttractionItem(attraction);
    }

    public static void Navigate(string tourUid, string tourLanguage, string touristAttractionUid = null)
    {
      ShellServiceFacade.NavigationService.UriFor<TourMapPartViewModel>()
        .WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Uid), tourUid)
        .WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Language), tourLanguage)
        .Navigate();
      //if (touristAttractionUid != null)
      //  TourMapPartViewModel._selectedTouristAttractionUid = touristAttractionUid;
    }
  }
}

