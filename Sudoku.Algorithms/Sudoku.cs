using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Algorithms
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Sudoku : FieldCollection2D, ICloneable
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        #region Constructor

        public Sudoku(Field[,] fields) : base(fields) { initSudoku(); }
        public Sudoku(int length = 9) : base(length) { initSudoku(); }

        #endregion Constructor

        #region Members

        private Square[,] _squares;
        public Square[,] Squares { get { return _squares; } }
        
        #endregion Members

        #region Methods

        private void initSudoku()
        {
            int length = (int)Math.Sqrt(_length);
            _squares = new Square[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    _squares[i, j] = new Square();

                    for (int k = 0; k < length; k++)
                    {
                        for (int l = 0; l < length; l++)
                        {
                            int rowIndex = i * length + k;
                            int columnIndex = j * length + l;

                            var field = _fields[rowIndex, columnIndex];
                            _squares[i, j].Fields[k, l] = field;

                            field.Row = _rows[rowIndex];
                            field.Column = _columns[columnIndex];
                            field.Square = _squares[i, j];

                            field.RowIndex = rowIndex;
                            field.ColumnIndex = columnIndex;
                        }
                    }
                }
            }
        }

        public bool IsValid()
        {
            bool ret = true;

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    if (_fields[i, j].GetPossibleValuesCount() == 0)
                    {
                        ret = false;
                        break;
                    }
                }
            }

            return ret;
        }

        public bool IsSolved()
        {
            return GetFreeFields().Count == 0 && IsValid();
        }

        public void EliminatePossibilities()
        {
            int solvedFields = GetSolvedFieldsCount();
            int newSolvedFields = solvedFields;

            do
            {
                solvedFields = newSolvedFields;
                GetFields1D().ToList().ForEach(x => x.SetValueIfDetermined());
                newSolvedFields = GetSolvedFieldsCount();
            }
            while (newSolvedFields > solvedFields);
        }

        public int GetSolvedFieldsCount()
        {
            return GetFields1D().Where(x => x.Value > 0).Count();
        }

        //public int GetSolvedFieldsCount()
        //{
        //    int count = 0;

        //    for (int i = 0; i < _length; i++)
        //    {
        //        for (int j = 0; j < _length; j++)
        //        {
        //            if (_fields[i, j].Value > 0) { count++; }
        //        }
        //    }

        //    return count;
        //}

        public List<Field> GetFreeFields()
        {
            return GetFields1D().Where(x => x.Value == 0).ToList();
        }

        public object Clone()
        {
            var fields = new Field[_length, _length];
            
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    fields[i, j] = (Field)_fields[i, j].Clone();
                }
            }

            var ret = new Sudoku(fields);
            return ret;
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            int square = (int)Math.Sqrt(_length);

            const string SEPARATOR = "+-------+-------+-------+";
            builder.AppendLine(SEPARATOR);

            for (int i = 0; i < square; i++)
            {
                for (int j = 0; j < square; j++)
                {
                    builder.Append("|");

                    for (int k = 0; k < square; k++)
                    {
                        for (int l = 0; l < square; l++)
                        {
                            builder.Append($" { _fields[i * square + j, k * square + l].Value }");
                        }

                        builder.Append(" |");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine(SEPARATOR);
            }

            builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            var sudoku = obj as Sudoku;

            if (sudoku != null)
            {
                for (int i = 0; i < _length; i++)
                {
                    for (int j = 0; j < _length; j++)
                    {
                        if (Fields[i, j].Value != sudoku.Fields[i, j].Value) { return false; }

                        for (int k = 0; k < _length; k++)
                        {
                            if (Fields[i, j].Possibilities[k] != sudoku.Fields[i, j].Possibilities[k]) { return false; }
                        }
                    }
                }

                return true;
            }

            return false;
        }

        #endregion Methods
    }
}
