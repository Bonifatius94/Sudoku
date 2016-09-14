using System;

namespace Sudoku.Client.Data
{
    public abstract class SudokuBase
    {
        #region Validation

        protected bool isComplete(int[,] matrix)
        {
            bool ret = false;

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (matrix[row, column] == 0) { goto Leave; }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        protected bool isComplete(bool[,,] possibilities)
        {
            bool ret = false;

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (getPossibilitiesCount(possibilities, row, column) != 1) { goto Leave; }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        protected bool isValidSudoku(int[,] matrix)
        {
            var ret = hasValidRows(matrix) && hasValidColumns(matrix) && hasValidBoxes(matrix);
            return ret;
        }

        protected bool isValidSudoku(bool[,,] possibilities)
        {
            var ret = hasValidRows(possibilities) && hasValidColumns(possibilities) && hasValidBoxes(possibilities);
            return ret;
        }

        protected bool hasValidRows(int[,] matrix)
        {
            var ret = false;

            for (int row = 0; row < 9; row++)
            {
                for (int column_1 = 0; column_1 < 9; column_1++)
                {
                    for (int column_2 = 0; column_2 < 9; column_2++)
                    {
                        int value1 = matrix[row, column_1];
                        int value2 = matrix[row, column_2];

                        if (column_1 != column_2 && value1 != 0 && value2 != 0 && value1 == value2) { goto Leave; }
                    }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        protected bool hasValidRows(bool[,,] possibilities)
        {
            var ret = false;
            int count = 0;

            for (int value = 0; value < 9; value++)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int column = 0; column < 9; column++)
                    {
                        if ((getPossibilitiesCount(possibilities, row, column) == 1) && (getPossibleValues(possibilities, row, column)[0] == value) && ++count > 1) { goto Leave; }
                    }

                    count = 0;
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        protected bool hasValidColumns(int[,] matrix)
        {
            var ret = false;

            for (int column = 0; column < 9; column++)
            {
                for (int row_1 = 0; row_1 < 9; row_1++)
                {
                    for (int row_2 = 0; row_2 < 9; row_2++)
                    {
                        int value1 = matrix[row_1, column];
                        int value2 = matrix[row_2, column];

                        if (row_1 != row_2 && value1 != 0 && value2 != 0 && value1 == value2) { goto Leave; }
                    }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        protected bool hasValidColumns(bool[,,] possibilities)
        {
            var ret = false;
            int count = 0;

            for (int value = 0; value < 9; value++)
            {
                for (int column = 0; column < 9; column++)
                {
                    for (int row = 0; row < 9; row++)
                    {
                        if ((getPossibilitiesCount(possibilities, row, column) == 1) && (getPossibleValues(possibilities, row, column)[0] == value) && ++count > 1) { goto Leave; }
                    }

                    count = 0;
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        protected bool hasValidBoxes(int[,] matrix)
        {
            var ret = false;

            for (int box_row_index = 0; box_row_index < 3; box_row_index++)
            {
                for (int box_column_index = 0; box_column_index < 3; box_column_index++)
                {
                    for (int box_row_1 = 0; box_row_1 < 3; box_row_1++)
                    {
                        for (int box_column_1 = 0; box_column_1 < 3; box_column_1++)
                        {
                            for (int box_row_2 = 0; box_row_2 < 3; box_row_2++)
                            {
                                for (int box_column_2 = 0; box_column_2 < 3; box_column_2++)
                                {
                                    int row_1    = box_row_index * 3 + box_row_1;
                                    int row_2    = box_row_index * 3 + box_row_2;
                                    int column_1 = box_column_index * 3 + box_column_1;
                                    int column_2 = box_column_index * 3 + box_column_2;

                                    int value1 = matrix[row_1, column_1];
                                    int value2 = matrix[row_2, column_2];

                                    if (row_1 != row_2 && column_1 != column_2 && value1 != 0 && value2 != 0 && value1 == value2) { goto Leave; }
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

        protected bool hasValidBoxes(bool[,,] possibilities)
        {
            var ret = false;
            int count = 0;
            
            for (int value = 0; value < 9; value++)
            {
                for (int box_index_row = 0; box_index_row < 3; box_index_row++)
                {
                    for (int box_index_column = 0; box_index_column < 3; box_index_column++)
                    {
                        for (int row = box_index_row * 3; row < box_index_row * 3 + 3; row++)
                        {
                            for (int column = box_index_column * 3; column < box_index_column * 3 + 3; column++)
                            {
                                if ((getPossibilitiesCount(possibilities, row, column) == 1) && (getPossibleValues(possibilities, row, column)[0] == value) && ++count > 1) { goto Leave; }
                            }
                        }

                        count = 0;
                    }
                }
            }

            ret = true;

        Leave:
            return ret;
        }

        #endregion Validation

        #region Count

        public int GetFixFieldsCount(int[,] matrix)
        {
            int ret = 0;

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (matrix[row, column] != 0) { ret++; }
                }
            }

            return ret;
        }

        public int GetFixFieldsCount(bool[,,] possibilities)
        {
            int ret = 0;

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (getPossibilitiesCount(possibilities, row, column) == 1) { ret++; }
                }
            }

            return ret;
        }

        #endregion Count

        #region Conversion

        protected int[,] possibilitiesToMatrix(bool[,,] possibilities)
        {
            var matrix = new int[9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    int[] possibleValues = getPossibleValues(possibilities, row, column);

                    if (possibleValues.Length == 1)
                    {
                        matrix[row, column] = possibleValues[0];
                    }
                }
            }

            return matrix;
        }

        protected bool[,,] matrixToPossibilities(int[,] matrix)
        {
            var possibilities = new bool[9, 9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    var possiblitiesOfField = getPossibilities(matrix, row, column);

                    for (int i = 0; i < 9; i++)
                    {
                        possibilities[row, column, i] = possiblitiesOfField[i];
                    }
                }
            }

            return possibilities;
        }

        #endregion Conversion

        #region Possibilities

        protected void setValue(bool[,,] possibilities, int row, int column, int value)
        {
            if (possibilities[row, column, value - 1])
            {
                for (int c = 0; c < 9; c++)
                {
                    if (c != column) { possibilities[row, c, value - 1] = false; }
                }

                for (int r = 0; r < 9; r++)
                {
                    if (r != row) { possibilities[r, column, value - 1] = false; }
                }

                for (int r = ((row / 3) * 3); r < ((row / 3) * 3) + 3; r++)
                {
                    for (int c = ((column / 3) * 3); c < ((column / 3) * 3) + 3; c++)
                    {
                        if (!(r == row && c == column)) { possibilities[r, c, value - 1] = false; }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Value is not possible any more!");
            }
        }
        
        protected bool[] getPossibilities(int[,] matrix, int row, int column)
        {
            bool[] possibilities = new bool[9];

            for (int val = 0; val < 9; val++)
            {
                bool possible = false;

                for (int c = 0; c < 9; c++)
                {
                    if (matrix[row, c] == val + 1) { goto Next; }
                }

                for (int r = 0; r < 9; r++)
                {
                    if (matrix[r, column] == val + 1) { goto Next; }
                }

                for (int i = ((row / 3) * 3); i < ((row / 3) * 3) + 3; i++)
                {
                    for (int j = ((column / 3) * 3); j < ((column / 3) * 3) + 3; j++)
                    {
                        if (matrix[i, j] == val + 1) { goto Next; }
                    }
                }

                possible = true;

            Next:
                possibilities[val] = possible;
            }

            return possibilities;
        }
        
        protected int[] getPossibleValues(bool[] possibilities)
        {
            int i = 0;
            int[] possibleValues = new int[getPossibilitiesCount(possibilities)];

            for (int val = 1; val < 10; val++)
            {
                if (possibilities[val - 1]) { possibleValues[i++] = val; }
            }

            return possibleValues;
        }

        protected int[] getPossibleValues(bool[,,] possibilities, int row, int column)
        {
            // TODO: fix overflow exception

            int i = 0;
            int[] possibleValues = new int[getPossibilitiesCount(possibilities, row, column)];

            for (int val = 1; val < 10; val++)
            {
                if (possibilities[row, column, val - 1]) { possibleValues[i++] = val; }
            }

            return possibleValues;
        }

        protected int getPossibilitiesCount(bool[,,] possibilities, int row, int column)
        {
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                if (possibilities[row, column, i]) { count++; }
            }

            return count;
        }

        protected int getPossibilitiesCount(bool[] possibilities)
        {
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                if (possibilities[i]) { count++; }
            }

            return count;
        }

        #endregion Possibilities

        #region Trace

        protected void writeMatrix(int[,] matrix)
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

        #endregion Trace
    }
}
