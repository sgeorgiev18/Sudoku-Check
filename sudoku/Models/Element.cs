using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku.Model
{
    public class Element
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Element(int value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
        }
    }
}
