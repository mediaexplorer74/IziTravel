// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Extensions.MapBindingExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Toolkit.Controls.Maps;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Extensions
{
  public static class MapBindingExtensions
  {
    private static readonly DependencyProperty BindableElementsManagerProperty = DependencyProperty.RegisterAttached("BindableElementsManager", typeof (MapElementCollectionManager), typeof (MapBindingExtensions), new PropertyMetadata((object) null));
    public static readonly DependencyProperty BindableElementsProperty = DependencyProperty.RegisterAttached("BindableElements", typeof (IEnumerable<MapElement>), typeof (MapBindingExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(MapBindingExtensions.OnBindableElementsPropertyChanged)));
    public static readonly DependencyProperty BindableRouteProperty = DependencyProperty.RegisterAttached("BindableRoute", typeof (MapRoute), typeof (MapBindingExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(MapBindingExtensions.OnBindableRoutePropertyChanged)));
    public static readonly DependencyProperty BindableItemsSourceProperty = DependencyProperty.RegisterAttached("BindableItemsSource", typeof (IEnumerable), typeof (MapBindingExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(MapBindingExtensions.OnBindableItemsSourcePropertyChanged)));

    public static IEnumerable<MapElement> GetBindableElements(Map map)
    {
      return (IEnumerable<MapElement>) map.GetValue(MapBindingExtensions.BindableElementsProperty);
    }

    public static void SetBindableElements(Map map, IEnumerable<MapElement> elements)
    {
      map.SetValue(MapBindingExtensions.BindableElementsProperty, (object) elements);
    }

    private static void OnBindableElementsPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Map map) || !(e.NewValue is IEnumerable<MapElement> newValue))
        return;
      if (!(map.GetValue(MapBindingExtensions.BindableElementsManagerProperty) is MapElementCollectionManager collectionManager))
      {
        collectionManager = new MapElementCollectionManager(map);
        map.SetValue(MapBindingExtensions.BindableElementsManagerProperty, (object) collectionManager);
      }
      collectionManager.Attach(newValue);
    }

    public static MapRoute GetBindableRoute(Map map)
    {
      return map != null ? (MapRoute) map.GetValue(MapBindingExtensions.BindableRouteProperty) : throw new ArgumentNullException(nameof (map));
    }

    public static void SetBindableRoute(Map map, MapRoute route)
    {
      if (map == null)
        throw new ArgumentNullException(nameof (map));
      map.SetValue(MapBindingExtensions.BindableRouteProperty, (object) route);
    }

    private static void OnBindableRoutePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Map map))
        return;
      if (e.OldValue is MapRoute oldValue)
        map.RemoveRoute(oldValue);
      if (!(e.NewValue is MapRoute newValue))
        return;
      map.AddRoute(newValue);
    }

    public static IEnumerable GetBindableItemsSource(MapItemsControl mapItemsControl)
    {
      return (IEnumerable) mapItemsControl.GetValue(MapBindingExtensions.BindableItemsSourceProperty);
    }

    public static void SetBindableItemsSource(MapItemsControl mapItemsControl, IEnumerable value)
    {
      mapItemsControl.SetValue(MapBindingExtensions.BindableItemsSourceProperty, (object) value);
    }

    private static void OnBindableItemsSourcePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(d is MapItemsControl mapItemsControl))
        return;
      mapItemsControl.ItemsSource = (IEnumerable) args.NewValue;
    }
  }
}
