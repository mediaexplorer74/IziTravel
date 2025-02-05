// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Download.ProfileDownloadTabViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;
using Izi.Travel.Shell.Views.Profile;
using System;
using System.Collections.Generic;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Download
{
  [View(typeof (ProfileTabView))]
  public class ProfileDownloadTabViewModel : ProfileTabViewModel<ProfileDownloadListViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelDownloads;
      set => throw new NotImplementedException();
    }

    public override ProfileType Type => ProfileType.Download;

    protected override IEnumerable<ButtonInfo> GetAppBarButtonList()
    {
      return (IEnumerable<ButtonInfo>) new ButtonInfo[2]
      {
        new ButtonInfo()
        {
          Key = "UpdateAll",
          Text = AppResources.CommandUpdateAll,
          ImageUrl = "/Assets/Icons/appbar.update.png",
          Command = (ICommand) this.ItemViewModel.UpdateCommand
        },
        new ButtonInfo()
        {
          Key = "ClearAll",
          Text = AppResources.CommandClear,
          ImageUrl = "/Assets/Icons/appbar.delete.png",
          Command = (ICommand) this.ItemViewModel.ClearCommand
        }
      };
    }
  }
}
