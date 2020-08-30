using System;
using System.Diagnostics;

namespace Sudoku_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sudoku Solver";
            
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

                        Sudoku sudoku = new Sudoku();

                        for (int x = 0; x < 9; x++)
                        {
                            for (int y = 0; y < 9; y++)
                            {
                                sudoku.SetValue(x, y, numbers[x, y]);
                            }
                        }

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
