using System;
using System.Linq;

namespace Sudoku.Client.Data
{
    public enum SudokuDifficutyLevel
    {
        Easy    = 45,
        Medium  = 40,
        Hard    = 35,
        Extreme = 30
    }

    public class SudokuGenerator : SudokuBase
    {
        #region Members

        private static readonly Random _random = new Random();
        private int _solutionCount = 0;

        #endregion Members

        #region Methods

        public int[,] GenerateSudoku(SudokuDifficutyLevel difficulty, out int[,] solution)
        {
            _solutionCount = 0;

            int[,] sudoku;
            bool[,,] possibilities = new bool[9, 9, 9];
            
            // initialize all possibilities with true
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    for (int v = 0; v < 9; v++)
                    {
                        possibilities[r, c, v] = true;
                    }
                }
            }

            while (true)
            {
                addRandomValue(ref possibilities);
                if (hasUniqueSolution(possibilities)) { break; }

                writeSudoku(possibilities);
                _solutionCount = 0;
            }

            sudoku = possibilitiesToMatrix(possibilities);
            solution = possibilitiesToMatrix(new SudokuSolver().SolveSudoku(possibilities));
            int columnsToFill = ((int)difficulty) - GetFixFieldsCount(sudoku);

            while (0 < columnsToFill--)
            {
                int row = _random.Next(9);
                int column = _random.Next(9);

                if (sudoku[row, column] == 0) { sudoku[row, column] = solution[row, column]; }
                else { columnsToFill++; }
            }

            return sudoku;
        }

        private void addRandomValue(ref bool[,,] possibilities)
        {
            int row;
            int column;
            bool[,,] temp = null;

            do
            {
                row = _random.Next(9);
                column = _random.Next(9);

                if (getPossibilitiesCount(possibilities, row, column) > 1)
                {
                    int[] possibleValues = null;

                    do
                    {
                        possibleValues = getPossibleValues(possibilities, row, column);
                        int index = _random.Next(possibleValues.Length);
                        int value = possibleValues[index];

                        temp = (bool[,,])possibilities.Clone();
                        setValue(temp, row, column, value);

                        if (hasAnySolution(temp)) { goto Leave; }
                        else { possibilities[row, column, value - 1] = false; }
                    }
                    while (possibleValues.Length > 0);
                }
            }
            while (true);

        Leave:
            possibilities = temp;
        }
        
        private bool hasAnySolution(int[,] matrix)
        {
            var ret = (new SudokuSolver().SolveSudoku(matrix) != null);
            return ret;
        }

        private bool hasAnySolution(bool[,,] possibilities)
        {
            var ret = (new SudokuSolver().SolveSudoku(possibilities) != null);
            return ret;
        }

        private bool hasUniqueSolution(ref bool[,,] possibilities)
        {
            int count = 0;
            var temp = (bool[,,])possibilities.Clone();
            getSolutionsCount(ref temp, ref count);

            if (count == 1) { possibilities = temp; }

            return (count == 1);
        }

        private bool hasUniqueSolution(bool[,,] possibilities)
        {
            int count = 0;
            var temp = (bool[,,])possibilities.Clone();
            getSolutionsCount(ref temp, ref count);
            
            return (count == 1);
        }

        private void getSolutionsCount(ref bool[,,] possibilities, ref int count, int row = 0, int column = 0)
        {
            // info: this method is optimized for use by hasUniqueSolution

            if (isValidSudoku(possibilities))
            {
                if (isComplete(possibilities))
                {
                    return;
                }
                else
                {
                    for (; row < 9; row++)
                    {
                        for (; column < 9; column++)
                        {
                            var possibleValues = getPossibleValues(possibilities, row, column);
                            int length = (possibleValues != null) ? possibleValues.Length : 0;

                            switch (length)
                            {
                                case 0:
                                    // move one layer down
                                    possibilities = null;
                                    return;
                                case 1:
                                    // shrink possibilities to reduce backtracking paths of algorithm
                                    setValue(possibilities, row, column, possibleValues[0]);
                                    break;
                                default:

                                    for (int i = 0; i < possibleValues.Length; i++)
                                    {
                                        // try out remaining possibilities
                                        var solution = (bool[,,])possibilities.Clone();
                                        setValue(solution, row, column, possibleValues[i]);

                                        // move one layer up
                                        int r = column + 1 < 9 ? row : row + 1;
                                        int c = column + 1 < 9 ? column + 1 : 0;
                                        getSolutionsCount(ref solution, ref count, r, c);

                                        if (solution != null && ++count > 1)
                                        {
                                            // second solution found. stop and return count greater than 1.
                                            return;
                                        }
                                    }

                                    // path has no valid solution. move one layer down.
                                    goto case 0;
                            }
                        }

                        column = 0;
                    }
                }


            }
            else
            {
                // move one layer down
                possibilities = null;
            }
        }

        //private bool hasUniqueSolution(int[,] original)
        //{
        //    bool ret = true;
        //    var matrix = (int[,])original.Clone();

        //    for (int i = 0; i < 9; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            if (matrix[i, j] == 0)
        //            {
        //                for (int value = 1; value < 10; value++)
        //                {
        //                    matrix[i, j] = value;

        //                    if (isValidSudoku(matrix))
        //                    {
        //                        if (isComplete(matrix))
        //                        {
        //                            // add solution to collection
        //                            _solutionCount++;
        //                            goto Leave;
        //                        }
        //                        else
        //                        {
        //                            // move one layer up
        //                            hasUniqueSolution(matrix);
                                    
        //                            if (_solutionCount > 1)
        //                            {
        //                                ret = false;
        //                                goto Leave;
        //                            }
                                    
        //                            // reset value
        //                            matrix[i, j] = 0;
        //                        }
        //                    }
        //                }

        //                // move one layer down
        //                goto Leave;
        //            }
        //        }
        //    }

        //Leave:
        //    return ret;
        //}

        #endregion Methods
    }
}
