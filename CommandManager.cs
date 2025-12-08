using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Extrusion_Calculator.CMD;

namespace Extrusion_Calculator
{
    public class CommandManager
    {
        private static readonly List<string> CommandSizeList = [ "add", "a", "del", "d", "box", "b" ];
        private static readonly List<string> CommandNoSizeList = ["help", "h", "inv", "i", "clear", "c"];

        public static void RunCommand(string input, SortedSet<double> invList)
        {
            string command = GetCommand(input).Item1;
            string commandParsed;
            double sizeParsed = 0;
            double[] boxDims = [];

            if (IsBoxCommand(command))
            {
                DoubleArrayCommand doubleArrayCommandParsed = ParseBoxCommand(input);
                commandParsed = doubleArrayCommandParsed.Command;
                boxDims = doubleArrayCommandParsed.Dims;
            }
            else
            {
                DoubleCommand doubleCommandParsed = ParseCommand(input);
                commandParsed = doubleCommandParsed.Command;
                sizeParsed = doubleCommandParsed.Size;
            }

            switch (commandParsed)
            {
                case "help":
                    Help.HelpCommand();
                    break;
                case "h":
                    Help.HelpCommand();
                    break;
                case "add":
                    invList.Add(sizeParsed);
                    break;
                case "a":
                    invList.Add(sizeParsed);
                    break;
                case "del":
                    if (invList.Contains(sizeParsed)) { invList.Remove(sizeParsed); }
                    break;
                case "d":
                    if (invList.Contains(sizeParsed)) { invList.Remove(sizeParsed); }
                    break;
                case "inv":
                    Inv.InvCommand(invList);
                    break;
                case "i":
                    Inv.InvCommand(invList);
                    break;
                case "clear":
                    Console.Clear();
                    Program.MainWrite();
                    break;
                case "c":
                    Console.Clear();
                    Program.MainWrite();
                    break;
                case "box":
                    Box.BoxCommand(boxDims, invList);
                    break;
                case "b":
                    Box.BoxCommand(boxDims, invList);
                    break;
                default:
                    Console.Write("");
                    break;
            }
        }

        public static DoubleCommand ParseCommand(string input)
        {
            (string, string[]) commandTuple = GetCommand(input);
            string command = commandTuple.Item1;
            string[] splitItems = commandTuple.Item2;
            double size = 0;

            if (splitItems.Length > 1 && CommandNoSizeList.Contains(command))
                Console.WriteLine($"Command Error: No size needed for command '{command}'");
            else if (splitItems.Length == 1 && CommandSizeList.Contains(command))
                Console.WriteLine($"Command Error: Command '{command}' needs a size");
            else if (splitItems.Length == 2 && CommandSizeList.Contains(command))
            {
                if (!double.TryParse(splitItems[1], out size))
                    Console.WriteLine("Size Error: Ensure the size only contains numbers");
            }

            DoubleCommand r = new(command, size);
            return r;
        }

        private static DoubleArrayCommand ParseBoxCommand(string input)
        {
            string[] splitItems = GetCommand(input).Item2;
            double[] boxDims = [0, 0];

            for (int i = 0; i < splitItems[1].Length; i++)
            {
                if (splitItems[i].ToLower() == "x")
                {
                    if (double.TryParse(splitItems[i - 1], out boxDims[0]))
                        Console.WriteLine("Size Error: Ensure the size only contains numbers");
                    if (double.TryParse(splitItems[i + 1], out boxDims[1]))
                        Console.WriteLine("Size Error: Ensure the size only contains numbers");
                }
            }

            DoubleArrayCommand r = new(splitItems[0], boxDims);
            return r;
        }

        private static bool IsBoxCommand(string command)
        {
            return command == "box" || command == "b";
        }

        private static string[] SplitInput(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<string>();

            string[] inputSplit = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return inputSplit;
        }

        private static (string, string[]) GetCommand(string input)
        {
            string[] inputSplit = SplitInput(input);
            return (inputSplit[0].ToLower(), SplitInput(input));
        }
    }
}
