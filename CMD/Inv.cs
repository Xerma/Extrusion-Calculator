using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Inv
    {
        public static void InvCommand(SortedSet<InventoryPiece> inv)
        {
            if (inv.Count == 0)
                Console.WriteLine("Inventory: (empty)");
            else
            {
                ConsoleHelper.WriteThickLine();
                Console.WriteLine("Inventory:");
                ConsoleHelper.WriteThinLine();
                for (int i = 0; i < inv.Count; i++)
                {
                    Console.WriteLine(
                        $"{inv.ElementAt(i).Length}\" " +
                        $"{char.ToUpperInvariant(inv.ElementAt(i).End1)}/" +
                        $"{char.ToUpperInvariant(inv.ElementAt(i).End2)}");
                }
                ConsoleHelper.WriteThickLine();
            }
        }
    }
}
