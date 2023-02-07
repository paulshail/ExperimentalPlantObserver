using System.Diagnostics;
using System.Windows.Input;

namespace MessageHandler;

        public class RelayCommand<T> : ICommand
        {
            /// <summary>
            /// Method or function to be executed and bool value indicating if it can be executed.
            /// </summary>
            private readonly Func<T?, bool>? _canExecute;

            /// <summary>
            /// Method or function that is to be executed.
            /// </summary>
            private readonly Action<T> _execute;

            /// <summary>
            /// Creates a new instance of the RelayCommand.
            /// Sets the execute and canExecute.
            /// </summary>
            /// <param name="execute">Method or object to execute</param>
            /// <param name="canExecute">Can execute, defaults to true</param>
            public RelayCommand(Action<T> execute, Func<T?, bool>? canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            /// <summary>
            /// CanExecuteChanged automatically picks up changes that
            /// could affect the outcome of our CanExecute Method
            /// </summary>
            public event EventHandler? CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }

            /// <summary>
            /// Determinds whether the command can be executed or not
            /// </summary>
            /// <param name="parameter"></param>
            /// <returns></returns>
            [DebuggerStepThrough]
            public bool CanExecute(object? parameter)
            {
                if (parameter is null)
                    return false;
                return _canExecute?.Invoke((T)parameter) ?? true;
            }

            /// <summary>
            /// Executes a command or function.
            /// </summary>
            /// <param name="parameter">Method or function to execute</param>
            [DebuggerStepThrough]
            public void Execute(object? parameter)
            {
                if (parameter is not null) _execute((T)parameter);
            }
        }

        public class RelayCommand : RelayCommand<object>
        {
            public RelayCommand(Action<object> execute)
                : base(execute) { }
        }
    
