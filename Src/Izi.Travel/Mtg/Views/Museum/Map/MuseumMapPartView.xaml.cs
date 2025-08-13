using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Mtg.ViewModels.Museum.Map;
using Izi.Travel.Toolkit.Controls.Maps;
using Windows.Storage.Streams;

namespace Izi.Travel.Mtg.Views.Museum.Map
{
    public sealed partial class MuseumMapPartView : Page
    {
        private bool _isMapInitialized;
        private readonly Dictionary<string, MapIcon> _mapIcons = new Dictionary<string, MapIcon>();

        public MuseumMapPartView()
        {
            this.InitializeComponent();
            this.Loaded += OnPageLoaded;
            this.Unloaded += OnPageUnloaded;
        }

        ~MuseumMapPartView()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            // Clean up event handlers
            this.Loaded -= OnPageLoaded;
            this.Unloaded -= OnPageUnloaded;
            
            if (Map != null)
            {
                Map.MapElementClick -= OnMapElementClick;
                Map.MapTapped -= OnMapTapped;
                
                // Clear map elements
                Map.MapElements.Clear();
                _mapIcons.Clear();
            }
        }

        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            if (Map != null && !_isMapInitialized && DataContext is MuseumMapPartViewModel viewModel)
            {
                try
                {
                    // Initialize the map
                    await InitializeMapAsync(viewModel);
                    _isMapInitialized = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error initializing map: {ex.Message}");
                }
            }
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            Cleanup();
        }

        private async Task InitializeMapAsync(MuseumMapPartViewModel viewModel)
        {
            // Set initial map position if needed
            if (viewModel.Center != null)
            {
                var center = new Geopoint(new BasicGeoposition
                {
                    Latitude = default,//viewModel.Center.Latitude,
                    Longitude = default,//viewModel.Center.Longitude
                });
                
                Map.ZoomLevel = viewModel.ZoomLevel;
                Map.Center = center;
            }

            // Subscribe to map events
            Map.MapElementClick += OnMapElementClick;
            Map.MapTapped += OnMapTapped;

            // Initialize map with any existing items
            if (viewModel.MapItems != null && viewModel.MapItems.Any())
            {
                await UpdateMapItems(viewModel.MapItems);
            }

            // Subscribe to collection changes if needed
            if (viewModel.MapItems is System.Collections.Specialized.INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += async (s, e) =>
                {
                    await UpdateMapItems(viewModel.MapItems);
                };
            }
        }

        private async Task UpdateMapItems(IEnumerable<object> items)
        {
            if (Map == null) return;

            // Clear existing map icons
            var iconsToRemove = _mapIcons.Values.ToList();
            foreach (var icon in iconsToRemove)
            {
                Map.MapElements.Remove(icon);
            }
            _mapIcons.Clear();

            // Add new map icons
            foreach (var item in items)
            {
                if (item is IMapItem mapItem && mapItem.Location != null)
                {
                    var mapIcon = new MapIcon
                    {
                        Location = new Geopoint(new BasicGeoposition
                        {
                            Latitude = default,//mapItem.Location.Latitude,
                            Longitude = default//mapItem.Location.Longitude
                        }),
                        Title = mapItem.Title,
                        NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0),
                        ZIndex = 0
                    };

                    // Set custom icon if available
                    if (!string.IsNullOrEmpty(mapItem.IconUrl))
                    {
                        mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///" + mapItem.IconUrl));
                    }

                    Map.MapElements.Add(mapIcon);
                    _mapIcons[mapItem.Id] = mapIcon;
                }
            }
        }

        private void OnMapItemClick(object sender, MapElementClickEventArgs args)
        {
            if (args.MapElements.Count > 0 && DataContext is MuseumMapPartViewModel viewModel)
            {
                // Handle map item click
                var mapIcon = args.MapElements[0] as MapIcon;
                if (mapIcon != null)
                {
                    var item = _mapIcons.FirstOrDefault(x => x.Value == mapIcon).Key;
                    if (item != null)
                    {
                        var mapItem = viewModel.MapItems?.FirstOrDefault(i => (i as IMapItem)?.Id == item);
                        if (mapItem != null)
                        {
                            viewModel.MapItemClickCommand?.Execute(mapItem);
                        }
                    }
                }
            }
        }

        private void OnMapElementClick(Windows.UI.Xaml.Controls.Maps.MapControl sender, MapElementClickEventArgs args)
        {
            // Handle map element click if needed
            if (args.MapElements.Count > 0 && DataContext is MuseumMapPartViewModel viewModel)
            {
                var mapIcon = args.MapElements[0] as MapIcon;
                if (mapIcon != null)
                {
                    var item = _mapIcons.FirstOrDefault(x => x.Value == mapIcon).Key;
                    if (item != null)
                    {
                        var mapItem = viewModel.MapItems?.FirstOrDefault(i => (i as IMapItem)?.Id == item);
                        if (mapItem != null)
                        {
                            viewModel.MapItemClickCommand?.Execute(mapItem);
                            return;
                        }
                    }
                }
                
                // If no map icon was clicked, treat as a map tap
                viewModel.MapTappedCommand?.Execute(args.Location.Position);
            }
        }

        private void OnMapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, MapInputEventArgs args)
        {
            // Handle map tap if needed
            if (DataContext is MuseumMapPartViewModel viewModel)
            {
                viewModel.MapTappedCommand?.Execute(args.Position);
            }
        }
    }
}
