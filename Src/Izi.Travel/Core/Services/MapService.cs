using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Izi.Travel.Mtg.ViewModels.Tour.Map;

namespace Izi.Travel.Core.Services
{
    public class MapService : IMapService, IDisposable
    {
        private bool _isInitialized;
        private Geolocator _geolocator;
        // Development key - replace with a production key before release
        // Note: This is a sample key and may have usage limits
        private const string MapServiceToken = "AqTGBsziZHIJYYxgivLBf0hVdhAcFj8m0KpEY16jQWhWBV70VoU9rRiLYaHVmk3d";

        public MapService()
        {
            Initialize(MapServiceToken);
        }

        public void Initialize(string mapServiceToken)
        {
            if (string.IsNullOrEmpty(mapServiceToken))
                throw new ArgumentNullException(nameof(mapServiceToken));

            // Set the authentication key for map services
            MapService.ServiceToken = mapServiceToken;
            _isInitialized = true;
            
            // Initialize geolocator
            _geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 50,
                ReportInterval = 5000
            };
        }

        public async Task<IEnumerable<MapItemViewModel>> GetMapItemsForTourAsync(string tourId, string language)
        {
            if (string.IsNullOrEmpty(tourId))
                throw new ArgumentNullException(nameof(tourId));

            if (!_isInitialized)
                throw new InvalidOperationException("MapService has not been initialized. Call Initialize() first.");

            // Return sample data for development
            var items = new List<MapItemViewModel>();
            
            try
            {
                // Sample data for development
                var currentLocation = await GetCurrentLocationAsync() ?? new Geopoint(new BasicGeoposition
                {
                    Latitude = 51.5074, // Default to London
                    Longitude = -0.1278
                });

                // Add sample points around the current location
                items.Add(new MapItemViewModel
                {
                    Id = "1",
                    Title = "Tour Start",
                    Description = "Starting point of the tour",
                    Location = currentLocation
                });

                // Add some sample points around the center
                var random = new Random();
                for (int i = 2; i <= 5; i++)
                {
                    items.Add(new MapItemViewModel
                    {
                        Id = i.ToString(),
                        Title = $"Point of Interest {i-1}",
                        Description = $"Description for point {i-1}",
                        Location = new Geopoint(new BasicGeoposition
                        {
                            Latitude = currentLocation.Position.Latitude + (random.NextDouble() * 0.01 - 0.005),
                            Longitude = currentLocation.Position.Longitude + (random.NextDouble() * 0.01 - 0.005)
                        })
                    });
                }

                return items;
            }
            catch (Exception ex)
            {
                // Log error
                System.Diagnostics.Debug.WriteLine($"Error loading map items: {ex.Message}");
                return Enumerable.Empty<MapItemViewModel>();
            }
        }

        public async Task<MapRoute> GetDirectionsAsync(Geopoint start, Geopoint end)
        {
            if (start == null) throw new ArgumentNullException(nameof(start));
            if (end == null) throw new ArgumentNullException(nameof(end));

            if (!_isInitialized)
                throw new InvalidOperationException("MapService has not been initialized. Call Initialize() first.");

            try
            {
                // Get the route between the points
                var routeResult = await MapRouteFinder.GetWalkingRouteAsync(start, end);
                
                if (routeResult.Status == MapRouteFinderStatus.Success)
                {
                    return routeResult.Route;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting directions: {ex.Message}");
                return null;
            }
        }

        public async Task<Geopoint> GetCurrentLocationAsync()
        {
            if (_geolocator == null)
                return null;

            try
            {
                // Request permission to access location
                var accessStatus = await Geolocator.RequestAccessAsync();
                if (accessStatus != GeolocationAccessStatus.Allowed)
                    return null;

                // Get the current location
                var position = await _geolocator.GetGeopositionAsync();
                return position?.Coordinate?.Point;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting current location: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _geolocator = null;
        }
    }
}
