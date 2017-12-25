using MT.Tools.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public enum SudokuDifficuty
    {
        Easy = 45,
        Medium = 38,
        Hard = 32,
        Extreme = 25
    }

    public class SudokuGenerator
    {
        #region Members

        private static readonly Random _random = new Random();

        #endregion Members

        #region Methods

        public Sudoku GenerateSudoku(SudokuDifficuty difficulty)
        {
            var solver = new SudokuSolver();
            var sudoku = getRandomSudoku();
            Sudoku temp;

            //do
            //{
                temp = (Sudoku)sudoku.Clone();

                for (int i = 0; i < 81 - (int)difficulty; i++)
                {
                    var field = getRandomFilledField(temp);
                    field.SetValue(0);
                }

            //} while (solver.HasSudokuUniqueSolution(temp));
            
            return temp;
        }

        private Sudoku getRandomSudoku()
        {
            var sudoku = new Sudoku();
            var solver = new SudokuSolver();

            for (int i = 0; i < 15; i++)
            {
                Sudoku temp;
                var field = getRandomFreeField(sudoku);
                var possibleValues = field.GetPossibleValues();
                int value;

                do
                {
                    value = getRandomPossibleValue(possibleValues);
                    temp = (Sudoku)sudoku.Clone();
                    temp.Fields[field.RowIndex, field.ColumnIndex].SetValue(value);
                    possibleValues = possibleValues.Except(new int[] { value }).ToArray();

                } while (solver.SolveSudoku(temp) == null);

                // set value in sudoku
                field.SetValue(value);
            }
            
            return solver.SolveSudoku(sudoku);
        }

        //private Sudoku getRandomSudoku()
        //{
        //    var sudoku = new Sudoku();
        //    var solver = new SudokuSolver();

        //    while (!solver.HasSudokuUniqueSolution(sudoku))
        //    {
        //        Sudoku temp;
        //        var field = getRandomFreeField(sudoku);
        //        var possibleValues = field.GetPossibleValues();
        //        int value;

        //        do
        //        {
        //            value = getRandomPossibleValue(possibleValues);
        //            temp = (Sudoku)sudoku.Clone();
        //            temp.Fields[field.RowIndex, field.ColumnIndex].SetValue(value);
        //            possibleValues = possibleValues.Except(new int[] { value }).ToArray();

        //        } while (solver.SolveSudoku(temp) == null);

        //        // set value in sudoku
        //        field.SetValue(value);
        //    }

        //    return sudoku;
        //}
        
        private int getRandomPossibleValue(int[] possibleValues)
        {
            int index = _random.Next(possibleValues.Length);
            return possibleValues[index];
        }

        private Field getRandomFilledField(Sudoku sudoku)
        {
            var fields = sudoku.GetFields1D().Where(x => x.Value > 0).ToList();
            int index = _random.Next(fields.Count);
            return fields[index];
        }

        private Field getRandomFreeField(Sudoku sudoku)
        {
            var freeFields = sudoku.GetFields1D().Where(x => x.Value == 0).ToList();
            int index = _random.Next(freeFields.Count);
            return freeFields[index];
        }

        #endregion Methods
    }
}
