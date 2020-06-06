using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    class SudokuParser
    {
        public static int[,] ParseText(string[] lines)
        {
            int[,] result = new int[9, 9];

            // check if lines is not null
            if (lines == null)
            {
                Console.WriteLine("unable to parse sudoku, no text supplied to parse");
                return null;
            }

            // remove all empty and whitespace lines
            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

            // check if lines contains exactly 9 lines
            if (lines.Length != 9)
            {
                Console.WriteLine("unable to parse sudoku, invalid number of rows");
                return null;
            }

            // parse every line
            for (int y = 0; y < lines.Length && y < 9; y++)
            {
                string line = lines[y];

                // split line on whitespace and remove empty strings
                string[] numbers = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                // check if numbers contains exactly 9 numbers
                if (numbers.Length != 9)
                {
                    Console.WriteLine($"unable to parse sudoku, invalid number of columns: {line}");
                    return null;
                }

                // parse every number
                for (int x = 0; x < 9; x++)
                {
                    if (int.TryParse(numbers[x], out int n))
                    {
                        // check if number is valid (between 0 and 9)
                        if (n >= 0 && n <= 9)
                        {
                            result[x, y] = n;
                        }
                        else
                        {
                            Console.WriteLine($"unable to parse sudoku, invalid number: {n}");
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"unable to parse sudoku, invalid character: {numbers[x]}");
                        return null;
                    }
                }
            }

            return result;
        }
    }
}
