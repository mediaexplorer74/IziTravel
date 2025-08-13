// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.History.ProfileHistoryListGroupItemViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.ViewModels.Profile.History
{
  public class ProfileHistoryListGroupItemViewModel : List<ProfileHistoryListItemViewModel>
  {
    public string Title { get; private set; }

    public ProfileHistoryListGroupItemViewModel(string title) => this.Title = title;
  }
}
