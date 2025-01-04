// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Model.SearchFlyoutResult
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Model
{
  public class SearchFlyoutResult
  {
    public static readonly SearchFlyoutResult Empty = new SearchFlyoutResult();

    public bool Success { get; internal set; }

    public MtgObject MtgObject { get; internal set; }

    public MtgObject MtgObjectParent { get; internal set; }

    public object Tag { get; internal set; }
  }
}
