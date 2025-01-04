// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Bookmark.ProfileBookmarkTabViewModel
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
namespace Izi.Travel.Shell.ViewModels.Profile.Bookmark
{
  [View(typeof (ProfileTabView))]
  public class ProfileBookmarkTabViewModel : ProfileTabViewModel<ProfileBookmarkListViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelBookmarks;
      set => throw new NotImplementedException();
    }

    public override ProfileType Type => ProfileType.Bookmark;

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
