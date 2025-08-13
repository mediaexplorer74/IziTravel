// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.Interfaces.IDetailViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Command;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail.Interfaces
{
  public interface IDetailViewModel : IMtgObjectViewModel, IMtgObjectProvider
  {
    IDetailPartViewModel DetailPartViewModel { get; }

    RelayCommand OpenMapCommand { get; }
  }
}
