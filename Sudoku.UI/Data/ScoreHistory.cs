using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sudoku.UI.Data
{
    public class ScoreHistory
    {
        #region Members

        [XmlArray]
        public List<ScoreSudokuPuzzle> historyAsList { get; set; } = new List<ScoreSudokuPuzzle>();

        private Stack<ScoreSudokuPuzzle> _history = null;
        [XmlIgnore]
        public Stack<ScoreSudokuPuzzle> History { get { return _history ?? (_history = new Stack<ScoreSudokuPuzzle>(historyAsList)); } }

        #endregion Members
    }
}
