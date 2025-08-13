using Izi.Travel.Core.Components.Extensions;
using Izi.Travel.Mtg.ViewModels.Tour.Map;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using Izi.Travel.Core.Components.Behaviors;
using Izi.Travel.Data.Entities.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls; // For SelectionChangedEventArgs
using Windows.UI.Xaml.Controls.Primitives; // For Flyout
// ReactiveUI

namespace Izi.Travel.Mtg.Views.Tour
{
    public partial class TourListView
    {
        private MapViewBehavior _mapViewBehavior;
        private Flyout _currentFlyout;
        private IDisposable _flyoutSubscription;

        public TourListView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            
            // Subscribe to view model events for flyout management
            DataContextChanged += OnDataContextChanged;
        }
        
        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is TourMapPartViewModel viewModel)
            {
                // Unsubscribe from previous view model if exists
                _flyoutSubscription?.Dispose();
                
                // Commented out ReactiveUI code that's causing build errors
                // TODO: Reimplement with proper ViewModel properties when available
                /*
                _flyoutSubscription = viewModel.WhenAnyValue(vm => vm.FlyoutAttractionListViewModel.IsOpen)
                    .Subscribe(ShowAttractionListFlyout);
                
                // TODO: Add similar subscriptions for other flyouts
                // viewModel.WhenAnyValue(vm => vm.FlyoutStartViewModel.IsOpen).Subscribe(ShowStartFlyout);
                // viewModel.WhenAnyValue(vm => vm.FlyoutRouteViewModel.IsOpen).Subscribe(ShowRouteFlyout);
                */
            }
        }
        
        private void ShowAttractionListFlyout(bool show)
        {
            if (show)
            {
                var flyout = (Flyout)FlyoutBase.GetAttachedFlyout(this);
                if (flyout != null)
                {
                    _currentFlyout = flyout;
                    flyout.Closed += OnFlyoutClosed;
                    flyout.ShowAt(this);
                }
            }
            else if (_currentFlyout != null)
            {
                _currentFlyout.Hide();
            }
        }
        
        private void OnFlyoutClosed(object sender, object e)
        {
            if (sender is Flyout flyout)
            {
                flyout.Closed -= OnFlyoutClosed;
                _currentFlyout = null;
                
                // Notify view model that flyout was closed
                // TODO: Reimplement with proper ViewModel properties when available
                /*
                if (DataContext is TourMapPartViewModel viewModel)
                {
                    viewModel.FlyoutAttractionListViewModel.IsOpen = false;
                    viewModel.FlyoutAttractionListViewModel.ClosedCommand?.Execute(null);
                }
                */
            }
        }

        private void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Initialize map view behavior
            _mapViewBehavior = new MapViewBehavior
            {
                ZoomDesiredMargin = new Windows.UI.Xaml.Thickness(20)
            };
            
            // Set up map interactions
            if (Map.MapControl != null)
            {
                // Initialize map with default view if needed
                if (Map.MapControl.Center == null && DataContext is TourMapPartViewModel viewModel && viewModel.Center != null)
                {
                    Map.MapControl.ZoomLevel = viewModel.ZoomLevel;
                    Map.MapControl.Center = new Geopoint(new BasicGeoposition
                    {
                        Latitude = default,//viewModel.Center.Latitude,
                        Longitude = default//viewModel.Center.Longitude
                    });
                }

                // Subscribe to map events
                Map.MapControl.ZoomLevelChanged += OnMapZoomLevelChanged;
                Map.MapControl.CenterChanged += OnMapCenterChanged;
            }
        }

        private void OnMapItemClick(object sender, MapElementClickEventArgs args)
        {
            if (args.MapElements.Count > 0 && DataContext is TourMapPartViewModel viewModel)
            {
                // Handle map item click
                //var mapItem = args.MapElements[0] as MapItemsControl;
                //if (mapItem?.DataContext != null)
                //{
                //    viewModel.MapItemClickCommand?.Execute(mapItem.DataContext);
                //}
            }
        }

        private void OnMapZoomLevelChanged(MapControl sender, object args)
        {
            if (DataContext is TourMapPartViewModel viewModel)
            {
                viewModel.ZoomLevel = sender.ZoomLevel;
                viewModel.MapZoomLevelChangedCommand?.Execute(sender.ZoomLevel);
            }
        }

        private void OnMapCenterChanged(MapControl sender, object args)
        {
            if (DataContext is TourMapPartViewModel viewModel && sender.Center != null)
            {
                viewModel.Center = new Geopoint(/*sender.Center.Position.Latitude*/default, /*sender.Center.Position.Longitude*/0);
                viewModel.MapCenterChangedCommand?.Execute(viewModel.Center);
            }
        }

        private void OnUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Clean up event handlers
            if (Map?.MapControl != null)
            {
                Map.MapControl.ZoomLevelChanged -= OnMapZoomLevelChanged;
                Map.MapControl.CenterChanged -= OnMapCenterChanged;
            }
            
            // Clean up flyout
            if (_currentFlyout != null)
            {
                _currentFlyout.Closed -= OnFlyoutClosed;
                _currentFlyout = null;
            }
            
            // Clean up subscriptions
            _flyoutSubscription?.Dispose();
            _flyoutSubscription = null;
            
            // Unsubscribe from events
            DataContextChanged -= OnDataContextChanged;
            Loaded -= OnLoaded;
            Unloaded -= OnUnloaded;
        }
        
        private void OnItemTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // Handle item tap if needed
            e.Handled = true;
        }
        
        private void OnAttractionSelected(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Reimplement with proper ViewModel properties when available
            /*
            if (e.AddedItems?.Count > 0 && DataContext is TourMapPartViewModel viewModel)
            {
                var selectedItem = e.AddedItems[0];
                viewModel.FlyoutAttractionListViewModel?.ItemSelectedCommand?.Execute(selectedItem);
                
                // Reset selection to allow re-selecting the same item
                if (sender is ListView listView)
                {
                    listView.SelectedItem = null;
                }
            }
            */
        }
        
        private void OnRouteItemSelected(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Reimplement with proper ViewModel properties when available
            /*
            if (e.AddedItems?.Count > 0 && DataContext is TourMapPartViewModel viewModel)
            {
                var selectedItem = e.AddedItems[0];
                viewModel.FlyoutRouteViewModel?.ItemSelectedCommand?.Execute(selectedItem);
                
                // Reset selection to allow re-selecting the same item
                if (sender is ListView listView)
                {
                    listView.SelectedItem = null;
                }
            }
            */
        }
        
        private void OnListItemTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is TourMapItemViewModel itemViewModel)
            {
                itemViewModel.NavigateCommand?.Execute(null);
                e.Handled = true;
            }
        }
        
        private void OnRouteItemTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is TourMapItemViewModel itemViewModel)
            {
                itemViewModel.NavigateCommand?.Execute(null);
                e.Handled = true;
            }
        }
    }
}