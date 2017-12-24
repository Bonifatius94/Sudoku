using MT.Tools.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public class SudokuSolver
    {
        #region Methods

        public Sudoku SolveSudoku(Sudoku sudoku)
        {
            return solveSudokuRecursive(ref sudoku);
        }

        private Sudoku solveSudokuRecursive(ref Sudoku original, int row = 0, int column = 0)
        {
            Sudoku result = null;
            Field field;
            
            original.EliminatePossibilities();

            while ((field = getNextFreeField(ref original, ref row, ref column)) != null)
            {
                var possibleValues = field.GetPossibleValues();

                if (possibleValues.Length > 1)
                {
                    result = tryNextLevel(ref original, row, column, possibleValues);
                    goto Leave;
                }
                else if (possibleValues.Length == 1)
                {
                    field.SetValueIfDetermined();
                }
                else
                {
                    result = null;
                    goto Leave;
                }
            }

            result = original.IsSolved() ? original : result;

        Leave:
            return result;
        }

        private Sudoku tryNextLevel(ref Sudoku sudoku, int row, int column, int[] possibleValues)
        {
            Sudoku result = null;

            // prepare row / column index for next recursion level
            int nextRow = (column == 8) ? row + 1 : row;
            int nextColumn = (column + 1) % 8;

            foreach (int value in possibleValues)
            {
                // make copy of sudoku and try out possibility
                var copy = (Sudoku)sudoku.Clone();
                copy.Fields[row, column].SetValue(value);

                if (copy.IsValid())
                {
                    // go to next recursion level
                    result = solveSudokuRecursive(ref copy, nextRow, nextColumn);

                    // pass correct solution to lower recursion levels
                    if (result != null) { break; }
                }
            }

            return result;
        }

        private Field getNextFreeField(ref Sudoku sudoku, ref int row, ref int column)
        {
            for (; row < 9; row++)
            {
                for (; column < 9; column++)
                {
                    var field = sudoku.Fields[row, column];

                    if (field.Value == 0)
                    {
                        return field;
                    }
                }

                column = 0;
            }

            return null;
        }
        
        #endregion Methods
    }
}
