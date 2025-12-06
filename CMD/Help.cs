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
            Console.WriteLine("Inv");
            Console.WriteLine("Lists all current pieces in inventory");
            Console.WriteLine(ThinLine);
            Console.WriteLine("Add");
            Console.WriteLine("Add a new size to inventory");
            Console.WriteLine("Example: Add 19.53");
            Console.WriteLine(ThinLine);
            Console.WriteLine("Del");
            Console.WriteLine("Delete a size from inventory");
            Console.WriteLine("Example: Del 19.53");
            Console.WriteLine(ThinLine);
            Console.WriteLine("Clear");
            Console.WriteLine("Clears the console");
            Console.WriteLine(ThickLine);

        }
    }
}
