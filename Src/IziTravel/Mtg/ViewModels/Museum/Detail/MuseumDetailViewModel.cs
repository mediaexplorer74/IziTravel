// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Museum.Detail.MuseumDetailViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Museum.Map;
using Izi.Travel.Shell.Mtg.Views.Common.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Museum.Detail
{
  [View(typeof (DetailView))]
  public class MuseumDetailViewModel : ParentObjectDetailViewModel
  {
    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.AddRange((IEnumerable<IScreen>) new IScreen[7]
      {
        (IScreen) IoC.Get<MuseumDetailInfoViewModel>(),
        (IScreen) IoC.Get<MuseumDetailNewsViewModel>(),
        (IScreen) IoC.Get<MuseumDetailCollectionListViewModel>(),
        (IScreen) IoC.Get<DetailExhibitListViewModel>(),
        (IScreen) IoC.Get<DetailReviewListViewModel>(),
        (IScreen) IoC.Get<DetailSponsorListViewModel>(),
        (IScreen) IoC.Get<DetailReferenceListViewModel>()
      });
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      if (this.MtgObjectContent == null || !string.IsNullOrWhiteSpace(this.MtgObjectContent.News))
        return;
      IScreen screen = this.Items.FirstOrDefault<IScreen>((Func<IScreen, bool>) (x => x.GetType() == typeof (MuseumDetailNewsViewModel)));
      if (screen == null)
        return;
      this.Items.Remove(screen);
    }

    protected override void ExecuteOpenMapCommand(object parameter)
    {
      if (this.MtgObject == null || this.MtgObject.Uid == null)
        return;
      ShellServiceFacade.NavigationService.UriFor<MuseumMapPartViewModel>().WithParam<string>((Expression<Func<MuseumMapPartViewModel, string>>) (x => x.TargetUid), this.MtgObject.Uid).Navigate();
    }
  }
}
