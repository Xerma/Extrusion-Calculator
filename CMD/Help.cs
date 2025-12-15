using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator.CMD
{
    public class Help
    {
        public static void HelpCommand()
        {
            ConsoleHelper.WriteThickLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("AVAILABLE COMMANDS:");
            ConsoleHelper.WriteThinLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'box' or 'b'");
            Console.WriteLine("Calculate what pieces are needed to form a box");
            Console.WriteLine("Example: box 48 x 96");
            ConsoleHelper.WriteThinLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'inv' or 'i'");
            Console.WriteLine("Lists all current pieces in inventory");
            ConsoleHelper.WriteThinLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'add' or 'a'");
            Console.WriteLine("Add a new size to inventory");
            Console.WriteLine("Example: add 19.53 f/m");
            ConsoleHelper.WriteThinLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'del' or 'd'");
            Console.WriteLine("Delete a size from inventory");
            Console.WriteLine("Example: del 19.53");
            ConsoleHelper.WriteThinLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'Clear' or 'c'");
            Console.WriteLine("Clears the console");
            ConsoleHelper.WriteThickLine();
            Console.ResetColor();
        }
    }
}
