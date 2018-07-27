using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms
{
    public interface ISudokuGenerator
    {
        Sudoku GenerateSudoku(SudokuDifficuty difficulty, int length = 9);
    }
}
