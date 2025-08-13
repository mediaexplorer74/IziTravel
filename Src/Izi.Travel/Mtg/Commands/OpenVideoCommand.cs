using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Common.Controls;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Controls.Flyout;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Core.Services.Entities;
using Izi.Travel.Media.Provider;
using Izi.Travel.Media.ViewModels;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Windows.UI.Xaml;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to open a video player for the specified media
    /// </summary>
    public class OpenVideoCommand : BaseCommand
    {
        private readonly MtgObject _mtgObject;
        private readonly MtgObject _mtgObjectRoot;
        private readonly MediaInfo _videoMediaInfo;

        /// <summary>
        /// Gets a value indicating whether the video is available
        /// </summary>
        public bool HasVideo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the OpenVideoCommand class
        /// </summary>
        /// <param name="mtgObject">The media object containing the video</param>
        /// <param name="mtgObjectRoot">The root media object</param>
        public OpenVideoCommand(MtgObject mtgObject, MtgObject mtgObjectRoot) : base(null)
        {
            _mtgObject = mtgObject;
            _mtgObjectRoot = mtgObjectRoot;
            
            if (_mtgObject?.ContentProvider != null && 
                _mtgObject.MainContent?.Video != null && 
                _mtgObject.MainContent.Video.Length > 0)
            {
                var media = _mtgObject.MainContent.Video[0];
                _videoMediaInfo = new MediaInfo()
                {
                    MediaUid = media.Uid,
                    MediaFormat = media.Format,
                    Title = _mtgObject.MainContent.Title,
                    ContentProviderUid = _mtgObject.ContentProvider.Uid,
                    VideoUrl = media.Url
                };
            }
            
            HasVideo = _videoMediaInfo != null;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => HasVideo;

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            if (!HasVideo || (!_mtgObject.IsParentType() && !PurchaseFlyoutDialog.ConditionalShow(_mtgObjectRoot)))
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

