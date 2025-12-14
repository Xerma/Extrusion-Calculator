using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Help
    {
        private const string ThickLine = "==================================================";
        private const string ThinLine = "--------------------------------------------------";

        public static void HelpCommand()
        {
            Console.WriteLine(ThickLine);
            Console.WriteLine("AVAILABLE COMMANDS:");
            Console.WriteLine(ThickLine);
            Console.WriteLine("'box' or 'b'");
            Console.WriteLine("Calculate what pieces are needed to form a box");
            Console.WriteLine("Example: box 48 x 96");
            Console.WriteLine(ThinLine);
            Console.WriteLine("'inv' or 'i'");
            Console.WriteLine("Lists all current pieces in inventory");
            Console.WriteLine(ThinLine);
            Console.WriteLine("'add' or 'a'");
            Console.WriteLine("Add a new size to inventory");
            Console.WriteLine("Example: add 19.53");
            Console.WriteLine(ThinLine);
            Console.WriteLine("'del' or 'd'");
            Console.WriteLine("Delete a size from inventory");
            Console.WriteLine("Example: del 19.53");
            Console.WriteLine(ThinLine);
            Console.WriteLine("'Clear' or 'c'");
            Console.WriteLine("Clears the console");
            Console.WriteLine(ThickLine);

        }
    }
}
