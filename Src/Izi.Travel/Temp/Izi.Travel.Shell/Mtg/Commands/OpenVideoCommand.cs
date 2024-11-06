// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.OpenVideoCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Media.Provider;
using Izi.Travel.Shell.Media.ViewModels;
using System;
using System.Linq.Expressions;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class OpenVideoCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;
    private readonly MtgObject _mtgObjectRoot;
    private readonly MediaInfo _videoMediaInfo;

    public bool HasVideo { get; private set; }

    public OpenVideoCommand(MtgObject mtgObject, MtgObject mtgObjectRoot)
    {
      this._mtgObject = mtgObject;
      this._mtgObjectRoot = mtgObjectRoot;
      if (this._mtgObject != null && this._mtgObject.ContentProvider != null && this._mtgObject.MainContent != null && this._mtgObject.MainContent.Video != null && this._mtgObject.MainContent.Video.Length != 0)
      {
        Izi.Travel.Business.Entities.Data.Media media = this._mtgObject.MainContent.Video[0];
        this._videoMediaInfo = new MediaInfo()
        {
          MediaUid = media.Uid,
          MediaFormat = media.Format,
          Title = this._mtgObject.MainContent.Title,
          ContentProviderUid = this._mtgObject.ContentProvider.Uid,
          VideoUrl = media.Url
        };
      }
      this.HasVideo = this._videoMediaInfo != null;
    }

    public override bool CanExecute(object parameter) => this.HasVideo;

    public override void Execute(object parameter)
    {
      if (!this.HasVideo || !this._mtgObject.IsParentType() && !PurchaseFlyoutDialog.ConditionalShow(this._mtgObjectRoot))
        return;
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      if (appSettings.TourPauseOnVideoPromptEnabled && TourPlaybackManager.Instance.TourPlaybackState == TourPlaybackState.Started)
        ShellServiceFacade.DialogService.Show(ManifestResources.ApplicationTitle, AppResources.MessageMediaVideoPauseTour, MessageBoxButtonContent.YesNo, (Action<FlyoutDialog>) (x => x.IsDontShowVisible = true), (Action<FlyoutDialog, MessageBoxResult>) ((x, y) =>
        {
          if (y != MessageBoxResult.Yes)
            return;
          if (x.IsDontShowEnabled)
          {
            appSettings.TourPauseOnVideoPromptEnabled = false;
            ServiceFacade.SettingsService.SaveAppSettings(appSettings);
          }
          this.Open();
        }));
      else
        this.Open();
    }

    private void Open()
    {
      if (!this.HasVideo)
        return;
      TourPlaybackManager.Instance.Pause();
      ServiceFacade.AudioService.Stop();
      MediaPlayerDataProvider.Instance.MediaData = new MediaInfo[1]
      {
        this._videoMediaInfo
      };
      ShellServiceFacade.NavigationService.UriFor<MediaPlayerPartViewModel>().WithParam<MediaFormat>((Expression<Func<MediaPlayerPartViewModel, MediaFormat>>) (x => x.MediaFormat), MediaFormat.Video).Navigate();
    }
  }
}
