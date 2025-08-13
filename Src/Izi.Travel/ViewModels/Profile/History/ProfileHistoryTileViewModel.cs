// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.History.ProfileHistoryTileViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Resources;
using Izi.Travel.Model.Profile;

#nullable disable
namespace Izi.Travel.ViewModels.Profile.History
{
  public class ProfileHistoryTileViewModel : ProfileTileViewModel
  {
    public override string Title => AppResources.LabelHistory;

    public override ProfileType Type => ProfileType.History;
  }
}
