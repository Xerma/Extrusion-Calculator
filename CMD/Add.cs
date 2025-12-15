using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Add
    {
        public static void AddCommand(InventoryPiece invPiece, SortedSet<InventoryPiece> invList)
        {
            invList.Add(new InventoryPiece(invPiece.Length, invPiece.End1, invPiece.End2));
        }
    }
}
