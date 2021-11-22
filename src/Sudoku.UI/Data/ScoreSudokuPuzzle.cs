using Sudoku.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sudoku.UI.Data
{
    public class ScoreSudokuPuzzle
    {
        #region Constructor

        public ScoreSudokuPuzzle() { }
        public ScoreSudokuPuzzle(SudokuPuzzle sudoku) { Serialize(sudoku); }

        #endregion Constructor

        #region Members

        [XmlArray]
        public List<ScoreSudokuField> fields { get; set; } = new List<ScoreSudokuField>();

        #endregion Members

        #region Methods

        public void Serialize(SudokuPuzzle sudoku)
        {
            sudoku.GetFields1D().ToList().ForEach(x => fields.Add(new ScoreSudokuField()
            {
                row = x.RowIndex,
                column = x.ColumnIndex,
                value = x.Value,
                possibilities = x.Possibilities
            }));
        }

        public SudokuPuzzle Deserialize()
        {
            var sudokufields = new SudokuField[9, 9];
            fields.ForEach(x => sudokufields[x.row, x.column] = new SudokuField(x.value));
            return new SudokuPuzzle(sudokufields);
        }

        #endregion Methods
    }
}
