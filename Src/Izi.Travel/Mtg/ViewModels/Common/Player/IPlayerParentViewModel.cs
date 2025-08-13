// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Player.IPlayerParentViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Player
{
  public interface IPlayerParentViewModel
  {
    bool AutoPlay { get; }

    string ParentUid { get; }

    string Uid { get; }

    string Language { get; }
  }
}
