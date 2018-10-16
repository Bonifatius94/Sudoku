using Caliburn.Micro;
using Sudoku.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI.Display
{
    public class SudokuNoteFieldItemViewModel : PropertyChangedBase
    {
        #region Members

        public int Value { get; set; }
        public bool IsSet { get; set; }

        #endregion Members

        #region Methods

        public void NotifyAll()
        {
            NotifyOfPropertyChange(() => Value);
            NotifyOfPropertyChange(() => IsSet);
        }

        #endregion Methods
    }
}
