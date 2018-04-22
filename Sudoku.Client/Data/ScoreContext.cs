using MT.Tools.Tracing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Sudoku.UI.Data
{
    public static class SudokuScoreSettings
    {
        #region Constructor

        static SudokuScoreSettings()
        {
            LoadData();
        }

        #endregion Constructor

        #region Members

        private static readonly string TEMP_SCORE_FILE = Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "SudokuScore.xml");
        public static UISudoku Sudoku { get; set; }

        #endregion Members

        #region Methods
        
        public static void LoadData()
        {
            TraceOut.Enter();

            Sudoku = new UISudoku();

            if (File.Exists(TEMP_SCORE_FILE))
            {
                score score = null;
                var serializer = new XmlSerializer(typeof(score));

                using (var reader = new StreamReader(TEMP_SCORE_FILE))
                {
                    score = serializer.Deserialize(reader) as score;
                }

                score.fields.ForEach(x => {
                    Sudoku.Fields[x.row, x.column].SetValue(x.value);
                    Sudoku.IsFix[x.row, x.column] = x.isFix;
                });
            }

            TraceOut.Leave();
        }

        public static void SaveData()
        {
            TraceOut.Enter();

            score score = new score();

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    score.fields.Add(new field() {
                        row = row,
                        column = column,
                        value = Sudoku.Fields[row, column].Value,
                        isFix = Sudoku.IsFix[row, column]
                    });
                }
            }

            var serializer = new XmlSerializer(typeof(score));

            using (var writer = new StreamWriter(TEMP_SCORE_FILE))
            {
                serializer.Serialize(writer, score);
            }

            TraceOut.Leave();
        }
        
        #endregion Methods
    }

    public class score
    {
        #region Members

        [XmlArray]
        public List<field> fields { get; set; } = new List<field>();

        #endregion Members

        #region Methods

        public Solver.Field[,] Convert()
        {
            var sudokufields = new Solver.Field[9, 9];
            fields.ForEach(x => sudokufields[x.row, x.column].SetValue(x.value));
            return sudokufields;
        }

        #endregion Methods
    }

    public class field
    {
        #region Members

        [XmlAttribute]
        public int row { get; set; }

        [XmlAttribute]
        public int column { get; set; }

        [XmlAttribute]
        public int value { get; set; }

        [XmlAttribute]
        public bool isFix { get; set; }

        #endregion Members
    }
}
