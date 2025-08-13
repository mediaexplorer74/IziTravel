using System.Collections.Generic;
using System.Threading.Tasks;
using Izi.Travel.Mtg.ViewModels.Common.Map;

namespace Izi.Travel.Mtg.Services
{
    public interface IMapService
    {
        Task<IEnumerable<BaseMapItemViewModel>> GetMapItemsForTourAsync(string uid, string language);
        
        // Add other required map service methods here
        // For example:
        // Task<Geopoint> GetCurrentLocationAsync();
        // Task<MapRoute> GetRouteAsync(Geopoint start, Geopoint end);
    }
}
