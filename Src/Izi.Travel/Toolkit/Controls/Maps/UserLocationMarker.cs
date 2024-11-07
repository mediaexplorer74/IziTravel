// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.UserLocationMarker
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Geofencing.Geotracker;
using Izi.Travel.Geofencing.Helpers;
using Izi.Travel.Geofencing.Primitives;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Device.Location;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  [System.Windows.Markup.ContentProperty("Content")]
  public sealed class UserLocationMarker : MapChildControl
  {
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof (Radius), typeof (Thickness), typeof (UserLocationMarker), new PropertyMetadata((object) new Thickness()));
    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof (IsActive), typeof (bool), typeof (UserLocationMarker), new PropertyMetadata((object) false, new PropertyChangedCallback(UserLocationMarker.IsActivePropertyChangedCallback)));
    private Map _map;

    private static void IsActivePropertyChangedCallback(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      UserLocationMarker userLocationMarker = (UserLocationMarker) dependencyObject;
      if ((bool) dependencyPropertyChangedEventArgs.NewValue)
        userLocationMarker.Activate();
      else
        userLocationMarker.Deactivate();
    }

    public Thickness Radius
    {
      get => (Thickness) this.GetValue(UserLocationMarker.RadiusProperty);
      set => this.SetValue(UserLocationMarker.RadiusProperty, (object) value);
    }

    public bool IsActive
    {
      get => (bool) this.GetValue(UserLocationMarker.IsActiveProperty);
      set => this.SetValue(UserLocationMarker.IsActiveProperty, (object) value);
    }

    public UserLocationMarker()
    {
      this.DefaultStyleKey = (object) typeof (UserLocationMarker);
      this.Opacity = 0.0;
    }

    private void Activate()
    {
      if (!Izi.Travel.Business.Managers.Geotracker.Instance.IsEnabled)
        return;
      this._map = this.GetParentByType<Map>();
      if (this._map == null)
        return;
      this._map.ZoomLevelChanged += new EventHandler<MapZoomLevelChangedEventArgs>(this.Map_ZoomLevelChanged);
      // ISSUE: method pointer
      Izi.Travel.Business.Managers.Geotracker.Instance.PositionChanged += new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(GeotrackerOnPositionChanged));
      ((Action) (async () =>
      {
        await Task.Delay(500);
        Geolocation position = Izi.Travel.Business.Managers.Geotracker.Instance.Position;
        if (position == null)
          position = await Izi.Travel.Business.Managers.Geotracker.Instance.GetPosition();
        Geolocation geolocation = position;
        if (geolocation == null)
          return;
        this.UpdateGeoCoordinate(geolocation.ToGeoCoordinate());
      }))();
    }

    private void Deactivate()
    {
      this.Opacity = 0.0;
      if (this._map == null)
        return;
      this._map.ZoomLevelChanged -= new EventHandler<MapZoomLevelChangedEventArgs>(this.Map_ZoomLevelChanged);
      // ISSUE: method pointer
      Izi.Travel.Business.Managers.Geotracker.Instance.PositionChanged -= new TypedEventHandler<IGeotracker, Geolocation>((object) this, __methodptr(GeotrackerOnPositionChanged));
    }

    private void GeotrackerOnPositionChanged(IGeotracker sender, Geolocation args)
    {
      this.UpdateGeoCoordinate(new GeoCoordinate(args.Latitude, args.Longitude)
      {
        HorizontalAccuracy = args.Accuracy
      });
    }

    private void Map_ZoomLevelChanged(object sender, MapZoomLevelChangedEventArgs e)
    {
      this.UpdateRadius();
    }

    private void UpdateGeoCoordinate(GeoCoordinate geoCoordinate)
    {
      if (!(geoCoordinate != (GeoCoordinate) null))
        return;
      this.Opacity = 1.0;
      this.GeoCoordinate = geoCoordinate;
      this.UpdateRadius();
    }

    private void UpdateRadius()
    {
      if (this.GeoCoordinate == (GeoCoordinate) null)
        return;
      if (double.IsNaN(this.GeoCoordinate.HorizontalAccuracy))
        this.GeoCoordinate.HorizontalAccuracy = 5.0;
      this.Radius = new Thickness(-GeoHelper.MetersToPixels(this.GeoCoordinate.HorizontalAccuracy, this.GeoCoordinate.Latitude, this._map.ZoomLevel).Clamp(0.0, 480.0));
    }
  }
}
