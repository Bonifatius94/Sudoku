using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sudoku.Client.Data
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

        private static readonly string TEMP_SCORE_FILE = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp", "SudokuScore.xml");

        private const string XML_NODE_ROOT = "score";
        private const string XML_NODE_FIELD = "field";
        private const string XML_ATTRIBUTE_ROW = "row";
        private const string XML_ATTRIBUTE_COLUMN = "column";
        private const string XML_ATTRIBUTE_VALUE = "value";

        public static int[,] Matrix { get; set; }

        #endregion Members

        #region Methods

        public static void LoadData()
        {
            Matrix = new int[9, 9];

            if (File.Exists(TEMP_SCORE_FILE))
            {
                using (var reader = XmlReader.Create(TEMP_SCORE_FILE))
                {
                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            case XML_NODE_FIELD:

                                int row = int.Parse(reader.GetAttribute(XML_ATTRIBUTE_ROW));
                                int column = int.Parse(reader.GetAttribute(XML_ATTRIBUTE_COLUMN));
                                int value = int.Parse(reader.GetAttribute(XML_ATTRIBUTE_VALUE));
                                Matrix[row, column] = value;

                                break;
                        }
                    }
                }
            }
            else
            {
                if (!Directory.Exists(Path.GetDirectoryName(TEMP_SCORE_FILE)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(TEMP_SCORE_FILE));
                }

                SaveData();
            }
        }

        public static void SaveData()
        {
            using (var writer = XmlWriter.Create(TEMP_SCORE_FILE))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(XML_NODE_ROOT);

                for (int row = 0; row < 9; row++)
                {
                    for (int column = 0; column < 9; column++)
                    {
                        int value = Matrix[row, column];

                        if (value != 0)
                        {
                            writer.WriteStartElement(XML_NODE_FIELD);
                            writer.WriteAttributeString(XML_ATTRIBUTE_ROW, row.ToString());
                            writer.WriteAttributeString(XML_ATTRIBUTE_COLUMN, column.ToString());
                            writer.WriteAttributeString(XML_ATTRIBUTE_VALUE, value.ToString());
                            writer.WriteEndElement();
                        }
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #endregion Methods
    }
}
