// using MT.Tools.Tracing;
using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace Sudoku.UI.Data
{
    public class SudokuScoreSettings
    {
        #region Singleton

        private static readonly Mutex _instanceCreationMutex = new Mutex();

        private static SudokuScoreSettings _instance = null;
        public static SudokuScoreSettings Instance
        {
            get
            {
                // make sure only one instance is created
                _instanceCreationMutex.WaitOne();
                _instance = (_instance != null) ? _instance : new SudokuScoreSettings();
                _instanceCreationMutex.ReleaseMutex();

                return _instance;
            }
        }

        private SudokuScoreSettings() { _score = LoadData(); }

        #endregion Singleton

        #region Members

        private static readonly string TEMP_SCORE_FILE = Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "SudokuScore.xml");

        private ScoreHistory _score = null;
        public ScoreHistory Score { get { return _score; } }

        #endregion Members

        #region Methods
        
        public ScoreHistory LoadData()
        {
            // TraceOut.Enter();

            var score = new ScoreHistory();

            if (File.Exists(TEMP_SCORE_FILE))
            {
                var serializer = new XmlSerializer(typeof(ScoreHistory));

                using (var reader = new StreamReader(TEMP_SCORE_FILE))
                {
                    score = serializer.Deserialize(reader) as ScoreHistory;
                }
            }

            // TraceOut.Leave();
            return score;
        }

        public void SaveData(ScoreHistory score = null)
        {
            // TraceOut.Enter();

            _score = score ?? _score;
            var serializer = new XmlSerializer(typeof(ScoreHistory));

            using (var writer = new StreamWriter(TEMP_SCORE_FILE))
            {
                serializer.Serialize(writer, _score);
            }

            // TraceOut.Leave();
        }
        
        #endregion Methods
    }
}
