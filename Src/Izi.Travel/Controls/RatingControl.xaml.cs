using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Shell.Controls
{
    public sealed partial class RatingControl : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(RatingControl), 
                new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(RatingControl), 
                new PropertyMetadata(false));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public bool ReadOnly
        {
            get => (bool)GetValue(ReadOnlyProperty);
            set => SetValue(ReadOnlyProperty, value);
        }

        public RatingControl()
        {
            this.InitializeComponent();
            UpdateStars();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RatingControl control)
            {
                control.UpdateStars();
            }
        }

        private void UpdateStars()
        {
            var stars = new[] { Star1, Star2, Star3, Star4, Star5 };
            double value = Value;
            
            for (int i = 0; i < stars.Length; i++)
            {
                double starValue = i + 1;
                var star = stars[i];
                
                if (value >= starValue)
                {
                    star.Text = "\uE1CF"; // Filled star
                    star.Foreground = (SolidColorBrush)Application.Current.Resources["IziTravelDarkBrush"];
                }
                else if (value >= starValue - 0.5)
                {
                    star.Text = "\uE1CE"; // Half-filled star
                    star.Foreground = (SolidColorBrush)Application.Current.Resources["IziTravelDarkGrayBrush"];
                }
                else
                {
                    star.Text = "\uE1CE"; // Empty star
                    star.Foreground = (SolidColorBrush)Application.Current.Resources["IziTravelGrayBrush"];
                }
            }
        }

        private void Star_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (ReadOnly) return;
            
            if (sender is TextBlock star && star.Tag is string tag && int.TryParse(tag, out int value))
            {
                Value = value;
                UpdateStars();
            }
        }
    }
}
