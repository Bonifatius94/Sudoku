using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public abstract class FieldCollection1D : ISudokuSubcollection
    {
        #region Constructor

        public FieldCollection1D(int length)
        {
            // runs in constant time
            _length = length;
            _fields = new Field[_length];

            for (int i = 0; i < _length; i++)
            {
                _fields[i] = new Field();
            }
        }

        public FieldCollection1D(Field[] fields)
        {
            // runs in constant time
            _fields = fields;
        }

        #endregion Constructor

        #region Members

        protected int _length;

        protected Field[] _fields;
        public Field[] Fields { get { return _fields; } }

        #endregion Members

        #region Methods

        public int[] GetPossibleValues()
        {
            var values = _fields.Select(x => x.Value).Distinct();
            return _fields.SelectMany(x => x.GetPossibleValues()).Distinct().Except(values).ToArray();
        }

        public void EliminatePossibility(int value)
        {
            _fields.ToList().ForEach(x => x.Possibilities[value - 1] = false);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            _fields.ToList().ForEach(x => builder.Append($"{ x.Value } "));
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public void PrintValues()
        {
            Console.WriteLine(this.ToString());
        }

        public void PrintPossibilities()
        {
            _fields.ToList().ForEach(x => x.PrintPossibilities());
        }

        #endregion Methods
    }
}
