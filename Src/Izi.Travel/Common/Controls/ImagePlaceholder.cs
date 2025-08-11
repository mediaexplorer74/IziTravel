// ********************************************************************
// Type: Izi.Travel.Shell.Common.Controls.ImagePlaceholder
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace Izi.Travel.Shell.Common.Controls
{
    public class ImagePlaceholder : Control
    {
        // Remove the following static constructor and its contents, as they are not valid in UWP/WinUI and cause CS1061 and CS0246:
        // static ImagePlaceholder()
        // {
        //     DefaultStyleKeyProperty.OverrideMetadata(typeof(ImagePlaceholder), new FrameworkPropertyMetadata(typeof(ImagePlaceholder)));
        // }

        // Instead, set the DefaultStyleKey in the constructor:
        public ImagePlaceholder()
        {
            this.DefaultStyleKey = typeof(ImagePlaceholder);
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ImagePlaceholder), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register(nameof(ImageStretch), typeof(Stretch), typeof(ImagePlaceholder), new PropertyMetadata(Stretch.Uniform));

        public Stretch ImageStretch
        {
            get => (Stretch)GetValue(ImageStretchProperty);
            set => SetValue(ImageStretchProperty, value);
        }
    }
}


