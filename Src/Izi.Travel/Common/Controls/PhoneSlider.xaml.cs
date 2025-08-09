using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Shell.Common.Controls
{
    public class PhoneSlider : Slider
    {
        public PhoneSlider()
        {
            this.DefaultStyleKey = typeof(PhoneSlider);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Update the slider's appearance when the template is applied
            UpdateSliderAppearance();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            UpdateSliderAppearance();
        }

        private void UpdateSliderAppearance()
        {
            var horizontalTrackRect = GetTemplateChild("HorizontalTrackRect") as Grid;
            var horizontalDecreaseRect = GetTemplateChild("HorizontalDecreaseRect") as Rectangle;
            var horizontalThumb = GetTemplateChild("HorizontalThumb") as Thumb;

            if (horizontalTrackRect != null && horizontalDecreaseRect != null && horizontalThumb != null)
            {
                // Calculate the percentage of the slider that should be filled
                double percentage = (Value - Minimum) / (Maximum - Minimum);
                
                // Update the width of the filled portion of the track
                if (horizontalDecreaseRect != null)
                {
                    horizontalDecreaseRect.Width = percentage * horizontalTrackRect.ActualWidth;
                }

                // Update the position of the thumb
                if (horizontalThumb != null)
                {
                    double thumbOffset = (horizontalThumb.ActualWidth / 2);
                    double trackWidth = horizontalTrackRect.ActualWidth - (thumbOffset * 2);
                    double newPosition = percentage * trackWidth;
                    
                    var transform = horizontalThumb.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        horizontalThumb.RenderTransform = transform;
                    }
                    
                    transform.X = newPosition - thumbOffset;
                }
            }
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            UpdateSliderAppearance();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            UpdateSliderAppearance();
        }
    }
}
