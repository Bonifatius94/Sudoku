using MT.Tools.Tracing;
using Sudoku.Data;
using System;
using System.Linq;

namespace Sudoku.Algorithms.v1
{
    public class SudokuGenerator : ISudokuGenerator
    {
        #region Members

        private static readonly Random _random = new Random();

        #endregion Members

        #region Methods
        
        public SudokuPuzzle GenerateSudoku(SudokuDifficuty difficulty, int length = 9)
        {
            // get filled out random sudoku
            var solver = new SudokuSolver();
            var sudoku = solver.SolveSudoku(new SudokuPuzzle());

            // reset several values (depending on desired difficulty)
            for (int i = 0; i < (length * length) - (int)difficulty; i++)
            {
                var field = sudoku.GetSetFields().ChooseRandom();
                field.SetValue(0);
            }
            
            // check if sudoku has a unique solution (if not, generate a new one and repeat procedure)
            sudoku = (!solver.HasSudokuUniqueSolution(sudoku)) ? GenerateSudoku(difficulty) : sudoku;
            
            return sudoku;
        }
        
        #endregion Methods
    }
}
