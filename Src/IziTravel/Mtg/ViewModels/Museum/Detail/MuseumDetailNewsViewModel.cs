// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Museum.Detail.MuseumDetailNewsViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Museum.Detail
{
  public class MuseumDetailNewsViewModel : DetailTabViewModel
  {
    public override string DisplayName
    {
      get => AppResources.LabelNews.ToLower();
      set => throw new NotImplementedException();
    }

    public string News
    {
      get => this.MtgObjectContent == null ? (string) null : this.MtgObjectContent.News;
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.News));
    }

    protected override string[] GetAppBarButtonKeys()
    {
      return new string[2]{ "NowPlaying", "QrCode" };
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      return new string[2]{ "GetDirections", "Share" };
    }
  }
}
