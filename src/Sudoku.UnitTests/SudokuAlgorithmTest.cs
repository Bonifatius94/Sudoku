using Xunit;
using Sudoku.Data;
using System.Linq;
using Sudoku.Algorithms;

namespace Sudoku.UnitTests
{
    public class SudokuAlgorithmTests
    {
        [Fact]
        public void ShouldNotViolateSudokuRules()
        {
            var validSudoku = getDifficultTestSudoku();
            Assert.True(validSudoku.IsValid());
        }

        [Fact]
        public void ShouldViolateSudokuRules()
        {
            var invalidSudoku = getUnsolvableTestSudoku();
            var invalidFields =
                invalidSudoku.GetAllFields().Where(x => x.GetPossibleValuesCount() == 0).ToList();
            Assert.True(!invalidSudoku.IsValid());
        }

        [Fact]
        public void ShouldSolveEasySudoku()
        {
            var sudoku = getEasyTestSudoku();
            var sudkuSolver = SudokuAlgorithmFactory.CreatePuzzleSolver();
            var solution = sudkuSolver.SolveSudoku(sudoku);
            Assert.True(solution.IsSolved());
        }

        [Fact]
        public void ShouldSolveDifficultSudoku()
        {
            var sudoku = getDifficultTestSudoku();
            var sudkuSolver = SudokuAlgorithmFactory.CreatePuzzleSolver();
            var solution = sudkuSolver.SolveSudoku(sudoku);
            Assert.True(solution.IsSolved());
        }

        #region Sudokus

        private static ISudokuPuzzle getEasyTestSudoku()
        {
            return SudokuFactory.CreatePuzzle(
                new int[] {
                    0, 0, 0, 0, 0, 0, 1, 9, 0,
                    2, 3, 0, 0, 0, 0, 6, 0, 0,
                    0, 0, 0, 2, 4, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 9, 6, 0,
                    0, 0, 0, 1, 6, 0, 0, 7, 0,
                    0, 4, 8, 0, 7, 0, 0, 0, 0,
                    0, 0, 7, 0, 0, 3, 4, 0, 5,
                    0, 0, 9, 0, 0, 8, 0, 0, 0,
                    0, 0, 6, 0, 0, 5, 8, 0, 0,
                }
            );
        }

        private static ISudokuPuzzle getDifficultTestSudoku()
        {
            return SudokuFactory.CreatePuzzle(
                new int[] {
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 3, 0, 8, 5,
                    0, 0, 1, 0, 2, 0, 0, 0, 0,
                    0, 0, 0, 5, 0, 7, 0, 0, 0,
                    0, 0, 4, 0, 0, 0, 1, 0, 0,
                    0, 9, 0, 0, 0, 0, 0, 0, 0,
                    5, 0, 0, 0, 0, 0, 0, 7, 3,
                    0, 0, 2, 0, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 4, 0, 0, 0, 9,
                }
            );
        }

        private static ISudokuPuzzle getUnsolvableTestSudoku()
        {
            return SudokuFactory.CreatePuzzle(
                new int[] {
                    2, 2, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 3, 0, 8, 5,
                    0, 0, 1, 0, 2, 0, 0, 0, 0,
                    0, 0, 0, 5, 0, 7, 0, 0, 0,
                    0, 0, 4, 0, 0, 0, 1, 0, 0,
                    0, 9, 0, 0, 0, 0, 0, 0, 0,
                    5, 0, 0, 0, 0, 0, 0, 7, 3,
                    0, 0, 2, 0, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 4, 0, 0, 9, 9,
                }
            );
        }

        #endregion Sudokus
    }
}
