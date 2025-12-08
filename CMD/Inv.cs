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
        public static void InvCommand(SortedSet<double> inv)
        {
            Console.WriteLine("Inventory: " + string.Join(", ", inv.Select(x => $"{x}\"")));
        }
    }
}
