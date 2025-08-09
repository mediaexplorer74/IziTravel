using System;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Mtg.ViewModels.Museum.Map;

namespace Izi.Travel.Shell.Mtg.Views.Museum.Map
{
    public sealed partial class MuseumMapPartView : Page
    {
        private bool _contentLoaded;
        private bool _isMapInitialized;

        public MuseumMapPartView()
        {
            this.InitializeComponent();
            this.Loaded += OnPageLoaded;
        }

        ~MuseumMapPartView()
        {
            // Clean up event handlers
            this.Loaded -= OnPageLoaded;
            if (Map?.MapControl != null)
            {
                Map.MapElementClick -= OnMapElementClick;
                Map.MapTapped -= OnMapTapped;
            }
        }

        private void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
                
            this._contentLoaded = true;
            var resourceLocator = new Uri("ms-appx:///Izi.Travel.Shell/Mtg/Views/Museum/Map/MuseumMapPartView.xaml");
            Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Application);
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            if (Map?.MapControl != null && !_isMapInitialized)
            {
                // Initialize map with default view if needed
                if (DataContext is MuseumMapPartViewModel viewModel)
                {
                    // Set initial map position if needed
                    if (viewModel.Center != null)
                    {
                        var center = new Geopoint(new BasicGeoposition
                        {
                            Latitude = viewModel.Center.Latitude,
                            Longitude = viewModel.Center.Longitude
                        });
                        
                        Map.MapControl.ZoomLevel = viewModel.ZoomLevel;
                        Map.MapControl.Center = center;
                    }

                    // Subscribe to map events
                    Map.MapElementClick += OnMapElementClick;
                    Map.MapTapped += OnMapTapped;
                    
                    _isMapInitialized = true;
                }
            }
        }

        private void OnMapItemClick(object sender, MapElementClickEventArgs args)
        {
            if (args.MapElements.Count > 0 && DataContext is MuseumMapPartViewModel viewModel)
            {
                // Handle map item click
                var mapItem = args.MapElements[0] as MapItemsControl;
                if (mapItem?.DataContext != null)
                {
                    viewModel.MapItemClickCommand?.Execute(mapItem.DataContext);
                }
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            // Handle map element click if needed
            if (DataContext is MuseumMapPartViewModel viewModel)
            {
                viewModel.MapTappedCommand?.Execute(args.Position);
            }
        }

        private void OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            // Handle map tap if needed
            if (DataContext is MuseumMapPartViewModel viewModel)
            {
                viewModel.MapTappedCommand?.Execute(args.Position);
            }
        }
    }
}
