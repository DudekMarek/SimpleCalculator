using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstWPFApp.Commands
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _predicate;

        public RelayCommand(Action<object> execute, Predicate<object> predicate)
        {
            _execute = execute;
            _predicate = predicate;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
            
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _predicate == null || _predicate(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
