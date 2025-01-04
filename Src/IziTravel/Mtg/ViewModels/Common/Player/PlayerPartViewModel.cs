// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.PlayerPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using System.Collections.Generic;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player
{
  public class PlayerPartViewModel : Conductor<IScreen>, IPlayerParentViewModel
  {
    private readonly PlayerViewModel _playerViewModel;
    private IEnumerable<ButtonInfo> _appBarButtons;
    private IEnumerable<MenuItemInfo> _appBarMenuItems;

    public string ParentUid { get; set; }

    public string Uid { get; set; }

    public string Language { get; set; }

    public bool AutoPlay { get; set; }

    public IEnumerable<ButtonInfo> AppBarButtons
    {
      get => this._appBarButtons;
      set
      {
        this.SetProperty<IEnumerable<ButtonInfo>>(ref this._appBarButtons, value, propertyName: nameof (AppBarButtons));
      }
    }

    public IEnumerable<MenuItemInfo> AppBarMenuItems
    {
      get => this._appBarMenuItems;
      set
      {
        this.SetProperty<IEnumerable<MenuItemInfo>>(ref this._appBarMenuItems, value, propertyName: nameof (AppBarMenuItems));
      }
    }

    public PlayerPartViewModel(PlayerViewModel playerViewModel)
    {
      this._playerViewModel = playerViewModel;
    }

    protected override void OnInitialize()
    {
      this.AppBarButtons = (IEnumerable<ButtonInfo>) new ButtonInfo[1]
      {
        new ButtonInfo()
        {
          Key = "Info",
          Text = AppResources.LabelInfo,
          ImageUrl = "/Assets/Icons/appbar.info.png",
          Command = (ICommand) this._playerViewModel.NavigateCommand
        }
      };
      this.ActiveItem = (IScreen) this._playerViewModel;
      base.OnInitialize();
    }
  }
}
