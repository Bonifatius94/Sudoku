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
            var sudoku = getRandomSudoku();
            TraceOut.WriteInformation("\r\n" + sudoku.ToString());

            //int solvedFields = sudoku.GetSolvedFieldsCount();
            //var solution = solver.SolveSudoku(sudoku);
            
            //for (int i = solvedFields; i < (int)difficulty; i++)
            //{
            //    var field = getRandomFreeField(sudoku);
            //    field.SetValue(solution.Fields[field.RowIndex, field.ColumnIndex].Value);
            //}

            return sudoku;
        }

        private Sudoku getRandomSudoku()
        {
            var sudoku = new Sudoku();
            var solver = new SudokuSolver();

            while (!solver.HasSudokuUniqueSolution(sudoku))
            {
                Sudoku temp;
                var field = getRandomFreeField(sudoku);
                var possibleValues = field.GetPossibleValues();

                do
                {
                    int value = getRandomPossibleValue(possibleValues);
                    temp = (Sudoku)sudoku.Clone();
                    temp.Fields[field.RowIndex, field.ColumnIndex].SetValue(value);
                    possibleValues = possibleValues.Except(new int[] { value }).ToArray();

                } while (solver.SolveSudoku(temp) == null);

                sudoku = temp;
                TraceOut.WriteInformation("\r\n" + sudoku.ToString());
            }

            return sudoku;
        }
        
        private int getRandomPossibleValue(int[] possibleValues)
        {
            int index = _random.Next(possibleValues.Length);
            return possibleValues[index];
        }

        private Field getRandomField(Sudoku sudoku)
        {
            var fields = sudoku.GetFields1D().ToList();
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
