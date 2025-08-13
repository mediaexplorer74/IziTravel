using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Izi.Travel.Controls
{
    public sealed partial class LongListSelector : ListView
    {
        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register(
                nameof(LoadCommand),
                typeof(ICommand),
                typeof(LongListSelector),
                new PropertyMetadata(null));

        public static readonly DependencyProperty SelectCommandProperty =
            DependencyProperty.Register(
                nameof(SelectCommand),
                typeof(ICommand),
                typeof(LongListSelector),
                new PropertyMetadata(null));

        public static readonly DependencyProperty IsGroupingEnabledProperty =
            DependencyProperty.Register(
                nameof(IsGroupingEnabled),
                typeof(bool),
                typeof(LongListSelector),
                new PropertyMetadata(false));

        public ICommand LoadCommand
        {
            get => (ICommand)GetValue(LoadCommandProperty);
            set => SetValue(LoadCommandProperty, value);
        }

        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }

        public bool IsGroupingEnabled
        {
            get => (bool)GetValue(IsGroupingEnabledProperty);
            set => SetValue(IsGroupingEnabledProperty, value);
        }

        public object ListHeader { get; set; }
        public DataTemplate ListHeaderTemplate { get; set; }
        public object ListFooter { get; set; }
        public DataTemplate ListFooterTemplate { get; set; }

        public LongListSelector()
        {
            this.InitializeComponent();
            this.SelectionChanged += OnSelectionChanged;
            this.Loaded += OnLoaded;
            this.RegisterPropertyChangedCallback(ItemsSourceProperty, OnItemsSourceChanged);
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.ItemsPanelRoot is ItemsStackPanel panel)
            {
                panel.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            }
        }

        private void OnItemsSourceChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (ItemsSource is INotifyCollectionChanged incc)
            {
                incc.CollectionChanged += ItemsSource_CollectionChanged;
            }
        }

        private void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && 
                LoadCommand != null && 
                LoadCommand.CanExecute(null) &&
                ItemsSource is ICollection collection &&
                e.NewStartingIndex >= collection.Count - 5) // Load more when we're near the end
            {
                LoadCommand.Execute(null);
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && SelectCommand != null && SelectCommand.CanExecute(SelectedItem))
            {
                SelectCommand.Execute(SelectedItem);
                SelectedItem = null; // Reset selection
            }
        }
    }
}

