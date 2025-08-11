using System.ServiceModel.Channels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Controls.Map
{
    public sealed partial class MapItemControl : Windows.UI.Xaml.Controls.UserControl
    {
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(Geopoint), typeof(MapItemControl), 
                new PropertyMetadata(null, OnLocationChanged));

        public static readonly DependencyProperty AnchorProperty =
            DependencyProperty.Register("Anchor", typeof(Point), typeof(MapItemControl), 
                new PropertyMetadata(new Point(0.5, 0.5)));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(MapItemControl), 
                new PropertyMetadata(false, OnIsSelectedChanged));

        public static readonly DependencyProperty IsHitTestVisibleProperty =
            DependencyProperty.Register("IsHitTestVisible", typeof(bool), typeof(MapItemControl), 
                new PropertyMetadata(true));
        //private Binding Bindings;

        public MapItemControl()
        {
            //this.InitializeComponent();
            //this.DataContextChanged += (s, e) => { this.Bindings.Update(); };
        }

        public Geopoint Location
        {
            get { return (Geopoint)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public Point Anchor
        {
            get { return (Point)GetValue(AnchorProperty); }
            set { SetValue(AnchorProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public bool IsHitTestVisible
        {
            get { return (bool)GetValue(IsHitTestVisibleProperty); }
            set { SetValue(IsHitTestVisibleProperty, value); }
        }

        private static void OnLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapItemControl;
            if (control != null && e.NewValue is Geopoint newLocation)
            {
                // Update the map control if needed
                MapControl.SetLocation(control, newLocation);
                MapControl.SetNormalizedAnchorPoint(control, control.Anchor);
            }
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Handle selection state changes if needed
            var control = d as MapItemControl;
            if (control != null && e.NewValue is bool isSelected)
            {
                // Update visual state or trigger animations
                VisualStateManager.GoToState(control, isSelected ? "Selected" : "Normal", true);
            }
        }
    }
}
