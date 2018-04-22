using Caliburn.Micro;
using Sudoku.UI.Display;
using Sudoku.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Solver;
using System.Collections.ObjectModel;
using MT.Tools.Tracing;

namespace Sudoku.UI.Main
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

        private UISudoku _solution;

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

        public void GenerateSudoku()
        {
            TraceOut.Enter();

            _creationMode = SudokuCreationMode.Automatic;
            NotifyOfPropertyChange(() => IsChecked_Automatic);
            NotifyOfPropertyChange(() => IsChecked_Manual);
            
            var sudoku = new UISudoku(new SudokuGenerator().GenerateSudoku(_selectedDifficulty));
            _solution = new UISudoku(new SudokuSolver().SolveSudoku(sudoku));

            _sudokuView.ClearSudoku();
            _sudokuView.ApplySudoku(sudoku);
            _sudokuView.MarkSetFieldsAsFix();

            TraceOut.Leave();
        }

        public void ClearSudoku()
        {
            TraceOut.Enter();

            _sudokuView.ClearSudoku();

            TraceOut.Leave();
        }

        public void SolveSudoku()
        {
            TraceOut.Enter();

            if (_creationMode == SudokuCreationMode.Manual)
            {
                var sudoku = _sudokuView.GetSudoku();
                _solution = new UISudoku(new SudokuSolver().SolveSudoku(sudoku) ?? sudoku);
            }

            _solution.IsFix = _sudokuView.GetSudoku().IsFix;
            _sudokuView.ApplySudoku(_solution);
            
            TraceOut.Leave();
        }

        public override void CanClose(Action<bool> callback)
        {
            TraceOut.Enter();

            // save score
            SudokuScoreSettings.Sudoku = _sudokuView.GetSudoku();
            SudokuScoreSettings.SaveData();

            base.CanClose(callback);

            TraceOut.Leave();
        }

        #endregion Methods
    }
}
