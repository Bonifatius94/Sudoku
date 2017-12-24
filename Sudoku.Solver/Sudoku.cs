using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver
{
    public class Sudoku : FieldCollection2D, ICloneable
    {
        #region Constructor

        public Sudoku(Field[,] fields) : base(fields) { initSudoku(); }
        public Sudoku() : base(9) { initSudoku(); }

        #endregion Constructor

        #region Members

        private Square[,] _squares;
        public Square[,] Squares { get { return _squares; } }

        #endregion Members

        #region Methods

        private void initSudoku()
        {
            const int length = 3;
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
                            int rowIndex = i * 3 + k;
                            int columnIndex = j * 3 + l;

                            var field = _fields[rowIndex, columnIndex];
                            _squares[i, j].Fields[k, l] = field;

                            field.Row = _rows[rowIndex];
                            field.Column = _columns[columnIndex];
                            field.Square = _squares[i, j];
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

        public void EliminatePossibilities()
        {
            int solvedFields = getSolvedFieldsCount();
            int newSolvedFields = solvedFields;

            do
            {
                solvedFields = newSolvedFields;
                GetFields1D().ToList().ForEach(x => x.SetValueIfDetermined());
                newSolvedFields = getSolvedFieldsCount();
            }
            while (newSolvedFields > solvedFields);
        }

        private int getSolvedFieldsCount()
        {
            int count = 0;

            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    if (_fields[i, j].Value > 0) { count++; }
                }
            }

            return count;
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

            for (int i = 0; i < _length; i++)
            {
                builder.AppendLine(_rows[i].ToString());
            }

            return builder.ToString();
        }
        
        #endregion Methods
    }
}
