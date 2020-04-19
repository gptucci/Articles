using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BlazorToDoListMVVMLib.Service
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;

        private readonly Func<object, bool> _condition;

        public DelegateCommand(Action<object> action, Func<object, bool> condition)
        {
            _action = action;
            _condition = condition;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _action.Invoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _condition == null || _condition.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
