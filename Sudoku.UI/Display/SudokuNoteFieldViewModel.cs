using Caliburn.Micro;
using Sudoku.UI.Helper;
using System;
using System.Windows.Input;

namespace Sudoku.UI.Display
{
    public class SudokuNoteFieldViewModel : SudokuFieldViewModelBase
    {
        #region Members
        
        private SudokuNoteFieldItemViewModel[,] _notes = new SudokuNoteFieldItemViewModel[3, 3]
        {
            {
                new SudokuNoteFieldItemViewModel() { Value = 1 },
                new SudokuNoteFieldItemViewModel() { Value = 2 },
                new SudokuNoteFieldItemViewModel() { Value = 3 }
            },
            {
                new SudokuNoteFieldItemViewModel() { Value = 4 },
                new SudokuNoteFieldItemViewModel() { Value = 5 },
                new SudokuNoteFieldItemViewModel() { Value = 6 }
            },
            {
                new SudokuNoteFieldItemViewModel() { Value = 7 },
                new SudokuNoteFieldItemViewModel() { Value = 8 },
                new SudokuNoteFieldItemViewModel() { Value = 9 }
            }
        };

        public SudokuNoteFieldItemViewModel[,] Notes { get { return _notes; } }

        public SudokuNoteFieldItemViewModel Note_0_0 { get { return _notes[0, 0]; } }
        public SudokuNoteFieldItemViewModel Note_0_1 { get { return _notes[0, 1]; } }
        public SudokuNoteFieldItemViewModel Note_0_2 { get { return _notes[0, 2]; } }
        public SudokuNoteFieldItemViewModel Note_1_0 { get { return _notes[1, 0]; } }
        public SudokuNoteFieldItemViewModel Note_1_1 { get { return _notes[1, 1]; } }
        public SudokuNoteFieldItemViewModel Note_1_2 { get { return _notes[1, 2]; } }
        public SudokuNoteFieldItemViewModel Note_2_0 { get { return _notes[2, 0]; } }
        public SudokuNoteFieldItemViewModel Note_2_1 { get { return _notes[2, 1]; } }
        public SudokuNoteFieldItemViewModel Note_2_2 { get { return _notes[2, 2]; } }
        
        #endregion Members

        #region Methods
        
        public override void onKeyPressed_1(object sender, EventArgs e) { SetPossibility(0, 0); }
        public override void onKeyPressed_2(object sender, EventArgs e) { SetPossibility(0, 1); }
        public override void onKeyPressed_3(object sender, EventArgs e) { SetPossibility(0, 2); }
        public override void onKeyPressed_4(object sender, EventArgs e) { SetPossibility(1, 0); }
        public override void onKeyPressed_5(object sender, EventArgs e) { SetPossibility(1, 1); }
        public override void onKeyPressed_6(object sender, EventArgs e) { SetPossibility(1, 2); }
        public override void onKeyPressed_7(object sender, EventArgs e) { SetPossibility(2, 0); }
        public override void onKeyPressed_8(object sender, EventArgs e) { SetPossibility(2, 1); }
        public override void onKeyPressed_9(object sender, EventArgs e) { SetPossibility(2, 2); }

        public override void onKeyPressed_Del(object sender, EventArgs e) { ResetValue(); }

        public void SetPossibility(int i, int j)
        {
            var notes = _notes[i, j];
            notes.IsSet = !notes.IsSet;
            notes.NotifyAll();
        }

        public bool[] GetPossibilities()
        {
            bool[] possibilities = new bool[9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var notes = _notes[i, j];
                    possibilities[i * 3 + j] = notes.IsSet;
                }
            }

            return possibilities;
        }

        public void ResetValue()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var notes = _notes[i, j];
                    notes.IsSet = false;
                    notes.NotifyAll();
                }
            }
        }

        #endregion Methods
    }
}
