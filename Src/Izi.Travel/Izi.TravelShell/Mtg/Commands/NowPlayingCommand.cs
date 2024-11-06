// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.NowPlayingCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class NowPlayingCommand : BaseCommand
  {
    private readonly IScreen _owner;

    public NowPlayingCommand(IScreen owner)
    {
      this._owner = owner;
      if (owner == null)
        return;
      owner.Activated += new EventHandler<ActivationEventArgs>(this.OnOwnerActivated);
      owner.Deactivated += new EventHandler<DeactivationEventArgs>(this.OnOwnerDeactivated);
    }

    private bool CanExecute(out Uri uri)
    {
      uri = (Uri) null;
      AudioTrackInfo nowPlaying = ServiceFacade.AudioService.NowPlaying;
      if (nowPlaying == null)
        return false;
      uri = NavigationHelper.UriToAudio(nowPlaying.MtgObjectType, nowPlaying.MtgObjectUid, nowPlaying.Language, nowPlaying.MtgParentUid);
      return !UriHelper.EqualsByCommonParameters(uri, ShellServiceFacade.NavigationService.CurrentSource);
    }

    public override bool CanExecute(object parameter)
    {
      Uri uri = (Uri) null;
      return this.CanExecute(out uri);
    }

    public override void Execute(object parameter)
    {
      Uri uri = (Uri) null;
      if (!this.CanExecute(out uri))
        return;
      ShellServiceFacade.NavigationService.Navigate(uri);
    }

    private void OnOwnerActivated(object sender, ActivationEventArgs activationEventArgs)
    {
      this.RaiseCanExecuteChanged();
      // ISSUE: method pointer
      ServiceFacade.AudioService.NowPlayingChanged += new TypedEventHandler<IAudioService, AudioTrackInfo>((object) this, __methodptr(Instance_NowPlayingChanged));
    }

    private void OnOwnerDeactivated(object sender, DeactivationEventArgs deactivationEventArgs)
    {
      // ISSUE: method pointer
      ServiceFacade.AudioService.NowPlayingChanged -= new TypedEventHandler<IAudioService, AudioTrackInfo>((object) this, __methodptr(Instance_NowPlayingChanged));
    }

    private void Instance_NowPlayingChanged(object sender, AudioTrackInfo e)
    {
      this.RaiseCanExecuteChanged();
    }
  }
}
