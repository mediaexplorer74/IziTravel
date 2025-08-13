using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Services;
using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Common;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to handle rating of MTG objects
    /// </summary>
    public class RateCommand : BaseCommand
    {
        private readonly MtgObject _mtgObject;

        /// <summary>
        /// Initializes a new instance of the RateCommand class
        /// </summary>
        /// <param name="mtgObject">The MTG object to rate</param>
        public RateCommand(MtgObject mtgObject) : base(null)
        {
            _mtgObject = mtgObject;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return _mtgObject != null && RateHelper.CanRate(_mtgObject.Uid, _mtgObject.Hash);
        }

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            if (_mtgObject?.MainContent == null)
                return Task.CompletedTask;
                
            ShellServiceFacade.NavigationService
                .UriFor<RatePartViewModel>()
                .WithParam(x => x.Uid, _mtgObject.Uid)
                .WithParam(x => x.Type, _mtgObject.Type)
                .WithParam(x => x.Hash, _mtgObject.Hash)
                .WithParam(x => x.Language, _mtgObject.MainContent.Language)
                .WithParam(x => x.Title, _mtgObject.MainContent.Title)
                .Navigate();
                
            return Task.CompletedTask;
        }
    }
}
