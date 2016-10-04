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

        #endregion Members

        #region Methods

        public void GenerateSudoku()
        {
            generateSudokuAsync();
        }

        private async void generateSudokuAsync()
        {
            var generator = new SudokuGenerator();
            var matrix = await Task.Run<int[,]>(() => generator.GenerateSudoku(SudokuDifficutyLevel.Medium));
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

            var matrix = _sudoku.GetMatrix();
            var solver = new SudokuSolver();
            var solution = solver.SolveSudoku(matrix);

            _sudoku.ApplyMatrix(solution != null ? solution : new int[9, 9]);
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
