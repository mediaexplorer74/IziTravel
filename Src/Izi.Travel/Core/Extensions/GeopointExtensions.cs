using System.Device.Location;
using Windows.Devices.Geolocation;

namespace Izi.Travel.Core.Extensions
{
    /// <summary>
    /// Extension methods for converting between Geopoint and GeoCoordinate
    /// </summary>
    public static class GeopointExtensions
    {
        /// <summary>
        /// Converts a Windows.Devices.Geolocation.Geopoint to a System.Device.Location.GeoCoordinate
        /// </summary>
        /// <param name="geopoint">The Geopoint to convert</param>
        /// <returns>A GeoCoordinate with the same latitude and longitude as the Geopoint</returns>
        public static GeoCoordinate ToGeoCoordinate(this Geopoint geopoint)
        {
            if (geopoint == null)
                return GeoCoordinate.Unknown;
                
            return new GeoCoordinate(
                geopoint.Position.Latitude,
                geopoint.Position.Longitude);
        }

        /// <summary>
        /// Converts a System.Device.Location.GeoCoordinate to a Windows.Devices.Geolocation.Geopoint
        /// </summary>
        /// <param name="coordinate">The GeoCoordinate to convert</param>
        /// <returns>A Geopoint with the same latitude and longitude as the GeoCoordinate</returns>
        public static Geopoint ToGeopoint(this GeoCoordinate coordinate)
        {
            if (coordinate == null || coordinate == GeoCoordinate.Unknown)
                return null;
                
            var position = new BasicGeoposition
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude
            };
            
            return new Geopoint(position);
        }
    }
}
