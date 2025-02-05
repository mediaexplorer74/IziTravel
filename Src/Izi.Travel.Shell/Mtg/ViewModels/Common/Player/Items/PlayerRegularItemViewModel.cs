// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items.PlayerRegularItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Mtg.Commands;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items
{
  public class PlayerRegularItemViewModel : PlayerItemViewModel
  {
    private OpenImageCommand _openImageCommand;

    public bool IsNavigationStory
    {
      get => this.MtgObject != null && this.MtgObject.Type == MtgObjectType.StoryNavigation;
    }

    public OpenImageCommand OpenImageCommand
    {
      get
      {
        return this._openImageCommand ?? (this._openImageCommand = new OpenImageCommand(this.MtgObject));
      }
    }

    public PlayerRegularItemViewModel(
      PlayerViewModel playerViewModel,
      int index,
      MtgObject mtgObjectRoot,
      MtgObject mtgObjectParent,
      MtgObject mtgObject)
      : base(playerViewModel, index, mtgObjectRoot, mtgObjectParent, mtgObject)
    {
    }

    protected override void OnDataRefresh()
    {
      base.OnDataRefresh();
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsNavigationStory));
    }
  }
}
