using Caliburn.Micro;

namespace Sudoku.Client.Display
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
        
        public int[,] GetMatrix()
        {
            var matrix = new int[9, 9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            var value = _boxes[i, j].Fields[k, l].Value;
                            matrix[i * 3 + k, j * 3 + l] = !string.IsNullOrEmpty(value) ? int.Parse(value) : 0;
                        }
                    }
                }
            }

            return matrix;
        }

        public void ApplyMatrix(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            _boxes[i, j].Fields[k, l].Value = matrix[i * 3 + k, j * 3 + l].ToString();
                        }
                    }
                }
            }
        }
        
        #endregion Methods
    }
}
