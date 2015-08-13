using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeedwayDatabaseModel
{
    class KeyCommand : ICommand
    {
        #region Fields

        private Action<object> _action;
        public Key KeyPressed { get; set; }

        #endregion

        #region Constructor

        public KeyCommand(Action<object> action)
        {
            if (action == null) throw new ArgumentNullException("Action can't by null.");
            _action = action;
        }

        #endregion 

        #region ICommand

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;

        #endregion

    }
}
