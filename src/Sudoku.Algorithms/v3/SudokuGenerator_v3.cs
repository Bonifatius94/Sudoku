using Sudoku.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms.v3
{
    public class SudokuGenerator : SudokuSolver, ISudokuGenerator
    {
        #region Members

        private static readonly Random _random = new Random();

        #endregion Members

        #region Methods

        public SudokuPuzzle GenerateSudoku(SudokuDifficuty difficulty, int length = 9)
        {
            // get randomly filled out sudoku
            SudokuPuzzle solution = SolveSudoku(new SudokuPuzzle());
            SudokuPuzzle sudoku = (SudokuPuzzle)solution.Clone();
            
            // check how many fields need to be removed (depending on desired difficulty)
            int fieldsToRemove = ((length * length) - (int)difficulty);

            while (fieldsToRemove > 0)
            {
                int freeFieldsCount = sudoku.GetFreeFields().Count;

                foreach (var field in sudoku.GetSetFields().Shuffle())
                {
                    if (fieldsToRemove > 0 && isFieldDetermined(sudoku, solution, field.RowIndex, field.ColumnIndex))
                    {
                        field.SetValue(0);
                        fieldsToRemove--;
                    }
                }

                // check if progress was made (if not => try with another sudoku puzzle)
                if ((freeFieldsCount - sudoku.GetFreeFields().Count) == 0)
                {
                    sudoku = GenerateSudoku(difficulty, length);
                    break;
                }
            }

            // check if the generated sudoku is unique. if not create a new one.
            return (HasSudokuUniqueSolution(sudoku)) ? sudoku : GenerateSudoku(difficulty, length);
        }

        #endregion Methods
    }
}
