using Izi.Travel.Shell.Mtg.ViewModels.Museum.Map;

namespace Izi.Travel.Shell.Mtg.Views.Museum.Map
{
    interface IMapItem
    {
        string Id { get; set; }
        string IconUrl { get; set; }
        object Location { get; set; }
        string Title { get; set; }
    }
}