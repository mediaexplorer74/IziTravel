using System.Windows.Input;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Izi.Travel.Shell.Core.Components.Behaviors
{
    public class ListViewLoadBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty LoadCommandProperty = DependencyProperty.Register(nameof(LoadCommand), typeof(ICommand), typeof(ListViewLoadBehavior), new PropertyMetadata(null));
        public static readonly DependencyProperty LoadItemCommandProperty = DependencyProperty.Register(nameof(LoadItemCommand), typeof(ICommand), typeof(ListViewLoadBehavior), new PropertyMetadata(null));

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
                // Check if the last item is being displayed
                if (args.Item == AssociatedObject.ItemsSource[AssociatedObject.ItemsSource.Count - 1])
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