// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.MapViewBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Controls;
using System;
using System.Device.Location;
using System.Windows;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class MapViewBehavior : Behavior<Map>
  {
    private bool _isRefreshing;
    public static readonly DependencyProperty ViewProperty = DependencyProperty.Register(nameof (View), typeof (LocationRectangle), typeof (MapViewBehavior), new PropertyMetadata((object) null, new PropertyChangedCallback(MapViewBehavior.OnViewPropertyChanged)));
    public static readonly DependencyProperty ViewCenterProperty = DependencyProperty.Register(nameof (ViewCenter), typeof (GeoCoordinate), typeof (MapViewBehavior), new PropertyMetadata((object) null, new PropertyChangedCallback(MapViewBehavior.OnViewCenterPropertyChanged)));
    public static readonly DependencyProperty ViewZoomLevelProperty = DependencyProperty.Register(nameof (ViewZoomLevel), typeof (double), typeof (MapViewBehavior), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(MapViewBehavior.OnViewZoomLevelPropertyChanged)));
    public static readonly DependencyProperty IsViewChangingProperty = DependencyProperty.Register(nameof (IsViewChanging), typeof (bool), typeof (MapViewBehavior), new PropertyMetadata((object) false));

    public double ZoomAnimationMaxValue { get; set; }

    public Thickness ZoomDesiredMargin { get; set; }

    public LocationRectangle View
    {
      get => (LocationRectangle) this.GetValue(MapViewBehavior.ViewProperty);
      set => this.SetValue(MapViewBehavior.ViewProperty, (object) value);
    }

    public GeoCoordinate ViewCenter
    {
      get => (GeoCoordinate) this.GetValue(MapViewBehavior.ViewCenterProperty);
      set => this.SetValue(MapViewBehavior.ViewCenterProperty, (object) value);
    }

    public double ViewZoomLevel
    {
      get => (double) this.GetValue(MapViewBehavior.ViewZoomLevelProperty);
      set => this.SetValue(MapViewBehavior.ViewZoomLevelProperty, (object) value);
    }

    public bool IsViewChanging
    {
      get => (bool) this.GetValue(MapViewBehavior.IsViewChangingProperty);
      set => this.SetValue(MapViewBehavior.IsViewChangingProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.CenterChanged += new EventHandler<MapCenterChangedEventArgs>(this.OnMapCenterChanged);
      this.AssociatedObject.SizeChanged += new SizeChangedEventHandler(this.OnMapSizeChanged);
      this.AssociatedObject.ZoomLevelChanged += new EventHandler<MapZoomLevelChangedEventArgs>(this.OnMapZoomLevelChanged);
      this.AssociatedObject.ViewChanging += new EventHandler<MapViewChangingEventArgs>(this.OnMapViewChanging);
      this.AssociatedObject.ViewChanged += new EventHandler<MapViewChangedEventArgs>(this.OnMapViewChanged);
      base.OnAttached();
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.ViewChanging -= new EventHandler<MapViewChangingEventArgs>(this.OnMapViewChanging);
      this.AssociatedObject.ViewChanged -= new EventHandler<MapViewChangedEventArgs>(this.OnMapViewChanged);
      this.AssociatedObject.CenterChanged -= new EventHandler<MapCenterChangedEventArgs>(this.OnMapCenterChanged);
      this.AssociatedObject.SizeChanged -= new SizeChangedEventHandler(this.OnMapSizeChanged);
      this.AssociatedObject.ZoomLevelChanged -= new EventHandler<MapZoomLevelChangedEventArgs>(this.OnMapZoomLevelChanged);
      base.OnDetaching();
    }

    private void RefreshView()
    {
      this._isRefreshing = true;
      GeoCoordinate geoCoordinate1 = this.AssociatedObject.ConvertViewportPointToGeoCoordinate(new Point(0.0, 0.0));
      GeoCoordinate geoCoordinate2 = this.AssociatedObject.ConvertViewportPointToGeoCoordinate(new Point(0.0, this.AssociatedObject.ActualHeight));
      if (geoCoordinate1 != (GeoCoordinate) null && geoCoordinate2 != (GeoCoordinate) null)
        this.View = LocationRectangle.CreateBoundingRectangle(geoCoordinate1, geoCoordinate2);
      this.ViewCenter = this.AssociatedObject.Center;
      this.ViewZoomLevel = this.AssociatedObject.ZoomLevel;
      this._isRefreshing = false;
    }

    private static void OnViewPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      MapViewBehavior mapBehavior = d as MapViewBehavior;
      if (mapBehavior == null || mapBehavior._isRefreshing)
        return;
      LocationRectangle locationRect = e.NewValue as LocationRectangle;
      if (locationRect == null)
        return;
      mapBehavior.AssociatedObject.Dispatcher.BeginInvoke((Action) (() => mapBehavior.AssociatedObject.SetView(locationRect, mapBehavior.ZoomDesiredMargin, MapAnimationKind.Parabolic)));
    }

    private static void OnViewCenterPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is MapViewBehavior mapViewBehavior) || mapViewBehavior._isRefreshing)
        return;
      GeoCoordinate newValue = e.NewValue as GeoCoordinate;
      if (newValue == (GeoCoordinate) null)
        return;
      mapViewBehavior.AssociatedObject.SetView(newValue, mapViewBehavior.AssociatedObject.ZoomLevel, MapAnimationKind.Parabolic);
    }

    private static void OnViewZoomLevelPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is MapViewBehavior mapViewBehavior) || mapViewBehavior._isRefreshing || !(e.NewValue is double))
        return;
      double newValue = (double) e.NewValue;
      mapViewBehavior.AssociatedObject.SetView(mapViewBehavior.ViewCenter, newValue.Clamp(1.0, 20.0), MapAnimationKind.Parabolic);
    }

    private void OnMapCenterChanged(object sender, MapCenterChangedEventArgs args)
    {
      this.RefreshView();
    }

    private void OnMapSizeChanged(object sender, SizeChangedEventArgs args) => this.RefreshView();

    private void OnMapZoomLevelChanged(object sender, MapZoomLevelChangedEventArgs args)
    {
      this.RefreshView();
    }

    private void OnMapViewChanging(object sender, MapViewChangingEventArgs e)
    {
      this.IsViewChanging = true;
    }

    private void OnMapViewChanged(object sender, MapViewChangedEventArgs e)
    {
      this.IsViewChanging = false;
    }
  }
}
