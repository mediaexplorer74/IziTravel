using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Controls
{
    public sealed partial class LabeledRating : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(LabeledRating), 
                new PropertyMetadata(0.0, OnRatingChanged));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(LabeledRating), 
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty FilledItemBackgroundProperty =
            DependencyProperty.Register(nameof(FilledItemBackground), typeof(Brush), typeof(LabeledRating), 
                new PropertyMetadata(null, OnAppearanceChanged));

        public static readonly DependencyProperty UnfilledItemBackgroundProperty =
            DependencyProperty.Register(nameof(UnfilledItemBackground), typeof(Brush), typeof(LabeledRating), 
                new PropertyMetadata(null, OnAppearanceChanged));

        public ObservableCollection<StarItem> StarItems { get; } = new ObservableCollection<StarItem>();

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public Brush FilledItemBackground
        {
            get => (Brush)GetValue(FilledItemBackgroundProperty);
            set => SetValue(FilledItemBackgroundProperty, value);
        }

        public Brush UnfilledItemBackground
        {
            get => (Brush)GetValue(UnfilledItemBackgroundProperty);
            set => SetValue(UnfilledItemBackgroundProperty, value);
        }

        public LabeledRating()
        {
            this.InitializeComponent();
            UpdateStars();
        }

        private static void OnRatingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LabeledRating control)
            {
                control.UpdateStars();
            }
        }

        private static void OnAppearanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LabeledRating control)
            {
                control.UpdateStars();
            }
        }

        private void UpdateStars()
        {
            StarItems.Clear();
            
            int fullStars = (int)Value;
            bool hasHalfStar = Value - fullStars >= 0.5;
            
            // Add filled stars
            for (int i = 0; i < fullStars; i++)
            {
                StarItems.Add(new StarItem { IsFilled = true, Foreground = FilledItemBackground });
            }
            
            // Add half star if needed
            if (hasHalfStar)
            {
                StarItems.Add(new StarItem { IsHalfFilled = true, Foreground = FilledItemBackground });
                fullStars++; // Account for the half star in the count
            }
            
            // Add empty stars to make total 5
            for (int i = fullStars; i < 5; i++)
            {
                StarItems.Add(new StarItem { IsFilled = false, Foreground = UnfilledItemBackground });
            }
        }
    }

    public class StarItem
    {
        public bool IsFilled { get; set; }
        public bool IsHalfFilled { get; set; }
        public Brush Foreground { get; set; }
    }
}
