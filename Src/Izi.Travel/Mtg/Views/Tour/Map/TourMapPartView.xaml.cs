using Izi.Travel.Utility;
using System;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;

namespace Izi.Travel.Shell.Mtg.Views.Tour.Map
{
    public sealed partial class TourMapPartView : Page
    {
        private Storyboard _tiltStoryboard;
        private bool _isMapInitialized;

        public TourMapPartView()
        {
            this.InitializeComponent();
            this.Loaded += OnPageLoaded;
        }

        ~TourMapPartView()
        {
            // Clean up event handlers
            this.Loaded -= OnPageLoaded;
            if (Map != null)
            {
                Map.MapElementClick -= OnMapElementClick;
                Map.MapTapped -= OnMapTapped;
            }
            
            if (MapControl != null)
            {
                MapControl.Loaded -= MapControl_Loaded;
            }
        }
        
        private async void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map style using the MapStyleSheet
            if (MapControl != null)
            {
                // Load the light style for the map
                MapControl.StyleSheet = MapStyleSheet.RoadLight();
            }
        }

        private void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
                
            this._contentLoaded = true;
            var resourceLocator = new Uri("ms-appx:///Izi.Travel.Shell/Mtg/Views/Tour/Map/TourMapPartView.xaml");
            Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Application);
            
            // Get the MapButton style and find the storyboard
            if (this.Resources.ContainsKey("MapButton") && this.Resources["MapButton"] is Style mapButtonStyle)
            {
                if (mapButtonStyle.Resources.ContainsKey("TiltStoryboard") && 
                    mapButtonStyle.Resources["TiltStoryboard"] is Storyboard storyboard)
                {
                    this._tiltStoryboard = storyboard;
                }
            }
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            if (Map?.MapControl != null && !_isMapInitialized)
            {
                // Initialize map with default view if needed
                if (DataContext is TourMapPartViewModel viewModel)
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
            if (args.MapElements.Count > 0 && DataContext is TourMapPartViewModel viewModel)
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
            if (DataContext is TourMapPartViewModel viewModel)
            {
                viewModel.MapTappedCommand?.Execute(args.Position);
            }
        }

        private void OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            // Handle map tap if needed
            if (DataContext is TourMapPartViewModel viewModel)
            {
                viewModel.MapTappedCommand?.Execute(args.Position);
            }
        }

        // Button animation handlers
        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is FrameworkElement element && _tiltStoryboard != null)
            {
                _tiltStoryboard.Stop();
                _tiltStoryboard.Seek(TimeSpan.Zero);
                _tiltStoryboard.Begin();
            }
        }

        private void Button_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is FrameworkElement element && _tiltStoryboard != null)
            {
                _tiltStoryboard.Stop();
                
                // Create reverse animation to return to normal
                var reverseStoryboard = new Storyboard();
                
                var scaleXAnimation = new DoubleAnimation
                {
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(150),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                
                var scaleYAnimation = new DoubleAnimation
                {
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(150),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                Storyboard.SetTarget(scaleXAnimation, element);
                Storyboard.SetTarget(scaleYAnimation, element);
                Storyboard.SetTargetProperty(scaleXAnimation, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
                Storyboard.SetTargetProperty(scaleYAnimation, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");
                
                reverseStoryboard.Children.Add(scaleXAnimation);
                reverseStoryboard.Children.Add(scaleYAnimation);
                
                reverseStoryboard.Begin();
            }
        }
    }
}
