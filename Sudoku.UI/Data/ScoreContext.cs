using MT.Tools.Tracing;
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

        private SudokuScoreSettings() { LoadData(); }

        #endregion Singleton

        #region Members

        private static readonly string TEMP_SCORE_FILE = Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "SudokuScore.xml");
        public ScoreHistory Score { get; set; } = null;

        #endregion Members

        #region Methods
        
        public void LoadData()
        {
            TraceOut.Enter();
            
            if (File.Exists(TEMP_SCORE_FILE))
            {
                ScoreHistory score = null;
                var serializer = new XmlSerializer(typeof(ScoreHistory));

                using (var reader = new StreamReader(TEMP_SCORE_FILE))
                {
                    score = serializer.Deserialize(reader) as ScoreHistory;
                }
            }

            TraceOut.Leave();
        }

        public void SaveData()
        {
            TraceOut.Enter();
            
            var serializer = new XmlSerializer(typeof(ScoreHistory));

            using (var writer = new StreamWriter(TEMP_SCORE_FILE))
            {
                serializer.Serialize(writer, Score);
            }

            TraceOut.Leave();
        }
        
        #endregion Methods
    }
}
