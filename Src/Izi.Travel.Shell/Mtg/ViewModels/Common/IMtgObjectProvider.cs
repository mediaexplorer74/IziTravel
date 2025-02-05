// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.IMtgObjectProvider
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common
{
  public interface IMtgObjectProvider
  {
    MtgObject MtgObject { get; }

    MtgObject MtgObjectParent { get; }

    MtgObject MtgObjectRoot { get; }
  }
}
