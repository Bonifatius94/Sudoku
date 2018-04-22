using Caliburn.Micro;
using Sudoku.UI.Helper;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.UI.Display
{
    public class SudokuSetFieldViewModel : Screen
    {
        #region Constructor

        public SudokuSetFieldViewModel()
        {
            initKeyBindings();
        }

        #endregion Constructor

        #region Members

        #region Commands

        private ICommand _keyPressed_1;
        private ICommand _keyPressed_2;
        private ICommand _keyPressed_3;
        private ICommand _keyPressed_4;
        private ICommand _keyPressed_5;
        private ICommand _keyPressed_6;
        private ICommand _keyPressed_7;
        private ICommand _keyPressed_8;
        private ICommand _keyPressed_9;
        private ICommand _keyPressed_0;
        private ICommand _keyPressed_Del;
        
        public ICommand KeyPressed_1 { get { return _keyPressed_1; } }
        public ICommand KeyPressed_2 { get { return _keyPressed_2; } }
        public ICommand KeyPressed_3 { get { return _keyPressed_3; } }
        public ICommand KeyPressed_4 { get { return _keyPressed_4; } }
        public ICommand KeyPressed_5 { get { return _keyPressed_5; } }
        public ICommand KeyPressed_6 { get { return _keyPressed_6; } }
        public ICommand KeyPressed_7 { get { return _keyPressed_7; } }
        public ICommand KeyPressed_8 { get { return _keyPressed_8; } }
        public ICommand KeyPressed_9 { get { return _keyPressed_9; } }
        public ICommand KeyPressed_0 { get { return _keyPressed_0; } }
        public ICommand KeyPressed_Del { get { return _keyPressed_Del; } }
        
        #endregion Commands

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

        public Brush FontColor { get { return _isFix ? Brushes.Black : Brushes.Blue; } }

        #endregion Members

        #region Methods
        
        private void initKeyBindings()
        {
            _keyPressed_1 = new DelegateCommand(onKeyPressed_1);
            _keyPressed_2 = new DelegateCommand(onKeyPressed_2);
            _keyPressed_3 = new DelegateCommand(onKeyPressed_3);
            _keyPressed_4 = new DelegateCommand(onKeyPressed_4);
            _keyPressed_5 = new DelegateCommand(onKeyPressed_5);
            _keyPressed_6 = new DelegateCommand(onKeyPressed_6);
            _keyPressed_7 = new DelegateCommand(onKeyPressed_7);
            _keyPressed_8 = new DelegateCommand(onKeyPressed_8);
            _keyPressed_9 = new DelegateCommand(onKeyPressed_9);
            _keyPressed_0 = new DelegateCommand(onKeyPressed_Del);
            _keyPressed_Del = new DelegateCommand(onKeyPressed_Del);
        }

        private void onKeyPressed_1(object sender, EventArgs e) { SetValue(1); }
        private void onKeyPressed_2(object sender, EventArgs e) { SetValue(2); }
        private void onKeyPressed_3(object sender, EventArgs e) { SetValue(3); }
        private void onKeyPressed_4(object sender, EventArgs e) { SetValue(4); }
        private void onKeyPressed_5(object sender, EventArgs e) { SetValue(5); }
        private void onKeyPressed_6(object sender, EventArgs e) { SetValue(6); }
        private void onKeyPressed_7(object sender, EventArgs e) { SetValue(7); }
        private void onKeyPressed_8(object sender, EventArgs e) { SetValue(8); }
        private void onKeyPressed_9(object sender, EventArgs e) { SetValue(9); }

        private void onKeyPressed_Del(object sender, EventArgs e) { ResetValue(); }

        public void SetValue(int value)
        {
            if (!_isFix)
            {
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public int GetValue() { return _value; }

        public void ResetValue()
        {
            if (!_isFix)
            {
                _value = 0;
                NotifyOfPropertyChange(() => Value);
            }
        }

        #endregion Methods
    }
}
