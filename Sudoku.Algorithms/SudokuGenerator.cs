using MT.Tools.Tracing;
using System;
using System.Linq;

namespace Sudoku.Algorithms
{
    //public enum SudokuDifficuty
    //{
    //    Easy = 45,
    //    Medium = 35,
    //    Hard = 28,
    //    Extreme = 20
    //}

    public enum SudokuDifficuty
    {
        Easy = 45,
        Medium = 38,
        Hard = 32,
        Extreme = 25
    }

    public class SudokuGenerator : ISudokuGenerator
    {
        #region Members

        private static readonly Random _random = new Random();

        #endregion Members

        #region Methods
        
        public Sudoku GenerateSudoku(SudokuDifficuty difficulty, int length = 9)
        {
            // get filled out random sudoku
            var solver = new SudokuSolver();
            var sudoku = solver.SolveSudoku(new Sudoku());

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
