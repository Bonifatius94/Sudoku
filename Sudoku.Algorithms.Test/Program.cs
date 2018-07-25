using MT.Tools.Tracing;
using System;

namespace Sudoku.Algorithms.Test
{
    public class Program
    {
        #region Main

        public static void Main(string[] args)
        {
            TraceOut.Enable(traceFile: @"C:\Trace\Sudoku.Solver.Test.trc.txt", level: TraceLevel.All);
            TraceOut.Enter();

            Program instance = new Program();

            instance.TestSudokuSolver();
            instance.TestUniquenessCheck();
            instance.TestSudokuGeneratorPerformance();

            TraceOut.Leave();
        }

        #endregion Main

        #region Methods

        public void TestSudokuSolver()
        {
            TraceOut.WriteInformation("==========================");
            TraceOut.WriteInformation("    Sudoku Solver Test");
            TraceOut.WriteInformation("==========================");
            TraceOut.WriteInformation("easy sudoku (average time)");

            DateTime start = DateTime.Now;
            var sudoku = getEasyTestSudoku();
            var solution = new SudokuSolver().SolveSudoku(sudoku);
            DateTime end = DateTime.Now;
            
            TraceOut.WriteInformation("\r\n" + sudoku.ToString());
            TraceOut.WriteInformation("\r\n" + solution?.ToString());
            TraceOut.WriteInformation($"solving sudoku took { (end - start).TotalMilliseconds }ms");
            //TraceOut.WriteInformation("==========================");
            //TraceOut.WriteInformation("difficuly sudoku (long time)");

            //start = DateTime.Now;
            //sudoku = getDifficultTestSudoku();
            //solution = new SudokuSolver().SolveSudoku(sudoku);
            //end = DateTime.Now;

            //TraceOut.WriteInformation("\r\n" + sudoku.ToString());
            //TraceOut.WriteInformation("\r\n" + solution?.ToString());
            //TraceOut.WriteInformation($"solving sudoku took { (end - start).TotalMilliseconds }ms");
            TraceOut.WriteInformation("");
        }

        public void TestUniquenessCheck()
        {
            TraceOut.WriteInformation("==========================");
            TraceOut.WriteInformation("    Uniqueness Test");
            TraceOut.WriteInformation("==========================");

            DateTime start = DateTime.Now;
            var sudoku = getEasyTestSudoku();
            var isUnique = new SudokuSolver().HasSudokuUniqueSolution(sudoku);
            TraceOut.WriteInformation(isUnique.ToString());
            DateTime end = DateTime.Now;

            TraceOut.WriteInformation($"solving sudoku took { (end - start).TotalMilliseconds }ms");

            //start = DateTime.Now;
            //sudoku = getDifficultTestSudoku();
            //isUnique = new SudokuSolver().HasSudokuUniqueSolution(sudoku);
            //TraceOut.WriteInformation(isUnique.ToString());
            //end = DateTime.Now;

            //TraceOut.WriteInformation($"solving sudoku took { (end - start).TotalMilliseconds }ms");
            TraceOut.WriteInformation("");
        }

        public void TestSudokuGeneratorPerformance()
        {
            TraceOut.WriteInformation("==========================");
            TraceOut.WriteInformation("Generator Performance Test");
            TraceOut.WriteInformation("==========================");

            DateTime start = DateTime.Now;

            // generate 100 sudokus, check if they have a unique solution and find out the solution
            for (int i = 0; i < 1000; i++)
            {
                var genSudoku = new SudokuGenerator().GenerateSudoku(SudokuDifficuty.Extreme);
                TraceOut.WriteInformation("\r\n" + genSudoku.ToString());
                TraceOut.WriteInformation(new SudokuSolver().HasSudokuUniqueSolution(genSudoku).ToString());
                TraceOut.WriteInformation("\r\n" + new SudokuSolver().SolveSudoku(genSudoku));
            }

            DateTime end = DateTime.Now;

            TraceOut.WriteInformation("");
            TraceOut.WriteInformation($"generating 1000 sudokus took { (end - start).TotalSeconds }s");
            TraceOut.WriteInformation("");
        }

        #endregion Methods

        #region Sudokus

        private static Sudoku getEasyTestSudoku()
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

        private static Sudoku getDifficultTestSudoku()
        {
            var sudoku = new Sudoku();

            sudoku.Fields[1, 5].SetValue(3);
            sudoku.Fields[1, 7].SetValue(8);
            sudoku.Fields[1, 8].SetValue(5);
            sudoku.Fields[2, 2].SetValue(1);
            sudoku.Fields[2, 4].SetValue(2);
            sudoku.Fields[3, 3].SetValue(5);
            sudoku.Fields[3, 5].SetValue(7);
            sudoku.Fields[4, 2].SetValue(4);
            sudoku.Fields[4, 6].SetValue(1);
            sudoku.Fields[5, 1].SetValue(9);
            sudoku.Fields[6, 0].SetValue(5);
            sudoku.Fields[6, 7].SetValue(7);
            sudoku.Fields[6, 8].SetValue(3);
            sudoku.Fields[7, 2].SetValue(2);
            sudoku.Fields[7, 4].SetValue(1);
            sudoku.Fields[8, 4].SetValue(4);
            sudoku.Fields[8, 8].SetValue(9);

            return sudoku;
        }

        #endregion Sudokus
    }
}
