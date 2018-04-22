using Caliburn.Micro;
using MT.Tools.Tracing;
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
        
        public UISudoku GetSudoku()
        {
            TraceOut.Enter();

            var sudoku = new UISudoku();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            sudoku.Fields[i * 3 + k, j * 3 + l].SetValue(_boxes[i, j].Fields[k, l].GetValue());
                            sudoku.IsFix[i * 3 + k, j * 3 + l] = _boxes[i, j].Fields[k, l].IsFix;
                        }
                    }
                }
            }

            TraceOut.Leave();
            return sudoku;
        }

        public void ApplySudoku(UISudoku sudoku)
        {
            TraceOut.Enter();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            _boxes[i, j].Fields[k, l].SetValue(sudoku.Fields[i * 3 + k, j * 3 + l].Value);
                            _boxes[i, j].Fields[k, l].IsFix = sudoku.IsFix[i * 3 + k, j * 3 + l];
                        }
                    }
                }
            }

            TraceOut.Leave();
        }

        public void ClearSudoku()
        {
            TraceOut.Enter();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            _boxes[i, j].Fields[k, l].IsFix = false;
                            _boxes[i, j].Fields[k, l].SetValue(0);
                        }
                    }
                }
            }

            TraceOut.Leave();
        }

        public void MarkSetFieldsAsFix()
        {
            TraceOut.Enter();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            var field = _boxes[i, j].Fields[k, l];
                            field.IsFix = (field.GetValue() != 0);
                        }
                    }
                }
            }

            TraceOut.Leave();
        }
        
        #endregion Methods
    }
}
