// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Collection.Detail.CollectionDetailViewModel
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
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Collection.Detail
{
  [View(typeof (DetailView))]
  public class CollectionDetailViewModel : ParentObjectDetailViewModel
  {
    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.Add((IScreen) IoC.Get<CollectionDetailInfoViewModel>());
      this.Items.Add((IScreen) IoC.Get<DetailExhibitListViewModel>());
      this.Items.Add((IScreen) IoC.Get<DetailReviewListViewModel>());
    }

    protected override void ExecuteOpenMapCommand(object parameter)
    {
      if (this.MtgObject == null || this.MtgObject.ParentUid == null)
        return;
      ShellServiceFacade.NavigationService.UriFor<MuseumMapPartViewModel>().WithParam<string>((Expression<Func<MuseumMapPartViewModel, string>>) (x => x.TargetUid), this.MtgObject.ParentUid).Navigate();
    }
  }
}
