// ********************************************************************
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.Map
// Updated for UWP compatibility

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    public class Map : DependencyObject
    {
        private readonly MapControl _mapControl;
        private readonly ObservableCollection<MapLayer> _layers = new ObservableCollection<MapLayer>();
        private MapExtensionsChildrenChangeManager _childrenChangeManager;

        public event EventHandler<MapZoomLevelChangedEventArgs> ZoomLevelChanged;

        public double ZoomLevel
        {
            get => _mapControl?.ZoomLevel ?? 0;
            set
            {
                if (_mapControl != null)
                {
                    _mapControl.ZoomLevel = value;
                }
            }
        }

        public Geopoint Center
        {
            get => _mapControl?.Center;
            set
            {
                if (_mapControl != null)
                {
                    _mapControl.Center = value;
                }
            }
        }

        public IList<MapLayer> Layers => _layers;

        public Map(MapControl mapControl)
        {
            _mapControl = mapControl ?? throw new ArgumentNullException(nameof(mapControl));
            _mapControl.ZoomLevelChanged += OnMapZoomLevelChanged;
        }

        internal DependencyObjectCollection<DependencyObject> GetValue(DependencyProperty childrenProperty)
        {
            if (_childrenChangeManager == null)
            {
                var children = new DependencyObjectCollection<DependencyObject>();
                //_childrenChangeManager = new MapExtensionsChildrenChangeManager(children, _mapControl);
                _childrenChangeManager = new MapExtensionsChildrenChangeManager((INotifyCollectionChanged)children, _mapControl);
                return children;
            }
            return null;
        }

        internal void SetValue(DependencyProperty childrenProperty, object sourceCollection)
        {
            if (childrenProperty == null)
                throw new ArgumentNullException(nameof(childrenProperty));

            if (sourceCollection is INotifyCollectionChanged collection)
            {
                _childrenChangeManager = new MapExtensionsChildrenChangeManager(collection, _mapControl);
            }
        }

        private void OnMapZoomLevelChanged(MapControl sender, object args)
        {
            ZoomLevelChanged?.Invoke(this, new MapZoomLevelChangedEventArgs(sender.ZoomLevel));
        }
    }

  
}