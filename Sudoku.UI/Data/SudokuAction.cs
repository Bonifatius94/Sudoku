using Sudoku.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI.Data
{
    public enum SudokuActionType
    {
        WriteNumber,
        WriteOption,
        DeleteNumber,
        DeleteOption
    }

    public class SudokuAction
    {
        #region Members

        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
        public SudokuActionType Type { get; set; }
        
        #endregion Members
    }

    public class SudokuBuilder
    {
        #region Methods

        public UISudoku BuildSudoku(List<SudokuAction> actions)
        {
            UISudoku sudoku = new UISudoku();

            foreach (SudokuAction action in actions)
            {
                switch (action.Type)
                {
                    case SudokuActionType.WriteNumber:
                        
                        // write the number by setting all other possiblities to false
                        for (int i = 0; i < 9; i++)
                        {
                            sudoku.Fields[action.Row, action.Column].Possibilities[i] = action.Value == i + 1;
                        }

                        break;
                    case SudokuActionType.WriteOption:

                        // apply the given option accordingly
                        if (sudoku.Fields[action.Row, action.Column].GetPossibleValuesCount() == 1)
                        {
                            sudoku.Fields[action.Row, action.Column].Possibilities[action.Value - 1] = true;
                        }

                        break;
                    case SudokuActionType.DeleteNumber:



                        break;
                }

                sudoku.Fields[action.Row, action.Column].SetValue(action.Value);
            }

            return sudoku;
        }

        #endregion Methods
    }
}
