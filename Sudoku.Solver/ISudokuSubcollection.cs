using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public interface ISudokuSubcollection
    {
        int[] GetPossibleValues();
        void EliminatePossibility(int value);

        void PrintValues();
        void PrintPossibilities();
    }
}
