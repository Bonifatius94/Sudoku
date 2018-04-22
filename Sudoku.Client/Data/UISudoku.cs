using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI.Data
{
    public class UISudoku : Solver.Sudoku
    {
        #region Constructor

        public UISudoku(score score) : base(score.Convert()) { }
        public UISudoku(int length = 9) : base(length) { }
        public UISudoku(Solver.Sudoku original) { initValues(original); }

        #endregion Constructor

        #region Members

        public bool[,] IsFix { get; set; } = new bool[9, 9];

        #endregion Members

        #region Methods

        private void initValues(Solver.Sudoku original)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    Fields[row, column].SetValue(original.Fields[row, column].Value);
                }
            }
        }

        #endregion Methods
    }

    public class UIField : Solver.Field
    {
        #region Constructor

        public UIField(int value = 0, int possibilitiesCount = 9) : base(value, possibilitiesCount) { }
        public UIField(bool[] possibilities) : base(possibilities) { }

        #endregion Constructor

        #region Members

        public bool IsFix { get; set; }

        #endregion Members
    }
}
