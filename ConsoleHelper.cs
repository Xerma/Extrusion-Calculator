using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public class ConsoleHelper
    {
        private const string ThickLine = "==================================================";
        private const string ThinLine = "--------------------------------------------------";

        public static void WriteThickLine()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(ThickLine);
            Console.ResetColor();
        }

        public static void WriteThinLine()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(ThinLine);
            Console.ResetColor();
        }

        public static void MainWrite()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Type 'help' or 'h' to see all commands    |    Return nothing to exit");
            Console.ResetColor();
        }

        public static void NotEnoughEndsWarn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Args Error: Missing ends on piece");
            Console.ResetColor();
        }

        public static void CommandNeedsArgsWarn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Command Error: Missing args");
            Console.ResetColor();
        }

        public static void SizeNumbersOnlyWarn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Args Error: Size must only contains numbers");
            Console.ResetColor();
        }
    }
}
