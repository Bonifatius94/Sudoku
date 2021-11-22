using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI.Display
{
    public enum SudokuInputMode
    {
        Determined,
        Notes
    }

    public class SudokuFieldViewModel : SudokuFieldViewModelBase
    {
        #region Members

        private SudokuSetFieldViewModel _determinedView = new SudokuSetFieldViewModel();
        public SudokuSetFieldViewModel DeterminedView { get { return _determinedView; } }

        private SudokuNoteFieldViewModel _notesView = new SudokuNoteFieldViewModel();
        public SudokuNoteFieldViewModel NotesView { get { return _notesView; } }

        public SudokuInputMode Mode { get; set; } = SudokuInputMode.Determined;
        public SudokuFieldViewModelBase FieldTemplate
        {
            get
            {
                if (Mode == SudokuInputMode.Determined)
                {
                    return _determinedView;
                }
                else
                {
                    return _notesView;
                }
            }
        }

        #endregion Members

        #region Methods

        public override void onKeyPressed_1(object sender, EventArgs e) { FieldTemplate.onKeyPressed_1(sender, e); }
        public override void onKeyPressed_2(object sender, EventArgs e) { FieldTemplate.onKeyPressed_2(sender, e); }
        public override void onKeyPressed_3(object sender, EventArgs e) { FieldTemplate.onKeyPressed_3(sender, e); }
        public override void onKeyPressed_4(object sender, EventArgs e) { FieldTemplate.onKeyPressed_4(sender, e); }
        public override void onKeyPressed_5(object sender, EventArgs e) { FieldTemplate.onKeyPressed_5(sender, e); }
        public override void onKeyPressed_6(object sender, EventArgs e) { FieldTemplate.onKeyPressed_6(sender, e); }
        public override void onKeyPressed_7(object sender, EventArgs e) { FieldTemplate.onKeyPressed_7(sender, e); }
        public override void onKeyPressed_8(object sender, EventArgs e) { FieldTemplate.onKeyPressed_8(sender, e); }
        public override void onKeyPressed_9(object sender, EventArgs e) { FieldTemplate.onKeyPressed_9(sender, e); }
        public override void onKeyPressed_Del(object sender, EventArgs e) { FieldTemplate.onKeyPressed_Del(sender, e); }

        #endregion Methods
    }
}
