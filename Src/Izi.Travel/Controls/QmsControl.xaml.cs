using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Shell.Controls
{
    public sealed partial class QmsControl : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register(
                nameof(IconSource),
                typeof(ImageSource),
                typeof(QmsControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(QmsControl),
                new PropertyMetadata(48.0));

        #endregion

        #region Public Properties

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        #endregion

        public QmsControl()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.Loaded += (s, e) =>
            {
                // Ensure the fallback image is hidden by default
                if (FallbackImage != null)
                    FallbackImage.Visibility = Visibility.Collapsed;
            };
        }

        private void IconImageBrush_ImageFailed(object sender, Windows.UI.Xaml.ExceptionRoutedEventArgs e)
        {
            // Show the fallback image when the main image fails to load
            if (FallbackImage != null)
                FallbackImage.Visibility = Visibility.Visible;
        }
    }
}
