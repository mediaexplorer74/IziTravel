// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Tour.Detail.TourDetailViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using Izi.Travel.Shell.Mtg.Views.Common.Detail;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Tour.Detail
{
  [View(typeof (DetailView))]
  public class TourDetailViewModel : ParentObjectDetailViewModel
  {
    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.AddRange((IEnumerable<IScreen>) new IScreen[5]
      {
        (IScreen) IoC.Get<TourDetailInfoViewModel>(),
        (IScreen) IoC.Get<TourDetailRouteViewModel>(),
        (IScreen) IoC.Get<DetailReviewListViewModel>(),
        (IScreen) IoC.Get<DetailSponsorListViewModel>(),
        (IScreen) IoC.Get<DetailReferenceListViewModel>()
      });
    }

    protected override void ExecuteOpenMapCommand(object parameter)
    {
      if (this.MtgObject == null || this.MtgObject.Uid == null || this.MtgObject.MainContent == null || this.MtgObject.MainContent.Language == null)
        return;
      TourMapPartViewModel.Navigate(this.MtgObject.Uid, this.MtgObject.MainContent.Language);
    }
  }
}
