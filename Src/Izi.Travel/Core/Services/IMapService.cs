using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;

namespace Izi.Travel.Shell.Core.Services
{
    public interface IMapService
    {
        /// <summary>
        /// Gets map items for a specific tour
        /// </summary>
        /// <param name="tourId">The ID of the tour</param>
        /// <param name="language">The language for the map items</param>
        /// <returns>A list of map items</returns>
        Task<IEnumerable<MapItemViewModel>> GetMapItemsForTourAsync(string tourId, string language);
        
        /// <summary>
        /// Gets directions between two points
        /// </summary>
        Task<MapRoute> GetDirectionsAsync(Geopoint start, Geopoint end);
        
        /// <summary>
        /// Gets the current device location
        /// </summary>
        Task<Geopoint> GetCurrentLocationAsync();
        
        /// <summary>
        /// Initializes the map service with required API keys
        /// </summary>
        void Initialize(string mapServiceToken);
    }
}
