// ********************************************************************
// Type: Izi.Travel.Common.ViewModels.List.IListWithSearchViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using System.ComponentModel;

#nullable disable
namespace Izi.Travel.Common.ViewModels.List
{
  public interface IListWithSearchViewModel : 
    IListViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
  {
    string Query { get; }

    void Clear();
  }
}
