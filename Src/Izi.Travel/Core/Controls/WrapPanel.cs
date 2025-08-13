using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Izi.Travel.Core.Controls
{
    public class WrapPanel : Panel
    {
        public static readonly DependencyProperty ItemWidthProperty = 
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(WrapPanel), 
                new PropertyMetadata(double.NaN, OnLayoutPropertyChanged));

        public static readonly DependencyProperty ItemHeightProperty = 
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(WrapPanel), 
                new PropertyMetadata(double.NaN, OnLayoutPropertyChanged));

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(WrapPanel),
                new PropertyMetadata(Orientation.Horizontal, OnLayoutPropertyChanged));

        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        private static void OnLayoutPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (WrapPanel)d;
            panel.InvalidateMeasure();
            panel.InvalidateArrange();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var totalMeasure = new Size();
            var maxMeasure = new Size();
            var lineMeasure = new Size();
            var isHorizontal = Orientation == Orientation.Horizontal;

            foreach (var child in Children)
            {
                if (child == null) continue;

                // Measure the child
                child.Measure(availableSize);
                var desiredSize = child.DesiredSize;

                if (isHorizontal)
                {
                    lineMeasure.Width += desiredSize.Width;
                    lineMeasure.Height = Math.Max(lineMeasure.Height, desiredSize.Height);

                    if (lineMeasure.Width > availableSize.Width)
                    {
                        // New line
                        totalMeasure.Width = Math.Max(lineMeasure.Width - desiredSize.Width, totalMeasure.Width);
                        totalMeasure.Height += lineMeasure.Height;
                        lineMeasure = desiredSize;

                        // If the element is wider than the constraint, give it a separate line
                        if (desiredSize.Width > availableSize.Width)
                        {
                            totalMeasure.Width = Math.Max(desiredSize.Width, totalMeasure.Width);
                            totalMeasure.Height += desiredSize.Height;
                            lineMeasure = new Size(0, 0);
                        }
                    }

                    maxMeasure.Width = Math.Max(lineMeasure.Width, maxMeasure.Width);
                    maxMeasure.Height = totalMeasure.Height + lineMeasure.Height;
                }
                else
                {
                    lineMeasure.Height += desiredSize.Height;
                    lineMeasure.Width = Math.Max(lineMeasure.Width, desiredSize.Width);

                    if (lineMeasure.Height > availableSize.Height)
                    {
                        // New column
                        totalMeasure.Height = Math.Max(lineMeasure.Height - desiredSize.Height, totalMeasure.Height);
                        totalMeasure.Width += lineMeasure.Width;
                        lineMeasure = desiredSize;

                        // If the element is taller than the constraint, give it a separate column
                        if (desiredSize.Height > availableSize.Height)
                        {
                            totalMeasure.Height = Math.Max(desiredSize.Height, totalMeasure.Height);
                            totalMeasure.Width += desiredSize.Width;
                            lineMeasure = new Size(0, 0);
                        }
                    }

                    maxMeasure.Height = Math.Max(lineMeasure.Height, maxMeasure.Height);
                    maxMeasure.Width = totalMeasure.Width + lineMeasure.Width;
                }
            }

            // Add the last line/column
            if (isHorizontal)
            {
                totalMeasure.Width = Math.Max(lineMeasure.Width, totalMeasure.Width);
                totalMeasure.Height += lineMeasure.Height;
            }
            else
            {
                totalMeasure.Width += lineMeasure.Width;
                totalMeasure.Height = Math.Max(lineMeasure.Height, totalMeasure.Height);
            }

            return new Size(
                double.IsInfinity(availableSize.Width) ? totalMeasure.Width : availableSize.Width,
                double.IsInfinity(availableSize.Height) ? totalMeasure.Height : availableSize.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var isHorizontal = Orientation == Orientation.Horizontal;
            var position = new Point(0, 0);
            var lineMeasure = new Size();

            foreach (var child in Children)
            {
                if (child == null) continue;

                var desiredSize = child.DesiredSize;
                var itemWidth = double.IsNaN(ItemWidth) ? desiredSize.Width : ItemWidth;
                var itemHeight = double.IsNaN(ItemHeight) ? desiredSize.Height : ItemHeight;

                if (isHorizontal)
                {
                    if (position.X + itemWidth > finalSize.Width)
                    {
                        // Move to next line
                        position.X = 0;
                        position.Y += lineMeasure.Height;
                        lineMeasure = new Size(0, 0);
                    }

                    child.Arrange(new Rect(position.X, position.Y, itemWidth, itemHeight));
                    position.X += itemWidth;
                    lineMeasure.Width += itemWidth;
                    lineMeasure.Height = Math.Max(itemHeight, lineMeasure.Height);
                }
                else
                {
                    if (position.Y + itemHeight > finalSize.Height)
                    {
                        // Move to next column
                        position.Y = 0;
                        position.X += lineMeasure.Width;
                        lineMeasure = new Size(0, 0);
                    }

                    child.Arrange(new Rect(position.X, position.Y, itemWidth, itemHeight));
                    position.Y += itemHeight;
                    lineMeasure.Height += itemHeight;
                    lineMeasure.Width = Math.Max(itemWidth, lineMeasure.Width);
                }
            }

            return finalSize;
        }
    }
}
