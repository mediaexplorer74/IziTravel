// ********************************************************************
// MapControl.xaml.cs
// Code-behind for the MapControl user control

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Izi.Travel.Toolkit.Controls.Maps
{
    public partial class MapControl : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(Map),
                typeof(MapControl),
                new PropertyMetadata(null, OnViewModelChanged));
        public Action<MapControl, object> ZoomLevelChanged;
        internal Action<MapControl, MapElementClickEventArgs> MapElementClick;
        internal Action<MapControl, MapInputEventArgs> MapTapped;
        internal Action<MapControl, object> CenterChanged;
        public Action<MapControl, MapActualCameraChangedEventArgs> ActualCameraChanged;
        public List<MapElement> MapElements;

        public Map ViewModel
        {
            get => (Map)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        public Geopoint Center { get; internal set; }
        public double ZoomLevel { get; internal set; }
        public static DependencyProperty LocationProperty { get; internal set; }

        public MapControl()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Initialize the map control when the control is loaded
            if (ViewModel == null)
            {
                var mapControl = MapControlHelper.CreateMapControl();
                ViewModel = new Map(mapControl);
                
                // Set the map control as the content of the root grid
                if (RootGrid.Children.Count > 0 && RootGrid.Children[0] is Windows.UI.Xaml.Controls.Maps.MapControl existingMap)
                {
                    // Copy properties from existing map control if needed
                    mapControl.Center = existingMap.Center;
                    mapControl.ZoomLevel = existingMap.ZoomLevel;
                }
                
                RootGrid.Children.Clear();
                RootGrid.Children.Add(mapControl);
            }
        }

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MapControl control && e.NewValue is Map newMap)
            {
                // Update the map control when the view model changes
                control.UpdateMapControl(newMap);
            }
        }

        private void UpdateMapControl(Map newMap)
        {
            // Update the map control with the new view model properties
            if (RootGrid.Children.Count > 0 && RootGrid.Children[0] is Windows.UI.Xaml.Controls.Maps.MapControl mapControl)
            {
                // Update map control properties from the view model
                mapControl.ZoomLevel = newMap.ZoomLevel;
                mapControl.Center = newMap.Center;
                
                // Subscribe to map control events
                mapControl.ZoomLevelChanged += (s, e) =>
                {
                    if (newMap.ZoomLevel != mapControl.ZoomLevel)
                    {
                        newMap.ZoomLevel = mapControl.ZoomLevel;
                    }
                };
                
                mapControl.CenterChanged += (s, e) =>
                {
                    if (newMap.Center != mapControl.Center)
                    {
                        newMap.Center = mapControl.Center;
                    }
                };
            }
        }

        public Geopoint GetVisibleRegion(MapVisibleRegionKind full)
        {
            throw new NotImplementedException();
        }

        public async Task TrySetViewAsync(Geopoint center, double zoomLevel, object value1, object value2, MapAnimationKind @default)
        {
            throw new NotImplementedException();
        }

        internal async Task TrySetViewBoundsAsync(GeoboundingBox boundingBox, Thickness zoomDesiredMargin, MapAnimationKind @default)
        {
            throw new NotImplementedException();
        }
    }
}
