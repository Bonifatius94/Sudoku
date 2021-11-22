// // using MT.Tools.Tracing;
// using Sudoku.Algorithms;
// using Sudoku.Data;
// using System;
// using Xunit;

// namespace Sudoku.UnitTests
// {
//     public class SudokuAlgorithmTests_v2
//     {
//         //#region Init

//         //[AssemblyInitialize]
//         //public static void AssemblyInit(TestContext context)
//         //{
//         //    TraceOut.Enable(traceFile: @"C:\Trace\Sudoku.UnitTests.trc.txt", level: TraceLevel.All);
//         //}

//         //#endregion Init

//         #region TestMethods

//         [Fact]
//         public void TestSudokuSolver_Easy_v2()
//         {
//             Console.WriteLine("==========================");
//             Console.WriteLine("    Sudoku Solver Test");
//             Console.WriteLine("==========================");
//             Console.WriteLine("easy sudoku (average time)");

//             DateTime start = DateTime.Now;
//             var sudoku = getEasyTestSudoku();
//             var solution = new Algorithms.v2.SudokuSolver().SolveSudoku(sudoku);
//             DateTime end = DateTime.Now;

//             Console.WriteLine("\r\n" + sudoku.ToString());
//             Console.WriteLine("\r\n" + solution?.ToString());
//             Console.WriteLine($"solving sudoku took { (end - start).TotalMilliseconds }ms");
//             Console.WriteLine("");

//             Assert.True(solution != null);
//         }

//         [Fact]
//         public void TestSudokuSolver_Hard_v2()
//         {
//             Console.WriteLine("==========================");
//             Console.WriteLine("    Sudoku Solver Test");
//             Console.WriteLine("==========================");
//             Console.WriteLine("difficuly sudoku (long time)");

//             DateTime start = DateTime.Now;
//             var sudoku = getDifficultTestSudoku();
//             var solution = new Algorithms.v2.SudokuSolver().SolveSudoku(sudoku);
//             DateTime end = DateTime.Now;

//             Console.WriteLine("\r\n" + sudoku.ToString());
//             Console.WriteLine("\r\n" + solution?.ToString());
//             Console.WriteLine($"solving sudoku took { (end - start).TotalMilliseconds }ms");
//             Console.WriteLine("");
//         }

//         [Fact]
//         public void TestUniquenessCheck_Easy_v2()
//         {
//             Console.WriteLine("==========================");
//             Console.WriteLine("    Uniqueness Test");
//             Console.WriteLine("==========================");

//             DateTime start = DateTime.Now;
//             var sudoku = getEasyTestSudoku();
//             var isUnique = new Algorithms.v2.SudokuSolver().HasSudokuUniqueSolution(sudoku);
//             Console.WriteLine(isUnique.ToString());
//             DateTime end = DateTime.Now;

//             Console.WriteLine($"solving sudoku took { (end - start).TotalMilliseconds }ms");
//             Console.WriteLine("");
//         }

//         [Fact]
//         public void TestUniquenessCheck_Hard_v2()
//         {
//             Console.WriteLine("==========================");
//             Console.WriteLine("    Uniqueness Test");
//             Console.WriteLine("==========================");

//             DateTime start = DateTime.Now;
//             var sudoku = getDifficultTestSudoku();
//             var isUnique = new Algorithms.v2.SudokuSolver().HasSudokuUniqueSolution(sudoku);
//             Console.WriteLine(isUnique.ToString());
//             DateTime end = DateTime.Now;
            
//             Console.WriteLine($"solving sudoku took { (end - start).TotalMilliseconds }ms");
//             Console.WriteLine("");
//         }

//         [Fact]
//         public void TestSudokuGeneratorPerformance_v2()
//         {
//             Console.WriteLine("==========================");
//             Console.WriteLine("Generator Performance Test");
//             Console.WriteLine("==========================");

//             int count = 10;
//             DateTime start = DateTime.Now;

//             // generate 10 sudokus, check if they have a unique solution and find out the solution
//             for (int i = 0; i < count; i++)
//             {
//                 var genSudoku = new Algorithms.v2.SudokuGenerator().GenerateSudoku(SudokuDifficuty.Extreme);
//                 Console.WriteLine("\r\n" + genSudoku.ToString());
//                 Console.WriteLine(new Algorithms.v2.SudokuSolver().HasSudokuUniqueSolution(genSudoku).ToString());
//                 Console.WriteLine("\r\n" + new Algorithms.v2.SudokuSolver().SolveSudoku(genSudoku));
//             }

//             DateTime end = DateTime.Now;

//             Console.WriteLine("");
//             Console.WriteLine($"generating { count } sudokus took { (end - start).TotalSeconds }s");
//             Console.WriteLine("");
//         }

//         #endregion TestMethods

//         #region Sudokus

//         private static SudokuPuzzle getEasyTestSudoku()
//         {
//             var sudoku = new SudokuPuzzle();

//             sudoku.Fields[0, 6].SetValue(1);
//             sudoku.Fields[0, 7].SetValue(9);
//             sudoku.Fields[1, 0].SetValue(2);
//             sudoku.Fields[1, 1].SetValue(3);
//             sudoku.Fields[1, 6].SetValue(6);
//             sudoku.Fields[2, 3].SetValue(2);
//             sudoku.Fields[2, 4].SetValue(4);
//             sudoku.Fields[3, 6].SetValue(9);
//             sudoku.Fields[3, 7].SetValue(6);
//             sudoku.Fields[4, 3].SetValue(1);
//             sudoku.Fields[4, 4].SetValue(6);
//             sudoku.Fields[4, 7].SetValue(7);
//             sudoku.Fields[5, 1].SetValue(4);
//             sudoku.Fields[5, 2].SetValue(8);
//             sudoku.Fields[5, 4].SetValue(7);
//             sudoku.Fields[6, 2].SetValue(7);
//             sudoku.Fields[6, 5].SetValue(3);
//             sudoku.Fields[6, 6].SetValue(4);
//             sudoku.Fields[6, 8].SetValue(5);
//             sudoku.Fields[7, 2].SetValue(9);
//             sudoku.Fields[7, 5].SetValue(8);
//             sudoku.Fields[8, 2].SetValue(6);
//             sudoku.Fields[8, 5].SetValue(5);
//             sudoku.Fields[8, 6].SetValue(8);

//             return sudoku;
//         }

//         private static SudokuPuzzle getDifficultTestSudoku()
//         {
//             var sudoku = new SudokuPuzzle();

//             sudoku.Fields[1, 5].SetValue(3);
//             sudoku.Fields[1, 7].SetValue(8);
//             sudoku.Fields[1, 8].SetValue(5);
//             sudoku.Fields[2, 2].SetValue(1);
//             sudoku.Fields[2, 4].SetValue(2);
//             sudoku.Fields[3, 3].SetValue(5);
//             sudoku.Fields[3, 5].SetValue(7);
//             sudoku.Fields[4, 2].SetValue(4);
//             sudoku.Fields[4, 6].SetValue(1);
//             sudoku.Fields[5, 1].SetValue(9);
//             sudoku.Fields[6, 0].SetValue(5);
//             sudoku.Fields[6, 7].SetValue(7);
//             sudoku.Fields[6, 8].SetValue(3);
//             sudoku.Fields[7, 2].SetValue(2);
//             sudoku.Fields[7, 4].SetValue(1);
//             sudoku.Fields[8, 4].SetValue(4);
//             sudoku.Fields[8, 8].SetValue(9);

//             return sudoku;
//         }

//         #endregion Sudokus
//     }
// }
