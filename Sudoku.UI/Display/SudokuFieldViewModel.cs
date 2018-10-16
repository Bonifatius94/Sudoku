using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI.Display
{
    public enum SudokuFieldMode { Determined, Notes }

    public class SudokuFieldViewModel : Screen
    {
        #region Members

        private SudokuSetFieldViewModel _determinedView = new SudokuSetFieldViewModel();
        private SudokuNoteFieldViewModel _notesView = new SudokuNoteFieldViewModel();

        private SudokuFieldMode _mode = SudokuFieldMode.Determined;
        public Screen Field { get { return (_mode == SudokuFieldMode.Determined) ? _determinedView as Screen : _notesView; } }

        #endregion Members
    }
}
