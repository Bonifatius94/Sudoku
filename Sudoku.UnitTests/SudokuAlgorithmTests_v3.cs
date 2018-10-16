using Microsoft.VisualStudio.TestTools.UnitTesting;
using MT.Tools.Tracing;
using Sudoku.Algorithms;
using Sudoku.Data;
using System;

namespace Sudoku.UnitTests
{
    [TestClass]
    public class SudokuAlgorithmTests_v3
    {
        //#region Init

        //[AssemblyInitialize]
        //public static void AssemblyInit(TestContext context)
        //{
        //    TraceOut.Enable(traceFile: @"C:\Trace\Sudoku.UnitTests.trc.txt", level: TraceLevel.All);
        //}

        //#endregion Init

        #region TestMethods
            
        [TestMethod]
        public void TestUniquenessCheck_Easy_v3()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("    Uniqueness Test");
            Console.WriteLine("==========================");

            DateTime start = DateTime.Now;
            var sudoku = getEasyTestSudoku();
            var isUnique = new Algorithms.v3.SudokuSolver().HasSudokuUniqueSolution(sudoku);
            Console.WriteLine(isUnique.ToString());
            DateTime end = DateTime.Now;

            Console.WriteLine($"solving sudoku took { (end - start).TotalMilliseconds }ms");
            Console.WriteLine("");
        }

        [TestMethod]
        public void TestUniquenessCheck_Hard_v3()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("    Uniqueness Test");
            Console.WriteLine("==========================");

            // test with unique sudoku
            DateTime start = DateTime.Now;
            var sudoku = getDifficultTestSudoku();
            bool isUnique = new Algorithms.v3.SudokuSolver().HasSudokuUniqueSolution(sudoku);
            Console.WriteLine(isUnique.ToString());
            DateTime end = DateTime.Now;

            Assert.IsTrue(isUnique);

            Console.WriteLine($"check for uniqueness of a unique sudoku took { (end - start).TotalMilliseconds }ms");
            Console.WriteLine("");

            // test with non-unique sudoku
            start = DateTime.Now;
            sudoku = getNonUniqueTestSudoku();
            isUnique = new Algorithms.v3.SudokuSolver().HasSudokuUniqueSolution(sudoku);
            Console.WriteLine(isUnique.ToString());
            end = DateTime.Now;

            Assert.IsTrue(!isUnique);

            Console.WriteLine($"check for uniqueness of a non-unique sudoku took { (end - start).TotalMilliseconds }ms");
            Console.WriteLine("");
        }

        [TestMethod]
        public void TestSudokuGeneratorPerformance_v3()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("Generator Performance Test");
            Console.WriteLine("==========================");

            int count = 10;
            //DateTime start = DateTime.Now;

            // generate 10 sudokus, check if they have a unique solution and find out the solution
            for (int i = 0; i < count; i++)
            {
                var genSudoku = new Algorithms.v3.SudokuGenerator().GenerateSudoku(SudokuDifficuty.Extreme);
                Console.WriteLine("\r\n" + genSudoku.ToString());
                //Console.WriteLine(new Algorithms.v3.SudokuSolver_v3().HasSudokuUniqueSolution(genSudoku).ToString());
                //Console.WriteLine("\r\n" + new Algorithms.v3.SudokuSolver_v3().SolveSudoku(genSudoku));
            }

            //DateTime end = DateTime.Now;

            //Console.WriteLine("");
            //Console.WriteLine($"generating {count } sudokus took { (end - start).TotalSeconds }s");
            //Console.WriteLine("");
        }

        #endregion TestMethods

        #region Sudokus

        private static SudokuPuzzle getEasyTestSudoku()
        {
            var sudoku = new SudokuPuzzle();

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

        private static SudokuPuzzle getDifficultTestSudoku()
        {
            var sudoku = new SudokuPuzzle();

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

        private static SudokuPuzzle getNonUniqueTestSudoku()
        {
            var sudoku = new SudokuPuzzle();

            sudoku.Fields[0, 1].SetValue(3);
            sudoku.Fields[0, 2].SetValue(9);
            sudoku.Fields[0, 6].SetValue(1);
            sudoku.Fields[0, 7].SetValue(2);

            sudoku.Fields[1, 3].SetValue(9);
            sudoku.Fields[1, 5].SetValue(7);

            sudoku.Fields[2, 0].SetValue(8);
            sudoku.Fields[2, 3].SetValue(4);
            sudoku.Fields[2, 5].SetValue(1);
            sudoku.Fields[2, 8].SetValue(6);

            sudoku.Fields[3, 1].SetValue(4);
            sudoku.Fields[3, 2].SetValue(2);
            sudoku.Fields[3, 6].SetValue(7);
            sudoku.Fields[3, 7].SetValue(9);
            
            sudoku.Fields[5, 1].SetValue(9);
            sudoku.Fields[5, 2].SetValue(1);
            sudoku.Fields[5, 6].SetValue(5);
            sudoku.Fields[5, 7].SetValue(4);

            sudoku.Fields[6, 0].SetValue(5);
            sudoku.Fields[6, 3].SetValue(1);
            sudoku.Fields[6, 5].SetValue(9);
            sudoku.Fields[6, 8].SetValue(3);

            sudoku.Fields[7, 3].SetValue(8);
            sudoku.Fields[7, 5].SetValue(5);

            sudoku.Fields[8, 1].SetValue(1);
            sudoku.Fields[8, 2].SetValue(4);
            sudoku.Fields[8, 6].SetValue(8);
            sudoku.Fields[8, 7].SetValue(7);

            return sudoku;
        }
        
        #endregion Sudokus
    }
}
