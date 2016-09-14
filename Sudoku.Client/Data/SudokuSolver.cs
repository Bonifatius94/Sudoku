using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Client.Data
{
    public class SudokuSolver : SudokuBase
    {
        #region Methods

        public int[,] SolveSudoku(int[,] original)
        {
            int[,] solution = null;

            var matrix = (int[,])original.Clone();
            var possibilities = matrixToPossibilities(matrix);
            solveSudoku(possibilities);
            solution = possibilitiesToMatrix(possibilities);

            return solution;
        }

        private void solveSudoku(bool[,,] possibilities)
        {
            if (isValidSudoku(possibilities))
            {
                if (isComplete(possibilities))
                {
                    return;
                }
                else
                {
                    for (int row = 0; row < 9; row++)
                    {
                        for (int column = 0; column < 9; column++)
                        {
                            // TODO: 

                            var possibleValues = getPossibleValues(possibilities, row, column);

                            switch (possibleValues.Length)
                            {
                                case 0:
                                    // move one layer down
                                    possibilities = null;
                                    return;
                                case 1:
                                    // shrink possibilities
                                    setValue(possibilities, row, column, possibleValues[0]);
                                    break;
                                default:

                                    for (int i = 0; i < possibleValues.Length; i++)
                                    {
                                        // move one layer up
                                        var solution = (bool[,,])possibilities.Clone();
                                        solveSudoku(solution);

                                        if (solution != null)
                                        {
                                            // pass through solution
                                            possibilities = solution;
                                            return;
                                        }
                                        else
                                        {
                                            // move one layer down
                                            return;
                                        }
                                    }

                                    goto case 0;
                            }
                        }
                    }
                }
            }
            else
            {
                // move one layer down
                possibilities = null;
            }
        }
        
        #endregion Methods
    }
}
