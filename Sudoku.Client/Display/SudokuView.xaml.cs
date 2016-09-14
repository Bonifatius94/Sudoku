using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku.Client.Display
{
    /// <summary>
    /// Interaction logic for SudokuViewModel.xaml
    /// </summary>
    public partial class SudokuView : UserControl
    {
        public SudokuView()
        {
            InitializeComponent();

            _boxes = new SudokuBoxView[3, 3]
            {
                {
                    Box_0_0,
                    Box_0_1,
                    Box_0_2,
                },
                {
                    Box_1_0,
                    Box_1_1,
                    Box_1_2,
                },
                {
                    Box_2_0,
                    Box_2_1,
                    Box_2_2,
                },
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var box = _boxes[i, j];

                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            var field = box.Fields[k, l];
                            field.Row = i*3 + k;
                            field.Column = j * 3 + l;
                            field.GotKeyboardFocus += Field_PreviewGotKeyboardFocus;
                        }
                    }
                }
            }
        }

        private SudokuBoxView[,] _boxes;
        private int _row = 0;
        private int _column = 0;

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _row = _row > 0 ? _row - 1 : _row;
                    break;
                case Key.Down:
                    _row = _row < 8 ? _row + 1 : _row;
                    break;
                case Key.Left:
                    _column = _column > 0 ? _column - 1 : _column;
                    break;
                case Key.Right:
                    _column = _column < 8 ? _column + 1 : _column;
                    break;
                default:
                    return;
            }

            var box = _boxes[_row / 3, _column / 3];
            var field = box.Fields[_row % 3, _column % 3];

            field.Focusable = true;
            Keyboard.Focus(field);
        }

        private void Field_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var field = sender as SudokuFieldView;

            if (field != null)
            {
                _row = field.Row;
                _column = field.Column;
            }
        }
    }
}
