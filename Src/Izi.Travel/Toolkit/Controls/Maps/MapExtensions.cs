// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Specialized;
using System.Device.Location;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  public static class MapExtensions
  {
    public static readonly DependencyProperty ChildrenProperty = DependencyProperty.RegisterAttached("Children", typeof (DependencyObjectCollection<DependencyObject>), typeof (MapExtensions), (PropertyMetadata) null);
    private static readonly DependencyProperty ChildrenChangedManagerProperty = DependencyProperty.RegisterAttached("ChildrenChangedManager", typeof (MapExtensionsChildrenChangeManager), typeof (MapExtensions), (PropertyMetadata) null);

    public static DependencyObjectCollection<DependencyObject> GetChildren(Map element)
    {
      DependencyObjectCollection<DependencyObject> sourceCollection = element != null ? (DependencyObjectCollection<DependencyObject>) element.GetValue(MapExtensions.ChildrenProperty) : throw new ArgumentNullException(nameof (element));
      if (sourceCollection == null)
      {
        sourceCollection = new DependencyObjectCollection<DependencyObject>();
        MapExtensionsChildrenChangeManager childrenChangeManager = new MapExtensionsChildrenChangeManager((INotifyCollectionChanged) sourceCollection)
        {
          Map = element
        };
        element.SetValue(MapExtensions.ChildrenProperty, (object) sourceCollection);
        element.SetValue(MapExtensions.ChildrenChangedManagerProperty, (object) childrenChangeManager);
      }
      return sourceCollection;
    }

    public static void Add(
      this DependencyObjectCollection<DependencyObject> childrenCollection,
      DependencyObject dependencyObject,
      GeoCoordinate geoCoordinate)
    {
      if (childrenCollection == null)
        throw new ArgumentNullException(nameof (childrenCollection));
      if (dependencyObject == null)
        throw new ArgumentNullException(nameof (dependencyObject));
      if (geoCoordinate == (GeoCoordinate) null)
        throw new ArgumentNullException(nameof (geoCoordinate));
      dependencyObject.SetValue(MapChild.GeoCoordinateProperty, (object) geoCoordinate);
      childrenCollection.Add(dependencyObject);
    }

    public static void Add(
      this DependencyObjectCollection<DependencyObject> childrenCollection,
      DependencyObject dependencyObject,
      GeoCoordinate geoCoordinate,
      Point positionOrigin)
    {
      if (childrenCollection == null)
        throw new ArgumentNullException(nameof (childrenCollection));
      if (dependencyObject == null)
        throw new ArgumentNullException(nameof (dependencyObject));
      if (geoCoordinate == (GeoCoordinate) null)
        throw new ArgumentNullException(nameof (geoCoordinate));
      dependencyObject.SetValue(MapChild.GeoCoordinateProperty, (object) geoCoordinate);
      dependencyObject.SetValue(MapChild.PositionOriginProperty, (object) positionOrigin);
      childrenCollection.Add(dependencyObject);
    }
  }
}
