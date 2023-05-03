using System;
using System.Windows.Input;

namespace JanetRevit.Core.Commands
{
    public class ParamCommand : ICommand
    {
        private Action<object> mAction = null;
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        public ParamCommand(Action<object> action)
        {
            mAction = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            mAction(parameter);
        }
    }
}
