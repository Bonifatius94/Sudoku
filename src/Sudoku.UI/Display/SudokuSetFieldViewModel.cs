using Caliburn.Micro;
using Sudoku.UI.Helper;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.UI.Display
{
    public class SudokuSetFieldViewModel : SudokuFieldViewModelBase
    {
        #region Members
        
        private int _value = 0;
        public string Value
        {
            get { return (_value == 0) ? string.Empty : _value.ToString(); }
            set { /* ignore user input */ }
        }
        
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

        private static readonly Brush COLOR_FIX = Brushes.Black;
        private static readonly Brush COLOR_MODIFYABLE = Brushes.Black;
        public Brush FontColor { get { return _isFix ? COLOR_FIX : COLOR_MODIFYABLE; } }

        #endregion Members

        #region Methods
        
        public override void onKeyPressed_1(object sender, EventArgs e) { SetValue(1); }
        public override void onKeyPressed_2(object sender, EventArgs e) { SetValue(2); }
        public override void onKeyPressed_3(object sender, EventArgs e) { SetValue(3); }
        public override void onKeyPressed_4(object sender, EventArgs e) { SetValue(4); }
        public override void onKeyPressed_5(object sender, EventArgs e) { SetValue(5); }
        public override void onKeyPressed_6(object sender, EventArgs e) { SetValue(6); }
        public override void onKeyPressed_7(object sender, EventArgs e) { SetValue(7); }
        public override void onKeyPressed_8(object sender, EventArgs e) { SetValue(8); }
        public override void onKeyPressed_9(object sender, EventArgs e) { SetValue(9); }

        public override void onKeyPressed_Del(object sender, EventArgs e) { ResetValue(); }

        public void SetValue(int value)
        {
            if (!_isFix)
            {
                _value = value;
                NotifyOfPropertyChange(() => Value);
                AppComponents.Main.ApplyChangesToHistory();
            }
        }

        public int GetValue() { return _value; }

        public void ResetValue()
        {
            if (!_isFix)
            {
                _value = 0;
                NotifyOfPropertyChange(() => Value);
                AppComponents.Main.ApplyChangesToHistory();
            }
        }

        #endregion Methods
    }
}
