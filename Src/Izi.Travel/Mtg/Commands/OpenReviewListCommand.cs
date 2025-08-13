using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Core.Command;
using Izi.Travel.Mtg.ViewModels.Common;
using System.Threading.Tasks;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to open the review list for an MTG object
    /// </summary>
    public class OpenReviewListCommand : BaseCommand
    {
        private readonly MtgObject _mtgObject;

        /// <summary>
        /// Initializes a new instance of the OpenReviewListCommand class
        /// </summary>
        /// <param name="mtgObject">The MTG object to show reviews for</param>
        public OpenReviewListCommand(MtgObject mtgObject) : base(null)
        {
            _mtgObject = mtgObject;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            ReviewListPartViewModel.Navigate(_mtgObject);
            return Task.CompletedTask;
        }
    }
}
