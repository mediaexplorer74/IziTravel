using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using Izi.Travel.Core.Command;

#if WINDOWS_PHONE_APP
using SelectorBase = Windows.UI.Xaml.Controls.Primitives.Selector;
#else
using SelectorBase = Windows.UI.Xaml.Controls.ListViewBase;
#endif
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;

namespace Izi.Travel.Controls.Placeholders
{
    #region Controls

    /// <summary>
    /// Placeholder for MarginSelector control
    /// </summary>
    public class MarginSelector : Control
    {
        public static readonly DependencyProperty PortraitProperty =
            DependencyProperty.Register("Portrait", typeof(string), typeof(MarginSelector), new PropertyMetadata("0,0,0,0"));

        public static readonly DependencyProperty LandscapeLeftProperty =
            DependencyProperty.Register("LandscapeLeft", typeof(string), typeof(MarginSelector), new PropertyMetadata("0,0,0,0"));

        public static readonly DependencyProperty LandscapeRightProperty =
            DependencyProperty.Register("LandscapeRight", typeof(string), typeof(MarginSelector), new PropertyMetadata("0,0,0,0"));

        public string Portrait
        {
            get { return (string)GetValue(PortraitProperty); }
            set { SetValue(PortraitProperty, value); }
        }

        public string LandscapeLeft
        {
            get { return (string)GetValue(LandscapeLeftProperty); }
            set { SetValue(LandscapeLeftProperty, value); }
        }

        public string LandscapeRight
        {
            get { return (string)GetValue(LandscapeRightProperty); }
            set { SetValue(LandscapeRightProperty, value); }
        }

        public MarginSelector()
        {
            this.DefaultStyleKey = typeof(MarginSelector);
        }

        public Thickness GetMargin(Windows.UI.ViewManagement.ApplicationViewOrientation orientation, bool isLandscapeLeft)
        {
            string marginString;
            if (orientation == Windows.UI.ViewManagement.ApplicationViewOrientation.Landscape)
            {
                marginString = isLandscapeLeft ? LandscapeLeft : LandscapeRight;
            }
            else
            {
                marginString = Portrait;
            }

            var margins = marginString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (margins.Length == 4)
            {
                return new Thickness(
                    double.Parse(margins[0]),
                    double.Parse(margins[1]),
                    double.Parse(margins[2]),
                    double.Parse(margins[3]));
            }
            return new Thickness(0);
        }
    }

    /// <summary>
    /// Placeholder for HeaderControl
    /// </summary>
    public class HeaderControl : ContentControl
    {
        public HeaderControl()
        {
            this.DefaultStyleKey = typeof(HeaderControl);
        }
    }


    /// <summary>
    /// Placeholder for ProgressOverlay control
    /// </summary>
    public class ProgressOverlay : ContentControl
    {
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(ProgressOverlay), new PropertyMetadata(false));

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public ProgressOverlay()
        {
            this.DefaultStyleKey = typeof(ProgressOverlay);
        }
    }

    /// <summary>
    /// Placeholder for LongListSelector (using ListView as base)
    /// </summary>
    public class LongListSelector : ListView
    {
        public static readonly DependencyProperty GroupHeaderTemplateProperty =
            DependencyProperty.Register("GroupHeaderTemplate", typeof(DataTemplate), typeof(LongListSelector), new PropertyMetadata(null));

        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register("LoadCommand", typeof(BaseCommand), typeof(LongListSelector), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectCommandProperty =
            DependencyProperty.Register("SelectCommand", typeof(BaseCommand), typeof(LongListSelector), new PropertyMetadata(null));

        public static readonly DependencyProperty IsGroupingEnabledProperty =
            DependencyProperty.Register("IsGroupingEnabled", typeof(bool), typeof(LongListSelector), new PropertyMetadata(false));

        public static readonly DependencyProperty ListFooterProperty =
            DependencyProperty.Register("ListFooter", typeof(object), typeof(LongListSelector), new PropertyMetadata(null));

        public DataTemplate GroupHeaderTemplate
        {
            get => (DataTemplate)GetValue(GroupHeaderTemplateProperty);
            set => SetValue(GroupHeaderTemplateProperty, value);
        }

        public BaseCommand LoadCommand
        {
            get => (BaseCommand)GetValue(LoadCommandProperty);
            set => SetValue(LoadCommandProperty, value);
        }

        public BaseCommand SelectCommand
        {
            get => (BaseCommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }

        public bool IsGroupingEnabled
        {
            get => (bool)GetValue(IsGroupingEnabledProperty);
            set => SetValue(IsGroupingEnabledProperty, value);
        }

        public object ListFooter
        {
            get => GetValue(ListFooterProperty);
            set => SetValue(ListFooterProperty, value);
        }

        public LongListSelector()
        {
            this.DefaultStyleKey = typeof(LongListSelector);
            this.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && SelectCommand?.CanExecute(e.AddedItems[0]) == true)
            {
                SelectCommand.Execute(e.AddedItems[0]);
                // Clear selection to allow re-selection of the same item
                this.SelectedItem = null;
            }
        }
    }


    #endregion

    #region Behaviors

    /// <summary>
    /// Base class for behaviors
    /// </summary>
    public abstract class Behavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        public virtual void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
        }

        public virtual void Detach()
        {
            AssociatedObject = null;
        }
    }

    /// <summary>
    /// Placeholder for ViewAnalyticsBehavior
    /// </summary>
    public class ViewAnalyticsBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            // No-op for placeholder
        }
    }

    /// <summary>
    /// Behavior for handling selection in a Selector (WP8) or ListViewBase (UWP)
    /// </summary>
    public class SelectorSelectBehavior : Behavior<SelectorBase>
    {
        private SelectorBase _selector;
        private bool _isUpdating;

        /// <summary>
        /// Gets or sets the selected item
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(SelectorSelectBehavior),
                new PropertyMetadata(null, OnSelectedItemChanged));

        /// <summary>
        /// Gets or sets the selected item
        /// </summary>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            _selector = AssociatedObject;
            _selector.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (_selector != null)
            {
                _selector.SelectionChanged -= OnSelectionChanged;
                _selector = null;
            }
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (SelectorSelectBehavior)d;
            if (behavior._isUpdating)
                return;

            behavior.UpdateSelectedItem();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdating || _selector == null)
                return;

            _isUpdating = true;
            SelectedItem = _selector.SelectedItem;
            _isUpdating = false;
        }

        private void UpdateSelectedItem()
        {
            if (_isUpdating || _selector == null)
                return;

            _isUpdating = true;
            _selector.SelectedItem = SelectedItem;
            _isUpdating = false;
        }
    }


    #endregion

    #region Converters

    /// <summary>
    /// Base class for value converters
    /// </summary>
    public abstract class ValueConverter : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, string language);
        
        public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Placeholder for MtgObjectTypeToStringConverter
    /// </summary>
    public class MtgObjectTypeToStringConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return value?.ToString() ?? string.Empty;
        }
    }


    /// <summary>
    /// Placeholder for BoolToResultTextConverter
    /// </summary>
    public class BoolToResultTextConverter : ValueConverter
    {
        public string TrueText { get; set; } = "True";
        public string FalseText { get; set; } = "False";

        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
                return boolValue ? TrueText : FalseText;
                
            return FalseText;
        }
    }

    #endregion
}

// Extension methods for IBehavior interface
namespace Microsoft.Xaml.Interactivity
{
    public static class BehaviorExtensions
    {
        public static T GetValue<T>(this IBehavior behavior) where T : class
        {
            return behavior?.AssociatedObject as T;
        }
    }
}
