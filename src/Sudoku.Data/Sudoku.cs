using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Data
{
    public class SudokuPuzzle : FieldCollection2D, ICloneable
    {
        #region Constructor

        public SudokuPuzzle(SudokuField[,] fields) : base(fields) { initSudoku(); }
        public SudokuPuzzle(int length = 9) : base(length) { initSudoku(); }

        private void initSudoku()
        {
            int squareLength = (int)Math.Sqrt(_length);
            _squares = new FieldCollection2D[squareLength, squareLength];

            for (int i = 0; i < squareLength; i++)
            {
                for (int j = 0; j < squareLength; j++)
                {
                    _squares[i, j] = new FieldCollection2D(squareLength);

                    for (int k = 0; k < squareLength; k++)
                    {
                        for (int l = 0; l < squareLength; l++)
                        {
                            int rowIndex = i * squareLength + k;
                            int columnIndex = j * squareLength + l;

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

        #endregion Constructor

        #region Members

        private FieldCollection2D[,] _squares;
        public FieldCollection2D[,] Squares { get { return _squares; } }
        
        #endregion Members

        #region Methods
        
        #region Fields

        public int GetSolvedFieldsCount()
        {
            return GetFields1D().Where(x => x.Value > 0).Count();
        }

        public List<SudokuField> GetFreeFields()
        {
            return GetFields1D().Where(x => x.Value == 0).ToList();
        }

        public List<SudokuField> GetSetFields()
        {
            return GetFields1D().Where(x => x.Value > 0).ToList();
        }

        #endregion Fields

        #region Possibilities

        public bool IsValid()
        {
            return GetFields1D().All(x => x.GetPossibleValuesCount() > 0);
        }

        public bool IsSolved()
        {
            return GetFreeFields().Count == 0 && IsValid();
        }

        public void EliminatePossibilities()
        {
            int diff = 0;
            int tempSolvedFields = GetSolvedFieldsCount();

            do
            {
                GetFields1D().ToList().ForEach(x => x.SetValueIfDetermined());
                diff = GetSolvedFieldsCount() - tempSolvedFields;
                tempSolvedFields += diff;
            }
            while (diff > 0);
        }
        
        #endregion Possibilities

        #region Overrides

        public virtual object Clone()
        {
            // clone all fields and apply them to a new sudoku
            var fields = new SudokuField[_length, _length];
            GetFields1D().ToList().ForEach(x => fields[x.RowIndex, x.ColumnIndex] = (SudokuField)x.Clone());
            var ret = new SudokuPuzzle(fields);

            return ret;
        }
        
        public override bool Equals(object obj)
        {
            var sudoku = obj as SudokuPuzzle;
            bool ret = false;

            if (sudoku != null)
            {
                // check if all possibilities are equal or not
                ret = sudoku.GetFields1D().All(x => x.Possibilities.SequenceEqual(_fields[x.RowIndex, x.ColumnIndex].Possibilities));
            }

            return ret;
        }

        public override int GetHashCode()
        {
            // this enforces the Equals() function for types like HashSet, Dictionary, etc.
            return 0;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            int squareLength = (int)Math.Sqrt(_length);

            const string SEPARATOR = "+-------+-------+-------+";
            builder.AppendLine(SEPARATOR);

            for (int i = 0; i < squareLength; i++)
            {
                for (int j = 0; j < squareLength; j++)
                {
                    builder.Append("|");

                    for (int k = 0; k < squareLength; k++)
                    {
                        for (int l = 0; l < squareLength; l++)
                        {
                            builder.Append($" { _fields[i * squareLength + j, k * squareLength + l].Value }");
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

        #endregion Overrides

        #endregion Methods
    }
}
