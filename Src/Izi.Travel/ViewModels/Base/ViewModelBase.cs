using Caliburn.Micro;
using Izi.Travel.Utility;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Izi.Travel.Shell.ViewModels.Base
{
    /// <summary>
    /// Base class for all ViewModels that includes common functionality and string handling.
    /// </summary>
    public abstract class ViewModelBase : Screen, INotifyPropertyChangedEx, INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _busyMessage;

        /// <summary>
        /// Gets or sets a value indicating whether the ViewModel is busy.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        /// <summary>
        /// Gets or sets a message describing the current busy state.
        /// </summary>
        public string BusyMessage
        {
            get => _busyMessage;
            set => Set(ref _busyMessage, value);
        }

        /// <summary>
        /// Sets the property and notifies listeners when the value changes.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingStore">Reference to the backing field.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed, false otherwise.</returns>
        protected bool Set<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(backingStore, value))
                return false;

            backingStore = value;
            NotifyOfPropertyChange(propertyName);
            return true;
        }

        #region String Comparison Helpers

        /// <summary>
        /// Determines whether two strings are equal, ignoring case, using ordinal comparison rules.
        /// </summary>
        protected static bool StringEqualsOrdinalIgnoreCase(string a, string b)
        {
            return StringHelper.EqualsOrdinalIgnoreCase(a, b);
        }

        /// <summary>
        /// Determines whether a string starts with the specified value using ordinal comparison rules.
        /// </summary>
        protected static bool StartsWithOrdinal(string source, string value)
        {
            return StringHelper.StartsWithOrdinal(source, value);
        }

        /// <summary>
        /// Determines whether a string contains the specified value using ordinal comparison rules.
        /// </summary>
        protected static bool ContainsOrdinal(string source, string value)
        {
            return StringHelper.ContainsOrdinal(source, value);
        }

        /// <summary>
        /// Determines whether a string starts with the specified value, ignoring case, using ordinal comparison rules.
        /// </summary>
        protected static bool StartsWithOrdinalIgnoreCase(string source, string value)
        {
            return StringHelper.StartsWithOrdinalIgnoreCase(source, value);
        }

        /// <summary>
        /// Determines whether a string contains the specified value, ignoring case, using ordinal comparison rules.
        /// </summary>
        protected static bool ContainsOrdinalIgnoreCase(string source, string value)
        {
            return StringHelper.ContainsOrdinalIgnoreCase(source, value);
        }

        #endregion

        #region Lifecycle Methods

        /// <summary>
        /// Called when the view is being initialized.
        /// </summary>
        protected override async void OnInitialize()
        {
            await InitializeAsync();
            base.OnInitialize();
        }

        /// <summary>
        /// Asynchronous initialization method for the ViewModel.
        /// </summary>
        protected virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the view is being activated.
        /// </summary>
        protected override async void OnActivate()
        {
            await ActivateAsync();
            base.OnActivate();
        }

        /// <summary>
        /// Asynchronous activation method for the ViewModel.
        /// </summary>
        protected virtual Task ActivateAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the view is being deactivated.
        /// </summary>
        /// <param name="close">True if the view is being closed, false if it's just being deactivated.</param>
        protected override async void OnDeactivate(bool close)
        {
            await DeactivateAsync(close);
            base.OnDeactivate(close);
        }

        /// <summary>
        /// Asynchronous deactivation method for the ViewModel.
        /// </summary>
        /// <param name="close">True if the view is being closed, false if it's just being deactivated.</param>
        protected virtual Task DeactivateAsync(bool close)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
