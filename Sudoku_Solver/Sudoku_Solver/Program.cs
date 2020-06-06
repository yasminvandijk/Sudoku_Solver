using System;

namespace Sudoku_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("Enter path to input file:");

                string filepath = Console.ReadLine();

                string[] lines = FileReader.ReadFromFile(filepath);

                if (lines != null)
                {
                    Console.WriteLine("\nContents read from file:\n");
                    
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
