// ********************************************************************
// Type: Izi.Travel.Common.ViewModels.List.IListViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Command;
using System.ComponentModel;

#nullable disable
namespace Izi.Travel.Common.ViewModels.List
{
  public interface IListViewModel : 
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
  {
    bool IsDataLoading { get; }

    bool IsListEmpty { get; }

    RelayCommand LoadDataCommand { get; }

    RelayCommand RefreshCommand { get; }

    RelayCommand NavigateCommand { get; }

    RelayCommand DeleteItemCommand { get; }
  }
}
