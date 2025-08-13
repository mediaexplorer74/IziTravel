using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Services;
using Izi.Travel.Media.Provider;
using Izi.Travel.Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to handle opening an image in the media player
    /// </summary>
    public class OpenImageCommand : BaseCommand
    {
        private readonly MtgObject _mtgObject;

        /// <summary>
        /// Initializes a new instance of the OpenImageCommand class
        /// </summary>
        /// <param name="mtgObject">The MTG object containing the image to open</param>
        public OpenImageCommand(MtgObject mtgObject) : base(null)
        {
            _mtgObject = mtgObject ?? throw new ArgumentNullException(nameof(mtgObject));
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            if (_mtgObject?.ContentProvider == null || _mtgObject.MainContent?.Images == null)
                return Task.CompletedTask;

            MediaPlayerDataProvider.Instance.MediaData = _mtgObject.MainContent.Images
                .Where(x => x.Type == MediaType.Story)
                .Select(x => new MediaInfo()
                {
                    MediaFormat = MediaFormat.Image,
                    MediaUid = x.Uid,
                    Title = x.Title,
                    ContentProviderUid = _mtgObject.ContentProvider.Uid,
                    PreviewUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, _mtgObject.ContentProvider.Uid, ImageFormat.Low480X360),
                    ImageUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, _mtgObject.ContentProvider.Uid, ImageFormat.High800X600)
                })
                .ToArray();

            ShellServiceFacade.NavigationService
                .UriFor<MediaPlayerPartViewModel>()
                .WithParam(x => x.MediaFormat, MediaFormat.Image)
                .Navigate();

            return Task.CompletedTask;
        }
    }
}
