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

        public int[,] GenerateSudoku(SudokuDifficutyLevel difficulty)
        {
            int[,] matrix = new int[9, 9];
            _solutionCount = 0;

            while (true)
            {
                addRandomValue(matrix);
                
                if (hasUniqueSolution(matrix))
                {
                    break;
                }

                //writeMatrix(matrix);
                _solutionCount = 0;
            }

            matrix = new SudokuSolver().SolveSudoku(matrix);
            int columnCount = 81 - ((int)difficulty);

            while (0 < columnCount--)
            {
                int row = _random.Next(9);
                int column = _random.Next(9);

                if (matrix[row, column] > 0)
                {
                    matrix[row, column] = 0;
                }
                else
                {
                    columnCount++;
                }
            }

            return matrix;
        }

        private void addRandomValue(int[,] matrix)
        {
            int row;
            int column;
            
            do
            {
                row = _random.Next(9);
                column = _random.Next(9);

                if (matrix[row, column] == 0) { break; }
            }
            while (true);

            var possibilities = getPossibilities(matrix, row, column);
            int[] possibleValues = null;

            do
            {
                possibleValues = getPossibleValues(possibilities);
                int value = possibleValues[_random.Next(possibleValues.Length)];
                matrix[row, column] = value;
                
                if (isValidSudoku(matrix)) { break; }
                else { possibilities[value-1] = false; }
            }
            while (possibleValues.Length > 0);
        }
        
        private void applyValue(bool[,,] possibilities, int row, int column, int value)
        {
            for (int c = 0; c < 9; c++)
            {
                possibilities[row, c, value - 1] = false;
            }
            
            for (int r = 0; r < 9; r++)
            {
                possibilities[r, column, value - 1] = false;
            }
            
            for (int i = ((row / 3) * 3); i < ((row / 3) * 3) + 3; i++)
            {
                for (int j = ((column / 3) * 3); j < ((column / 3) * 3) + 3; j++)
                {
                    possibilities[i, j, value - 1] = false;
                }
            }
        }

        private bool hasAnySolution(int[,] matrix)
        {
            var ret = (new SudokuSolver().SolveSudoku(matrix) != null);
            return ret;
        }

        private bool hasUniqueSolution(int[,] original)
        {
            bool ret = true;
            var matrix = (int[,])original.Clone();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        for (int value = 1; value < 10; value++)
                        {
                            matrix[i, j] = value;

                            if (isValidSudoku(matrix))
                            {
                                if (isComplete(matrix))
                                {
                                    // add solution to collection
                                    _solutionCount++;
                                    goto Leave;
                                }
                                else
                                {
                                    // move one layer up
                                    hasUniqueSolution(matrix);
                                    
                                    if (_solutionCount > 1)
                                    {
                                        ret = false;
                                        goto Leave;
                                    }
                                    
                                    // reset value
                                    matrix[i, j] = 0;
                                }
                            }
                        }

                        // move one layer down
                        goto Leave;
                    }
                }
            }

        Leave:
            return ret;
        }

        #endregion Methods
    }
}
