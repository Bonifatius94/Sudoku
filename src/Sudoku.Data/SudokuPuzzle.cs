using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Data
{
    public interface ISudokuPuzzle
    {
        bool IsValid();
        bool IsSolved();
        void EliminatePossibilities();

        int GetSolvedFieldsCount();
        IEnumerable<SudokuField> GetFreeFields();
        IEnumerable<SudokuField> GetSetFields();
        IEnumerable<SudokuField> GetAllFields();

        void ForbidPossibility(int row, int col, int value);
        void AllowPossibility(int row, int col, int value);

        int GetValue(int row, int col);
        void SetValue(int row, int col, int value);
        ISudokuPuzzle DeepCopy();
    }

    internal class SudokuPuzzle : FieldCollection2D, ICloneable, ISudokuPuzzle
    {
        #region Constructor

        internal SudokuPuzzle(SudokuField[,] fields) : base(fields) { initSudoku(); }

        internal SudokuPuzzle(int[] fields) : base(fields) { initSudoku(); }

        internal SudokuPuzzle(int length = 9) : base(length) { initSudoku(); }

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

            EliminatePossibilities();
        }

        #endregion Constructor

        #region Members

        private FieldCollection2D[,] _squares;
        // public FieldCollection2D[,] Squares { get { return _squares; } }
        
        #endregion Members

        #region Methods
        
        #region Fields

        public int GetSolvedFieldsCount()
            => GetFields1D().Where(x => x.Value > 0).Count();

        public IEnumerable<SudokuField> GetFreeFields()
            => GetFields1D().Where(x => x.Value == 0);

        public IEnumerable<SudokuField> GetSetFields()
            => GetFields1D().Where(x => x.Value > 0);

        public IEnumerable<SudokuField> GetAllFields() => GetFields1D();

        public int GetValue(int row, int col)
            => _fields[row, col].Value;

        public void SetValue(int row, int col, int value)
            => _fields[row, col].SetValue(value);

        #endregion Fields

        #region Possibilities

        public bool IsValid() => GetAllFields().All(x => x.GetPossibleValuesCount() > 0);

        public bool IsSolved() => GetFreeFields().Count() == 0 && IsValid();

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

        public void ForbidPossibility(int row, int col, int value)
        {
            _fields[row, col].Possibilities[value-1] = false;
        }

        public void AllowPossibility(int row, int col, int value)
        {
            _fields[row, col].Possibilities[value-1] = true;
        }
        
        #endregion Possibilities

        #region Overrides

        public ISudokuPuzzle DeepCopy()
            => (ISudokuPuzzle)Clone();

        public virtual object Clone()
        {
            // clone all fields and apply them to a new sudoku
            var fields = new SudokuField[_length, _length];
            GetFields1D().ToList().ForEach(x =>
                fields[x.RowIndex, x.ColumnIndex] = (SudokuField)x.Clone());
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
                ret = sudoku.GetFields1D().All(x => x.Possibilities.SequenceEqual(
                    _fields[x.RowIndex, x.ColumnIndex].Possibilities));
            }

            return ret;
        }

        public override int GetHashCode()
        {
            // this enforces the Equals() function for types like HashSet, Dictionary, etc.
            return 0;
            // TODO: think of something more intelligent to at least differ sudokus in most cases
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
                            int row = i * squareLength + j;
                            int col = k * squareLength + l;
                            builder.Append($" { _fields[row, col].Value }");
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
