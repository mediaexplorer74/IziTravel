using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Izi.Travel.Controls
{
    public class WrapPanel : Panel
    {
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(WrapPanel), new PropertyMetadata(double.NaN, OnLayoutPropertyChanged));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(WrapPanel), new PropertyMetadata(double.NaN, OnLayoutPropertyChanged));

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(WrapPanel), new PropertyMetadata(Orientation.Horizontal, OnLayoutPropertyChanged));

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count == 0)
                return new Size(0, 0);

            double availableWidth = double.IsPositiveInfinity(availableSize.Width) ? double.MaxValue : availableSize.Width;
            double availableHeight = double.IsPositiveInfinity(availableSize.Height) ? double.MaxValue : availableSize.Height;

            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;
            double x = 0;
            double y = 0;
            double rowHeight = 0;
            double maxWidth = 0;

            foreach (UIElement child in Children)
            {
                if (child == null)
                    continue;

                // Measure the child
                child.Measure(availableSize);
                Size desiredSize = child.DesiredSize;

                if (Orientation == Orientation.Horizontal)
                {
                    double childWidth = double.IsNaN(itemWidth) ? desiredSize.Width : itemWidth;
                    double childHeight = double.IsNaN(itemHeight) ? desiredSize.Height : itemHeight;

                    if (x + childWidth > availableWidth && x > 0)
                    {
                        // Move to the next line
                        x = 0;
                        y += rowHeight;
                        rowHeight = 0;
                    }

                    rowHeight = Math.Max(rowHeight, childHeight);
                    x += childWidth;
                    maxWidth = Math.Max(maxWidth, x);
                }
                else
                {
                    double childWidth = double.IsNaN(itemWidth) ? desiredSize.Width : itemWidth;
                    double childHeight = double.IsNaN(itemHeight) ? desiredSize.Height : itemHeight;

                    if (y + childHeight > availableHeight && y > 0)
                    {
                        // Move to the next column
                        y = 0;
                        x += rowHeight;
                        rowHeight = 0;
                    }

                    rowHeight = Math.Max(rowHeight, childWidth);
                    y += childHeight;
                    maxWidth = Math.Max(maxWidth, x + rowHeight);
                }
            }

            if (Orientation == Orientation.Horizontal)
            {
                return new Size(maxWidth, y + rowHeight);
            }
            else
            {
                return new Size(maxWidth, y);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0)
                return finalSize;

            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;
            double x = 0;
            double y = 0;
            double rowHeight = 0;

            foreach (UIElement child in Children)
            {
                if (child == null)
                    continue;

                Size desiredSize = child.DesiredSize;

                if (Orientation == Orientation.Horizontal)
                {
                    double childWidth = double.IsNaN(itemWidth) ? desiredSize.Width : itemWidth;
                    double childHeight = double.IsNaN(itemHeight) ? desiredSize.Height : itemHeight;

                    if (x + childWidth > finalSize.Width && x > 0)
                    {
                        // Move to the next line
                        x = 0;
                        y += rowHeight;
                        rowHeight = 0;
                    }

                    child.Arrange(new Rect(x, y, childWidth, childHeight));
                    rowHeight = Math.Max(rowHeight, childHeight);
                    x += childWidth;
                }
                else
                {
                    double childWidth = double.IsNaN(itemWidth) ? desiredSize.Width : itemWidth;
                    double childHeight = double.IsNaN(itemHeight) ? desiredSize.Height : itemHeight;

                    if (y + childHeight > finalSize.Height && y > 0)
                    {
                        // Move to the next column
                        y = 0;
                        x += rowHeight;
                        rowHeight = 0;
                    }

                    child.Arrange(new Rect(x, y, childWidth, childHeight));
                    rowHeight = Math.Max(rowHeight, childWidth);
                    y += childHeight;
                }
            }

            return finalSize;
        }

        private static void OnLayoutPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as WrapPanel;
            if (panel != null)
            {
                panel.InvalidateMeasure();
                panel.InvalidateArrange();
            }
        }
    }
}
