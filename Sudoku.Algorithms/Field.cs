using System;
using System.Linq;
using System.Text;

namespace Sudoku.Algorithms
{
    public class Field : ICloneable
    {
        #region Constructor

        public Field(int value = 0, int maxPossibilities = 9)
        {
            _maxPossibilities = maxPossibilities;
            _possibilities = new bool[_maxPossibilities];
            
            if (value == 0)
            {
                for (int i = 0; i < _maxPossibilities; i++)
                {
                    _possibilities[i] = true;
                }
            }
            else
            {
                _possibilities[value - 1] = true;
            }
        }

        public Field(bool[] possibilities)
        {
            _possibilities = possibilities;
            _maxPossibilities = _possibilities.Length;
        }

        #endregion Constructor

        #region Members
        
        protected bool[] _possibilities;
        protected int _maxPossibilities;
        
        /// <summary>
        /// This represents the current value of the field. If value is zero, no value is set (zero is default).
        /// </summary>
        public int Value
        {
            get
            {
                var possibleValues = GetPossibleValues();
                return (possibleValues != null && possibleValues.Length == 1) ? possibleValues[0] : 0;
            }
        }
        
        /// <summary>
        /// This represents the currently possible values of the field (value = index - 1, true means value is still possible). If only one possibility is left, the value is set automatically.
        /// </summary>
        public bool[] Possibilities { get { return _possibilities; } }

        public FieldCollection1D Row { get; set; }
        public FieldCollection1D Column { get; set; }
        public FieldCollection2D Square { get; set; }

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        #endregion Members

        #region Methods

        public void SetValue(int value)
        {
            if (value > 0 && value <= _maxPossibilities)
            {
                Row.EliminatePossibility(value);
                Column.EliminatePossibility(value);
                Square.EliminatePossibility(value);

                for (int i = 0; i < _maxPossibilities; i++)
                {
                    _possibilities[i] = (i == value - 1);
                }
            }
            else if (value == 0)
            {
                for (int i = 0; i < _maxPossibilities; i++)
                {
                    _possibilities[i] = true;
                }
            }
            else
            {
                throw new ArgumentException("Invalid value!");
            }
        }

        public void SetValueIfDetermined()
        {
            var possibleValues = GetPossibleValues();

            if (possibleValues.Length == 1)
            {
                SetValue(possibleValues[0]);
            }
        }

        public int[] GetPossibleValues()
        {
            int i = 1;
            return _possibilities.Select(x => new { Index = i++, IsPossible = x }).Where(x => x.IsPossible).Select(x => x.Index).ToArray();
        }

        public int GetPossibleValuesCount()
        {
            return _possibilities.Where(x => x).Count();
        }

        //public bool HasError()
        //{
        //    return (GetPossibleValuesCount() == 0);
        //}

        public object Clone()
        {
            var possibilities = new bool[_maxPossibilities];
            _possibilities.CopyTo(possibilities, 0);
            var field = new Field(possibilities);
            return field;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            GetPossibleValues().ToList().ForEach(x => builder.Append($"{ x }, "));
            builder.Remove(builder.Length - 2, 2);

            return builder.ToString();
        }

        public void PrintPossibilities()
        {
            Console.WriteLine(this.ToString());
        }
        
        #endregion Methods
    }
}
