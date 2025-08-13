using Microsoft.Xaml.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using System.Windows.Input;
using System.Threading.Tasks;
using Izi.Travel.Core.Command;

namespace Izi.Travel.Core.Components.Behaviors
{
    /// <summary>
    /// Behavior for handling loading more items when scrolling in a ListView
    /// </summary>
    public class ListViewLoadBehavior : Behavior<ListView>
    {
        private bool _isLoading;
        private const int LoadThreshold = 5; // Number of items from the end to trigger load more

        public static readonly DependencyProperty LoadCommandProperty = 
            DependencyProperty.Register(nameof(LoadCommand), typeof(ICommand), typeof(ListViewLoadBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty LoadItemCommandProperty = 
            DependencyProperty.Register(nameof(LoadItemCommand), typeof(ICommand), typeof(ListViewLoadBehavior), new PropertyMetadata(null));

        public ICommand LoadCommand
        {
            get => (ICommand)GetValue(LoadCommandProperty);
            set => SetValue(LoadCommandProperty, value);
        }

        public ICommand LoadItemCommand
        {
            get => (ICommand)GetValue(LoadItemCommandProperty);
            set => SetValue(LoadItemCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            
            if (AssociatedObject == null) return;
            
            // Handle container content changing for virtualization
            AssociatedObject.ContainerContentChanging += OnContainerContentChanging;
            
            // Handle scroll viewer changes for infinite scroll
            AssociatedObject.Loaded += OnLoaded;
            
            // Handle item click/selection
            AssociatedObject.ItemClick += OnItemClick;
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.ContainerContentChanging -= OnContainerContentChanging;
                AssociatedObject.Loaded -= OnLoaded;
                AssociatedObject.ItemClick -= OnItemClick;
            }
            
            base.OnDetaching();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // In UWP, get ScrollViewer via VisualTreeHelper or extension: simply try GetDescendant.
            var scrollViewer = FindScrollViewer(AssociatedObject);
            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged += OnViewChanged;
            }
        }

        private static ScrollViewer FindScrollViewer(DependencyObject root)
        {
            if (root == null) return null;
            if (root is ScrollViewer sv) return sv;

            int count = Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++)
            {
                var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(root, i);
                var result = FindScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

        private async void OnViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_isLoading || LoadCommand == null || !(sender is ScrollViewer scrollViewer) || 
                !(AssociatedObject?.ItemsSource is ICollection items) || items.Count == 0)
            {
                return;
            }

            // Check if we're near the bottom of the list
            var scrollPosition = scrollViewer.VerticalOffset;
            var maxScroll = scrollViewer.ScrollableHeight;
            var threshold = Math.Max(100, scrollViewer.ViewportHeight * 0.2); // 20% of viewport or 100px

            if (maxScroll - scrollPosition <= threshold)
            {
                await ExecuteLoadCommandAsync();
            }
        }

        private void OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue || LoadItemCommand == null) 
                return;

            // Check if this is one of the last few items
            if (AssociatedObject?.ItemsSource is ICollection items && args.ItemIndex >= items.Count - LoadThreshold - 1)
            {
                _ = ExecuteLoadCommandAsync();
            }

            // Execute item load command if available
            if (LoadItemCommand?.CanExecute(args.Item) == true)
            {
                LoadItemCommand.Execute(args.Item);
            }
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (LoadItemCommand?.CanExecute(e.ClickedItem) == true)
            {
                LoadItemCommand.Execute(e.ClickedItem);
            }
        }

        private async Task ExecuteLoadCommandAsync()
        {
            if (_isLoading || LoadCommand == null || !LoadCommand.CanExecute(null))
                return;

            try
            {
                _isLoading = true;
                
                if (LoadCommand is IAsyncCommand asyncCommand)
                {
                    await asyncCommand.ExecuteAsync(null);
                }
                else
                {
                    LoadCommand.Execute(null);
                }
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
