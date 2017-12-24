using MT.Tools.Tracing;
using Sudoku.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TraceOut.Enable(traceFile: @"C:\Trace\Sudoku.Solver.Test.trc.txt", level: TraceLevel.All);

            var sudoku = getTestSudoku();
            TraceOut.WriteInformation("\r\n" + sudoku.ToString());

            var solution = new SudokuSolver().SolveSudoku(sudoku);
            TraceOut.WriteInformation("\r\n" + solution?.ToString());
        }

        private static Sudoku getTestSudoku()
        {
            var sudoku = new Sudoku();

            sudoku.Fields[0, 6].SetValue(1);
            sudoku.Fields[0, 7].SetValue(9);
            sudoku.Fields[1, 0].SetValue(2);
            sudoku.Fields[1, 1].SetValue(3);
            sudoku.Fields[1, 6].SetValue(6);
            sudoku.Fields[2, 3].SetValue(2);
            sudoku.Fields[2, 4].SetValue(4);
            sudoku.Fields[3, 6].SetValue(9);
            sudoku.Fields[3, 7].SetValue(6);
            sudoku.Fields[4, 3].SetValue(1);
            sudoku.Fields[4, 4].SetValue(6);
            sudoku.Fields[4, 7].SetValue(7);
            sudoku.Fields[5, 1].SetValue(4);
            sudoku.Fields[5, 2].SetValue(8);
            sudoku.Fields[5, 4].SetValue(7);
            sudoku.Fields[6, 2].SetValue(7);
            sudoku.Fields[6, 5].SetValue(3);
            sudoku.Fields[6, 6].SetValue(4);
            sudoku.Fields[6, 8].SetValue(5);
            sudoku.Fields[7, 2].SetValue(9);
            sudoku.Fields[7, 5].SetValue(8);
            sudoku.Fields[8, 2].SetValue(6);
            sudoku.Fields[8, 5].SetValue(5);
            sudoku.Fields[8, 6].SetValue(8);
            
            return sudoku;
        }
    }
}
