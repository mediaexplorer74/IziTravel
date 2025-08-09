using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Izi.Travel.Shell.Core.Converters;

namespace Izi.Travel.Shell.Controls
{
    [ContentProperty(Name = "ItemTemplate")]
    public sealed partial class FlipView : UserControl
    {
        //private FlipView FlipViewControl;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(FlipView),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                nameof(SelectedIndex),
                typeof(int),
                typeof(FlipView),
                new PropertyMetadata(-1, OnSelectedIndexChanged));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(FlipView),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ItemTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(ItemTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(FlipView),
                new PropertyMetadata(null));

        public static readonly DependencyProperty IsItemClickEnabledProperty =
            DependencyProperty.Register(
                nameof(IsItemClickEnabled),
                typeof(bool),
                typeof(FlipView),
                new PropertyMetadata(false));

        public static readonly DependencyProperty ShowIndexerProperty =
            DependencyProperty.Register(
                nameof(ShowIndexer),
                typeof(bool),
                typeof(FlipView),
                new PropertyMetadata(true));

        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register(
                nameof(ItemClickCommand),
                typeof(ICommand),
                typeof(FlipView),
                new PropertyMetadata(null));

        public event EventHandler<ItemClickEventArgs> ItemClick;
        private bool _isUpdatingSelection;

        public FlipView()
        {
            this.InitializeComponent();
      
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            set => SetValue(ItemTemplateSelectorProperty, value);
        }

        public bool IsItemClickEnabled
        {
            get => (bool)GetValue(IsItemClickEnabledProperty);
            set => SetValue(IsItemClickEnabledProperty, value);
        }

        public bool ShowIndexer
        {
            get => (bool)GetValue(ShowIndexerProperty);
            set => SetValue(ShowIndexerProperty, value);
        }

        public ICommand ItemClickCommand
        {
            get => (ICommand)GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlipView flipView)
            {
                if (e.OldValue is INotifyCollectionChanged oldCollection)
                {
                    oldCollection.CollectionChanged -= flipView.OnItemsCollectionChanged;
                }

                if (e.NewValue is INotifyCollectionChanged newCollection)
                {
                    newCollection.CollectionChanged += flipView.OnItemsCollectionChanged;
                }

                flipView.UpdateIndexerSelection();
            }
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlipView flipView && !flipView._isUpdatingSelection)
            {
                flipView.UpdateIndexerSelection();
            }
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateIndexerSelection();
        }

        private void UpdateIndexerSelection()
        {
            if (ItemsSource == null || !ShowIndexer)
                return;

            var items = ItemsSource.Cast<object>().ToList();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is ISelectable selectable)
                {
                    selectable.IsSelected = i == SelectedIndex;
                }
            }
        }

        private void FlipViewControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlipViewControl.SelectedIndex >= 0 && FlipViewControl.SelectedIndex != SelectedIndex)
            {
                _isUpdatingSelection = true;
                SelectedIndex = FlipViewControl.SelectedIndex;
                _isUpdatingSelection = false;
                UpdateIndexerSelection();
            }
        }

        private void FlipViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Ensure the initial selection is set
            if (SelectedIndex >= 0 && FlipViewControl.SelectedIndex != SelectedIndex)
            {
                FlipViewControl.SelectedIndex = SelectedIndex;
            }
            UpdateIndexerSelection();
        }
    }

    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
