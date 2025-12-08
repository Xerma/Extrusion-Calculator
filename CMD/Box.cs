using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Box
    {
        public static void BoxCommand(double[] d, SortedSet<double> inv)
        {
            double boxWidth = d[0];
            double boxHeight = d[1];
            string widthPieces = "";
            string heightPieces = "";

            // start with last item (largest in sorted set)
            // if remainder, try next size, repeat
            // if remainder at the end, custom size needed
        }
    }
}
