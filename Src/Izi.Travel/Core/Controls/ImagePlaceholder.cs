using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Izi.Travel.Core.Controls
{
    public class ImagePlaceholder : Control
    {
        public static readonly DependencyProperty SourceProperty = 
            DependencyProperty.Register(nameof(Source), typeof(object), typeof(ImagePlaceholder), new PropertyMetadata(null));

        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(nameof(Stretch), typeof(Windows.UI.Xaml.Media.Stretch), 
                typeof(ImagePlaceholder), new PropertyMetadata(Windows.UI.Xaml.Media.Stretch.Uniform));

        public Windows.UI.Xaml.Media.Stretch Stretch
        {
            get => (Windows.UI.Xaml.Media.Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        public ImagePlaceholder()
        {
            this.DefaultStyleKey = typeof(ImagePlaceholder);
        }
    }
}
