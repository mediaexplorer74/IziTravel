using Izi.Travel.Core.Command;
using System;
using System.Threading.Tasks;
using Windows.System;

namespace Izi.Travel.Common.Commands
{
    /// <summary>
    /// Command to handle launching a URI
    /// </summary>
    public class LaunchUriCommand : BaseCommand
    {
        private readonly Uri _uri;

        /// <summary>
        /// Initializes a new instance of the LaunchUriCommand class
        /// </summary>
        /// <param name="uri">The URI to launch</param>
        public LaunchUriCommand(Uri uri) : base(null)
        {
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            await Launcher.LaunchUriAsync(_uri);
        }
    }
}
