using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public delegate int RowOffsetter(int index, int width);
    public delegate int ColumnOffsetter(int index);

    interface IIndexOffsetter
    {
        int Offset(int index, int width);
    }

    class IndexOffsetter : IIndexOffsetter
    {
        ColumnOffsetter _columnOffsetter;
        RowOffsetter _rowOffsetter;

        static ColumnOffsetter _moveRight = (index) => ++index;
        static ColumnOffsetter _moveLeft = (index) => --index;
        static ColumnOffsetter _fixedColumn = (index) => index;
        static RowOffsetter _moveDown = (index, width) => index + width;
        static RowOffsetter _moveUp = (index, width) => index - width;
        static RowOffsetter _fixedRow = (index, width) => index;

        private IndexOffsetter(RowOffsetter rowOffsetter, ColumnOffsetter colOffsetter)
        {
            _rowOffsetter = rowOffsetter;
            _columnOffsetter = colOffsetter;
        }

        public static IIndexOffsetter Upward
            => new IndexOffsetter(_moveUp, _fixedColumn);

        public static IIndexOffsetter Downward
            => new IndexOffsetter(_moveDown, _fixedColumn);

        public static IIndexOffsetter RightToLeft
            => new IndexOffsetter(_fixedRow, _moveLeft);

        public static IIndexOffsetter LeftToRight
            => new IndexOffsetter(_fixedRow, _moveRight);

        public static IndexOffsetter DiagonalDownLeft
            => new IndexOffsetter(_moveDown, _moveLeft);

        public static IndexOffsetter DiagonalUpLeft
            => new IndexOffsetter(_moveUp, _moveLeft);

        public static IndexOffsetter DiagonalUpRight
           => new IndexOffsetter(_moveUp, _moveRight);

        public static IndexOffsetter DiagonalDownRight
            => new IndexOffsetter(_moveDown, _moveRight);

        public int Offset(int index, int width)
        {
            var newIndex = _rowOffsetter(index, width);
            newIndex = _columnOffsetter(newIndex);
            return newIndex;
        }
           
        
    }

}
