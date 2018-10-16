using Sudoku.UI.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI
{
    public class AppComponents
    {
        #region Members

        private static MainViewModel _main;
        public static MainViewModel Main
        {
            get { return _main; }
            set { _main = value; }
        }

        #endregion Members
    }
}
