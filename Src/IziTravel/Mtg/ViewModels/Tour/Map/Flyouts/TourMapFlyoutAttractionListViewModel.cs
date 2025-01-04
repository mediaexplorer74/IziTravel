// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.Flyouts.TourMapFlyoutAttractionListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Mtg.ViewModels.Common.Map;
using System.Collections.ObjectModel;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.Flyouts
{
  public sealed class TourMapFlyoutAttractionListViewModel : TourMapFlyoutViewModel
  {
    public ObservableCollection<BaseMapItemViewModel> Items { get; set; }

    public TourMapFlyoutAttractionListViewModel(TourMapPartViewModel mapViewModel)
      : base(mapViewModel)
    {
      this.Items = new ObservableCollection<BaseMapItemViewModel>();
    }
  }
}
