// Temporary shims to allow UWP build while WP8 maps/routing are being ported.
// These do NOT implement real routing. Replace with UWP MapRouteFinder integration later.

using System;
using System.Collections.Generic;

namespace System.Device.Location
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static GeoCoordinate Unknown { get; } = new GeoCoordinate();

        public GeoCoordinate() { }
        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude; Longitude = longitude;
        }

        internal double GetDistanceTo(GeoCoordinate geoCoordinate)
        {
            throw new NotImplementedException();
        }
    }

    public class GeoCoordinateCollection : List<GeoCoordinate> { }
}

namespace Microsoft.Phone.Maps.Services
{
    using System.Device.Location;

    public enum TravelMode { Driving, Walking }
    public enum RouteOptimization { MinimizeTime, MinimizeDistance }

    public class Route
    {
        public List<RouteLeg> Legs { get; } = new List<RouteLeg>();
    }

    public class RouteLeg
    {
        public List<RouteManeuver> Maneuvers { get; } = new List<RouteManeuver>();
    }

    public class RouteManeuver
    {
        public int LengthInMeters { get; set; }
        public string InstructionText { get; set; }
    }

    public class QueryCompletedEventArgs<T> : EventArgs
    {
        public T Result { get; set; }
        public QueryCompletedEventArgs(T result) { Result = result; }
    }

    public class RouteQuery
    {
        public RouteOptimization RouteOptimization { get; set; }
        public TravelMode TravelMode { get; set; }
        public IEnumerable<GeoCoordinate> Waypoints { get; set; }

        public event EventHandler<QueryCompletedEventArgs<Route>> QueryCompleted;

        public void QueryAsync()
        {
            // Return empty route for now
            QueryCompleted?.Invoke(this, new QueryCompletedEventArgs<Route>(new Route()));
        }
    }
}
