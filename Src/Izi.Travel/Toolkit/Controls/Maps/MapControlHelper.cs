// ********************************************************************
// MapControlHelper.cs
// Helper class for initializing and managing the MapControl in UWP

using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Controls.Maps;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    public static class MapControlHelper
    {
        private static bool _isInitialized;
        private static string _mapServiceToken = ""; // TODO: Replace with your actual MapServiceToken

        /// <summary>
        /// Initializes the map control with the required service token and settings
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized)
                return;

            try
            {
                // Set your Bing Maps API key here
                if (!string.IsNullOrEmpty(_mapServiceToken))
                {
                    MapService.ServiceToken = _mapServiceToken;
                }

                // Configure default map settings
                MapService.DataUsagePreference = default; // MapDataUsagePreference.Online;
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                // Log error or handle as appropriate for your app
                System.Diagnostics.Debug.WriteLine($"Error initializing map control: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Creates a new MapControl instance with default settings
        /// </summary>
        public static MapControl CreateMapControl()
        {
            Initialize();

            return new MapControl
            {
                ZoomLevel = 12,
                Center = new Geopoint(new BasicGeoposition { Latitude = 47.6062, Longitude = -122.3321 }), // Default to Seattle
                //MapServiceToken = _mapServiceToken,
                //ZoomInteractionMode = MapInteractionMode.GestureAndControl,
                //TiltInteractionMode = MapInteractionMode.GestureAndControl,
                //RotateInteractionMode = MapInteractionMode.GestureAndControl,
                //Style = MapStyle.Road, // Default to road map style
                //LandmarksVisible = true,
                //PedestrianFeaturesVisible = true,
                //TransitFeaturesVisible = true,
                //TrafficFlowVisible = true
            };
        }

        /// <summary>
        /// Sets the map service token for Bing Maps
        /// </summary>
        public static void SetMapServiceToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            _mapServiceToken = token;
            if (_isInitialized)
            {
                MapService.ServiceToken = _mapServiceToken;
            }
        }
    }
}
