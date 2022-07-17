using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    /// <summary>
    /// Flags enum that can represent 
    /// the flow of elements in an (mxn) matrix
    /// </summary>
    [Flags]
    public enum Directions
    {
        None = 0,
        Right = 1<<0,
        Left = 1<<1,
        Down = 1<<2,
        Up = 1<<3,
    }

    static public class DirectionExtensions
    {
        public static bool Contains(this Directions thisDirections, Directions direction)
            => (thisDirections & direction) == direction;
    }
}
