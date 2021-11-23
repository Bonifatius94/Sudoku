using System;

namespace Sudoku.Data
{
    public static class SudokuFactory
    {
        public static ISudokuPuzzle CreateEmptyPuzzle(int digits = 9)
        {
            return new SudokuPuzzle(digits);
        }

        public static ISudokuPuzzle CreatePuzzle(int[] sudokuDigits)
        {
            return new SudokuPuzzle(sudokuDigits);
        }
    }
}
