using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms
{
    public class SudokuGenerator_v2 : SudokuSolver_v2, ISudokuGenerator
    {
        #region Members

        private static readonly Random _random = new Random();

        #endregion Members

        #region Methods

        public Sudoku GenerateSudoku(SudokuDifficuty difficulty, int length = 9)
        {
            // get randomly filled out sudoku
            Sudoku source = SolveSudoku(new Sudoku());
            Sudoku result;

            int i;
            int fieldsToRemove = ((length * length) - (int)difficulty);

            DateTime genStart = DateTime.Now;
            Console.WriteLine($"====================================================");

            do
            {
                result = (Sudoku)source.Clone();
                result.GetFields1D().ChooseRandom().SetValue(0);
                
                for (i = 0; i < fieldsToRemove; i++)
                {
                    DateTime start = DateTime.Now;

                    if (!removeField(result, source)) { break; }

                    DateTime end = DateTime.Now;
                    Console.WriteLine($"iteration { i } took { (end - start).TotalMilliseconds } ms");
                }

                if (i < fieldsToRemove) { Console.WriteLine($"generator failed"); }
                else { Console.WriteLine($"generator successful (total time elapsed: { (DateTime.Now - genStart).TotalMilliseconds } ms)"); }
            }
            while (i < fieldsToRemove);

            Console.WriteLine($"====================================================");

            return result;
        }

        private bool removeField(Sudoku sudoku, Sudoku solution)
        {
            foreach (Field field in sudoku.GetSetFields().Shuffle())
            {
                // clone sudoku and set the field value to 0
                Sudoku temp = (Sudoku)sudoku.Clone();
                Field tempField = temp.Fields[field.RowIndex, field.ColumnIndex];
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
