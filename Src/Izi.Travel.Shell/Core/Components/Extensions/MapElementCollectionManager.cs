// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Extensions.MapElementCollectionManager
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Controls;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Extensions
{
  public class MapElementCollectionManager
  {
    private readonly Map _map;
    private IEnumerable<MapElement> _elements;

    public MapElementCollectionManager(Map map) => this._map = map;

    public void Attach(IEnumerable<MapElement> elements)
    {
      if (this._elements is INotifyCollectionChanged elements1)
        elements1.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnElementCollectionChanged);
      this.ResetElements();
      this._elements = elements;
      if (this._elements is INotifyCollectionChanged elements2)
        elements2.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnElementCollectionChanged);
      this.AddElements((IEnumerable) this._elements);
    }

    private void AddElements(IEnumerable elements)
    {
      if (elements == null)
        return;
      foreach (MapElement mapElement in elements.OfType<MapElement>())
        this._map.MapElements.Add(mapElement);
    }

    private void RemoveElements(IEnumerable elements)
    {
      if (elements == null)
        return;
      foreach (MapElement mapElement in elements.OfType<MapElement>())
        this._map.MapElements.Remove(mapElement);
    }

    private void ResetElements() => this._map.MapElements.Clear();

    private void OnElementCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Reset)
        this.ResetElements();
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        this.AddElements((IEnumerable) e.NewItems);
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Remove)
          return;
        this.RemoveElements((IEnumerable) e.OldItems);
      }
    }
  }
}
