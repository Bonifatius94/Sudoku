using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public abstract class FieldCollection2D : ISudokuSubcollection
    {
        #region Constructor

        public FieldCollection2D(int length)
        {
            // runs in constant time
            _length = length;
            _fields = new Field[_length, _length];

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    _fields[i, j] = new Field();
                }
            }

            init();
        }

        public FieldCollection2D(Field[,] fields)
        {
            // runs in constant time
            _fields = fields;
            _length = (int)Math.Sqrt(_fields.Length);
            init();
        }

        #endregion Constructor

        #region Members

        protected int _length;

        protected Field[,] _fields;
        public Field[,] Fields { get { return _fields; } }

        protected Row[] _rows;
        public Row[] Rows { get { return _rows; } }

        protected Column[] _columns;
        public Column[] Columns { get { return _columns; } }

        #endregion Members

        #region Methods

        private void init()
        {
            _rows = new Row[_length];
            _columns = new Column[_length];

            for (int i = 0; i < _length; i++)
            {
                _rows[i] = new Row();
                _columns[i] = new Column();
            }

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    _rows[i].Fields[j] = _fields[i, j];
                    _columns[j].Fields[i] = _fields[i, j];
                }
            }
        }

        public Field[] GetFields1D()
        {
            // runs in constant time
            var fields = new Field[_length * _length];

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    fields[i * _length + j] = _fields[i, j];
                }
            }

            return fields;
        }

        public int[] GetPossibleValues()
        {
            var fields = GetFields1D();
            var values = fields.Select(x => x.Value).Distinct();
            return fields.SelectMany(x => x.GetPossibleValues()).Distinct().Except(values).ToArray();
        }

        public void EliminatePossibility(int value)
        {
            var fields = GetFields1D();
            fields.ToList().ForEach(x => x.Possibilities[value - 1] = false);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            GetFields1D().ToList().ForEach(x => builder.Append($"{ x.Value } "));
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public void PrintValues()
        {
            Console.WriteLine(this.ToString());
        }

        public void PrintPossibilities()
        {
            GetFields1D().ToList().ForEach(x => x.PrintPossibilities());
        }

        #endregion Methods
    }
}
