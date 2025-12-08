using System;
using System.Threading.Channels;

namespace Extrusion_Calculator
{
    public class Program()
    {
        private static string? _input;
        public static SortedSet<double>? InventoryData;

        static void Main()
        {
            InventoryManager.SetupInventory();
            InventoryData = InventoryManager.LoadInventory();

            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                InventoryManager.SaveInventory(InventoryData);
            };
            MainWrite();
            MainLoop();
        }

        static void MainLoop()
        {
            do
            {
                _input = Console.ReadLine();
                CommandManager.RunCommand(_input, InventoryData);
            } while (!String.IsNullOrWhiteSpace(_input));
        }

        public static void MainWrite()
        {
            Console.WriteLine("Type help to see all commands");
            Console.WriteLine("Return nothing to exit");
        }
    }
}