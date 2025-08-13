// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.DetailSponsorListViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Resources;
using Izi.Travel.Mtg.ViewModels.Common.List;
using Izi.Travel.Mtg.Views.Common.Detail;
using System;
using Izi.Travel.Utility.Extensions;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail
{
  [View(typeof (DetailTabView))]
  public class DetailSponsorListViewModel : DetailTabViewModel
  {
    public override string DisplayName
    {
      get => AppResources.LabelSponsors.ToLower();
      set => throw new NotImplementedException();
    }

    protected override void OnInitialize()
    {
      this.ActiveItem = (IScreen) IoC.Get<SponsorListViewModel>();
      base.OnInitialize();
    }

    protected override string[] GetAppBarButtonKeys()
    {
      System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
      list.Add("NowPlaying");
      list.Add("QrCode", this.MtgObject.IsMuseumOrCollection());
      return list.ToArray();
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      return new string[2]{ "GetDirections", "Share" };
    }
  }
}
