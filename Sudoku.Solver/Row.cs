using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public class Row : FieldCollection1D
    {
        #region Constructor

        public Row(Field[] fields) : base(fields) { }
        public Row() : base(9) { }

        #endregion Constructor
    }
}
