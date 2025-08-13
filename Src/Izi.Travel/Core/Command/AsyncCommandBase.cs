using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Izi.Travel.Core.Command
{
    /// <summary>
    /// Base class for asynchronous command implementations
    /// </summary>
    public abstract class AsyncCommandBase : IAsyncCommand, ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract Task ExecuteAsync(object parameter);

        /// <summary>
        /// Raises the CanExecuteChanged event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Public Execute method to support existing call sites invoking Execute directly.
        /// Internally forwards to the ICommand explicit implementation which runs asynchronously.
        /// </summary>
        public void Execute(object parameter)
        {
            ((ICommand)this).Execute(parameter);
        }

        /// <inheritdoc/>
        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync(parameter).ConfigureAwait(false);
        }
    }
}
