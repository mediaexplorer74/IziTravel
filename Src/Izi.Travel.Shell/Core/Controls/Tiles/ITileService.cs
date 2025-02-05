// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Tiles.ITileService
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Tiles
{
  public interface ITileService
  {
    void Initialize(BaseTileControl control);

    void Deinitialize(BaseTileControl control);

    void FreezeTile(BaseTileControl control);

    void UnfreezeTile(BaseTileControl control);
  }
}
