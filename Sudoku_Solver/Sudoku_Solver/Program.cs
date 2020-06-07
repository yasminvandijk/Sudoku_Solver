using System;
using System.Diagnostics;

namespace Sudoku_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                // get filepath from input and read it's contents
                Console.WriteLine("Enter path to text file:");

                string filepath = Console.ReadLine();

                string[] lines = FileReader.ReadFromFile(filepath);

                if (lines != null)
                {
                    // print contents from file
                    Console.WriteLine("\nContents read from file:\n");
                    
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }

                    Console.WriteLine();

                    // parse text to sudoku grid
                    int[,] numbers = SudokuParser.ParseText(lines);

                    if (numbers != null)
                    {
                        Console.WriteLine("succesfully parsed sudoku\n");

                        // create sudoku object
                        Sudoku sudoku = new Sudoku(numbers);

                        sudoku.PrintSudoku();

                        Console.WriteLine("solving sudoku...");

                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        bool solved = sudoku.Solve();

                        stopwatch.Stop();
                        
                        if (solved)
                        {
                            Console.WriteLine($"solution found in {stopwatch.ElapsedMilliseconds} milliseconds");
                        }
                        else
                        {
                            Console.WriteLine($"no solution found in {stopwatch.ElapsedMilliseconds} milliseconds");
                        }

                        sudoku.PrintSudoku();
                    }
                }
            }
        }
    }
}
