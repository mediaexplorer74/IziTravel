// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.TourMapPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

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
using Izi.Travel.Shell.Mtg.ViewModels.Common.Map;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.Flyouts;
using Izi.Travel.Utility.Extensions;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Tour.Map
{
  public class TourMapPartViewModel : BaseMapViewModel
  {
    private MtgObject _mtgObject;
    private TourMapFlyoutAttractionListViewModel _flyoutAttractionListViewModel;
    private TourMapFlyoutStartViewModel _flyoutStartViewModel;
    private bool _isTriggerZoneVisible;
    private readonly List<MapElement> _triggerZones;
    private readonly ObservableCollection<MapElement> _elements;
    private RelayCommand _getDirectionsCommand;
    private NowPlayingCommand _nowPlayingCommand;
    private RelayCommand _startTourCommand;
    private RelayCommand _pauseTourCommand;
    private RelayCommand _stopTourCommand;
    private RelayCommand _toggleTriggerZoneModeCommand;
    private RelayCommand _openListCommand;
    private static string _selectedTouristAttractionUid;

    public TourMapFlyoutAttractionListViewModel FlyoutAttractionListViewModel
    {
      get
      {
        return this._flyoutAttractionListViewModel ?? (this._flyoutAttractionListViewModel = new TourMapFlyoutAttractionListViewModel(this));
      }
    }

    public TourMapFlyoutStartViewModel FlyoutStartViewModel
    {
      get
      {
        return this._flyoutStartViewModel ?? (this._flyoutStartViewModel = new TourMapFlyoutStartViewModel(this));
      }
    }

    public string Uid { get; set; }

    public string Language { get; set; }

    public MtgObject MtgObject
    {
      get => this._mtgObject;
      set
      {
        this.SetProperty<MtgObject, string>(ref this._mtgObject, value, (Expression<Func<string>>) (() => this.StartTourLabel), propertyName: nameof (MtgObject));
      }
    }

    public ObservableCollection<MapElement> Elements => this._elements;

    public BaseMapItemViewModel SelectedItem
    {
      get
      {
        return this.Items.FirstOrDefault<BaseMapItemViewModel>((Func<BaseMapItemViewModel, bool>) (x => x.IsSelected));
      }
      set
      {
        if (value == null)
          return;
        value.IsSelected = true;
      }
    }

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

    public TourMapPartViewModel()
    {
      this._triggerZones = new List<MapElement>();
      this._elements = new ObservableCollection<MapElement>();
    }

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
      if (this.FlyoutStartViewModel.IsEnabled && this.FlyoutStartViewModel.OpenCommand.CanExecute((object) null) && !TourPlaybackManager.IsTourAttached(this.Uid, this.Language))
        this.FlyoutStartViewModel.OpenCommand.Execute((object) null);
      else
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
        if (!this._isTriggerZoneVisible)
          this.Elements.Add(triggerZone);
        else
          this.Elements.Remove(triggerZone);
      }
      this._isTriggerZoneVisible = !this._isTriggerZoneVisible;
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.ToggleTriggerZoneModeLabel));
    }

    public string ToggleTriggerZoneModeLabel
    {
      get
      {
        return !this._isTriggerZoneVisible ? AppResources.CommandShowTriggerZones : AppResources.CommandHideTriggerZones;
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
      this.FlyoutAttractionListViewModel.IsOpen = true;
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.RefreshCommands();
      this.PerformItemsAction((Action<TourMapItemViewModel>) (x => x.Activate()));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.PositionStatusChanged += new TypedEventHandler<TourPlaybackManager, PositionStatus>((object) this, __methodptr(OnTourPlaybackPositionStatusChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.PositionChanged += new TypedEventHandler<TourPlaybackManager, GeoCoordinate>((object) this, __methodptr(OnTourPlaybackPositionChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackStateChanged += new TypedEventHandler<TourPlaybackManager, EventArgs>((object) this, __methodptr(OnTourPlaybackStateChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackTriggerZoneStateChanged += new TypedEventHandler<TourPlaybackTriggerZone, EventArgs>((object) this, __methodptr(OnTourPlaybackTriggerZoneStateChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackAttractionIsPlayingChanged += new TypedEventHandler<TourPlaybackAttraction, EventArgs>((object) this, __methodptr(OnTourPlaybackAttractionIsPlayingChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackAttractionIsVisitedChanged += new TypedEventHandler<TourPlaybackAttraction, EventArgs>((object) this, __methodptr(OnTourPlaybackAttractionIsVisitedChanged));
      // ISSUE: method pointer
      PurchaseManager.Instance.IsPurchasedChanged += new TypedEventHandler<string, bool>((object) this, __methodptr(PurchaseManagerIsPurchasedChanged));
      this.TrySelectTouristAttraction();
    }

    protected override void OnDeactivate(bool close)
    {
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackAttractionIsVisitedChanged -= new TypedEventHandler<TourPlaybackAttraction, EventArgs>((object) this, __methodptr(OnTourPlaybackAttractionIsVisitedChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackAttractionIsPlayingChanged -= new TypedEventHandler<TourPlaybackAttraction, EventArgs>((object) this, __methodptr(OnTourPlaybackAttractionIsPlayingChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackTriggerZoneStateChanged -= new TypedEventHandler<TourPlaybackTriggerZone, EventArgs>((object) this, __methodptr(OnTourPlaybackTriggerZoneStateChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.TourPlaybackStateChanged -= new TypedEventHandler<TourPlaybackManager, EventArgs>((object) this, __methodptr(OnTourPlaybackStateChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.PositionChanged -= new TypedEventHandler<TourPlaybackManager, GeoCoordinate>((object) this, __methodptr(OnTourPlaybackPositionChanged));
      // ISSUE: method pointer
      TourPlaybackManager.Instance.PositionStatusChanged -= new TypedEventHandler<TourPlaybackManager, PositionStatus>((object) this, __methodptr(OnTourPlaybackPositionStatusChanged));
      // ISSUE: method pointer
      PurchaseManager.Instance.IsPurchasedChanged -= new TypedEventHandler<string, bool>((object) this, __methodptr(PurchaseManagerIsPurchasedChanged));
      this.PerformItemsAction((Action<TourMapItemViewModel>) (x => x.Deactivate()));
      base.OnDeactivate(close);
    }

    protected override async Task OnInitializeProcessAsync()
    {
      Izi.Travel.Geofencing.Primitives.Geolocation position1 = Geotracker.Instance.Position;
      if (position1 != null)
        this.Center = position1.ToGeoCoordinate();
      string[] languages;
      if (string.IsNullOrWhiteSpace(this.Language))
        languages = ServiceFacade.SettingsService.GetAppSettings().Languages;
      else
        languages = new string[1]{ this.Language };
      MtgObjectFilter filter = new MtgObjectFilter(this.Uid, languages);
      filter.Includes = ContentSection.Children;
      this.MtgObject = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
      if (this.MtgObject == null)
        return;
      Content content = this.MtgObject.MainContent;
      if (content == null)
        return;
      Izi.Travel.Geofencing.Primitives.Geolocation position2 = await Geotracker.Instance.GetPosition();
      if (position2 != null)
        this.UserLocation = position2.ToGeoCoordinate();
      this.Items.Clear();
      if (content.Children != null && content.Children.Length != 0)
      {
        string[] order = this.MtgObject.MainContent.Playback != null ? this.MtgObject.MainContent.Playback.Order : new string[0];
        List<MtgObject> list = ((IEnumerable<MtgObject>) content.Children).Where<MtgObject>((Func<MtgObject, bool>) (x => x.Type == MtgObjectType.TouristAttraction)).OrderBy<MtgObject, int>((Func<MtgObject, int>) (x => Array.IndexOf<string>(order, x.Uid))).ToList<MtgObject>();
        List<MtgObject> visibleAttractions = list.Where<MtgObject>((Func<MtgObject, bool>) (x => !x.Hidden)).ToList<MtgObject>();
        this.Items.AddRange<BaseMapItemViewModel>((IEnumerable<BaseMapItemViewModel>) list.Select<MtgObject, TourMapItemViewModel>((Func<MtgObject, TourMapItemViewModel>) (x => new TourMapItemViewModel(this, x, visibleAttractions.IndexOf(x) + 1))));
        this.FlyoutAttractionListViewModel.Items.AddRange<BaseMapItemViewModel>((IEnumerable<BaseMapItemViewModel>) visibleAttractions.Select<MtgObject, TourMapItemViewModel>((Func<MtgObject, TourMapItemViewModel>) (x => new TourMapItemViewModel(this, x, visibleAttractions.IndexOf(x) + 1))));
        this._triggerZones.Clear();
        this._triggerZones.AddRange(TourHelper.CreateTriggerZoneMapElements((IEnumerable<MtgObject>) list));
      }
      this.Elements.Clear();
      MapElement routeMapElement = TourHelper.CreateRouteMapElement(this.MtgObject);
      if (routeMapElement != null)
        this.Elements.Add(routeMapElement);
      if (this.Items.Count > 0)
        this.LocationRect = LocationRectangle.CreateBoundingRectangle(this.Items.Select<BaseMapItemViewModel, GeoCoordinate>((Func<BaseMapItemViewModel, GeoCoordinate>) (x => x.Location)));
      this.PerformItemsAction((Action<TourMapItemViewModel>) (x => x.Activate()));
      this.PerformItemsAction((Action<TourMapItemViewModel>) (x => x.RefreshAttractionState()));
      this.TrySelectTouristAttraction();
    }

    protected override void RefreshCommands()
    {
      base.RefreshCommands();
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsStartTourButtonVisible));
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsPauseTourButtonVisible));
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsStopTourButtonVisible));
      this.StartTourCommand.RaiseCanExecuteChanged();
      this.PauseTourCommand.RaiseCanExecuteChanged();
      this.StopTourCommand.RaiseCanExecuteChanged();
      this.OpenListCommand.RaiseCanExecuteChanged();
    }

    private void TrySelectTouristAttraction()
    {
      if (TourMapPartViewModel._selectedTouristAttractionUid == null || this.Items.Count == 0)
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
      TourMapPartViewModel._selectedTouristAttractionUid = (string) null;
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
      if (status != 3 && status != 5)
        return;
      if (await DialogHelper.CheckForLocationServices())
        return;
      this.StopTour();
    }

    private void OnTourPlaybackPositionChanged(TourPlaybackManager manager, GeoCoordinate position)
    {
      this.UserLocation = position;
    }

    private void OnTourPlaybackStateChanged(TourPlaybackManager manager, EventArgs args)
    {
      this.RefreshCommands();
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
      Uri uri = ShellServiceFacade.NavigationService.UriFor<TourMapPartViewModel>().WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Uid), tourUid).WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Language), tourLanguage).BuildUri();
      if (ShellServiceFacade.NavigationService.CurrentSource.OriginalString != uri.OriginalString)
      {
        int index1 = ShellServiceFacade.NavigationService.BackStack.ToList<JournalEntry>().FindIndex((Predicate<JournalEntry>) (x => x.Source.OriginalString == uri.OriginalString));
        if (index1 < 0)
        {
          ShellServiceFacade.NavigationService.Navigate(uri);
        }
        else
        {
          for (int index2 = 0; index2 < index1; ++index2)
            ShellServiceFacade.NavigationService.RemoveBackEntry();
          ShellServiceFacade.NavigationService.GoBack();
        }
      }
      if (touristAttractionUid == null)
        return;
      TourMapPartViewModel._selectedTouristAttractionUid = touristAttractionUid;
    }
  }
}
