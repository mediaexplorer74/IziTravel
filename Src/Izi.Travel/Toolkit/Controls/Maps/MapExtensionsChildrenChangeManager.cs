// ********************************************************************
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapExtensionsChildrenChangeManager
// Updated for UWP compatibility

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    internal class MapExtensionsChildrenChangeManager : CollectionChangeListener<DependencyObject>
    {
        private readonly MapControl _mapControl;
        private readonly Dictionary<DependencyObject, MapLayer> _objectToMapLayerMapping;
        private INotifyCollectionChanged sourceCollection;
        internal Map Map;

        public MapExtensionsChildrenChangeManager(INotifyCollectionChanged sourceCollection, MapControl mapControl)
        {
            if (sourceCollection == null)
                throw new ArgumentNullException(nameof(sourceCollection));
            
            _mapControl = mapControl ?? throw new ArgumentNullException(nameof(mapControl));
            _objectToMapLayerMapping = new Dictionary<DependencyObject, MapLayer>();
            
            sourceCollection.CollectionChanged += (s, e) => OnCollectionChanged(s, e);
        }

        private void OnCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public MapExtensionsChildrenChangeManager(INotifyCollectionChanged sourceCollection)
        {
            this.sourceCollection = sourceCollection;
        }

        protected override void InsertItemInternal(int index, DependencyObject obj)
        {
            if (_objectToMapLayerMapping.ContainsKey(obj))
                throw new InvalidOperationException("Attempted to insert the same object twice");

            var mapLayer = GetMapLayerForObject(obj);
            _objectToMapLayerMapping[obj] = mapLayer;
            
            // In UWP, we don't need to manage layers the same way
            // The MapElements are managed by the MapControl directly
        }

        protected override void RemoveItemInternal(DependencyObject obj)
        {
            if (!_objectToMapLayerMapping.TryGetValue(obj, out var mapLayer))
                return;

            mapLayer.Clear();
            _objectToMapLayerMapping.Remove(obj);
        }

        protected override void ResetInternal()
        {
            foreach (var layer in _objectToMapLayerMapping.Values)
            {
                layer.Clear();
            }
            _objectToMapLayerMapping.Clear();
        }

        protected override void AddInternal(DependencyObject obj)
        {
            if (_objectToMapLayerMapping.ContainsKey(obj))
                throw new InvalidOperationException("Attempted to add the same object twice");

            var mapLayer = GetMapLayerForObject(obj);
            _objectToMapLayerMapping[obj] = mapLayer;
        }

        protected override void MoveInternal(DependencyObject obj, int newIndex)
        {
            // In UWP, the order of elements in the map is not as critical
            // as they are positioned based on their coordinates
            if (_objectToMapLayerMapping.ContainsKey(obj))
            {
                // Refresh the element to ensure it's on top
                var layer = _objectToMapLayerMapping[obj];
                // No direct equivalent in UWP, may need to adjust ZIndex if needed
            }
        }

        private MapLayer GetMapLayerForObject(object obj)
        {
            if (obj is MapItemsControl mapItemsControl)
            {
                // Handle MapItemsControl if needed
                return new MapLayer(_mapControl);
            }
            
            // For other objects, create a new layer with a single overlay
            var mapLayer = new MapLayer(_mapControl);
            
            if (obj is UIElement element)
            {
                var overlay = new MapOverlay { Content = element };
                mapLayer.Add(overlay);
            }
            
            return mapLayer;
        }
    }
}

