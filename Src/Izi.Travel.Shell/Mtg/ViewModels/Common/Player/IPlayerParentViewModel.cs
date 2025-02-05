// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.IPlayerParentViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player
{
  public interface IPlayerParentViewModel
  {
    bool AutoPlay { get; }

    string ParentUid { get; }

    string Uid { get; }

    string Language { get; }
  }
}
