using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sudoku.Client.Helper
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(EventHandler onExecuteHandler)
        {
            _onExecuteHandler = onExecuteHandler;
        }
        
        #region CanExecute

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion CanExecute

        #region Execute

        private EventHandler _onExecuteHandler;

        public void Execute(object parameter)
        {
            _onExecuteHandler?.Invoke(null, null);
        }
        
        #endregion Execute
    }
}
