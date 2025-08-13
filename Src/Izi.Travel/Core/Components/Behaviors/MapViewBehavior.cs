// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.MapViewBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Toolkit.Controls.Maps;
using Izi.Travel.Utility;
using Microsoft.Xaml.Interactivity;
using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;


namespace Izi.Travel.Core.Components.Behaviors
{
  /// <summary>
  /// Behavior for managing the view state of a MapControl
  /// </summary>
  public class MapViewBehavior : Behavior<Toolkit.Controls.Maps.MapControl>
  {
    private bool _isRefreshing;
    private bool _isInitialized;
    private bool _isViewChangingInternal;

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
        AssociatedObject.SizeChanged += OnMapSizeChanged;
        
        // Set initial view if center is already set
        if (ViewCenter != null)
        {
            _ = UpdateMapViewAsync(ViewCenter, ViewZoomLevel);
        }
        
        // Initial refresh
        RefreshView();
    }

        private void OnMapZoomLevelChanged(Toolkit.Controls.Maps.MapControl control, object arg2)
        {
            throw new NotImplementedException();
        }

        private void OnMapCameraChanged(Toolkit.Controls.Maps.MapControl control, MapActualCameraChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void OnMapCenterChanged(Toolkit.Controls.Maps.MapControl control, object arg2)
        {
            throw new NotImplementedException();
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

    /*private void OnMapCenterChanged(Toolkit.Controls.Maps.MapControl sender, object args)
    {
        if (!_isInitialized || _isRefreshing) return;

        var center = sender.Center;
        if (center != null && !_isViewChangingInternal)
        {
            ViewCenter = new Geopoint(new BasicGeoposition
            {
                Latitude = center.Position.Latitude,
                Longitude = center.Position.Longitude
            });
        }
    }*/

    /*private void OnMapZoomLevelChanged(Toolkit.Controls.Maps.MapControl sender, object args)
    {
        if (!_isInitialized || _isRefreshing || _isViewChangingInternal) return;
        ViewZoomLevel = sender.ZoomLevel;
    }*/

    /*private void OnMapCameraChanged(Toolkit.Controls.Maps.MapControl sender, MapActualCameraChangedEventArgs args)
    {
        if (!_isInitialized || _isRefreshing || _isViewChangingInternal) return;
        RefreshView();
    }*/

    private void OnMapSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!_isInitialized || _isRefreshing || _isViewChangingInternal) return;
        RefreshView();
    }

    private async void RefreshView()
    {
        if (!_isInitialized || AssociatedObject == null || _isRefreshing) return;

        _isRefreshing = true;

        try
        {
            // Get the current map bounds
            Geopoint bounds = AssociatedObject.GetVisibleRegion(MapVisibleRegionKind.Full);
            /*if (bounds != null && bounds.Positions.Count >= 2)
            {
                var nw = new Geopoint(bounds.Positions[0]);
                // Use positions[2] if available, otherwise use positions[1]
                var sePosition = bounds.Positions.Count > 2 ? bounds.Positions[2] : bounds.Positions[1];
                var se = new Geopoint(sePosition);
                
                View = LocationRectangle.CreateBoundingRectangle(nw, se);
            }*/

            // Update center and zoom level from the map control
            var center = AssociatedObject.Center;
            if (center != null && !_isViewChangingInternal)
            {
                // Only update ViewCenter if it's not already being set programmatically
                ViewCenter = center;
            }

            ViewZoomLevel = AssociatedObject.ZoomLevel;
        }
        catch (Exception ex)
        {
            // Log error if needed
            System.Diagnostics.Debug.WriteLine($"Error in RefreshView: {ex.Message}");
        }
        finally
        {
            _isRefreshing = false;
        }
    }

    private static async void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (!(d is MapViewBehavior behavior) || behavior._isRefreshing || !behavior._isInitialized) 
            return;
            
        if (e.NewValue is LocationRectangle newValue)
        {
            try
            {
                behavior._isViewChangingInternal = true;
                
                // Convert to GeoboundingBox
                var boundingBox = newValue.ToGeoboundingBox();
                if (boundingBox != null)
                {
                    // Update map view
                    await behavior.AssociatedObject.TrySetViewBoundsAsync(
                        boundingBox, 
                        behavior.ZoomDesiredMargin, 
                        MapAnimationKind.Default);
                }
            }
            finally
            {
                behavior._isViewChangingInternal = false;
            }
        }
    }

    private static async void OnViewCenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (!(d is MapViewBehavior behavior) || behavior._isRefreshing || !behavior._isInitialized) 
            return;

        if (e.NewValue is Geopoint newCenter)
        {
            try
            {
                behavior._isViewChangingInternal = true;
                await behavior.UpdateMapViewAsync(newCenter, behavior.ViewZoomLevel);
            }
            finally
            {
                behavior._isViewChangingInternal = false;
            }
        }
    }
       

    private static async void OnViewZoomLevelPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        if (!(d is MapViewBehavior behavior) || behavior._isRefreshing || !behavior._isInitialized) 
            return;
            
        if (e.NewValue is double newZoomLevel)
        {
            try
            {
                behavior._isViewChangingInternal = true;
                await behavior.UpdateMapViewAsync(behavior.ViewCenter, newZoomLevel);
            }
            finally
            {
                behavior._isViewChangingInternal = false;
            }
        }
    }

    // Removed duplicate event handlers - using the ones above with additional checks
    
    private async Task UpdateMapViewAsync(Geopoint center, double zoomLevel)
    {
        if (center == null || AssociatedObject == null) return;
        
        try
        {
            await AssociatedObject.TrySetViewAsync(
                center,
                zoomLevel,
                null, // No heading
                null, // No pitch
                MapAnimationKind.Default);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating map view: {ex.Message}");
        }
    }
  }
}
