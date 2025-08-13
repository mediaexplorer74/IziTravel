using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Helper;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Helpers;
using Windows.System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to share content from the application
    /// </summary>
    public class ShareCommand : BaseCommand
    {
        private readonly MtgObject _mtgObject;
        private readonly MtgObject _mtgObjectRoot;

        /// <summary>
        /// Initializes a new instance of the ShareCommand class
        /// </summary>
        /// <param name="mtgObject">The MTG object to share</param>
        /// <param name="mtgObjectRoot">The root MTG object if applicable</param>
        public ShareCommand(MtgObject mtgObject, MtgObject mtgObjectRoot) : base(null)
        {
            _mtgObject = mtgObject;
            _mtgObjectRoot = mtgObjectRoot;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return _mtgObject != null && 
                   _mtgObject.MainContent != null && 
                   _mtgObject.Type != MtgObjectType.StoryNavigation && 
                   _mtgObject.Type != 0;
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            if (_mtgObject?.MainContent == null)
                return;
                
            var uri = MtgLinkHelper.CreateUri(new MtgLinkInfo()
            {
                Uid = _mtgObject.Uid,
                Language = _mtgObject.MainContent.Language
            });
            
            if (uri == null)
                return;
                
            AnalyticsHelper.SendShare(_mtgObject);
            
            var values = new List<string>();
            if (_mtgObjectRoot != null)
                values.Add(_mtgObjectRoot.Title);
                
            values.Add(_mtgObject.Title);
            
            // For UWP, use DataTransferManager to show the share UI
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += (sender, args) =>
            {
                var request = args.Request;
                request.Data.Properties.Title = string.Join(" - ", values);
                request.Data.SetWebLink(uri);
                
                if (!string.IsNullOrEmpty(_mtgObject.MainContent.Description))
                {
                    request.Data.Properties.Description = _mtgObject.MainContent.Description;
                }
            };
            
            DataTransferManager.ShowShareUI();
            
            // Return completed task since we're using the event-based UWP sharing API
            return;
        }
    }
}
