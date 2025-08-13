using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Izi.Travel.Core.Command
{
    /// <summary>
    /// A command that relays its functionality to a delegate
    /// </summary>
    public class RelayCommand : BaseCommand
    {
        private readonly Func<object, Task> _executeAsync;
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked</param>
        /// <param name="canExecute">The function that determines if the command can execute</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            : base(canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class for async operations
        /// </summary>
        /// <param name="executeAsync">The async action to execute when the command is invoked</param>
        /// <param name="canExecute">The function that determines if the command can execute</param>
        public RelayCommand(Func<object, Task> executeAsync, Func<object, bool> canExecute = null)
            : base(canExecute)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            if (_executeAsync != null)
            await _executeAsync(parameter);
            else
                _execute(parameter);
        }
    }

    /// <summary>
    /// A generic command that relays its functionality to a delegate
    /// </summary>
    /// <typeparam name="T">The type of the command parameter</typeparam>
    public class RelayCommand<T> : BaseCommand
    {
        private readonly Func<T, Task> _executeAsync;
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked</param>
        /// <param name="canExecute">The function that determines if the command can execute</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
            : base(p => canExecute == null || (p is T && canExecute((T)p)))
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class for async operations
        /// </summary>
        /// <param name="executeAsync">The async action to execute when the command is invoked</param>
        /// <param name="canExecute">The function that determines if the command can execute</param>
        public RelayCommand(Func<T, Task> executeAsync, Func<T, bool> canExecute = null)
            : base(p => canExecute == null || (p is T && canExecute((T)p)))
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = canExecute;
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            if (parameter is T typedParameter)
            {
                if (_executeAsync != null)
                    await _executeAsync(typedParameter);
                else
                    _execute(typedParameter);
            }
            else if (parameter == null && default(T) == null)
            {
                if (_executeAsync != null)
                    await _executeAsync(default);
                else
                    _execute(default);
            }
            else
            {
                throw new ArgumentException("Invalid command parameter type", nameof(parameter));
            }
        }
    }
}
