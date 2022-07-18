using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using WordSearch;
using System.Text;

namespace WordPuzzleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /////////////////////////////////////
            /// Checks are lax on this one :D
            /// Not so much documentation either

            DateTime startTime = DateTime.Now;

            if (args.Length < 2)
            {
                WriteLine(_hintMessage);
#if DEBUG
                WriteLine("\n\nPress any key...");
                ReadKey();
#endif
                return;
            }

            string puzzlePath = args[0];
            string listPath = args[1];

            if (!File.Exists(puzzlePath))
            {
                WriteLine("ERROR! Puzzle file {0} is not found.", puzzlePath);
#if DEBUG
                WriteLine("\n\nPress any key...");
                ReadKey();
#endif
                return;
            }
            if (!File.Exists(listPath))
            {
                WriteLine("ERROR! List file {0} is not found.", listPath);
#if DEBUG
                WriteLine("\n\nPress any key...");
                ReadKey();
#endif
                return;
            }

            try
            {
                
                DisplayPuzzle(puzzlePath, out string[] puzzleLines);
                int matrixWidth = puzzleLines[0].Length;
                string puzzleContent = CreatePuzzleInput(puzzleLines);
                WriteLine(Environment.NewLine);
                string[] words = GetWords(listPath);

                WordPuzzle puzzle = new WordPuzzle(puzzleContent, matrixWidth);

                foreach (string word in words)
                {
                    var res = puzzle.FindWord(word);
                    if (res != null)
                        WriteLine($"{word} {res}");
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
#if DEBUG
                message += $"\n{ex.StackTrace}";
#endif
                WriteLine("ERROR: {0}", message);
            }


            WriteLine("\n\nRunning time: {0} ms", (DateTime.Now - startTime).TotalMilliseconds);

#if DEBUG
            WriteLine("\n\nPress any key...");
            ReadKey();
#endif
        }

        static void DisplayPuzzle(string path, out string[] puzzleLines)
        {
            List<string> lines = new();
            using (var reader = new StreamReader(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null) 
                {
                    lines.Add(line);
                    WriteLine(line);
                }               
            }
            puzzleLines = lines.ToArray();
        }

        static string CreatePuzzleInput(string[] puzzleLines)
        {
            StringBuilder sb = new();
            foreach (var line in puzzleLines)
                sb.Append(line);
            return sb.ToString();
        }

        static string[] GetWords(string listWordSource)
        {
            using (var reader = new StreamReader(listWordSource))
            {
                var list = new List<String>();
                string? line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }

                return list.ToArray();
            }
        }


#region NOTE: Leave the format as is!
        const string _hintMessage = @"Usage: wordpuzzle <puzzle path> <list path>
    Where:
            <puzzle path> is a text file containing the mxn chars puzzle
            <list path>   is a text file with list of words to find

    Examples:    wordpuzzle source.txt list.txt
                 wordpuzzle c:\puzzle.txt c:\search.txt

";
#endregion
    }
}
