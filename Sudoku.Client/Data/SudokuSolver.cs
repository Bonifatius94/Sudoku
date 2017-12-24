//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sudoku.Client.Data
//{
//    public class SudokuSolver : SudokuBase
//    {
//        #region Methods

//        public int[,] SolveSudoku(int[,] original)
//        {
//            int[,] solution = null;

//            var matrix = (int[,])original.Clone();
//            var possibilities = matrixToPossibilities(matrix);
//            solveSudoku(ref possibilities);

//            if (possibilities != null) { solution = possibilitiesToMatrix(possibilities); }

//            return solution;
//        }

//        public bool[,,] SolveSudoku(bool[,,] original)
//        {
//            bool[,,] solution = null;

//            var possibilities = (bool[,,])original.Clone();
//            solveSudoku(ref possibilities);

//            if (possibilities != null) { solution = possibilities; }

//            return solution;
//        }

//        private void solveSudoku(ref bool[,,] possibilities, int row = 0, int column = 0)
//        {
//            if (isValidSudoku(possibilities))
//            {
//                if (isComplete(possibilities))
//                {
//                    return;
//                }
//                else
//                {
//                    for (; row < 9; row++)
//                    {
//                        for (; column < 9; column++)
//                        {
//                            var possibleValues = getPossibleValues(possibilities, row, column);
//                            int length = (possibleValues != null) ? possibleValues.Length : 0;

//                            switch (length)
//                            {
//                                case 0:
//                                    // move one layer down
//                                    possibilities = null;
//                                    return;
//                                case 1:
//                                    // shrink possibilities to reduce backtracking paths of algorithm
//                                    setValue(possibilities, row, column, possibleValues[0]);
//                                    break;
//                                default:

//                                    for (int i = 0; i < possibleValues.Length; i++)
//                                    {
//                                        // try out remaining possibilities
//                                        var solution = (bool[,,])possibilities.Clone();
//                                        setValue(solution, row, column, possibleValues[i]);

//                                        // move one layer up
//                                        int r = column + 1 < 9 ? row : row + 1;
//                                        int c = column + 1 < 9 ? column + 1 : 0;
//                                        solveSudoku(ref solution, r, c);

//                                        if (solution != null)
//                                        {
//                                            // pass through solution
//                                            possibilities = solution;
//                                            return;
//                                        }
//                                    }

//                                    // path has no valid solution. move one layer down.
//                                    goto case 0;
//                            }
//                        }

//                        column = 0;
//                    }
//                }
//            }
//            else
//            {
//                // move one layer down
//                possibilities = null;
//            }
//        }
        
//        #endregion Methods
//    }
//}
