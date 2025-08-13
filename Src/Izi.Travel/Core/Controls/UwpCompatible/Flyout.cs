using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Izi.Travel.Core.Controls.Flyout
{
    public class Flyout : FlyoutBase
    {
        private Windows.UI.Xaml.Controls.Flyout _hostFlyout;
        private FrameworkElement _placementTarget;

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), 
                typeof(object), 
                typeof(Flyout), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register(nameof(Placement), 
                typeof(FlyoutPlacementMode), 
                typeof(Flyout), 
                new PropertyMetadata(FlyoutPlacementMode.Bottom));

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public FlyoutPlacementMode Placement
        {
            get => (FlyoutPlacementMode)GetValue(PlacementProperty);
            set => SetValue(PlacementProperty, value);
        }

        protected override void ShowImpl()
        {
            if (Owner == null)
                return;

            _hostFlyout = new Windows.UI.Xaml.Controls.Flyout
            {
                Content = Content,
                Placement = Placement
            };

            _hostFlyout.Opened += OnHostFlyoutOpened;
            _hostFlyout.Closed += OnHostFlyoutClosed;

            _placementTarget = Owner;
            _hostFlyout.ShowAt(_placementTarget);

            OnOpening();
        }

        protected override void HideImpl()
        {
            if (_hostFlyout != null)
            {
                OnClosing();
                _hostFlyout.Hide();
            }
        }

        private void OnHostFlyoutOpened(object sender, object e)
        {
            OnOpened();
        }

        private void OnHostFlyoutClosed(object sender, object e)
        {
            if (_hostFlyout != null)
            {
                _hostFlyout.Opened -= OnHostFlyoutOpened;
                _hostFlyout.Closed -= OnHostFlyoutClosed;
                _hostFlyout = null;
            }

            OnClosed();
        }
    }
}
