// ********************************************************************
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapOverlay
// Updated for UWP compatibility

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Izi.Travel.Data.Entities.Common;
using Windows.Foundation;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    public class MapOverlay : DependencyObject
    {
        private FrameworkElement _content;
        private MapControl _mapControl;
        private MapIcon _mapIcon;
        private BasicGeoposition _position;
        private string _title;
        internal GeoCoordinate GeoCoordinate;
        internal Point PositionOrigin;

        public object Content
        {
            get => _content;
            set
            {
                _content = value as FrameworkElement;
                UpdateMapIcon();
            }
        }

        public BasicGeoposition Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateMapIcon();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                UpdateMapIcon();
            }
        }

        public MapOverlay()
        {
            _mapIcon = new MapIcon();
        }

        public void SetValue(DependencyProperty property, object value)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            /*if (property.PropertyType.IsInstanceOfType(value) || value == null)
            {
                if (property == MapControl.LocationProperty)
                {
                    if (value is Geopoint geopoint)
                    {
                        Position = geopoint.Position;
                    }
                }
                else if (property == MapControl.NormalizedAnchorPointProperty)
                {
                    // Handle anchor point if needed
                }
                else
                {
                    // Handle other properties
                    SetValue(property, value);
                }
            }*/
        }

        private void UpdateMapIcon()
        {
            if (_mapControl != null && _content != null)
            {
                _mapIcon = new MapIcon
                {
                    Location = new Geopoint(Position),
                    Title = _title,
                    NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0)
                };

                // If we have a MapControl, add the icon to it
                _mapControl.MapElements.Add(_mapIcon);
            }
        }

        internal void SetMapControl(MapControl mapControl)
        {
            if (_mapControl != null)
            {
                // Remove from old map
                _mapControl.MapElements.Remove(_mapIcon);
            }

            _mapControl = mapControl;
            UpdateMapIcon();
        }
    }
}