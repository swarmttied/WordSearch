namespace WordSearch
{
    public class WordLocation
    {
        public WordLocation(Coordinate coordinate, WordDirection direction)
        {
            Coordinate = coordinate;
            Direction = direction;
        }

        public Coordinate Coordinate { get; }
        public WordDirection Direction { get; }
        public override string ToString()
        {
            return $"{Coordinate} {Direction}";
        }
    }
}
