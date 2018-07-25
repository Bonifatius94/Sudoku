using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI.Display
{
    public class SudokuBoxViewModel : Screen
    {
        #region Members

        private SudokuSetFieldViewModel[,] _fields = new SudokuSetFieldViewModel[3, 3]
        {
            {
                new SudokuSetFieldViewModel(),
                new SudokuSetFieldViewModel(),
                new SudokuSetFieldViewModel()
            },
            {
                new SudokuSetFieldViewModel(),
                new SudokuSetFieldViewModel(),
                new SudokuSetFieldViewModel()
            },
            {
                new SudokuSetFieldViewModel(),
                new SudokuSetFieldViewModel(),
                new SudokuSetFieldViewModel()
            }
        };
        public SudokuSetFieldViewModel[,] Fields { get { return _fields; } }

        public SudokuSetFieldViewModel Field_0_0 { get { return _fields[0, 0]; } }
        public SudokuSetFieldViewModel Field_0_1 { get { return _fields[0, 1]; } }
        public SudokuSetFieldViewModel Field_0_2 { get { return _fields[0, 2]; } }
        public SudokuSetFieldViewModel Field_1_0 { get { return _fields[1, 0]; } }
        public SudokuSetFieldViewModel Field_1_1 { get { return _fields[1, 1]; } }
        public SudokuSetFieldViewModel Field_1_2 { get { return _fields[1, 2]; } }
        public SudokuSetFieldViewModel Field_2_0 { get { return _fields[2, 0]; } }
        public SudokuSetFieldViewModel Field_2_1 { get { return _fields[2, 1]; } }
        public SudokuSetFieldViewModel Field_2_2 { get { return _fields[2, 2]; } }

        #endregion Members
    }
}
