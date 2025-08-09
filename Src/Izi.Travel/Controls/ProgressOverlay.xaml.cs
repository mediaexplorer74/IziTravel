using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Controls
{
    public sealed partial class ProgressOverlay : ContentControl
    {
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(
                nameof(IsBusy),
                typeof(bool),
                typeof(ProgressOverlay),
                new PropertyMetadata(false, OnIsBusyChanged));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(ProgressOverlay),
                new PropertyMetadata("Loading..."));

        public static readonly DependencyProperty ProgressBrushProperty =
            DependencyProperty.Register(
                nameof(ProgressBrush),
                typeof(Brush),
                typeof(ProgressOverlay),
                new PropertyMetadata(null));

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Brush ProgressBrush
        {
            get => (Brush)GetValue(ProgressBrushProperty);
            set => SetValue(ProgressBrushProperty, value);
        }

        public ProgressOverlay()
        {
            this.DefaultStyleKey = typeof(ProgressOverlay);
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressOverlay control)
            {
                string state = control.IsBusy ? "Busy" : "Normal";
                VisualStateManager.GoToState(control, state, true);
            }
        }
    }
}
