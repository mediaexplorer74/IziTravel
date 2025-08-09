// ********************************************************************
// Type: Izi.Travel.Business.Entities.Data.LocationRectangle
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Data.Entities.Common;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Izi.Travel.Business.Entities.Data
{
    /// <summary>
    /// Represents a rectangular area on a map defined by its northwest and southeast corners.
    /// </summary>
    public class LocationRectangle
    {
        private Geopoint location;
        private double v1;
        private double v2;
        public Geopoint Center;

        public LocationRectangle(Geopoint location, double v1, double v2)
        {
            this.location = location;
            this.v1 = v1;
            this.v2 = v2;
        }

        /// <summary>
        /// Gets or sets the northwest corner of the rectangle.
        /// </summary>
        public GeoCoordinate Northwest { get; set; }

        /// <summary>
        /// Gets or sets the southeast corner of the rectangle.
        /// </summary>
        public GeoCoordinate Southeast { get; set; }

        /// <summary>
        /// Creates a bounding rectangle that contains the two specified points.
        /// </summary>
        /// <param name="point1">The first point to include in the rectangle.</param>
        /// <param name="point2">The second point to include in the rectangle.</param>
        /// <returns>A rectangle that contains both points.</returns>
        public static LocationRectangle CreateBoundingRectangle(Geopoint point1, Geopoint point2)
        {
            if (point1 == null) throw new ArgumentNullException(nameof(point1));
            if (point2 == null) throw new ArgumentNullException(nameof(point2));

            double north = default;//Math.Max(point1.Latitude, point2.Latitude);
            double south = default;//Math.Min(point1.Latitude, point2.Latitude);
            double east = default;//Math.Max(point1.Longitude, point2.Longitude);
            double west = default;//Math.Min(point1.Longitude, point2.Longitude);

            return new LocationRectangle(point2, 20, 20) // Assuming v1 and v2 are some values you want to set
            {
                Northwest = new GeoCoordinate(north, west),
                Southeast = new GeoCoordinate(south, east)
            };
        }

        /// <summary>
        /// Converts this LocationRectangle to a Windows.Devices.Geolocation.GeoboundingBox.
        /// </summary>
        /// <returns>A GeoboundingBox that represents the same area as this LocationRectangle.</returns>
        public GeoboundingBox ToGeoboundingBox()
        {
            if (Northwest == null || Southeast == null)
                return null;

            var northwest = new BasicGeoposition { Latitude = Northwest.Latitude, Longitude = Northwest.Longitude };
            var southeast = new BasicGeoposition { Latitude = Southeast.Latitude, Longitude = Southeast.Longitude };
            
            return new GeoboundingBox(northwest, southeast);
        }

        /// <summary>
        /// Creates a LocationRectangle from a GeoboundingBox.
        /// </summary>
        /// <param name="box">The GeoboundingBox to convert.</param>
        /// <returns>A LocationRectangle that represents the same area as the GeoboundingBox.</returns>
        public static LocationRectangle FromGeoboundingBox(GeoboundingBox box)
        {
            if (box == null)
                return null;

            //return new LocationRectangle
            //{
            //    Northwest = new GeoCoordinate(box.NorthwestCorner.Latitude, box.NorthwestCorner.Longitude),
            //    Southeast = new GeoCoordinate(box.SoutheastCorner.Latitude, box.SoutheastCorner.Longitude)
            //};
            return new LocationRectangle(
                new Geopoint(new BasicGeoposition { Latitude = box.NorthwestCorner.Latitude, Longitude = box.NorthwestCorner.Longitude }),
                /*box.Width*/20,
                /*box.Heigth*/20);
        }

        public static LocationRectangle CreateBoundingRectangle(IEnumerable<Geopoint> locations)
        {
            throw new NotImplementedException();
        }
    }
}