using Izi.Travel.Mtg.ViewModels.Tour.Map;

namespace Izi.Travel.Core.Messages
{
    /// <summary>
    /// Message published when a map item is selected
    /// </summary>
    public class MapItemSelectedMessage
    {
        /// <summary>
        /// Gets the selected map item
        /// </summary>
        public MapItemViewModel MapItem { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapItemSelectedMessage"/> class
        /// </summary>
        /// <param name="mapItem">The selected map item</param>
        public MapItemSelectedMessage(MapItemViewModel mapItem)
        {
            MapItem = mapItem;
        }
    }
}
