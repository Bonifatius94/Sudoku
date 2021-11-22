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

        public SudokuPuzzle GenerateSudoku(SudokuDifficuty difficulty, int length = 9)
        {
            // get randomly filled out sudoku
            SudokuPuzzle source = SolveSudoku(new SudokuPuzzle());
            SudokuPuzzle result;

            int i;
            int fieldsToRemove = ((length * length) - (int)difficulty);

            //DateTime genStart = DateTime.Now;
            //Console.WriteLine($"====================================================");

            do
            {
                result = (SudokuPuzzle)source.Clone();
                result.GetFields1D().ChooseRandom().SetValue(0);
                
                for (i = 0; i < fieldsToRemove; i++)
                {
                    //DateTime start = DateTime.Now;

                    if (!removeField(result, source)) { break; }

                    //DateTime end = DateTime.Now;
                    //Console.WriteLine($"iteration { i } took { (end - start).TotalMilliseconds } ms");
                }

                //if (i < fieldsToRemove) { Console.WriteLine($"generator failed"); }
                //else { Console.WriteLine($"generator successful (total time elapsed: { (DateTime.Now - genStart).TotalMilliseconds } ms)"); }
            }
            while (i < fieldsToRemove);

            //Console.WriteLine($"====================================================");

            return result;
        }

        private bool removeField(SudokuPuzzle sudoku, SudokuPuzzle solution)
        {
            foreach (SudokuField field in sudoku.GetSetFields().Shuffle())
            {
                // clone sudoku and set the field value to 0
                SudokuPuzzle temp = (SudokuPuzzle)sudoku.Clone();
                SudokuField tempField = temp.Fields[field.RowIndex, field.ColumnIndex];
                tempField.SetValue(0);

                if (HasSudokuUniqueSolution(temp))
                {
                    field.SetValue(0);
                    return true;
                }
            }
            
            return false;
        }

        #endregion Methods
    }
}
