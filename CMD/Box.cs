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
            double remainderWidth = 0;
            double remainderHeight = 0;
            string widthPieceCount = "";
            string heightPieceCount = "";
            Dictionary<double, int> widthDict = new Dictionary<double, int>();
            Dictionary<double, int> heightDict = new Dictionary<double, int>();

            foreach (double piece in inv.Reverse()) 
            {
                // need to check width/height divided by all sizes (verify no pieces can be used full dimension)
                // update size after each piece

                int widthCount = (int)Math.Floor(boxWidth / piece);
                int heightCount = (int)Math.Floor(boxHeight / piece);
                remainderWidth = boxWidth % piece;
                remainderHeight = boxHeight % piece;

                if (Math.Abs(remainderWidth) > 1e-9)
                {
                    widthDict.Add(piece, widthCount);
                }

                if (Math.Abs(remainderHeight) > 1e-9)
                {
                    heightDict.Add(piece, heightCount);
                }
            }

            foreach (KeyValuePair<double, int> item in widthDict)
            {
                widthPieceCount += $"Piece: {item.Key}\" : Qty {item.Value}\n";
            }

            foreach (KeyValuePair<double, int> item in heightDict)
            {
                heightPieceCount += $"Piece: {item.Key}\" : Qty {item.Value}\n";
            }

            Console.WriteLine($"Pieces consumed for a {boxWidth}\" x {boxHeight}\" box:\n");
            Console.WriteLine("Width:");
            Console.WriteLine(widthPieceCount + "\n");
            Console.WriteLine("Height:");
            Console.WriteLine(heightPieceCount + "\n");
            if (remainderWidth != 0)
                Console.WriteLine($"Custom width needed: {remainderWidth}\"");
            if (remainderHeight != 0)
                Console.WriteLine($"Custom height needed: {remainderHeight}\"");
        }
    }
}
