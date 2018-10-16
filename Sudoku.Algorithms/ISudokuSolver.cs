using Sudoku.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms
{
    public interface ISudokuSolver
    {
        SudokuPuzzle SolveSudoku(SudokuPuzzle sudoku);
        bool HasSudokuUniqueSolution(SudokuPuzzle sudoku);
    }
}
