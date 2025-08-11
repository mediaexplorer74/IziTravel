using Microsoft.Xaml.Interactivity;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;

namespace Izi.Travel.Shell.Core.Components.Behaviors
{
    public class ListViewLoadBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty LoadCommandProperty = DependencyProperty.Register(nameof(LoadCommand), typeof(System.Windows.Input.ICommand), typeof(ListViewLoadBehavior), new PropertyMetadata(null));
        public static readonly DependencyProperty LoadItemCommandProperty = DependencyProperty.Register(nameof(LoadItemCommand), typeof(System.Windows.Input.ICommand), typeof(ListViewLoadBehavior), new PropertyMetadata(null));

        public System.Windows.Input.ICommand LoadCommand
        {
            get => (System.Windows.Input.ICommand)GetValue(LoadCommandProperty);
            set => SetValue(LoadCommandProperty, value);
        }

        public System.Windows.Input.ICommand LoadItemCommand
        {
            get => (System.Windows.Input.ICommand)GetValue(LoadItemCommandProperty);
            set => SetValue(LoadItemCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ContainerContentChanging += AssociatedObject_ContainerContentChanging;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ContainerContentChanging -= AssociatedObject_ContainerContentChanging;
        }

        private void AssociatedObject_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue) return;

            if (LoadCommand != null && AssociatedObject.ItemsSource != null)
            {
                // Safely get the count of items
                int itemCount = 0;
                if (AssociatedObject.ItemsSource is ICollection collection)
                {
                    itemCount = collection.Count;
                }
                else if (AssociatedObject.ItemsSource is IEnumerable<object> enumerable)
                {
                    itemCount = System.Linq.Enumerable.Count(enumerable);
                }

                // Only proceed if we have items
                if (itemCount > 0 && args.ItemIndex == itemCount - 1)
                {
                    if (LoadCommand.CanExecute(null))
                    {
                        LoadCommand.Execute(null);
                    }
                }
            }

            if (LoadItemCommand != null && AssociatedObject.ItemsSource != null)
            {
                if (LoadItemCommand.CanExecute(args.Item))
                {
                    LoadItemCommand.Execute(args.Item);
                }
            }
        }
    }
}
