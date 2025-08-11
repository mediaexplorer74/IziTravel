using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Izi.Travel.Shell.Core.Controls.Flyout
{
    public abstract class FlyoutBase : DependencyObject
    {
        private bool _silentIsOpen;
        private Flyout _hostFlyout;
        private FrameworkElement _owner;

        public static readonly DependencyProperty AttachedFlyoutsProperty =
            DependencyProperty.RegisterAttached("AttachedFlyouts", 
                typeof(FlyoutCollection), 
                typeof(FlyoutBase), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", 
                typeof(bool), 
                typeof(FlyoutBase), 
                new PropertyMetadata(false, OnIsOpenPropertyChanged));

        public event EventHandler<object> Opening;
        public event EventHandler<object> Opened;
        public event EventHandler<object> Closing;
        public event EventHandler<object> Closed;

        public string Key { get; set; }

        protected FrameworkElement Owner => _owner;

        public static FlyoutCollection GetAttachedFlyouts(FrameworkElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var flyouts = (FlyoutCollection)element.GetValue(AttachedFlyoutsProperty);
            if (flyouts == null)
            {
                flyouts = new FlyoutCollection(element);
                element.SetValue(AttachedFlyoutsProperty, flyouts);
            }
            return flyouts;
        }

        public static void SetIsOpen(FlyoutBase element, bool value)
        {
            element.SetValue(IsOpenProperty, value);
        }

        public static bool GetIsOpen(FlyoutBase element)
        {
            return (bool)element.GetValue(IsOpenProperty);
        }

        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FlyoutBase flyout) || flyout._silentIsOpen || !(e.NewValue is bool isOpen))
                return;

            if (isOpen)
                flyout.Show();
            else
                flyout.Hide();
        }

        public void Show()
        {
            ShowImpl();
            SetIsOpenSilent(true);
        }

        public void Hide()
        {
            HideImpl();
            SetIsOpenSilent(false);
        }

        internal void SetOwner(FrameworkElement owner)
        {
            _owner = owner;
        }

        protected abstract void ShowImpl();
        protected abstract void HideImpl();

        protected virtual void OnOpening()
        {
            Opening?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOpened()
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClosing()
        {
            Closing?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        protected void SetIsOpenSilent(bool isOpen)
        {
            _silentIsOpen = true;
            try
            {
                SetValue(IsOpenProperty, isOpen);
            }
            finally
            {
                _silentIsOpen = false;
            }
        }
    }

    public class FlyoutCollection : ObservableCollection<FlyoutBase>
    {
        private readonly FrameworkElement _owner;

        public FlyoutCollection(FrameworkElement owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (FlyoutBase flyout in e.NewItems)
                flyout.SetOwner(_owner);
            }

            if (e.OldItems != null)
            {
                foreach (FlyoutBase flyout in e.OldItems)
                    if (flyout != null)
                        flyout.SetOwner(null);
            }
        }
    }
}
