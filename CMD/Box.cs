using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Box
    {
        private const double Epsilon = 1e-9;
        private const double MinCustom = 24.0;

        public static void BoxCommand(double[] d, SortedSet<double> inv)
        {
            double boxWidth = d[0];
            double boxHeight = d[1];

            var (widthPieces, remainderWidth) = PlanPiecesForDimension(boxWidth, inv);
            var (heightPieces, remainderHeight) = PlanPiecesForDimension(boxHeight, inv);

            Console.WriteLine($"Pieces needed for a {boxWidth}\" x {boxHeight}\" box:\n");

            Console.WriteLine("Width:");
            PrintPiecesForDimension(boxWidth, widthPieces, remainderWidth);

            Console.WriteLine();

            Console.WriteLine("Height:");
            PrintPiecesForDimension(boxHeight, heightPieces, remainderHeight);

            Console.WriteLine();
        }

        private static void PrintPiecesForDimension(double dimension, List<double> pieces, double remainder)
        {
            if (pieces.Count == 0)
            {
                Console.WriteLine("No standard pieces used.");
            }
            else
            {
                var grouped = pieces
                    .GroupBy(x => x)
                    .OrderByDescending(g => g.Key);

                foreach (var g in grouped)
                {
                    Console.WriteLine($"Piece: {g.Key}\"  Qty: {g.Count()}");
                }
            }

            if (remainder > Epsilon)
                Console.WriteLine($"Custom piece needed: {remainder}\"");
        }

        private static List<double>? FindExactCombination(double dimension, SortedSet<double> inventory)
        {
            var sizes = inventory.OrderByDescending(x => x).ToArray();
            var best = (List<double>?)null;
            var current = new List<double>();

            void Backtrack(double remaining, int startIndex)
            {
                if (Math.Abs(remaining) < Epsilon)
                {
                    if (best == null || current.Count < best.Count)
                        best = new List<double>(current);
                    return;
                }

                if (remaining < -Epsilon) return;

                for (int i = startIndex; i < sizes.Length; i++)
                {
                    double piece = sizes[i];
                    if (piece > remaining + Epsilon) continue;

                    current.Add(piece);
                    Backtrack(remaining - piece, i);
                    current.RemoveAt(current.Count - 1);
                }
            }

            Backtrack(dimension, 0);
            return best;
        }

        private static (List<double> pieces, double remainder)
            PlanPiecesForDimension(double dimension, SortedSet<double> inventory)
        {
            var exact = FindExactCombination(dimension, inventory);
            if (exact != null)
            {
                return (exact, 0);
            }

            foreach (double piece in inventory.Reverse())
            {
                if (piece <= 0)
                    continue;

                int maxCount = (int)Math.Floor(dimension / piece);
                if (maxCount <= 0)
                    continue;

                for (int count = maxCount; count >= 1; count--)
                {
                    double used = piece * count;
                    double remainder = dimension - used;

                    if (Math.Abs(remainder) < Epsilon)
                    {
                        var list = Enumerable.Repeat(piece, count).ToList();
                        return (list, 0);
                    }

                    if (remainder >= MinCustom - Epsilon)
                    {
                        var list = Enumerable.Repeat(piece, count).ToList();
                        return (list, remainder);
                    }
                }
            }

            return (new List<double>(), dimension);
        }
    }
}
