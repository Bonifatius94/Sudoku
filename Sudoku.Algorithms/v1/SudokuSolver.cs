using MT.Tools.Tracing;
using Sudoku.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Algorithms.v1
{
    public class SudokuSolver
    {
        #region Methods

        public SudokuPuzzle SolveSudoku(SudokuPuzzle sudoku)
        {
            return solveSudokuRecursive(sudoku);
        }
        
        public bool HasSudokuUniqueSolution(SudokuPuzzle sudoku)
        {
            // solve sudoku (gets one solution out of many solutions)
            var solution = SolveSudoku(sudoku);
            bool ret = false;

            if (solution != null)
            {
                int row = 0;
                int column = 0;
                SudokuField originalField;
                var temp = (SudokuPuzzle)sudoku.Clone();

                // go through all empty fields (filled out fields are ignored)
                while ((originalField = getNextFreeField(temp, ref row, ref column)) != null)
                {
                    // get value of field from solution
                    int solutionValue = solution.Fields[row, column].Value;

                    // try to solve the sudoku again, but without the value from the solution
                    var possibleValues = originalField.GetPossibleValues().Except(new int[] { solutionValue }).ToArray();
                    var secondSolution = tryNextLevel(temp, row, column, possibleValues);

                    // if there is a second solution, the sudoku has definitely not a unique solution => leave with false
                    if (secondSolution != null && !solution.Equals(secondSolution)) { goto Leave; }

                    // increment row / column index => this avoids checking a field twice
                    incrementFieldIndices(ref row, ref column, sudoku.Length);
                }
            }

            // all empty fields have a unique solution => there is no second solution => leave with true
            ret = true;

        Leave:
            return ret;
        }
        
        private SudokuPuzzle solveSudokuRecursive(SudokuPuzzle original, int row = 0, int column = 0)
        {
            SudokuPuzzle result = null;
            SudokuField field;
            
            // make a copy of overloaded sudoku
            var copy = (SudokuPuzzle)original.Clone();

            // eliminate possibilities in copy / fill out fields with a single remaining possibility
            copy.EliminatePossibilities();

            // go through all empty fields
            while ((field = getNextFreeField(copy, ref row, ref column)) != null)
            {
                var possibleValues = field.GetPossibleValues().Shuffle();
                
                if (possibleValues.Count > 1)
                {
                    // case with multiple remaining possibilities => try each of them with brute force
                    result = tryNextLevel(copy, row, column, possibleValues);
                    goto Leave;
                }
                else if (possibleValues.Count == 1)
                {
                    // case with a single remaining possibility => set value
                    field.SetValueIfDetermined();
                }
                else
                {
                    result = null;
                    goto Leave;
                }
            }

            result = copy.IsSolved() ? copy : result;

        Leave:
            return result;
        }

        private SudokuPuzzle tryNextLevel(SudokuPuzzle sudoku, int row, int column, IEnumerable<int> possibleValues)
        {
            SudokuPuzzle result = null;

            // prepare row / column index for next recursion level
            int nextRow = row;
            int nextColumn = column;
            incrementFieldIndices(ref nextRow, ref nextColumn, sudoku.Length);

            foreach (int value in possibleValues)
            {
                // make copy of sudoku and try out possibility
                var copy = (SudokuPuzzle)sudoku.Clone();
                copy.Fields[row, column].SetValue(value);

                if (copy.IsValid())
                {
                    // go to next recursion level
                    result = solveSudokuRecursive(copy, nextRow, nextColumn);

                    // pass correct solution to lower recursion levels
                    if (result != null) { break; }
                }
            }
            
            return result;
        }

        private SudokuField getNextFreeField(SudokuPuzzle sudoku, ref int row, ref int column)
        {
            // TODO: refactor this code. it is very complicated.

            SudokuField field;
            
            for (; row < sudoku.Length; row++)
            {
                for (; column < sudoku.Length; column++)
                {
                    field = sudoku.Fields[row, column];
                    if (field.Value == 0) { goto Leave; }
                }

                column = 0;
            }

            field = null;

        Leave:
            return field;
        }

        private void incrementFieldIndices(ref int row, ref int column, int length)
        {
            row = (column == length - 1) ? row + 1 : row;
            column = (column + 1) % length;
        }
        
        #endregion Methods
    }
}
