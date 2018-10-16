using Caliburn.Micro;
using MT.Tools.Tracing;
using Sudoku.Data;
using Sudoku.UI.Data;

namespace Sudoku.UI.Display
{
    public class SudokuViewModel : Screen
    {
        #region Members
        
        private SudokuBoxViewModel[,] _boxes = new SudokuBoxViewModel[3, 3]
        {
            {
                new SudokuBoxViewModel(),
                new SudokuBoxViewModel(),
                new SudokuBoxViewModel()
            },
            {
                new SudokuBoxViewModel(),
                new SudokuBoxViewModel(),
                new SudokuBoxViewModel()
            },
            {
                new SudokuBoxViewModel(),
                new SudokuBoxViewModel(),
                new SudokuBoxViewModel()
            }
        };

        public SudokuBoxViewModel Box_0_0 { get { return _boxes[0, 0]; } }
        public SudokuBoxViewModel Box_0_1 { get { return _boxes[0, 1]; } }
        public SudokuBoxViewModel Box_0_2 { get { return _boxes[0, 2]; } }
        public SudokuBoxViewModel Box_1_0 { get { return _boxes[1, 0]; } }
        public SudokuBoxViewModel Box_1_1 { get { return _boxes[1, 1]; } }
        public SudokuBoxViewModel Box_1_2 { get { return _boxes[1, 2]; } }
        public SudokuBoxViewModel Box_2_0 { get { return _boxes[2, 0]; } }
        public SudokuBoxViewModel Box_2_1 { get { return _boxes[2, 1]; } }
        public SudokuBoxViewModel Box_2_2 { get { return _boxes[2, 2]; } }
        
        #endregion Members

        #region Methods
        
        public ScoreSudokuPuzzle GetSudoku()
        {
            TraceOut.Enter();

            var score = new ScoreSudokuPuzzle();

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    // TODO: check if values are correct
                    int i = row / 3;
                    int j = row % 3;
                    int k = column / 3;
                    int l = column % 3;
                    
                    var fieldSwitch = _boxes[i, j].Fields[k, l] as SudokuFieldViewModel;

                    if (fieldSwitch.Mode == SudokuInputMode.Determined)
                    {
                        var field = fieldSwitch.DeterminedView;

                        score.fields.Add(new ScoreSudokuField()
                        {
                            row = row,
                            column = column,
                            isFix = field.IsFix,
                            value = field.GetValue()
                        });
                    }
                    else // if (fieldViewModel.Mode == SudokuFieldMode.Notes)
                    {
                        var field = fieldSwitch.NotesView;

                        score.fields.Add(new ScoreSudokuField()
                        {
                            row = row,
                            column = column,
                            isFix = false,
                            value = 0,
                            possibilities = field.GetPossibilities()
                        });
                    }
                }
            }

            TraceOut.Leave();
            return score;
        }

        public void ApplySudoku(ScoreSudokuPuzzle sudoku)
        {
            TraceOut.Enter();

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    // TODO: check if values are correct
                    int i = row / 3;
                    int j = row % 3;
                    int k = column / 3;
                    int l = column % 3;

                    var field = _boxes[i, j].Fields[k, l].DeterminedView;
                    field.SetValue(sudoku.fields[row * 9 + column].value);
                    field.IsFix = sudoku.fields[row * 9 + column].isFix;
                }
            }

            TraceOut.Leave();
        }

        public void ClearSudoku()
        {
            TraceOut.Enter();

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    // TODO: check if values are correct
                    int i = row / 3;
                    int j = row % 3;
                    int k = column / 3;
                    int l = column % 3;

                    var field = _boxes[i, j].Fields[k, l].DeterminedView;
                    field.SetValue(0);
                    field.IsFix = false;
                }
            }

            TraceOut.Leave();
        }

        public void MarkSetFieldsAsFix()
        {
            TraceOut.Enter();

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    // TODO: check if values are correct
                    int i = row / 3;
                    int j = row % 3;
                    int k = column / 3;
                    int l = column % 3;

                    var field = _boxes[i, j].Fields[k, l].DeterminedView;
                    field.IsFix = (field.GetValue() != 0);
                }
            }

            TraceOut.Leave();
        }
        
        #endregion Methods
    }
}
