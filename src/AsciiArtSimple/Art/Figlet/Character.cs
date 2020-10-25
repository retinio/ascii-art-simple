using System.Collections.Generic;

namespace AsciiArtSimple.Art.Figlet
{
    public class Character
    {
        private readonly List<string> _rows;

        /// <summary>
        /// Return a collection of element by index row
        /// </summary>
        /// <param name="row">Row index</param>
        /// <returns></returns>
        public string this[int row] => _rows[row];

        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="rows">Coolection of row</param>
        public Character(IEnumerable<string> rows)
        {
            _rows = new List<string>(rows);
        }
    }
}