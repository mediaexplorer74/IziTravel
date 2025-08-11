using Izi.Travel.Shell.Core.Components.Extensions;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using System;
using Izi.Travel.Shell.Core.Components.Behaviors;
using Izi.Travel.Data.Entities.Common;

namespace Izi.Travel.Shell.Mtg.Views.Tour
{
    public partial class TourListView
    {
        private MapViewBehavior _mapViewBehavior;

        public TourListView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
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
        }
    }
}