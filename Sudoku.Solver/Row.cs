using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms
{
    public class Row : FieldCollection1D
    {
        #region Constructor

        public Row(Field[] fields) : base(fields) { }
        public Row(int length = 9) : base(length) { }

        #endregion Constructor
    }
}
