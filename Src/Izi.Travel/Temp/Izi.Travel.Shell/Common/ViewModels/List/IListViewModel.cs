// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.ViewModels.List.IListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using System.ComponentModel;

#nullable disable
namespace Izi.Travel.Shell.Common.ViewModels.List
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
