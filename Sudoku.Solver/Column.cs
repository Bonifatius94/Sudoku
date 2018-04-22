using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public class Column : FieldCollection1D
    {
        #region Constructor

        public Column(Field[] fields) : base(fields) { }
        public Column(int length = 9) : base(length) { }

        #endregion Constructor
    }
}
