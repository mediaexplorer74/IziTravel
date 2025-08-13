using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Izi.Travel.Core.Command
{
    /// <summary>
    /// Base class for all commands in the application
    /// </summary>
    public abstract class BaseCommand : AsyncCommandBase, IAsyncCommand, ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private bool _isExecuting;

        /// <summary>
        /// Initializes a new instance of the BaseCommand class
        /// </summary>
        /// <param name="canExecute">Function that determines if the command can execute</param>
        protected BaseCommand(Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        /// <inheritdoc/>
        public override async Task ExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();
                
                await OnExecuteAsync(parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">The parameter for the command</param>
        /// <returns>A task representing the asynchronous operation</returns>
        protected abstract Task OnExecuteAsync(object parameter);
    }
}

