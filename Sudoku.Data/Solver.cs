using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Data
{
    public class Solver
    {
        #region Methods

        //public void Run()
        //{
        //    var original = createMatrix();
        //    var solution = SolveSudoku(original);

        //    writeMatrix(original);
        //    Console.WriteLine();

        //    if (solution != null)
        //    {
        //        writeMatrix(solution);
        //    }
        //    else
        //    {
        //        Console.WriteLine("No solution!!!");
        //    }

        //    Console.ReadKey();
        //}
        
        public int[,] SolveSudoku(int[,] original)
        {
            int[,] solution = null;
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
                                    solution = matrix;
                                    goto Leave;
                                }
                                else
                                {
                                    // move one layer up
                                    solution = SolveSudoku(matrix);

                                    if (solution != null)
                                    {
                                        // pass through solution
                                        goto Leave;
                                    }
                                    else
                                    {
                                        // reset value
                                        matrix[i, j] = 0;
                                    }
                                }
                            }
                        }

                        // move one layer down
                        solution = null;
                        goto Leave;
                    }
                }
            }

        Leave:
            return solution;
        }

        private bool isComplete(int[,] matrix)
        {
            bool ret = false;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (matrix[i, j] == 0) { goto Leave; }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        private bool isValidSudoku(int[,] matrix)
        {
            var ret = false;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        int value1 = matrix[i, j];
                        int value2 = matrix[i, k];

                        if (j != k && value1 != 0 && value2 != 0 && value1 == value2) { goto Leave; }
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        int value1 = matrix[j, i];
                        int value2 = matrix[k, i];

                        if (j != k && value1 != 0 && value2 != 0 && value1 == value2) { goto Leave; }
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            for (int m = 0; m < 3; m++)
                            {
                                for (int n = 0; n < 3; n++)
                                {
                                    int x1 = i * 3 + k;
                                    int y1 = j * 3 + l;
                                    int x2 = i * 3 + m;
                                    int y2 = j * 3 + n;
                                    int value1 = matrix[x1, y1];
                                    int value2 = matrix[x2, y2];

                                    if (x1 != x2 && y1 != y2 && value1 != 0 && value2 != 0 && value1 == value2) { goto Leave; }
                                }
                            }
                        }
                    }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        private int[,] createMatrix()
        {
            int[,] matrix = new int[9, 9];

            // + ----- + ----- + ----- +
            // | 2 9 7 |     5 | 1     |
            // |     6 | 1     | 5   7 |
            // |       | 9 4   |       |
            // + ----- + ----- + ----- +
            // |   5   | 2 7   |       |
            // |       | 5   8 |   3 9 |
            // |   6 4 |       |       |
            // + ----- + ----- + ----- +
            // |       |     3 | 2 4   |
            // | 1   5 |       |       |
            // | 4 7   |   9 1 | 3     |
            // + ----- + ----- + ----- +

            //matrix[0, 0] = 2;
            //matrix[0, 1] = 9;
            //matrix[0, 2] = 7;
            //matrix[0, 5] = 5;
            //matrix[0, 6] = 1;

            //matrix[1, 2] = 6;
            //matrix[1, 3] = 1;
            //matrix[1, 6] = 5;
            //matrix[1, 8] = 7;

            //matrix[2, 3] = 9;
            //matrix[2, 4] = 4;

            //matrix[3, 1] = 5;
            //matrix[3, 3] = 2;
            //matrix[3, 4] = 7;

            //matrix[4, 3] = 5;
            //matrix[4, 5] = 8;
            //matrix[4, 7] = 3;
            //matrix[4, 8] = 9;

            //matrix[5, 1] = 6;
            //matrix[5, 2] = 4;

            //matrix[6, 5] = 3;
            //matrix[6, 6] = 2;
            //matrix[6, 7] = 4;

            //matrix[7, 0] = 1;
            //matrix[7, 2] = 5;

            //matrix[8, 0] = 4;
            //matrix[8, 1] = 7;
            //matrix[8, 4] = 9;
            //matrix[8, 5] = 1;
            //matrix[8, 6] = 3;

            // + ----- + ----- + ----- +
            // |     9 | 1     |       |
            // | 4   6 | 8   2 |       |
            // |   8 7 |     9 | 2 1 5 |
            // + ----- + ----- + ----- +
            // |   4   |     1 | 7     |
            // |   6 2 |     4 |       |
            // |   3   |     5 |   6 9 |
            // + ----- + ----- + ----- +
            // | 8     |   3   |   7   |
            // |       |   9   |     4 |
            // |       |       | 9 2 6 |
            // + ----- + ----- + ----- +

            matrix[0, 2] = 9;
            matrix[0, 3] = 1;

            matrix[1, 0] = 4;
            matrix[1, 2] = 6;
            matrix[1, 3] = 8;
            matrix[1, 5] = 2;

            matrix[2, 1] = 8;
            matrix[2, 2] = 7;
            matrix[2, 5] = 9;
            matrix[2, 6] = 2;
            matrix[2, 7] = 1;
            matrix[2, 8] = 5;

            matrix[3, 1] = 4;
            matrix[3, 5] = 1;
            matrix[3, 6] = 7;

            matrix[4, 1] = 6;
            matrix[4, 2] = 2;
            matrix[4, 5] = 4;

            matrix[5, 1] = 3;
            matrix[5, 5] = 5;
            matrix[5, 7] = 6;
            matrix[5, 8] = 9;

            matrix[6, 0] = 8;
            matrix[6, 4] = 3;
            matrix[6, 7] = 7;

            matrix[7, 4] = 9;
            matrix[7, 8] = 4;

            matrix[8, 6] = 9;
            matrix[8, 7] = 2;
            matrix[8, 8] = 6;

            return matrix;
        }

        private void writeMatrix(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("+ ----- + ----- + ----- +");

                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Console.Write("| ");

                        for (int l = 0; l < 3; l++)
                        {
                            int value = matrix[i * 3 + j, k * 3 + l];
                            Console.Write($"{ (value != 0 ? value.ToString() : " ") } ");
                        }
                    }

                    Console.WriteLine("|");
                }
            }

            Console.WriteLine("+ ----- + ----- + ----- +");
        }

        #endregion Methods
    }
}
