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

namespace Sudoku.UI.Display
{
    /// <summary>
    /// Interaction logic for FieldView.xaml
    /// </summary>
    public partial class SudokuSetFieldView : UserControl
    {
        public SudokuSetFieldView()
        {
            InitializeComponent();
        }

        public int Row;
        public int Column;

        //private void TextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    e.Handled = true;
        //}
    }
}
