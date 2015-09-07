using System;
using System.Windows.Input;

namespace SpeedwayDatabaseViewModel
{
    public class RelayCommand : ICommand
    {
        #region Fields
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        #endregion

        #region Constructors
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute is NULL");
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }
        #endregion

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion
    }
}
