using Sudoku.Algorithms.v1;
using Sudoku.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms.v2
{
    public class SudokuGenerator : v1.SudokuSolver, ISudokuGenerator
    {
        #region Members

        private static readonly Random _random = new Random();

        #endregion Members

        #region Methods

        public ISudokuPuzzle GenerateSudoku(SudokuDifficuty difficulty, int length = 9)
        {
            // get randomly filled out sudoku
            var source = SolveSudoku(SudokuFactory.CreateEmptyPuzzle(length));
            ISudokuPuzzle result;

            int i;
            int fieldsToRemove = ((length * length) - (int)difficulty);

            do
            {
                result = source.DeepCopy();
                result.GetSetFields().ChooseRandom().SetValue(0);
                
                for (i = 0; i < fieldsToRemove; i++)
                {
                    if (!removeField(result, source)) { break; }
                }
            }
            while (i < fieldsToRemove);

            return result;
        }

        private bool removeField(ISudokuPuzzle sudoku, ISudokuPuzzle solution)
        {
            foreach (SudokuField field in sudoku.GetSetFields().Shuffle())
            {
                // clone sudoku and set the field value to 0
                var temp = sudoku.DeepCopy();
                temp.SetValue(field.RowIndex, field.ColumnIndex, 0);

                if (HasSudokuUniqueSolution(temp))
                {
                    temp.SetValue(field.RowIndex, field.ColumnIndex, 0);
                    return true;
                }
            }
            
            return false;
        }

        #endregion Methods
    }
}
