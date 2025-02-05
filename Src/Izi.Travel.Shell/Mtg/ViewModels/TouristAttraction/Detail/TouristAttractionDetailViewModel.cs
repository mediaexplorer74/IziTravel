// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.Detail.TouristAttractionDetailViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using Izi.Travel.Shell.Mtg.Views.Common.Detail;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.Detail
{
  [View(typeof (DetailView))]
  public class TouristAttractionDetailViewModel : DetailViewModel
  {
    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.Add((IScreen) IoC.Get<TouristAttractionDetailInfoViewModel>());
      this.Items.Add((IScreen) IoC.Get<DetailReferenceListViewModel>());
    }

    protected override void ExecuteOpenMapCommand(object parameter)
    {
      if (this.DetailPartViewModel == null || this.MtgObject == null || this.MtgObject.Uid == null || this.MtgObject.MainContent == null || this.MtgObject.MainContent.Language == null)
        return;
      string tourUid = this.DetailPartViewModel.ParentUid ?? this.MtgObject.ParentUid;
      if (tourUid == null)
        return;
      TourMapPartViewModel.Navigate(tourUid, this.MtgObject.MainContent.Language, this.MtgObject.Uid);
    }
  }
}
