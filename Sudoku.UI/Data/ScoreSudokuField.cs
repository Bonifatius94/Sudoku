using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sudoku.UI.Data
{
    public class ScoreSudokuField
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

        [XmlArray]
        public bool[] possibilities { get; set; } = new bool[9];

        #endregion Members
    }
}
