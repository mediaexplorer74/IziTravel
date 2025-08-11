using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Izi.Travel.Data.Entities.Common;

namespace Izi.Travel.Helpers
{
    public static class GeoHelper
    {
        /// <summary>
        /// Converts a Windows.Devices.Geolocation.Geopoint to a GeoCoordinate
        /// </summary>
        public static GeoCoordinate ToGeoCoordinate(this Geopoint point)
        {
            if (point == null)
                return GeoCoordinate.Unknown;
                
            return point.Position.ToGeoCoordinate();
        }

        public static GeoCoordinate ToGeoCoordinate(this BasicGeoposition position)
        {
            return new GeoCoordinate
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Altitude = position.Altitude
            };
        }

        public static BasicGeoposition ToBasicGeoposition(this GeoCoordinate coordinate)
        {
            return new BasicGeoposition
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude,
                Altitude = coordinate.Altitude
            };
        }

        public static Geopoint ToGeopoint(this GeoCoordinate coordinate)
        {
            return new Geopoint(coordinate.ToBasicGeoposition());
        }

        public static IEnumerable<GeoCoordinate> ToGeoCoordinates(this IEnumerable<BasicGeoposition> positions)
        {
            return positions?.Select(p => p.ToGeoCoordinate()) ?? Enumerable.Empty<GeoCoordinate>();
        }

        public static IEnumerable<BasicGeoposition> ToBasicGeopositions(this IEnumerable<GeoCoordinate> coordinates)
        {
            return coordinates?.Select(c => c.ToBasicGeoposition()) ?? Enumerable.Empty<BasicGeoposition>();
        }
    }
}
