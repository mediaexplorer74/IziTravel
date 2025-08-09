using System;
using System.Collections;
using System.Collections.Specialized;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Markup;

namespace Izi.Travel.Controls.Map
{
    [ContentProperty(Name = "ItemTemplate")]
    public sealed partial class MapItemsControl : Windows.UI.Xaml.Controls.UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MapItemsControl), 
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(MapItemsControl), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty ZoomLevelProperty =
            DependencyProperty.Register("ZoomLevel", typeof(double), typeof(MapItemsControl), 
                new PropertyMetadata(15.0, OnZoomLevelChanged));

        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Geopoint), typeof(MapItemsControl), 
                new PropertyMetadata(null, OnCenterChanged));

        public static readonly DependencyProperty MapStyleProperty =
            DependencyProperty.Register("MapStyle", typeof(MapStyle), typeof(MapItemsControl), 
                new PropertyMetadata(MapStyle.Road));

        public static readonly DependencyProperty MapServiceTokenProperty =
            DependencyProperty.Register("MapServiceToken", typeof(string), typeof(MapItemsControl), 
                new PropertyMetadata(string.Empty));

        public event EventHandler<MapElementClickEventArgs> ItemClick;

        public MapItemsControl()
        {
            this.InitializeComponent();
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public double ZoomLevel
        {
            get { return (double)GetValue(ZoomLevelProperty); }
            set { SetValue(ZoomLevelProperty, value); }
        }

        public Geopoint Center
        {
            get { return (Geopoint)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        public MapStyle MapStyle
        {
            get { return (MapStyle)GetValue(MapStyleProperty); }
            set { SetValue(MapStyleProperty, value); }
        }

        public string MapServiceToken
        {
            get { return (string)GetValue(MapServiceTokenProperty); }
            set { SetValue(MapServiceTokenProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapItemsControl;
            if (control == null) return;

            if (e.OldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= control.OnCollectionChanged;
            }

            if (e.NewValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += control.OnCollectionChanged;
            }
        }

        private static void OnZoomLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapItemsControl;
            if (control?.Map != null && e.NewValue is double zoomLevel)
            {
                control.Map.ZoomLevel = zoomLevel;
            }
        }

        private static void OnCenterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MapItemsControl;
            if (control?.Map != null && e.NewValue is Geopoint center)
            {
                control.Map.Center = center;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Handle collection changes if needed
            // This is where you would update the map when items are added/removed
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            // Forward the click event to the parent control
            ItemClick?.Invoke(this, args);
        }
    }
}
