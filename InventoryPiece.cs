using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public enum EndPattern
    {
        FlatFlat,
        FlatMiter,
        MiterMiter
    }

    public class InventoryPiece : IComparable<InventoryPiece>
    {
        public double Length { get; set; }

        public char End1 { get; set; } = 'F';
        public char End2 { get; set; } = 'F';

        public InventoryPiece() { }

        public InventoryPiece(double length, char end1, char end2)
        {
            Length = length;
            End1 = char.ToUpperInvariant(end1);
            End2 = char.ToUpperInvariant(end2);
        }

        public int CompareTo(InventoryPiece? other)
        {
            if (other is null) return 1;

            int cmp = Length.CompareTo(other.Length);
            if (cmp != 0) return cmp;

            cmp = char.ToUpperInvariant(End1).CompareTo(char.ToUpperInvariant(other.End1));
            if (cmp != 0) return cmp;

            return char.ToUpperInvariant(End2).CompareTo(char.ToUpperInvariant(other.End2));
        }
    }
}
