using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator.Interfaces
{   
    public interface IMatrixSource
    {
        double[,] Generate(int rows, int cols);
    }
}