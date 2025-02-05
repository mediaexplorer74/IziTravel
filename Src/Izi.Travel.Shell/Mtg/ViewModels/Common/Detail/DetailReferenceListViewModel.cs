// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.DetailReferenceListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using Izi.Travel.Shell.Mtg.Views.Common.Detail;
using System;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  [View(typeof (DetailTabView))]
  public class DetailReferenceListViewModel : DetailTabViewModel
  {
    public override string DisplayName
    {
      get => AppResources.LabelRelated.ToLower();
      set => throw new NotImplementedException();
    }

    protected override void OnInitialize()
    {
      this.ActiveItem = (IScreen) IoC.Get<ReferenceListViewModel>();
      base.OnInitialize();
    }

    protected override string[] GetAppBarButtonKeys()
    {
      System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
      list.Add("NowPlaying");
      list.Add<string>("QrCode", this.MtgObject.IsMuseumOrCollection());
      return list.ToArray();
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      return new string[2]{ "GetDirections", "Share" };
    }
  }
}
