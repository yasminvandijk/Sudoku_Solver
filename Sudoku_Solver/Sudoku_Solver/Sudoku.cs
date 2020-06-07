using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Solve()
        {
            // for each fixed cell, update possible values for empty cells in the same row, column or block
            _fixedCells.ForEach(fixedCell =>
            {
                UpdatePossibleValues(fixedCell);
            });

            while (_emptyCells.Count > 0)
            {
                // move all cells that now have a value from the empty cells to the fixed cells
                List<SudokuCell> newFixedCells = _emptyCells.Where(c => c.Value != 0).ToList();

                int updates = 0;

                // eliminate possible values in empty cells                
                newFixedCells.ForEach(cell =>
                {
                    updates += UpdatePossibleValues(cell);
                    _fixedCells.Add(cell);
                    _emptyCells.Remove(cell);
                    _values[cell.Row, cell.Column] = cell.Value;
                });

                // fill in missing values in rows, columns and blocks
                for (int i = 0; i < 9; i++)
                {
                    updates += FillInMissingValues(_rows[i]);
                    updates += FillInMissingValues(_columns[i]);
                    updates += FillInMissingValues(_blocks[i]);
                }

                if (updates == 0)
                {
                    break;
                }
            }

            // check if sudoku is solved
            return _emptyCells.Count == 0 && IsSudokuValid();
        }

        /// <summary>
        /// checks if values in rows, columns and blocks are unique (except for 0)
        /// </summary>
        /// <returns></returns>
        private bool IsSudokuValid()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsListValid(_rows[i]) || !IsListValid(_columns[i]) || !IsListValid(_blocks[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///  checks if every value in the list is unique (except for 0)
        /// </summary>
        /// <returns></returns>
        private bool IsListValid(List<SudokuCell> cells)
        {
            List<int> nonZeroValues = cells.Where(c => c.Value != 0).Select(c => c.Value).ToList();

            return nonZeroValues.Count == nonZeroValues.Distinct().Count();
        }

        /// <summary>
        /// removes the value from the given cell from possible values in cells in same row, column or block
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private int UpdatePossibleValues(SudokuCell cell)
        {
            int updates = 0;
            
            _rows[cell.Row].ForEach(c => updates += c.RemoveFromPossibleValues(cell.Value));
            _columns[cell.Column].ForEach(c => updates += c.RemoveFromPossibleValues(cell.Value));
            _blocks[cell.Block].ForEach(c => updates += c.RemoveFromPossibleValues(cell.Value));

            return updates;
        }

        /// <summary>
        /// fill in missing values in a row, column or block if possible
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        private int FillInMissingValues(List<SudokuCell> cells)
        {
            int updates = 0;

            List<int> missingValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            cells.ForEach(c => missingValues.RemoveAll(v => v == c.Value));

            missingValues.ForEach(v =>
            {
                List<SudokuCell> possibleCells = cells.Where(c => c.PossibleValues.Contains(v)).ToList();

                if (possibleCells.Count() == 1)
                {
                    updates += possibleCells.First().SetValue(v);
                }
            });

            return updates;
        }
    }
}
