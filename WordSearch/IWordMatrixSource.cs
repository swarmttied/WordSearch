using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public interface IWordMatrixSource
    {
        char[,] GetSource();
    }
}
