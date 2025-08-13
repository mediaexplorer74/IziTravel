using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Izi.Travel.Common.Controls
{
    public sealed partial class ImagePlaceholder : UserControl
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImagePlaceholder), new PropertyMetadata(null, OnImageSourceChanged));

        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImagePlaceholder), new PropertyMetadata(Stretch.Uniform));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public Stretch ImageStretch
        {
            get => (Stretch)GetValue(ImageStretchProperty);
            set => SetValue(ImageStretchProperty, value);
        }

        public bool ShowPlaceholder => ImageSource == null;
        public bool ShowImage => ImageSource != null;

        public ImagePlaceholder()
        {
            this.InitializeComponent();
        }

        private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImagePlaceholder control)
            {
                control.UpdateImageSource();
            }
        }

        private void UpdateImageSource()
        {
            Bindings.Update();
        }
    }
}
