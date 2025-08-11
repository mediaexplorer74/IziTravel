using Caliburn.Micro;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Map;

namespace Izi.Travel.Shell.Mtg.Messages
{
    public class MapItemSelectedMessage
    {
        public BaseMapItemViewModel MapItem { get; private set; }

        public MapItemSelectedMessage(BaseMapItemViewModel mapItem)
        {
            MapItem = mapItem;
        }
    }
}
