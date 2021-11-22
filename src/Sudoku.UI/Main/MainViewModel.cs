using Caliburn.Micro;
// using MT.Tools.Tracing;
using Sudoku.Algorithms;
using Sudoku.UI.Data;
using Sudoku.UI.Display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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
            AppComponents.Main = this;
            DisplayName = "Sudoku Solver v1.0";

            if (SudokuScoreSettings.Instance.Score.History?.Count > 0)
            {
                _sudokuView.ApplySudoku(SudokuScoreSettings.Instance.Score.History.Peek());
            }
        }

        #endregion Constructor

        #region Members
        
        private SudokuViewModel _sudokuView = new SudokuViewModel();
        public SudokuViewModel Sudoku { get { return _sudokuView; } }

        //private ScoreHistory _history = new ScoreHistory();
        //public ScoreHistory History { get { return _history; } }

        private ScoreSudokuPuzzle _solution = new ScoreSudokuPuzzle();

        #region InputMode

        private SudokuInputMode _inputMode = SudokuInputMode.Determined;

        public bool InputMode_Determined
        {
            get { return _inputMode == SudokuInputMode.Determined; }
            set
            {
                if (value)
                {
                    _inputMode = SudokuInputMode.Determined;
                    NotifyOfPropertyChange(() => InputMode_Determined);
                    NotifyOfPropertyChange(() => InputMode_Notes);
                }
            }
        }

        public bool InputMode_Notes
        {
            get { return _inputMode == SudokuInputMode.Notes; }
            set
            {
                if (value)
                {
                    _inputMode = SudokuInputMode.Notes;
                    NotifyOfPropertyChange(() => InputMode_Determined);
                    NotifyOfPropertyChange(() => InputMode_Notes);
                }
            }
        }

        #endregion InputMode

        #region GeneratorSettings

        private bool _isGeneratorRunning = false;
        
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

        #endregion GeneratorSettings

        #endregion Members

        #region Methods

        public async void GenerateSudoku()
        {
            // TraceOut.Enter();

            await Task.Run(() =>
            {
                if (!_isGeneratorRunning)
                {
                    _isGeneratorRunning = true;

                    // set creation mode to automatic
                    _creationMode = SudokuCreationMode.Automatic;
                    NotifyOfPropertyChange(() => IsChecked_Automatic);
                    NotifyOfPropertyChange(() => IsChecked_Manual);

                    // create a new sudoku puzzle and solve it
                    var sudoku = new Algorithms.v2.SudokuGenerator().GenerateSudoku(_selectedDifficulty);
                    var sudokuAsScore = new ScoreSudokuPuzzle(sudoku);
                    _solution = new ScoreSudokuPuzzle(new Algorithms.v2.SudokuSolver().SolveSudoku(sudoku));
                    
                    // apply the sudoku to the view
                    _sudokuView.ClearSudoku();
                    _sudokuView.ApplySudoku(sudokuAsScore);
                    _sudokuView.MarkSetFieldsAsFix();

                    // apply the changes to the history
                    ApplyChangesToHistory();

                    _isGeneratorRunning = false;
                }
            });
            
            // TraceOut.Leave();
        }

        public void ClearSudoku()
        {
            // TraceOut.Enter();

            _sudokuView.ClearSudoku();

            // TraceOut.Leave();
        }

        public void SolveSudoku()
        {
            // TraceOut.Enter();

            // get a solution for the sudoku if not already available
            if (_creationMode == SudokuCreationMode.Manual)
            {
                var sudoku = _sudokuView.GetSudoku();
                _solution = new ScoreSudokuPuzzle(new Algorithms.v2.SudokuSolver().SolveSudoku(sudoku.Deserialize()) ?? sudoku.Deserialize());
            }

            // check if this is still necessary
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    var puzzle = _sudokuView.GetSudoku();
                    _solution.fields[row * 9 + column].isFix = puzzle.fields[row * 9 + column].isFix;
                }
            }
            
            // apply the solution to the view
            _sudokuView.ApplySudoku(_solution);
            
            // TraceOut.Leave();
        }

        public void ApplyChangesToHistory()
        {
            // TODO: implement saving in write-through mode if the app freezes
            var currentState = _sudokuView.GetSudoku();
            SudokuScoreSettings.Instance.Score.History.Push(currentState);
            SudokuScoreSettings.Instance.SaveData();
        }

        public void RevertRecentChanges()
        {
            // TODO: add revert button in UI that invokes this method
            // TODO: implement saving in write-through mode if the app freezes
            SudokuScoreSettings.Instance.Score.History.Pop();
            var lastState = SudokuScoreSettings.Instance.Score.History.Peek();
            SudokuScoreSettings.Instance.SaveData();
        }

        //public override void CanClose(Action<bool> callback)
        //{
        //    TraceOut.Enter();

        //    // save score
        //    SudokuScoreSettings.Instance.Score.History.Peek() = _sudokuView.GetSudoku();
        //    SudokuScoreSettings.Instance.SaveData();

        //    base.CanClose(callback);

        //    TraceOut.Leave();
        //}

        #endregion Methods
    }
}
