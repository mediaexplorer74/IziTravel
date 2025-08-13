using Caliburn.Micro;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Services;
using Izi.Travel.Mtg.Helpers;
using System;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to handle the "Now Playing" functionality
    /// </summary>
    public class NowPlayingCommand : BaseCommand
    {
        private readonly IScreen _owner;

        /// <summary>
        /// Initializes a new instance of the NowPlayingCommand class
        /// </summary>
        /// <param name="owner">The owner screen</param>
        public NowPlayingCommand(IScreen owner) : base(null)
        {
            _owner = owner;
            if (owner == null)
                return;
                
            owner.Activated += OnOwnerActivated;
            owner.Deactivated += OnOwnerDeactivated;
        }

        private bool CanExecute(out AudioTrackInfo track)
        {
            track = null;
            var nowPlaying = ServiceFacade.AudioService.NowPlaying;
            if (nowPlaying == null)
                return false;
                
            track = nowPlaying;
            return true;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            AudioTrackInfo track;
            return CanExecute(out track);
        }

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            AudioTrackInfo track;
            if (!CanExecute(out track))
                return Task.CompletedTask;
                
            NavigationHelper.NavigateToAudio(track.MtgObjectType, track.MtgObjectUid, track.Language, track.MtgParentUid);
            return Task.CompletedTask;
        }

        private void OnOwnerActivated(object sender, ActivationEventArgs e)
        {
            RaiseCanExecuteChanged();
            ServiceFacade.AudioService.NowPlayingChanged += Instance_NowPlayingChanged;
        }

        private void OnOwnerDeactivated(object sender, DeactivationEventArgs e)
        {
            ServiceFacade.AudioService.NowPlayingChanged -= Instance_NowPlayingChanged;
        }

        private void Instance_NowPlayingChanged(object sender, AudioTrackInfo e)
        {
            RaiseCanExecuteChanged();
        }
  }
}
