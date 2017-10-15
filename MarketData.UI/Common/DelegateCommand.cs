using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarketData.UI.Common
{
    public class DelegateCommand : ICommand
    {
        Action<object> _executeAction;
        Predicate<object> _canExecuteAction;

        public DelegateCommand(Action<object> executeAction, Predicate<object> canExecuteAction = null)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            if (null == _canExecuteAction)
            {
                return true;
            }
            return _canExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeAction.Invoke(parameter);
        }
    }
}
