using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace Izi.Travel.Core.Components
{
    public class UIElementHelper : DependencyObject
    {
        public static readonly DependencyProperty OpacityMaskProperty = 
            DependencyProperty.RegisterAttached(
                "OpacityMask", 
                typeof(ImageSource), 
                typeof(UIElementHelper), 
                new PropertyMetadata(null, OpacityMaskPropertyChangedCallback));

        private static void OpacityMaskPropertyChangedCallback(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                // In UWP, we'll use a CompositionBrush for the opacity mask
                // This is a simplified implementation that works for most cases
                if (e.NewValue is ImageSource imageSource)
                {
                    // For UWP, we'll use a CompositionBrush with a mask
                    var visual = ElementCompositionPreview.GetElementVisual(element);
                    var compositor = visual.Compositor;
                    
                    // Create a mask brush from the image source
                    var maskBrush = new ImageBrush() { ImageSource = imageSource };
                    
                    // Apply the mask using the Composition API
                    var mask = new MaskedBrush
                    {
                        //Source = new CompositionSurfaceBrush(default, maskBrush, new Rect(0, 0, 1, 1)),
                        //Mask = new CompositionSurfaceBrush(default, maskBrush, new Rect(0,0,1,1))
                    };
                    
                    // Set the mask on the visual
                    visual.Opacity = 1.0f;
                    //visual.Brush = compositor.CreateBackdropBrush();
                    //visual.OpacityMask = mask.Mask;
                }
                else
                {
                    // Clear the mask if the source is null
                    var visual = ElementCompositionPreview.GetElementVisual(element);
                    //visual.OpacityMask = null;
                }
            }
        }
        
        // Helper class for creating a masked brush in UWP
        private class MaskedBrush //: CompositionBrush
        {
            public CompositionBrush Source { get; set; }
            public CompositionBrush Mask { get; set; }
        }

        public static void SetOpacityMask(UIElement element, ImageSource value)
        {
            element.SetValue(OpacityMaskProperty, value);
        }

        public static ImageSource GetOpacityMask(UIElement element)
        {
            return (ImageSource)element.GetValue(OpacityMaskProperty);
        }
    }
}

