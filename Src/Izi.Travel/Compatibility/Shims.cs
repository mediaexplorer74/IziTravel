// Compatibility shims to allow building UWP project after WP8 to UWP port.
// These are minimal placeholders for WP8-era types used in the codebase.
// They are NOT full implementations and may need replacement with proper UWP APIs.

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
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool Equals(object obj)
        {
            var other = obj as GeoCoordinate;
            if (other == null) return false;
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }

        public static bool operator ==(GeoCoordinate a, GeoCoordinate b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }
        public static bool operator !=(GeoCoordinate a, GeoCoordinate b) => !(a == b);
    }

    public class GeoCoordinateCollection : List<GeoCoordinate>
    {
        public GeoCoordinateCollection() { }
        public GeoCoordinateCollection(IEnumerable<GeoCoordinate> items) : base(items) { }
    }
}

namespace Microsoft.Phone.Maps.Services
{
    public enum TravelMode
    {
        Driving,
        Walking,
        Transit
    }

    public enum RouteOptimization
    {
        MinimizeTime,
        MinimizeDistance
    }

    public class QueryCompletedEventArgs<T> : EventArgs
    {
        public T Result { get; set; }
        public Exception Error { get; set; }
        public bool Cancelled { get; set; }
    }

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

    public class RouteQuery
    {
        public RouteOptimization RouteOptimization { get; set; }
        public TravelMode TravelMode { get; set; }
        public System.Collections.Generic.List<System.Device.Location.GeoCoordinate> Waypoints { get; set; } = new System.Collections.Generic.List<System.Device.Location.GeoCoordinate>();

        public event EventHandler<QueryCompletedEventArgs<Route>> QueryCompleted;

        public void QueryAsync()
        {
            // Minimal stub: immediately raise completed with an empty route.
            QueryCompleted?.Invoke(this, new QueryCompletedEventArgs<Route> { Result = new Route() });
        }
    }
}
