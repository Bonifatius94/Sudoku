using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public class Square : FieldCollection2D
    {
        #region Constructor

        public Square(Field[,] fields) : base(fields) { }
        public Square(int length = 3) : base(length) { }

        #endregion Constructor
    }
}
