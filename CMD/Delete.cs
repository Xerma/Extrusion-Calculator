using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Delete
    {
        public static void DeleteCommand(InventoryPiece invPiece, SortedSet<InventoryPiece> invList)
        {
            if (invList.Contains(invPiece))
            {
                invList.Remove(invPiece);
            }
        }
    }
}
