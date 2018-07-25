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

        public UIField Model { get; set; }

        // TODO: add notes
        //public string Value { get { return Model } }

        #endregion Members
    }
}
