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
        public ScoreSudokuPuzzle(ISudokuPuzzle sudoku) { Serialize(sudoku); }

        #endregion Constructor

        #region Members

        [XmlArray]
        public List<ScoreSudokuField> fields { get; set; } = new List<ScoreSudokuField>();

        #endregion Members

        #region Methods

        public void Serialize(ISudokuPuzzle sudoku)
        {
            var indices = Enumerable.Range(0, 81).Select(i => new { row = i / 9, col = i % 9 });
            var values = indices.Select(x => new { row = x.row, col = x.col, value = sudoku.GetValue(x.row, x.col) });
            values.ToList().ForEach(x => fields.Add(new ScoreSudokuField() {
                row = x.row,
                column = x.col,
                value = x.value,
                possibilities = new bool[9] { true, true, true, true, true, true, true, true, true }
            }));
        }

        public ISudokuPuzzle Deserialize()
        {
            var digits = fields.Select(x => x.value).ToArray();
            return SudokuFactory.CreatePuzzle(digits);
        }

        #endregion Methods
    }
}
