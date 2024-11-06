// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.Detail.TouristAttractionDetailInfoViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.Views.Common.Detail;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.Detail
{
  [View(typeof (DetailInfoView))]
  public class TouristAttractionDetailInfoViewModel : ChildObjectDetailInfoViewModel
  {
    protected override string[] GetAppBarButtonKeys()
    {
      return new string[4]
      {
        "NowPlaying",
        "Bookmark",
        "Language",
        "GetDirections"
      };
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      return new string[1]{ "Share" };
    }
  }
}
