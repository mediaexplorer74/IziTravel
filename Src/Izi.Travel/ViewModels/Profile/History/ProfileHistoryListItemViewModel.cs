// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.History.ProfileHistoryListItemViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Common.ViewModels.List;

#nullable disable
namespace Izi.Travel.ViewModels.Profile.History
{
  public class ProfileHistoryListItemViewModel : ProfileListItemViewModel
  {
    public string DateTime { get; set; }

    public ProfileHistoryListItemViewModel(IListViewModel listViewModel, MtgObject entity)
      : base(listViewModel, entity, true)
    {
      this.Initialize();
    }

    private void Initialize()
    {
      if (this.MtgObject == null)
        return;
      this.DateTime = this.MtgObject.DateTime.ToString("HH:mm:ss");
    }
  }
}
