// ********************************************************************
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapChildControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll


using Izi.Travel.Data.Entities.Common;
using System.ComponentModel;
//using System.Device.Location;
using System.Windows;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  [Windows.UI.Xaml.Markup.ContentProperty(Name="Content")]//"Content")]
  public class MapChildControl : ContentControl
  {
    public static readonly DependencyProperty GeoCoordinateProperty
            = DependencyProperty.Register(nameof (GeoCoordinate), typeof (GeoCoordinate), typeof (MapChildControl), new PropertyMetadata(new PropertyChangedCallback(MapChildControl.OnGeoCoordinateChangedCallback)));
    public static readonly DependencyProperty PositionOriginProperty 
            = DependencyProperty.Register(nameof (PositionOrigin), typeof (Point), typeof (MapChildControl), new PropertyMetadata(new PropertyChangedCallback(MapChildControl.OnPositionOriginChangedCallback)));

    //[TypeConverter(typeof (GeoCoordinateConverter))]
    public GeoCoordinate GeoCoordinate
    {
      get => (GeoCoordinate) this.GetValue(MapChildControl.GeoCoordinateProperty);
      set => this.SetValue(MapChildControl.GeoCoordinateProperty, (object) value);
    }

    public Point PositionOrigin
    {
      get => (Point) this.GetValue(MapChildControl.PositionOriginProperty);
      set => this.SetValue(MapChildControl.PositionOriginProperty, (object) value);
    }

    private static void OnGeoCoordinateChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      d.SetValue(MapChild.GeoCoordinateProperty, e.NewValue);
    }

    private static void OnPositionOriginChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      d.SetValue(MapChild.PositionOriginProperty, e.NewValue);
    }
  }
}
