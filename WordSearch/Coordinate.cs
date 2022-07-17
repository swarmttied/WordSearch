namespace WordSearch
{
    public struct Coordinate
    {
        public int M { get; set; }

        public int N { get; set; }

        public override string ToString()
        {
            return $"({M},{N})";
        }
    }
}
