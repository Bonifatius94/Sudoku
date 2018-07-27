using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms
{
    public interface ISudokuSolver
    {
        Sudoku SolveSudoku(Sudoku sudoku);
        bool HasSudokuUniqueSolution(Sudoku sudoku);
    }
}
