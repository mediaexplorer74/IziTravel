// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapExtensionsChildrenChangeManager
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  internal class MapExtensionsChildrenChangeManager : CollectionChangeListener<DependencyObject>
  {
    public MapExtensionsChildrenChangeManager(INotifyCollectionChanged sourceCollection)
    {
      if (sourceCollection == null)
        throw new ArgumentNullException(nameof (sourceCollection));
      this.ObjectToMapLayerMapping = new Dictionary<DependencyObject, MapLayer>();
      sourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(((CollectionChangeListener<DependencyObject>) this).CollectionChanged);
    }

    public Map Map { get; set; }

    private Dictionary<DependencyObject, MapLayer> ObjectToMapLayerMapping { get; set; }

    protected override void InsertItemInternal(int index, DependencyObject obj)
    {
      MapLayer mapLayer = !this.ObjectToMapLayerMapping.ContainsKey(obj) ? MapExtensionsChildrenChangeManager.GetMapLayerForObject((object) obj) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.ObjectToMapLayerMapping[obj] = mapLayer;
      this.Map.Layers.Insert(index, mapLayer);
    }

    protected override void RemoveItemInternal(DependencyObject obj)
    {
      if (!this.ObjectToMapLayerMapping.ContainsKey(obj))
        return;
      MapLayer mapLayer = this.ObjectToMapLayerMapping[obj];
      this.ObjectToMapLayerMapping.Remove(obj);
      this.Map.Layers.Remove(mapLayer);
      MapItemsControl mapItemsControl = obj as MapItemsControl;
      foreach (MapOverlay mapOverlay in (Collection<MapOverlay>) mapLayer)
        MapChild.ClearMapOverlayBindings(mapOverlay);
    }

    protected override void ResetInternal()
    {
      foreach (Collection<MapOverlay> layer in this.Map.Layers)
      {
        foreach (MapOverlay mapOverlay in layer)
          MapChild.ClearMapOverlayBindings(mapOverlay);
      }
      this.Map.Layers.Clear();
      this.ObjectToMapLayerMapping.Clear();
    }

    protected override void AddInternal(DependencyObject obj)
    {
      MapLayer mapLayer = !this.ObjectToMapLayerMapping.ContainsKey(obj) ? MapExtensionsChildrenChangeManager.GetMapLayerForObject((object) obj) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.ObjectToMapLayerMapping[obj] = mapLayer;
      this.Map.Layers.Add(mapLayer);
    }

    protected override void MoveInternal(DependencyObject obj, int newIndex)
    {
      if (!this.ObjectToMapLayerMapping.ContainsKey(obj))
        return;
      ObservableCollection<MapLayer> layers = (ObservableCollection<MapLayer>) this.Map.Layers;
      layers.Move(layers.IndexOf(this.ObjectToMapLayerMapping[obj]), newIndex);
    }

    private static MapLayer GetMapLayerForObject(object obj)
    {
      MapLayer mapLayerForObject;
      if (obj is MapItemsControl mapItemsControl)
      {
        mapLayerForObject = mapItemsControl.MapLayer;
      }
      else
      {
        mapLayerForObject = new MapLayer();
        MapOverlay mapOverlay = MapChild.CreateMapOverlay(obj, (DataTemplate) null);
        mapLayerForObject.Add(mapOverlay);
      }
      return mapLayerForObject;
    }
  }
}
