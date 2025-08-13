using Windows.Devices.Geolocation;
using Caliburn.Micro;
using Izi.Travel.Core.Models;

namespace Izi.Travel.Mtg.ViewModels.Tour.Map
{
    public class MapItemViewModel : PropertyChangedBase
    {
        private string _id;
        private string _title;
        private string _description;
        private Geopoint _location;
        private bool _isSelected;
        private string _iconUrl;

        /// <summary>
        /// Unique identifier for the map item
        /// </summary>
        public string Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        /// <summary>
        /// Display title of the map item
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        /// <summary>
        /// Additional description for the map item
        /// </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        /// <summary>
        /// Geographic location of the item
        /// </summary>
        public Geopoint Location
        {
            get => _location;
            set => Set(ref _location, value);
        }

        /// <summary>
        /// Indicates if this item is currently selected
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        /// <summary>
        /// URL to an icon representing this item
        /// </summary>
        public string IconUrl
        {
            get => _iconUrl;
            set => Set(ref _iconUrl, value);
        }

        /// <summary>
        /// Optional data associated with this map item
        /// </summary>
        public object Tag { get; set; }

        public override string ToString()
        {
            return $"{Title} ({Location?.Position.Latitude}, {Location?.Position.Longitude})";
        }
    }
}
