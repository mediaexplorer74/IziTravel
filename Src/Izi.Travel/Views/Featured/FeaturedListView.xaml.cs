using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Izi.Travel.ViewModels.Featured;
using Windows.UI.Xaml.Controls.Primitives;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Izi.Travel.Views.Featured
{
    public sealed partial class FeaturedListView : UserControl
    {
        private ScrollViewer _scrollViewer;
        private bool _isLoading;
        private const double LoadMoreThreshold = 0.8; // 80% scrolled

        public FeaturedListView()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is FeaturedListViewModel viewModel)
            {
                // Initialize view model if needed

            }

            // Get the ScrollViewer for manual scroll handling
            _scrollViewer = FindVisualChild<ScrollViewer>(FeaturedList);
            if (_scrollViewer != null)
            {
                _scrollViewer.ViewChanged += OnScrollViewerViewChanged;
            }
        }

        private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_scrollViewer == null || _isLoading || !(DataContext is FeaturedListViewModel viewModel) || 
                viewModel.IsLoadingMore || !viewModel.HasMoreItems)
            {
                return;
            }

            // Check if we're near the bottom (80% scrolled)
            var scrollPosition = _scrollViewer.VerticalOffset;
            var maxScroll = _scrollViewer.ScrollableHeight;
            var threshold = maxScroll * LoadMoreThreshold;

            if (scrollPosition >= threshold)
            {
                _ = LoadMoreItemsAsync(viewModel);
            }
        }

        private async Task LoadMoreItemsAsync(FeaturedListViewModel viewModel)
        {
            if (_isLoading || viewModel.IsLoadingMore || !viewModel.LoadMoreCommand.CanExecute(null))
                return;

            try
            {
                _isLoading = true;
                await viewModel.LoadMoreCommand.ExecuteAsync(null);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            // Handle container content changing if needed
            // This can be used for virtualization optimizations
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && DataContext is FeaturedListViewModel viewModel)
            {
                viewModel.NavigateCommand?.Execute(e.ClickedItem);
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changed if needed
            // For now, just clear the selection to allow re-selection of the same item
            if (sender is ListView listView)
            {
                listView.SelectedItem = null;
            }
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }

                var result = FindVisualChild<T>(child);
                if (result != null) return result;
            }

            return null;
        }
    }
}

