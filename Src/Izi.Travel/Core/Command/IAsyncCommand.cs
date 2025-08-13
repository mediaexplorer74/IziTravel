using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Izi.Travel.Core.Command
{
    /// <summary>
    /// Interface for asynchronous command implementations
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Executes the command asynchronously
        /// </summary>
        /// <param name="parameter">The parameter for the command</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task ExecuteAsync(object parameter);

        /// <summary>
        /// Determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">The parameter for the command</param>
        /// <returns>True if the command can execute; otherwise, false</returns>
        bool CanExecute(object parameter);

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Implementation of IAsyncCommand that uses delegates for execution
    /// </summary>
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;
        private bool _isExecuting;

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class
        /// </summary>
        /// <param name="execute">The function to execute when the command is invoked</param>
        /// <param name="canExecute">The function that determines whether the command can execute</param>
        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class for parameterless async execution.
        /// </summary>
        /// <param name="execute">The parameterless async function to execute.</param>
        /// <param name="canExecute">Optional parameterless function that determines whether the command can execute.</param>
        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = _ => execute();
            _canExecute = canExecute != null ? new Func<object, bool>(_ => canExecute()) : null;
        }

        /// <summary>
        /// Initializes a new instance of the AsyncCommand class with object-parameter execute and parameterless canExecute.
        /// </summary>
        /// <param name="execute">The function to execute when the command is invoked</param>
        /// <param name="canExecute">The parameterless function that determines whether the command can execute</param>
        public AsyncCommand(Func<object, Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute != null ? new Func<object, bool>(_ => canExecute()) : null;
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
                
                await _execute(parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}
