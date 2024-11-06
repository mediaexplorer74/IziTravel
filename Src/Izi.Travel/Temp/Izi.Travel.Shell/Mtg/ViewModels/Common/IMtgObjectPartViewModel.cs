// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.IMtgObjectPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Command;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common
{
  public interface IMtgObjectPartViewModel : IMtgObjectProvider
  {
    string Uid { get; set; }

    string Language { get; }

    string ParentUid { get; set; }

    bool IsDataLoading { get; }

    RelayCommand RefreshCommand { get; }
  }
}
