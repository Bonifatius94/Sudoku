using Caliburn.Micro;
using Sudoku.Client.Display;
using Sudoku.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Solver;
using System.Collections.ObjectModel;

namespace Sudoku.Client.Main
{
    public enum SudokuCreationMode
    {
        Automatic,
        Manual
    }

    public class MainViewModel : Screen, IShell
    {
        #region Constructor

        public MainViewModel()
        {
            DisplayName = "Sudoku Solver v1.0";
            _sudokuView.ApplySudoku(SudokuScoreSettings.Sudoku);
        }

        #endregion Constructor

        #region Members
        
        private SudokuViewModel _sudokuView = new SudokuViewModel();
        public SudokuViewModel Sudoku { get { return _sudokuView; } }

        private Solver.Sudoku _solution;

        #region CreationMode

        private SudokuCreationMode _creationMode = SudokuCreationMode.Manual;

        public bool IsChecked_Automatic
        {
            get { return _creationMode == SudokuCreationMode.Automatic; }
            set
            {
                if (value)
                {
                    _creationMode = SudokuCreationMode.Automatic;
                    NotifyOfPropertyChange(() => IsChecked_Automatic);
                    NotifyOfPropertyChange(() => IsChecked_Manual);
                }
            }
        }

        public bool IsChecked_Manual
        {
            get { return _creationMode == SudokuCreationMode.Manual; }
            set
            {
                if (value)
                {
                    _creationMode = SudokuCreationMode.Manual;
                    NotifyOfPropertyChange(() => IsChecked_Automatic);
                    NotifyOfPropertyChange(() => IsChecked_Manual);
                }
            }
        }

        #endregion CreationMode

        #region Difficulty

        private static readonly Dictionary<SudokuDifficuty, string> _difficultyValues = new Dictionary<SudokuDifficuty, string>()
        {
            { SudokuDifficuty.Easy, SudokuDifficuty.Easy.ToString() },
            { SudokuDifficuty.Medium, SudokuDifficuty.Medium.ToString() },
            { SudokuDifficuty.Hard, SudokuDifficuty.Hard.ToString() },
            { SudokuDifficuty.Extreme, SudokuDifficuty.Extreme.ToString() },
        };

        private ObservableCollection<string> _difficulties = new ObservableCollection<string>(_difficultyValues.Values);
        public ObservableCollection<string> Difficulties { get { return _difficulties; } }

        private SudokuDifficuty _selectedDifficulty = SudokuDifficuty.Medium;
        public string SelectedDifficulty
        {
            get { return _selectedDifficulty.ToString(); }
            set
            {
                _selectedDifficulty = _difficultyValues.Where(x => x.Value.Equals(value)).FirstOrDefault().Key;
                NotifyOfPropertyChange(() => SelectedDifficulty);
            }
        }

        #endregion Difficulty

        #endregion Members

        #region Methods

        public async void GenerateSudoku()
        {
            _creationMode = SudokuCreationMode.Automatic;
            NotifyOfPropertyChange(() => IsChecked_Automatic);
            NotifyOfPropertyChange(() => IsChecked_Manual);

            await Task.Run(() =>
            {
                var sudoku = new SudokuGenerator().GenerateSudoku(_selectedDifficulty);
                var temp = (Solver.Sudoku)sudoku.Clone();
                _solution = new SudokuSolver().SolveSudoku(temp);

                _sudokuView.ApplySudoku(sudoku);
                _sudokuView.MarkSetFieldsAsFix();
            });
        }

        public void ClearSudoku()
        {
            _sudokuView.ApplySudoku(new Solver.Sudoku());
            _sudokuView.MarkSetFieldsAsFix();
        }

        public void SolveSudoku()
        {
            if (_creationMode == SudokuCreationMode.Manual)
            {
                var sudoku = _sudokuView.GetSudoku();
                _solution = new SudokuSolver().SolveSudoku(sudoku) ?? sudoku;
            }

            _sudokuView.MarkSetFieldsAsFix();
            _sudokuView.ApplySudoku(_solution);

            //var matrix = _sudoku.GetMatrix();
            //var solver = new SudokuSolver();
            //var solution = solver.SolveSudoku(matrix);

            //_sudoku.ApplyMatrix(solution != null ? solution : new int[9, 9]);
        }

        public override void CanClose(Action<bool> callback)
        {
            // save score
            SudokuScoreSettings.Sudoku = _sudokuView.GetSudoku();
            SudokuScoreSettings.SaveData();

            base.CanClose(callback);
        }

        #endregion Methods
    }
}
