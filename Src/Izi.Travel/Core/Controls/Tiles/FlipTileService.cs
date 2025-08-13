// ********************************************************************
// Type: Izi.Travel.Core.Controls.Tiles.FlipTileService
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

#nullable disable
namespace Izi.Travel.Core.Controls.Tiles
{
  internal class FlipTileService : TileServiceBase
  {
    protected override void ProcessTile(BaseTileControl control)
    {
      if (!(control is FlipTileControl flipTileControl))
        return;
      int num = flipTileControl.State == FlipTileState.Front ? 1 : 0;
      flipTileControl.State = (FlipTileState) num;
    }
  }
}
