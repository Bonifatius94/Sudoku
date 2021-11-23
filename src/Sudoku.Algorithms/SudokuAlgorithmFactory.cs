namespace Sudoku.Algorithms
{
    public static class SudokuAlgorithmFactory
    {
        public static ISudokuSolver CreatePuzzleSolver()
        {
            return new v3.SudokuSolver();
        }

        public static ISudokuGenerator CreatePuzzleGenerator()
        {
            return new v3.SudokuGenerator();
        }
    }
}
