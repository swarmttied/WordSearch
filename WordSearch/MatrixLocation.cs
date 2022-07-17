using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public class MatrixLocation
    {
        public MatrixLocation(Coordinate start, IEnumerable<WordDirection> directions)
        {
            Start = start;
            Directions = directions;
        }
       
        public Coordinate Start { get; private set; }

        public IEnumerable<WordDirection> Directions { get; private set; }
        
    }
}
