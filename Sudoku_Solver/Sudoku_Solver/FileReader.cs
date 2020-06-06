using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    class FileReader
    {
        /// <summary>
        /// reads all lines from the file at the given filepath, returns null if file contents can't be read
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string[] ReadFromFile(string filepath)
        {
            try
            {
                return System.IO.File.ReadAllLines(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"unable to read from {filepath}");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
