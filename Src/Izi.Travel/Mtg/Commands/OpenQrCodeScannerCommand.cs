using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Core.Command;
using Izi.Travel.Mtg.Components.Enums;
using Izi.Travel.Mtg.Components.Tasks;
using System;
using System.Threading.Tasks;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to handle opening the QR code scanner
    /// </summary>
    public class OpenQrCodeScannerCommand : BaseCommand
    {
        private readonly IScreen _owner;
        private readonly string _parentUid;
        private readonly string _parentLanguage;
        private readonly MtgObjectType _parentType;

        /// <summary>
        /// Initializes a new instance of the OpenQrCodeScannerCommand class
        /// </summary>
        /// <param name="owner">The owner screen</param>
        /// <param name="parentUid">The parent UID</param>
        /// <param name="parentLanguage">The parent language</param>
        /// <param name="parentType">The parent type</param>
        public OpenQrCodeScannerCommand(
            IScreen owner,
            string parentUid,
            string parentLanguage,
            MtgObjectType parentType) : base(null)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _parentUid = parentUid ?? throw new ArgumentNullException(nameof(parentUid));
            _parentLanguage = parentLanguage ?? throw new ArgumentNullException(nameof(parentLanguage));
            _parentType = parentType;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter) => true;

        /// <inheritdoc/>
        protected override Task OnExecuteAsync(object parameter)
        {
            var barcodeScannerTask = new BarcodeScannerTask
            {
                ParentUid = _parentUid,
                ParentLanguage = _parentLanguage,
                ParentType = _parentType,
                ParentScreen = _owner,
                ActivationMode = FlyoutSearchActivationMode.None,
                NavigationMode = FlyoutSearchNavigationMode.Player,
                CloseMode = FlyoutSearchCloseMode.Silent
            };

            barcodeScannerTask.Show();
            return Task.CompletedTask;
        }
    }
}
