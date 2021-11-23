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
        ISudokuPuzzle SolveSudoku(ISudokuPuzzle sudoku);
        bool HasSudokuUniqueSolution(ISudokuPuzzle sudoku);
    }
}
