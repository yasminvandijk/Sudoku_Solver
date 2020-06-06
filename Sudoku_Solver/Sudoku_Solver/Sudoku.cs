using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    class Sudoku
    {
        private int[,] _values = new int[9, 9];

        private List<SudokuCell>[] _rows = new List<SudokuCell>[9];
        private List<SudokuCell>[] _columns = new List<SudokuCell>[9];
        private List<SudokuCell>[] _blocks = new List<SudokuCell>[9];

        private List<SudokuCell> _fixedCells = new List<SudokuCell>();
        private List<SudokuCell> _emptyCells = new List<SudokuCell>();
        
        public Sudoku(int[,] values)
        {
            // check if given sudoku is not empty
            if (values == null)
            {
                throw new Exception("can't initialize sudoku from empty array");
            }

            // check if given sudoku is valid
            if (values.GetLength(0) != 9 || values.GetLength(1) != 9)
            {
                throw new Exception("sudoku dimensions should be 9 by 9");
            }

            // initialize lists for sudoku cells
            for (int i = 0; i < 9; i++)
            {
                _rows[i] = new List<SudokuCell>();
                _columns[i] = new List<SudokuCell>();
                _blocks[i] = new List<SudokuCell>();
            }

            // store values
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int value = values[x, y];
                    
                    // check if value is valid
                    if (value < 0 || value > 9)
                    {
                        throw new Exception($"invalid value: {value}");
                    }
                    
                    _values[x, y] = value;

                    SudokuCell cell = new SudokuCell(x, y, value);

                    _rows[cell.Row].Add(cell);
                    _columns[cell.Column].Add(cell);
                    _blocks[cell.Block].Add(cell);

                    if (value == 0)
                    {
                        _emptyCells.Add(cell);
                    }
                    else
                    {
                        _fixedCells.Add(cell);
                    }
                }
            }
        }

        public void PrintSudoku()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Console.Write(_values[x, y] + " ");

                    if (x % 3 == 2) { Console.Write(" "); }
                }

                Console.WriteLine();

                if (y % 3 == 2) { Console.WriteLine(" "); }
            }

            Console.WriteLine();
        }
    }
}
