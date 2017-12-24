using System;
using System.Linq;
using System.Text;

namespace Sudoku.Solver
{
    public class Field : ICloneable
    {
        #region Constructor

        public Field(int value = 0, int possibilitiesCount = 9)
        {
            _value = value;
            _possibilitiesCount = possibilitiesCount;
            _possibilities = new bool[_possibilitiesCount];
            
            if (_value == 0)
            {
                for (int i = 0; i < _possibilitiesCount; i++)
                {
                    _possibilities[i] = true;
                }
            }
            else
            {
                _possibilities[_value - 1] = true;
            }
        }

        public Field(bool[] possibilities)
        {
            _possibilities = possibilities;
            _possibilitiesCount = _possibilities.Length;

            var possibleValues = GetPossibleValues();
            _value = (possibleValues != null && possibleValues.Length == 1) ? possibleValues[0] : 0;
        }

        #endregion Constructor

        #region Members

        protected int _value;
        protected bool[] _possibilities;
        protected int _possibilitiesCount;
        
        /// <summary>
        /// This represents the current value of the field. If value is zero, no value is set (zero is default).
        /// </summary>
        public int Value { get { return _value; } }
        
        /// <summary>
        /// This represents the currently possible values of the field (value = index - 1, true means value is still possible). If only one possibility is left, the value is set automatically.
        /// </summary>
        public bool[] Possibilities { get { return _possibilities; } }

        public Row Row { get; set; }
        public Column Column { get; set; }
        public Square Square { get; set; }

        #endregion Members

        #region Methods

        public void SetValue(int value)
        {
            _value = value;

            if (value > 0 && value <= _possibilitiesCount)
            {
                Row.EliminatePossibility(value);
                Column.EliminatePossibility(value);
                Square.EliminatePossibility(value);

                _possibilities = new bool[_possibilitiesCount];
                _possibilities[_value - 1] = true;
            }
            else if (value < 0)
            {
                throw new ArgumentException("Invalid value!");
            }
        }

        public void SetValueIfDetermined()
        {
            if (GetPossibleValuesCount() == 1)
            {
                SetValue(GetPossibleValues()[0]);
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

        public bool HasError()
        {
            return (GetPossibleValuesCount() == 0);
        }

        public object Clone()
        {
            var possibilities = new bool[_possibilitiesCount];
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
