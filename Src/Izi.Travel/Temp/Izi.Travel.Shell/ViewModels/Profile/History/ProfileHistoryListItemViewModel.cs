// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.History.ProfileHistoryListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.History
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
