// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces.IDetailPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces
{
  public interface IDetailPartViewModel : IMtgObjectPartViewModel, IMtgObjectProvider
  {
    string SelectedLanguage { get; set; }

    string ActivationType { get; }

    bool AutoPlay { get; }
  }
}
