// ********************************************************************
// Type: Izi.Travel.Shell.Core.Components.Behaviors.MapViewBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Toolkit.Controls.Maps;
using Izi.Travel.Utility;
using Microsoft.Xaml.Interactivity;
using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;


namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  /// <summary>
  /// Behavior for managing the view state of a MapControl
  /// </summary>
  public class MapViewBehavior : Behavior<MapControl>
  {
    private bool _isRefreshing;
    private bool _isInitialized;

    #region Dependency Properties

    public static readonly DependencyProperty ViewProperty =
        DependencyProperty.Register(nameof(View), typeof(LocationRectangle), typeof(MapViewBehavior),
            new PropertyMetadata(null, OnViewPropertyChanged));

    public static readonly DependencyProperty ViewCenterProperty =
        DependencyProperty.Register(nameof(ViewCenter), typeof(Geopoint), typeof(MapViewBehavior),
            new PropertyMetadata(null, OnViewCenterPropertyChanged));

    public static readonly DependencyProperty ViewZoomLevelProperty =
        DependencyProperty.Register(nameof(ViewZoomLevel), typeof(double), typeof(MapViewBehavior),
            new PropertyMetadata(0.0, OnViewZoomLevelPropertyChanged));

    public static readonly DependencyProperty IsViewChangingProperty =
        DependencyProperty.Register(nameof(IsViewChanging), typeof(bool), typeof(MapViewBehavior),
            new PropertyMetadata(false));

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the maximum zoom level for animations
    /// </summary>
    public double ZoomAnimationMaxValue { get; set; } = 20.0;

    /// <summary>
    /// Gets or sets the desired margin when setting the map view
    /// </summary>
    public Thickness ZoomDesiredMargin { get; set; } = new Thickness(40);

    /// <summary>
    /// Gets or sets the current map view as a bounding rectangle
    /// </summary>
    public LocationRectangle View
    {
        get => (LocationRectangle)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }

    /// <summary>
    /// Gets or sets the center point of the map view
    /// </summary>
    public Geopoint ViewCenter
    {
        get => (Geopoint)GetValue(ViewCenterProperty);
        set => SetValue(ViewCenterProperty, value);
    }

    /// <summary>
    /// Gets or sets the zoom level of the map view
    /// </summary>
    public double ViewZoomLevel
    {
        get => (double)GetValue(ViewZoomLevelProperty);
        set => SetValue(ViewZoomLevelProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the map view is currently changing
    /// </summary>
    public bool IsViewChanging
    {
        get => (bool)GetValue(IsViewChangingProperty);
        set => SetValue(IsViewChangingProperty, value);
    }

    #endregion

    protected override void OnAttached()
    {
        base.OnAttached();
            
        if (AssociatedObject == null) return;
            
        _isInitialized = true;
            
        // Subscribe to map events
        AssociatedObject.CenterChanged += OnMapCenterChanged;
        AssociatedObject.ZoomLevelChanged += OnMapZoomLevelChanged;
        AssociatedObject.ActualCameraChanged += OnMapCameraChanged;
            
        // Initial refresh
        RefreshView();
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
        {
            // Unsubscribe from map events
            AssociatedObject.CenterChanged -= OnMapCenterChanged;
            AssociatedObject.ZoomLevelChanged -= OnMapZoomLevelChanged;
            AssociatedObject.ActualCameraChanged -= OnMapCameraChanged;
        }
            
        _isInitialized = false;
        base.OnDetaching();
    }

    private void OnMapCenterChanged(MapControl sender, object args)
    {
        if (!_isInitialized || _isRefreshing) return;

        var center = sender.Center;
        if (center != null)
        {
            ViewCenter = new Geopoint(new BasicGeoposition
            {
                Latitude = center.Position.Latitude,
                Longitude = center.Position.Longitude
            });
        }
    }

    private void OnMapZoomLevelChanged(MapControl sender, object args)
    {
        if (!_isInitialized || _isRefreshing) return;
            
        ViewZoomLevel = sender.ZoomLevel;
    }

    private void OnMapCameraChanged(MapControl sender, MapActualCameraChangedEventArgs args)
    {
        if (!_isInitialized || _isRefreshing) return;
            
        RefreshView();
    }

    private void OnMapSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!_isInitialized || _isRefreshing) return;
            
        RefreshView();
    }

    private async void RefreshView()
    {
        if (!_isInitialized || AssociatedObject == null) return;

        _isRefreshing = true;

        try
        {
            // Get the current map bounds
            //var bounds = await AssociatedObject.GetVisibleRegionAsync(MapVisibleRegion.Full);
            Geopath bounds = AssociatedObject.GetVisibleRegion(/*MapVisibleRegion.Full*/MapVisibleRegionKind.Full);
            // Get the current map bounds
            //GeoboundingBox bounds = AssociatedObject.GetVisibleRegion(MapVisibleRegionKind.Full);
            if (bounds != null)
            {
                var nw = new Geopoint(new BasicGeoposition
                {
                    Latitude = default,//bounds.NorthwestCorner.Latitude,
                    Longitude = default//bounds.NorthwestCorner.Longitude
                });
                var se = new Geopoint(new BasicGeoposition
                {
                    Latitude = default,//bounds.SoutheastCorner.Latitude,
                    Longitude =default //bounds.SoutheastCorner.Longitude
                });

                View = LocationRectangle.CreateBoundingRectangle(nw, se);
            }

            // Update center and zoom level
            var center = AssociatedObject.Center;
            if (center != null)
            {
                ViewCenter = new Geopoint(new BasicGeoposition
                {
                    Latitude = center.Position.Latitude,
                    Longitude = center.Position.Longitude
                });
            }

            ViewZoomLevel = AssociatedObject.ZoomLevel;
        }
        finally
        {
            _isRefreshing = false;
        }
    }

    private static async void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = d as MapViewBehavior;
        if (behavior == null || behavior._isRefreshing || !behavior._isInitialized) return;
            
        var newValue = e.NewValue as LocationRectangle;
        if (newValue == null) return;
            
        // Convert to GeoboundingBox
        var boundingBox = newValue.ToGeoboundingBox();
        if (boundingBox == null) return;
            
        // Update map view
        await behavior.AssociatedObject.TrySetViewBoundsAsync(
            boundingBox, 
            behavior.ZoomDesiredMargin, 
            MapAnimationKind.Default);
    }

    private static async void OnViewCenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //var boundingBox = new GeoboundingBox(default, default, default);
        var behavior = d as MapViewBehavior;
        if (behavior == null || behavior._isRefreshing || !behavior._isInitialized) return;

        if (e.NewValue is BasicGeoposition newCenter)
        {
            var center = new Geopoint(newCenter);
            await behavior.AssociatedObject.TrySetViewAsync(
                center,
                /*boundingBox*/default,
                /*behavior.ZoomDesiredMargin,*/default,
                /*MapAnimationKind.Default*/default);
        }
        else
        {
            return;
        }
    }
       

   private static void OnViewZoomLevelPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is MapViewBehavior mapViewBehavior) || mapViewBehavior._isRefreshing || !(e.NewValue is double))
        return;
      double newValue = (double) e.NewValue;
      //mapViewBehavior.AssociatedObject.SetView(mapViewBehavior.ViewCenter, newValue.Clamp(1.0, 20.0), MapAnimationKind.Parabolic);
    }

    private void OnMapCenterChanged(object sender, MapCenterChangedEventArgs args)
    {
      this.RefreshView();
    }

    /*private void OnMapSizeChanged(object sender, SizeChangedEventArgs args) => this.RefreshView();*/

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
