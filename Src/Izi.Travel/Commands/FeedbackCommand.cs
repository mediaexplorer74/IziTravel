using Caliburn.Micro;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Services;
using Izi.Travel.Settings.ViewModels.Application;
using System;
using System.Threading.Tasks;

namespace Izi.Travel.Commands
{
    /// <summary>
    /// Command to navigate to the feedback page
    /// </summary>
    public class FeedbackCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the FeedbackCommand class
        /// </summary>
        public FeedbackCommand() : base(null)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            ShellServiceFacade.NavigationService.UriFor<SettingsAppFeedbackMessageViewModel>().Navigate();
            return Task.CompletedTask;
        }
    }
}
