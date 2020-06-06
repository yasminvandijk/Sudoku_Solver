using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    class SudokuCell
    {
        public readonly int Row;
        public readonly int Column;
        public readonly int Block;
        
        public int Value { get; private set; }
        public List<int> PossibleValues { get; private set; } = new List<int>();

        public SudokuCell(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Block = (int)(3 * Math.Floor(row / 3.0) + Math.Floor(column / 3.0));
            Value = value;

            if (value == 0)
            {
                for(int i = 1; i <= 9; i++)
                {
                    PossibleValues.Add(i);
                }
            }
        }
    }
}
