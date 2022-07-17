using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using WordSearch;

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
                int matrixWidth;
                string puzzleContent = GetPuzzle(puzzlePath, out matrixWidth);
                string[] words = GetWords(listPath);

                WordPuzzle puzzle = new WordPuzzle(puzzleContent, matrixWidth);

                foreach (string word in words)
                {
                    var res = puzzle.FindWord(word);
                    if (res != null)
                        WriteLine(res);
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


            WriteLine("\n\nRunning time: {0}", (DateTime.Now - startTime).TotalMilliseconds);

#if DEBUG
            WriteLine("\n\nPress any key...");
            ReadKey();
#endif
        }

        static string GetPuzzle(string path, out int matrixWidth)
        {
            using (var reader = new StreamReader(path))
            {
                string? firstLine = reader.ReadLine();
                matrixWidth = firstLine!.Length;
                string content = reader!.ReadToEnd().Replace("\r\n", string.Empty);
                content = string.Concat(firstLine, content);

                return content;
            }
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
