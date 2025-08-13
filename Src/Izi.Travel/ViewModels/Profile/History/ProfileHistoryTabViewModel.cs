// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.History.ProfileHistoryTabViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Common.Model;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Resources;
using Izi.Travel.Model.Profile;
using Izi.Travel.Views.Profile;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Input;
#nullable disable
namespace Izi.Travel.ViewModels.Profile.History
{
  [View(typeof (ProfileTabView))]
  public class ProfileHistoryTabViewModel : ProfileTabViewModel<ProfileHistoryListViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelHistory;
      set => throw new NotImplementedException();
    }

    public override ProfileType Type => ProfileType.History;

    protected override IEnumerable<ButtonInfo> GetAppBarButtonList()
    {
      return (IEnumerable<ButtonInfo>) new ButtonInfo[1]
      {
        new ButtonInfo()
        {
          Order = 5,
          Key = "ClearAll",
          Text = AppResources.CommandClear,
          ImageUrl = "/Assets/Icons/appbar.delete.png",
          Command = (ICommand) this.ItemViewModel.ClearCommand
        }
      };
    }
  }
}

