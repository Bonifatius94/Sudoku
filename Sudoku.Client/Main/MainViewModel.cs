using Caliburn.Micro;
using Sudoku.Client.Display;
using Sudoku.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Client.Main
{
    public class MainViewModel : Screen, IShell
    {
        #region Constructor

        public MainViewModel()
        {
            DisplayName = "Sudoku Solver v1.0";
            _sudoku.ApplyMatrix(SudokuScoreSettings.Matrix);
        }

        #endregion Constructor

        #region Members

        private SudokuViewModel _sudoku = new SudokuViewModel();
        public SudokuViewModel Sudoku { get { return _sudoku; } }

        private int[,] _solution;

        #endregion Members

        #region Methods

        public void GenerateSudoku()
        {
            generateSudokuAsync();
        }

        private async void generateSudokuAsync()
        {
            var matrix = await Task.Run<int[,]>(() => new SudokuGenerator().GenerateSudoku(SudokuDifficutyLevel.Medium, out _solution));

            _sudoku.ApplyMatrix(matrix);
            _sudoku.MarkSetFieldsAsFix();
        }

        public void ClearSudoku()
        {
            _sudoku.ApplyMatrix(new int[9, 9]);
            _sudoku.MarkSetFieldsAsFix();
        }

        public void SolveSudoku()
        {
            _sudoku.MarkSetFieldsAsFix();
            _sudoku.ApplyMatrix(_solution);

            //var matrix = _sudoku.GetMatrix();
            //var solver = new SudokuSolver();
            //var solution = solver.SolveSudoku(matrix);

            //_sudoku.ApplyMatrix(solution != null ? solution : new int[9, 9]);
        }

        public override void CanClose(Action<bool> callback)
        {
            // save score
            SudokuScoreSettings.Matrix = _sudoku.GetMatrix();
            SudokuScoreSettings.SaveData();

            base.CanClose(callback);
        }

        #endregion Methods
    }
}
