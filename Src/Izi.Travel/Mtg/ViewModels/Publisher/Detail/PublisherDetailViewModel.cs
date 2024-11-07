// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail.PublisherDetailViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.Messages;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using System.Collections.Generic;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail
{
  public class PublisherDetailViewModel : 
    Conductor<IScreen>.Collection.OneActive,
    IMtgObjectViewModel,
    IMtgObjectProvider,
    IHandle<DataLoadingMessage>,
    IHandle,
    IHandle<DataLoadedMessage>
  {
    private readonly ILog _logger;
    private static readonly IEventAggregator EventAggregator = IoC.Get<IEventAggregator>();
    private IEnumerable<ButtonInfo> _availableAppBarButtons;
    private IEnumerable<MenuItemInfo> _availableAppBarMenuItems;
    private BaseCommand _nowPlayingCommand;

    protected ILog Logger => this._logger;

    public MtgObject MtgObject { get; private set; }

    public MtgObject MtgObjectParent => (MtgObject) null;

    public MtgObject MtgObjectRoot => (MtgObject) null;

    public IEnumerable<ButtonInfo> AvailableAppBarButtons => this._availableAppBarButtons;

    public IEnumerable<MenuItemInfo> AvailableAppBarMenuItems => this._availableAppBarMenuItems;

    public PublisherDetailViewModel() => this._logger = LogManager.GetLog(this.GetType());

    public BaseCommand NowPlayingCommand
    {
      get
      {
        return this._nowPlayingCommand ?? (this._nowPlayingCommand = (BaseCommand) new Izi.Travel.Shell.Mtg.Commands.NowPlayingCommand((IScreen) this));
      }
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      PublisherDetailViewModel.EventAggregator.Subscribe((object) this);
      this.Items.AddRange((IEnumerable<IScreen>) new IScreen[2]
      {
        (IScreen) IoC.Get<PublisherDetailContentViewModel>(),
        (IScreen) IoC.Get<PublisherDetailInfoViewModel>()
      });
    }

    protected override void OnActivate()
    {
      if (!(this.Parent is IMtgObjectPartViewModel parent))
        return;
      this.MtgObject = parent.MtgObject;
      if (this.MtgObject == null)
        return;
      if (this._availableAppBarButtons == null)
        this._availableAppBarButtons = this.GetAvailableAppBarButtons();
      if (this._availableAppBarMenuItems == null)
        this._availableAppBarMenuItems = this.GetAvailableAppBarMenuItems();
      this.DisplayName = AppResources.LabelAuthor.ToUpper();
      base.OnActivate();
    }

    protected virtual IEnumerable<ButtonInfo> GetAvailableAppBarButtons()
    {
      return (IEnumerable<ButtonInfo>) new ButtonInfo[1]
      {
        new ButtonInfo()
        {
          Order = 5,
          Key = "NowPlaying",
          Text = AppResources.CommandNavigateNowPlaying,
          ImageUrl = "/Assets/Icons/appbar.nowplaying.png",
          Command = (ICommand) this.NowPlayingCommand
        }
      };
    }

    protected virtual IEnumerable<MenuItemInfo> GetAvailableAppBarMenuItems()
    {
      return (IEnumerable<MenuItemInfo>) null;
    }

    public void Handle(DataLoadingMessage message)
    {
    }

    public void Handle(DataLoadedMessage message)
    {
    }
  }
}
