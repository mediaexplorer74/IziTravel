using Izi.Travel.Shell.Toolkit.Controls.Maps;
using Izi.Travel.Utility;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using MapControl = Izi.Travel.Shell.Toolkit.Controls.Maps.MapControl;

namespace Izi.Travel.Shell.Mtg.Views.Tour.Map
{
    public sealed partial class TourMapPartView : Page
    {
        private Storyboard _tiltStoryboard;
        private bool _isMapInitialized;
        private MapControl _mapControl;

        public TourMapPartView()
        {
            this.InitializeComponent();
            this.Loaded += OnPageLoaded;
        }

        ~TourMapPartView()
        {
            // Clean up event handlers
            this.Loaded -= OnPageLoaded;
            if (_mapControl != null)
            {
                _mapControl.MapElementClick -= OnMapElementClick;
                _mapControl.MapTapped -= OnMapTapped;
                _mapControl.Loaded -= MapControl_Loaded;
            }
        }
        
        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _mapControl = sender as MapControl;
                if (_mapControl == null) return;

                // Initialize map with view model data
                if (DataContext is TourMapPartViewModel viewModel)
                {
                    // The Map property should be set by the view model
                    if (viewModel.Map == null)
                    {
                        viewModel.InitializeMap(_mapControl);
                    }
                    
                    _isMapInitialized = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing map: {ex.Message}");
            }
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            // Initialization now happens in MapControl_Loaded
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            try
            {
                if (DataContext is TourMapPartViewModel viewModel)
                {
                    // Check if any map elements were clicked
                    if (args.MapElements.Any())
                    {
                        var mapElement = args.MapElements.First();
                        
                        // If it's a map item with data context, execute command
                        //if (mapElement is MapControlItem mapItem && mapItem.DataContext != null)
                        //{
                        //    viewModel.MapItemClickCommand?.Execute(mapItem.DataContext);
                            return;
                        //}
                    }
                    
                    // If we get here, it was a click on the map itself
                    viewModel.MapTappedCommand?.Execute(args.Location);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling map element click: {ex.Message}");
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
