using System;
using System.Threading.Channels;

namespace Extrusion_Calculator
{
    public class Program()
    {
        private static string? _input;

        static void Main()
        {
            InventoryManager.SetupInventory();

            Console.WriteLine("Type help to see all commands");
            Console.WriteLine("Return nothing to exit");

            MainLoop();
        }

        static void MainLoop()
        {
            do
            {
                _input = Console.ReadLine();
                CommandManager.RunCommand(_input);
            } while (!String.IsNullOrWhiteSpace(_input));
        }
    }
}