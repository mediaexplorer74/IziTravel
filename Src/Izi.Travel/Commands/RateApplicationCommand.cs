using Izi.Travel.Core.Command;
using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.ApplicationModel;

namespace Izi.Travel.Commands
{
    /// <summary>
    /// Command to open the app's store page for rating/review
    /// </summary>
    public class RateApplicationCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the RateApplicationCommand class
        /// </summary>
        public RateApplicationCommand() : base(null)
        {
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            // UWP: open Microsoft Store review page for this app
            string pfn = Package.Current.Id.FamilyName;
            var uri = new Uri($"ms-windows-store://review/?PFN={pfn}");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
