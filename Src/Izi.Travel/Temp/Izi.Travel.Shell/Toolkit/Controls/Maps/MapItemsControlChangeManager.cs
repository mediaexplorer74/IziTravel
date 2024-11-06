// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapItemsControlChangeManager
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
  internal class MapItemsControlChangeManager : CollectionChangeListener<object>
  {
    public MapItemsControlChangeManager(INotifyCollectionChanged sourceCollection)
    {
      if (sourceCollection == null)
        throw new ArgumentNullException(nameof (sourceCollection));
      this.ObjectToMapOverlayMapping = new Dictionary<object, MapOverlay>();
      sourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(((CollectionChangeListener<object>) this).CollectionChanged);
    }

    public DataTemplate ItemTemplate { get; set; }

    public MapLayer MapLayer { get; set; }

    private Dictionary<object, MapOverlay> ObjectToMapOverlayMapping { get; set; }

    protected override void InsertItemInternal(int index, object obj)
    {
      MapOverlay mapOverlay = !this.ObjectToMapOverlayMapping.ContainsKey(obj) ? MapChild.CreateMapOverlay(obj, this.ItemTemplate) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.MapLayer.Insert(index, mapOverlay);
      this.ObjectToMapOverlayMapping.Add(obj, mapOverlay);
    }

    protected override void RemoveItemInternal(object obj)
    {
      if (!this.ObjectToMapOverlayMapping.ContainsKey(obj))
        return;
      MapOverlay mapOverlay = this.ObjectToMapOverlayMapping[obj];
      this.ObjectToMapOverlayMapping.Remove(obj);
      this.MapLayer.Remove(mapOverlay);
      MapChild.ClearMapOverlayBindings(mapOverlay);
    }

    protected override void ResetInternal()
    {
      foreach (MapOverlay mapOverlay in (Collection<MapOverlay>) this.MapLayer)
        MapChild.ClearMapOverlayBindings(mapOverlay);
      this.MapLayer.Clear();
      this.ObjectToMapOverlayMapping.Clear();
    }

    protected override void AddInternal(object obj)
    {
      MapOverlay mapOverlay = !this.ObjectToMapOverlayMapping.ContainsKey(obj) ? MapChild.CreateMapOverlay(obj, this.ItemTemplate) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.ObjectToMapOverlayMapping[obj] = mapOverlay;
      this.MapLayer.Add(mapOverlay);
    }

    protected override void MoveInternal(object obj, int newIndex)
    {
      if (!this.ObjectToMapOverlayMapping.ContainsKey(obj))
        return;
      this.MapLayer.Move(this.MapLayer.IndexOf(this.ObjectToMapOverlayMapping[obj]), newIndex);
    }

    public void MoveToTop(object obj)
    {
      if (!this.ObjectToMapOverlayMapping.ContainsKey(obj))
        return;
      MapOverlay mapOverlay = this.ObjectToMapOverlayMapping[obj];
      this.MapLayer.Remove(mapOverlay);
      this.MapLayer.Add(mapOverlay);
    }
  }
}
