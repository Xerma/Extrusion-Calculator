using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Box
    {
        private const double Epsilon = 1e-9;
        private const double MinCustom = 24.0;

        public static void BoxCommand(double[] d, SortedSet<InventoryPiece> inv)
        {
            if (inv.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Inventory is empty, add pieces first");
                Console.ResetColor();
                return;
            }

            double boxWidth = d[0];
            double boxHeight = d[1];

            SortedSet<double> lengthInv = new SortedSet<double>(inv.Select(p => p.Length));
            var (widthLengths, remainderWidth) = PlanPiecesForDimension(boxWidth, lengthInv);
            var (heightLengths, remainderHeight) = PlanPiecesForDimension(boxHeight, lengthInv);

            var widthPieces = AssignPiecesForSpan(widthLengths, inv);
            var heightPieces = AssignPiecesForSpan(heightLengths, inv);

            Console.WriteLine($"Pieces needed for a {boxWidth}\" x {boxHeight}\" box:\n");

            Console.WriteLine("Width:");
            PrintPiecesForDimension(widthPieces, remainderWidth);

            Console.WriteLine();

            Console.WriteLine("Height:");
            PrintPiecesForDimension(heightPieces, remainderHeight);

            Console.WriteLine();
        }

        private static bool ValidateEndsForSpan(
            List<double> pieces,
            SortedSet<InventoryPiece> inventory,
            string spanLabel)
        {
            if (pieces.Count < 2)
                return true;

            var candidates = inventory
                .Where(p => pieces.Contains(p.Length))
                .ToList();

            int piecesWithM = candidates.Count(p =>
                char.ToUpperInvariant(p.End1) == 'M' ||
                char.ToUpperInvariant(p.End2) == 'M');

            bool hasMM = candidates.Any(p =>
                char.ToUpperInvariant(p.End1) == 'M' &&
                char.ToUpperInvariant(p.End2) == 'M');

            if (piecesWithM >= 2 && hasMM)
                return true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                $"{spanLabel}: Not enough mitered pieces in inventory to satisfy corner rules.\n" +
                "  • Need at least 2 pieces with an M end\n" +
                "  • Need at least 1 piece that is M/M for this span.");
            Console.ResetColor();

            return false;
        }

        private static (List<double> pieces, double remainder) PlanPiecesForDimension(double dimension, SortedSet<double> inventory)
        {
            var exact = FindExactCombination(dimension, inventory);
            if (exact != null)
                return (exact, 0);

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

        private static void PrintPiecesForDimension(List<InventoryPiece> pieces, double remainder)
        {
            if (pieces.Count == 0)
                Console.WriteLine("No standard pieces used.");
            else
            {
                var grouped = pieces
                    .GroupBy(p => new { p.Length, E1 = char.ToUpperInvariant(p.End1), E2 = char.ToUpperInvariant(p.End2)})
                    .OrderByDescending(g => g.Key.Length);

                foreach (var g in grouped)
                {
                    Console.WriteLine($"Piece: {g.Key.Length}\" ({g.Key.E1}/{g.Key.E2}) Qty: {g.Count() * 2}");
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

        private static List<InventoryPiece> AssignPiecesForSpan(List<double> pieceLengths, SortedSet<InventoryPiece> inventory)
        {
            var result = new List<InventoryPiece>();

            if (pieceLengths.Count == 0)
                return result;

            var byLength = inventory
                .GroupBy(p => p.Length)
                .ToDictionary(g => g.Key, g => g.ToList());

            InventoryPiece MakePiece(double length, Func<InventoryPiece, bool> predicate,
                                     InventoryPiece? fallback = null)
            {
                if (byLength.TryGetValue(length, out var list))
                {
                    var match = list.FirstOrDefault(predicate);
                    if (match != null)
                    {
                        return new InventoryPiece(length, match.End1, match.End2);
                    }
                }

                if (fallback != null)
                    return new InventoryPiece(length, fallback.End1, fallback.End2);

                return new InventoryPiece(length, 'F', 'F');
            }

            if (pieceLengths.Count == 1)
            {
                double len = pieceLengths[0];

                var corner = MakePiece(
                    len,
                    p => char.ToUpperInvariant(p.End1) == 'M' && char.ToUpperInvariant(p.End2) == 'M'
                );

                result.Add(corner);
                return result;
            }

            int lastIndex = pieceLengths.Count - 1;

            double firstLen = pieceLengths[0];
            var firstCorner = MakePiece(
                firstLen,
                p => char.ToUpperInvariant(p.End1) == 'M' && char.ToUpperInvariant(p.End2) == 'M'
            );

            bool firstIsMM = char.ToUpperInvariant(firstCorner.End1) == 'M' &&
                             char.ToUpperInvariant(firstCorner.End2) == 'M';

            result.Add(firstCorner);

            for (int i = 1; i < lastIndex; i++)
            {
                double midLen = pieceLengths[i];

                var mid = MakePiece(
                    midLen,
                    p => char.ToUpperInvariant(p.End1) == 'F' && char.ToUpperInvariant(p.End2) == 'F'
                );

                result.Add(mid);
            }

            double lastLen = pieceLengths[lastIndex];

            InventoryPiece lastCorner;

            if (firstIsMM)
            {
                lastCorner = MakePiece(
                    lastLen,
                    p => char.ToUpperInvariant(p.End1) == 'M' || char.ToUpperInvariant(p.End2) == 'M',
                    new InventoryPiece(lastLen, 'M', 'M')
                );
            }
            else
            {
                lastCorner = MakePiece(
                    lastLen,
                    p => char.ToUpperInvariant(p.End1) == 'M' && char.ToUpperInvariant(p.End2) == 'M', 
                    new InventoryPiece(lastLen, 'M', 'F')
                );
            }

            result.Add(lastCorner);

            return result;
        }
    }
}
