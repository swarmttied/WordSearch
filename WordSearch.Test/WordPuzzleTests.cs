using Xunit;

namespace WordSearch.Tests
{
    public class WordPuzzleTests
    {
        [Fact]
        public void Ctor_When_Puzzle_Arg_IsEmpty_Throws()
        {
            string puzzle = "";
            int width = 2;
            Assert.Throws<ArgumentException>(() =>
            {
                var target = new WordPuzzle(puzzle, width);
            });
        }

        [Fact]
        public void Ctor_When_Width_Arg_IsLessThanOrEqualsZero_Throws()
        {
            string puzzle = "abcd";
            int width = 0;
            Assert.Throws<ArgumentException>(() =>
            {
                var target = new WordPuzzle(puzzle, width);
            });
        }

        [Fact]
        public void Ctor_When_Puzzle_Arg_IsNot_Implied_As_Matrix_Throws()
        {
            string puzzle = "abcd";
            int width = 3;
            Assert.Throws<ArgumentException>(() =>
            {
                var target = new WordPuzzle(puzzle, width);
            });

        }

        [Fact]
        public void Ctor_With_MxN_Matrix_Should_Set_Width_And_Height()
        {
            string puzzle = "abcdef";
            int width = 3;
            var target = new WordPuzzle(puzzle, width);

            Assert.Equal(width, target.Width);
            Assert.Equal(2, target.Height);
        }

        [Fact]
        public void Ctor_With_1xN_Matrix()
        {
            string puzzle = "abcd";
            int width = 4;
            var target = new WordPuzzle(puzzle, width);

            Assert.Equal(width, target.Width);
            Assert.Equal(1, target.Height);
        }

        [Fact]
        public void Ctor_With_Mx1_Matrix()
        {
            string puzzle = "abcd";
            int width = 1;
            var target = new WordPuzzle(puzzle, width);

            Assert.Equal(width, target.Width);
            Assert.Equal(4, target.Height);
        }


        [Fact]
        public void FindWord_Gio_Right()
        {
            string puzzle = "gIoAGObCD";

            var target = new WordPuzzle(puzzle, 3);
            var res = target.FindWord("GIO")!;

            Assert.Equal(0, res.Coordinate.M);
            Assert.Equal(0, res.Coordinate.N);
            Assert.Equal(WordDirection.LeftToRight, res.Direction);
        }

        [Fact]
        public void FindWord_Gio_UpLeft()
        {
            string puzzle = "oRoAIObCG";

            var target = new WordPuzzle(puzzle, 3);
            var res = target.FindWord("GIO")!;

            Assert.Equal(2, res.Coordinate.M);
            Assert.Equal(2, res.Coordinate.N);
            Assert.Equal(WordDirection.DiagonalUpLeft, res.Direction);
        }

        [Fact]
        public void FindWord_Van_NotFound()
        {
            string puzzle = "oRoAIObCG";

            var target = new WordPuzzle(puzzle, 3);
            var res = target.FindWord("VAN");

            Assert.Null(res);
        }

        [Fact]
        public void FindWord_Van_FirstLetterPresent_NotFound()
        {
            string puzzle = "oRoAIObCG";

            var target = new WordPuzzle(puzzle, 3);
            var res = target.FindWord("GAN");

            Assert.Null(res);
        }

        static string puzzle = "ZRQKtBLTALOLKZnKLABchJXaRzo1lK";

        [Fact]
        public void FindWord_Kat_UpRight_Down()
        {
            var target = new WordPuzzle(puzzle, 5);

            var res = target.FindWord("KAT")!;

            Assert.Equal(WordDirection.DiagonalUpRight, res.Direction);
            Assert.Equal("(2,2)", res.Coordinate.ToString());
        }

        [Fact]
        public void FindWord_UpLeft()
        {
            var target = new WordPuzzle(puzzle, 5);

            var res = target.FindWord("LXLO")!;

            Assert.Equal("(5,3)", res.Coordinate.ToString());
            Assert.Equal(WordDirection.DiagonalUpLeft, res.Direction);
            
        }

        [Fact]
        public void FindWord_DownLeft()
        {
            var target = new WordPuzzle(puzzle, 5);

            var res = target.FindWord("nbxo")!;

            Assert.Equal("(2,4)", res.Coordinate.ToString());
            Assert.Equal(WordDirection.DiagonalDownLeft, res.Direction);

        }

        [Fact]
        public void FindWord_Left()
        {
            var target = new WordPuzzle(puzzle, 5);

            var res = target.FindWord("cbal")!;

            Assert.Equal("(3,4)", res.Coordinate.ToString());
            Assert.Equal(WordDirection.RightToLeft, res.Direction);

        }
    }
}
