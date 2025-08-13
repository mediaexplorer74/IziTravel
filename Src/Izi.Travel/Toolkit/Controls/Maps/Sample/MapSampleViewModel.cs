// ********************************************************************
// MapSampleViewModel.cs
// Sample ViewModel demonstrating how to use the Map control

using System;
using System.Collections.ObjectModel;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Caliburn.Micro;

namespace Izi.Travel.Toolkit.Controls.Maps.Sample
{
    public class MapSampleViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private Map _map;
        private Geopoint _center;
        private double _zoomLevel;
        private string _statusMessage;

        public MapSampleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            
            // Initialize commands
            ZoomInCommand = new RelayCommand(ZoomIn);
            ZoomOutCommand = new RelayCommand(ZoomOut);
            CenterOnLocationCommand = new RelayCommand(CenterOnCurrentLocation);
            
            // Initialize map
            InitializeMap();
        }

        public Map Map
        {
            get => _map;
            set
            {
                if (_map != value)
                {
                    _map = value;
                    NotifyOfPropertyChange(() => Map);
                }
            }
        }

        public Geopoint Center
        {
            get => _center;
            set
            {
                if (_center != value)
                {
                    _center = value;
                    NotifyOfPropertyChange(() => Center);
                }
            }
        }

        public double ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                if (Math.Abs(_zoomLevel - value) > double.Epsilon)
                {
                    _zoomLevel = value;
                    NotifyOfPropertyChange(() => ZoomLevel);
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    NotifyOfPropertyChange(() => StatusMessage);
                }
            }
        }

        public IRelayCommand ZoomInCommand { get; }
        public IRelayCommand ZoomOutCommand { get; }
        public IRelayCommand CenterOnLocationCommand { get; }

        private async void InitializeMap()
        {
            try
            {
                // Initialize map control helper
                MapControlHelper.Initialize();

                // Create a new map control
                var mapControl = MapControlHelper.CreateMapControl();
                Map = new Map(mapControl);

                // Set initial position (Seattle)
                Center = new Geopoint(new BasicGeoposition
                {
                    Latitude = 47.6062,
                    Longitude = -122.3321
                });
                
                ZoomLevel = 12;
                StatusMessage = "Map initialized successfully";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error initializing map: {ex.Message}";
                // Log the error as needed
                System.Diagnostics.Debug.WriteLine(StatusMessage);
            }
        }

        private void ZoomIn()
        {
            if (ZoomLevel < 20) // Max zoom level
            {
                ZoomLevel += 1;
            }
        }

        private void ZoomOut()
        {
            if (ZoomLevel > 1) // Min zoom level
            {
                ZoomLevel -= 1;
            }
        }

        private async void CenterOnCurrentLocation()
        {
            try
            {
                StatusMessage = "Getting current location...";
                
                // Request permission to access location
                var accessStatus = await Geolocator.RequestAccessAsync();
                if (accessStatus != GeolocationAccessStatus.Allowed)
                {
                    StatusMessage = "Location access denied. Please enable location services.";
                    return;
                }

                // Get current location
                var geolocator = new Geolocator { DesiredAccuracyInMeters = 100 };
                var position = await geolocator.GetGeopositionAsync();
                
                // Center map on current location
                Center = position.Coordinate.Point;
                StatusMessage = "Centered on your current location";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error getting location: {ex.Message}";
                System.Diagnostics.Debug.WriteLine(StatusMessage);
            }
        }
    }
}
