using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    /// <summary>
    /// An mxn logical matrix of chars
    /// </summary>
    public class WordPuzzle
    {
        readonly string _puzzle;
        readonly Dictionary<char, int[]> _indicesOfLetters;

        public WordPuzzle(string puzzle, int logicalWidth)
        {
            ValidateCtorParams(puzzle, logicalWidth);

            _puzzle = puzzle.Trim().ToUpper();

            Width = logicalWidth;
            Height = _puzzle.Length / logicalWidth;

            _indicesOfLetters = GetLettersLocations(_puzzle);
        }

        public int Height { get; }

        public int Width { get; }

        public WordLocation? FindWord(string word)
        {
            ValidateWord(word);

            word = NormalizeWord(word);

            int[] possibleLocationIndices = GetStartIndicesOfPossibleLocations(word);

            foreach (int startIndex in possibleLocationIndices)
            {
                Coordinate coordinate = ConvertToCoordinate(startIndex);
                Directions directions = GetPossibleDirections(coordinate, word);

                if (RightIsPossible(directions))
                {
                    if (WordExists(word, IndexOffsetter.LeftToRight, startIndex))
                        return new WordLocation(coordinate, WordDirection.LeftToRight);

                    if (DownIsPossible(directions))
                    {
                        if (WordExists(word, IndexOffsetter.DiagonalDownRight, startIndex))
                            return new WordLocation(coordinate, WordDirection.DiagonalDownRight);
                    }

                    if (UpIsPossible(directions))
                    {
                        if (WordExists(word, IndexOffsetter.DiagonalUpRight, startIndex))
                            return new WordLocation(coordinate, WordDirection.DiagonalUpRight);
                    }
                }

                if (LeftIsPossible(directions))
                {
                    if (WordExists(word, IndexOffsetter.RightToLeft, startIndex))
                        return new WordLocation(coordinate, WordDirection.RightToLeft);

                    if (DownIsPossible(directions))
                    {
                        if (WordExists(word, IndexOffsetter.DiagonalDownLeft, startIndex))
                            return new WordLocation(coordinate, WordDirection.DiagonalDownLeft);
                    }
                    if (UpIsPossible(directions))
                    {
                        if (WordExists(word, IndexOffsetter.DiagonalUpLeft, startIndex))
                            return new WordLocation(coordinate, WordDirection.DiagonalUpLeft);
                    }
                }

                if (UpIsPossible(directions))
                {
                    if (WordExists(word, IndexOffsetter.Upward, startIndex))
                        return new WordLocation(coordinate, WordDirection.Upward);
                }

                if (DownIsPossible(directions))
                {
                    if (WordExists(word, IndexOffsetter.Downward, startIndex))
                        return new WordLocation(coordinate, WordDirection.Downward);
                }

            }

            return null;
        }

        private static Dictionary<char, int[]> GetLettersLocations(string puzzle)
        {
            var locations = new Dictionary<char, int[]>();
            var chars = puzzle.ToCharArray();

            // Assumes that only letters in alphabet
            for (var i = 'A'; i <= 'Z'; i++)
            {
                int charIndex = -1;
                var indices = new List<int>();
                do
                {
                    charIndex = Array.FindIndex(chars, charIndex + 1, c => c == i);
                    indices.Add(charIndex);
                }
                while (charIndex > -1);

                if (indices.Count() > 1)
                {
                    indices.Remove(-1);
                    locations.Add(i, indices.ToArray());
                }
            }

            return locations;
        }

        private static void ValidateCtorParams(string puzzleContent, int width)
        {
            if (string.IsNullOrEmpty(puzzleContent))
                throw new ArgumentException("Word puzzle is empty or null.");

            if (width <= 0)
                throw new ArgumentException("Width must be greather than 0.");

            if (NotLogicalSquareMatrix(puzzleContent, width))
                throw new ArgumentException("Char array is not implied as square matrix. Its length should be divisible by the width.");
        }

        private static bool NotLogicalSquareMatrix(string puzzleContent, int width)
        {
            return puzzleContent.Length % width > 0;
        }

        private static void ValidateWord(string word)
        {
            if (word?.Trim().Length < 2)
                throw new ArgumentException("Word must be 2-char long.");
        }

        private static string NormalizeWord(string word)
        {
            word = word.Trim().ToUpper();
            return word;
        }

        private static bool UpIsPossible(Directions directions)
        {
            return directions.Contains(Directions.Up);
        }

        private static bool DownIsPossible(Directions directions)
        {
            return directions.Contains(Directions.Down);
        }

        private static bool LeftIsPossible(Directions directions)
        {
            return directions.Contains(Directions.Left);
        }

        private static bool RightIsPossible(Directions directions)
        {
            return directions.Contains(Directions.Right);
        }

        private int[] GetStartIndicesOfPossibleLocations(string wordToLocate)
        {
            char firstLetter = wordToLocate[0];
            if (!_indicesOfLetters.ContainsKey(firstLetter))
                return Array.Empty<int>();
            return _indicesOfLetters[firstLetter];
        }

        private Coordinate ConvertToCoordinate(int index)
        {
            int m = index / Width;
            int n = index % Width;

            return new Coordinate { M = m, N = n };
        }

        private Directions GetPossibleDirections(Coordinate start, string wordToSearch)
        {
            Directions directions = Directions.None;

            var elementsLength = wordToSearch.Trim().Length - 1;

            if (start.N + elementsLength <= Width - 1)
                directions |= Directions.Right;
            if (start.N - elementsLength >= 0)
                directions |= Directions.Left;
            if (start.M + elementsLength <= Height - 1)
                directions |= Directions.Down;
            if (start.M - elementsLength >= 0)
                directions |= Directions.Up;

            return directions;
        }

        private bool WordExists(string word, IIndexOffsetter indexOffsetter, int searchStartIndex)
        {
            int puzzleIndex = searchStartIndex;
            int lenWord = word.Length;

            for (int i = 1; i < lenWord; i++)
            {
                puzzleIndex = indexOffsetter.Offset(puzzleIndex, Width);

                if (_puzzle[puzzleIndex] != word[i])
                    return false; 
            }

            return true;
        }
    }
}