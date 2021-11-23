// using MT.Tools.Tracing;
using Sudoku.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Algorithms.v1
{
    public class SudokuSolver
    {
        #region Methods

        public ISudokuPuzzle SolveSudoku(ISudokuPuzzle sudoku)
        {
            return solveSudokuRecursive(sudoku);
        }

        public bool HasSudokuUniqueSolution(ISudokuPuzzle sudoku)
        {
            // solve sudoku (gets one solution out of many solutions)
            var solution = SolveSudoku(sudoku);
            bool ret = false;

            if (solution != null)
            {
                SudokuField originalField;
                var temp = sudoku.DeepCopy();

                // go through all empty fields (filled out fields are ignored)
                while ((originalField = temp.GetFreeFields().ChooseRandomOrDefault()) != null)
                {
                    int row = originalField.RowIndex;
                    int column = originalField.ColumnIndex;

                    // get value of field from solution
                    int solutionValue = solution.GetValue(row, column);

                    // try to solve the sudoku again, but without the value from the solution
                    var possibleValues = originalField.GetPossibleValues()
                        .Except(new int[] { solutionValue }).ToArray();
                    var secondSolution = tryNextLevel(temp, null); // tryNextLevel(temp, row, column, possibleValues);

                    // if there is a second solution, the sudoku has definitely not a unique solution => leave with false
                    if (secondSolution != null && !solution.Equals(secondSolution)) { return ret; }
                }
            }

            // all empty fields have a unique solution
            // => there is no second solution => leave with true
            return true;
        }
        
        private ISudokuPuzzle solveSudokuRecursive(ISudokuPuzzle original)
        {
            ISudokuPuzzle result = null;
            SudokuField field;
            
            // make a copy of overloaded sudoku
            var copy = original.DeepCopy();

            // eliminate possibilities in copy / fill out fields with a single remaining possibility
            copy.EliminatePossibilities();

            // go through all empty fields
            while ((field = copy.GetFreeFields().ChooseRandomOrDefault()) != null)
            {
                int possibleValuesCount = field.GetPossibleValuesCount();
                if (possibleValuesCount > 1)
                {
                    // case with multiple remaining possibilities => try each of them with brute force
                    return tryNextLevel(copy, field);
                }
                else if (possibleValuesCount == 1)
                {
                    // case with a single remaining possibility => set value
                    field.SetValueIfDetermined();
                }
                else { return null; }
            }

            result = copy.IsSolved() ? copy : result;
            return result;
        }

        private ISudokuPuzzle tryNextLevel(ISudokuPuzzle sudoku, SudokuField field)
        {
            ISudokuPuzzle result = null;

            foreach (int value in field.GetPossibleValues())
            {
                // make copy of sudoku and try out possibility
                var copy = sudoku.DeepCopy();
                copy.SetValue(field.RowIndex, field.ColumnIndex, value);

                if (copy.IsValid())
                {
                    // go to next recursion level
                    result = solveSudokuRecursive(copy);

                    // pass correct solution to lower recursion levels
                    if (result != null) { break; }
                }
            }
            
            return result;
        }

        // private SudokuField getNextFreeField(ISudokuPuzzle sudoku, ref int row, ref int column)
        // {
        //     // TODO: refactor this code. it is very complicated.

        //     SudokuField field;
            
        //     for (; row < sudoku.Length; row++)
        //     {
        //         for (; column < sudoku.Length; column++)
        //         {
        //             field = sudoku.Fields[row, column];
        //             if (field.Value == 0) { return field; }
        //         }

        //         column = 0;
        //     }

        //     field = null;

        // Leave:
        //     return field;
        // }

        // private void incrementFieldIndices(ref int row, ref int column, int length)
        // {
        //     row = (column == length - 1) ? row + 1 : row;
        //     column = (column + 1) % length;
        // }
        
        #endregion Methods
    }
}
