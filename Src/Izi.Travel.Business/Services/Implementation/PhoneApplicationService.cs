using System;
using Windows.UI.Xaml;

namespace Izi.Travel.Business.Services.Implementation
{
    public class PhoneApplicationService
    {
        private static readonly Lazy<PhoneApplicationService> _instance = 
            new Lazy<PhoneApplicationService>(() => new PhoneApplicationService());

        public static PhoneApplicationService Current => _instance.Value;

        public event EventHandler<ClosingEventArgs> Closing;

        private PhoneApplicationService()
        {
            // Subscribe to UWP application lifecycle events
            Application.Current.Suspending += OnSuspending;
        }

        private void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            try
            {
                var args = new ClosingEventArgs();
                Closing?.Invoke(this, args);
                
                if (args.Cancel)
                {
                    // If cancellation is requested, we might want to handle it here
                    // In UWP, we can't cancel the suspension, but we can save state
                }
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}