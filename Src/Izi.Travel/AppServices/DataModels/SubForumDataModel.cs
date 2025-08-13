// IziTravel.AppServices.DataModels.SubForumDataModel

//using Windows.UI.Xaml.Media;
using Windows.UI;

#nullable disable
namespace Izi.Travel.AppServices.DataModels
{
  public class SubForumDataModel : ForumDataModel
  {
    public int SquareWidth { get; set; }

    public int SquareHeight { get; set; }

    public Color Color { get; set; }

    public bool IsRoot => this.SquareHeight == 0;

    public int FontSize { get; set; }
  }
}
