using Caliburn.Micro;
using System.Windows.Media;

namespace Sudoku.Client.Display
{
    public class SudokuFieldViewModel : Screen
    {
        #region Members

        private int _value = 0;
        public string Value
        {
            get { return _value != 0 ? _value.ToString() : string.Empty ; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length == 1)
                    {
                        int temp;
                        _value = int.TryParse(value, out temp) ? temp : _value;
                    }
                    else if (value.Length == 2)
                    {
                        int temp;
                        _value = int.TryParse(value.Substring(1, 1), out temp) ? temp : _value;
                    }
                }
                else
                {
                    _value = 0;
                }

                NotifyOfPropertyChange(() => Value);
                NotifyOfPropertyChange(() => SelectionStart);
            }
        }
        
        public int SelectionStart { get { return _value != 0 ? 1 : 0; } }

        private bool _isFix = false;
        public bool IsFix
        {
            get { return _isFix; }
            set
            {
                _isFix = value;
                NotifyOfPropertyChange(() => IsFix);
                NotifyOfPropertyChange(() => FontColor);
            }
        }

        public Brush FontColor { get { return _isFix ? Brushes.Black : Brushes.Blue; } }

        #endregion Members
    }
}
