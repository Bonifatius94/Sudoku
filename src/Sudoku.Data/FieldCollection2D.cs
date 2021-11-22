using System;
using System.Linq;
using System.Text;

namespace Sudoku.Data
{
    public class FieldCollection2D : ISudokuSubcollection
    {
        #region Constructor

        public FieldCollection2D(int length)
        {
            _length = length;
            _fields = new SudokuField[_length, _length];

            for (int row = 0; row < _length; row++)
            {
                for (int column = 0; column < _length; column++)
                {
                    _fields[row, column] = new SudokuField();
                }
            }

            init();
        }

        public FieldCollection2D(SudokuField[,] fields)
        {
            _fields = fields;
            _length = (int)Math.Sqrt(_fields.Length);
            init();
        }

        #endregion Constructor

        #region Members

        protected int _length;
        public int Length { get { return _length; } }

        protected SudokuField[,] _fields;
        public SudokuField[,] Fields { get { return _fields; } }

        protected FieldCollection1D[] _rows;
        public FieldCollection1D[] Rows { get { return _rows; } }

        protected FieldCollection1D[] _columns;
        public FieldCollection1D[] Columns { get { return _columns; } }

        #endregion Members

        #region Methods

        private void init()
        {
            _rows = new FieldCollection1D[_length];
            _columns = new FieldCollection1D[_length];

            for (int i = 0; i < _length; i++)
            {
                _rows[i] = new FieldCollection1D(_length);
                _columns[i] = new FieldCollection1D(_length);
            }
            
            for (int row = 0; row < _length; row++)
            {
                for (int column = 0; column < _length; column++)
                {
                    _rows[row].Fields[column] = _fields[row, column];
                    _columns[column].Fields[row] = _fields[row, column];
                }
            }
        }

        public SudokuField[] GetFields1D()
        {
            var fields = new SudokuField[_length * _length];

            for (int row = 0; row < _length; row++)
            {
                for (int column = 0; column < _length; column++)
                {
                    fields[row * _length + column] = _fields[row, column];
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

        //public void PrintValues()
        //{
        //    Console.WriteLine(this.ToString());
        //}

        public void PrintPossibilities()
        {
            GetFields1D().ToList().ForEach(x => x.PrintPossibilities());
        }

        #endregion Methods
    }
}
