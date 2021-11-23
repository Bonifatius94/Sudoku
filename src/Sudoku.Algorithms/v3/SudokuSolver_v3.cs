using Sudoku.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Algorithms.v3
{
    public class SudokuSolver : ISudokuSolver
    {
        #region Methods

        public ISudokuPuzzle SolveSudoku(ISudokuPuzzle sudoku)
        {
            return solveSudokuRecursive(sudoku);
        }

        public bool HasSudokuUniqueSolution(ISudokuPuzzle sudoku)
        {
            // make sure that the sudoku has more than 16 determined fields
            // info: there's an exhaustive proof that sudokus with 16 or less determined fields
            //       don't have unique solutions (there's always ambiguity)
            // see proof: https://www.technologyreview.com/s/426554/mathematicians-solve-minimum-sudoku-problem/
            if (sudoku.GetSetFields().Count() <= 16) { return false; }

            // make sure that the sudoku is solvable
            var solution = SolveSudoku(sudoku);
            if (solution == null) { return false; }

            // go through all empty fields (filled out fields are ignored)
            var temp = sudoku.DeepCopy();
            while (!temp.IsSolved())
            {
                // choose a free field with a minimum amount of remaining possibilities
                int minPossibleValuesCount = temp.GetFreeFields().Select(x => x.GetPossibleValuesCount()).Min();
                var field = temp.GetFreeFields().Where(x => x.GetPossibleValuesCount() == minPossibleValuesCount).ChooseRandom();

                // check if the field is determined; if not, there is a second solution => return false
                if (!isFieldDetermined(temp, solution, field)) { return false; }

                // apply the determined value to the sudoku => less possibilities
                temp.SetValue(field.RowIndex, field.ColumnIndex,
                    solution.GetValue(field.RowIndex, field.ColumnIndex));
            }

            return true;
        }

        protected bool isFieldDetermined(ISudokuPuzzle sudoku, ISudokuPuzzle solution, SudokuField field)
        {
            // // get value from the first solution
            // var field = sudoku.Fields[row, column];
            // int value = solution.Fields[row, column].Value;

            // try to solve the sudoku again, but now without the value from the first solution
            sudoku.ForbidPossibility(field.RowIndex, field.ColumnIndex, field.Value);
            var secondSolution = guessNextField(sudoku, field);

            // if there is no second solution, the sudoku has definitely a unique solution
            return (secondSolution == null || solution.Equals(secondSolution));
        }

        protected ISudokuPuzzle solveSudokuRecursive(ISudokuPuzzle original)
        {
            // make a copy of overloaded sudoku
            var copy = original.DeepCopy();
            var result = copy;
            
            // eliminate possibilities in copy / fill out fields with a single remaining possibility
            copy.EliminatePossibilities();

            // go through all empty fields
            if (copy.GetFreeFields().Count() > 0)
            {
                // choose a free field with a minimum amount of possibilities
                int minPossibleValuesCount = copy.GetFreeFields().Select(x => x.GetPossibleValuesCount()).Min();
                var field = copy.GetFreeFields().Where(x => x.GetPossibleValuesCount() == minPossibleValuesCount).ChooseRandom();

                var possibleValues = field.GetPossibleValues().Shuffle();

                if (possibleValues.Count > 1)
                {
                    // case with multiple remaining possibilities => try each of them with brute force
                    result = guessNextField(copy, field);
                }
                else if (possibleValues.Count == 1)
                {
                    // case with a single remaining possibility => set value
                    field.SetValueIfDetermined();
                }
                else
                {
                    // sudoku has no valid solution => go back to the previous recursion step
                    result = null;
                }
            }

            return result;
        }

        protected ISudokuPuzzle guessNextField(ISudokuPuzzle sudoku, SudokuField field)
        {
            ISudokuPuzzle result = null;

            foreach (int value in field.GetPossibleValues().Shuffle())
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
        
        #endregion Methods
    }
}
