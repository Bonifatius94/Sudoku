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
    /// Interaction logic for SudokuBoxView.xaml
    /// </summary>
    public partial class SudokuBoxView : UserControl
    {
        public SudokuBoxView()
        {
            InitializeComponent();

            Fields = new SudokuFieldView[3, 3]
            {
                {
                    Field_0_0,
                    Field_0_1,
                    Field_0_2
                },
                {
                    Field_1_0,
                    Field_1_1,
                    Field_1_2
                },
                {
                    Field_2_0,
                    Field_2_1,
                    Field_2_2
                }
            };
        }

        public SudokuFieldView[,] Fields;
    }
}
