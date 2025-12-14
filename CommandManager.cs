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
        private static readonly List<string> ArgsNeeded = [ "add", "a", "del", "d", "box", "b" ];
        private static readonly List<string> ArgsNotNeeded = ["help", "h", "inv", "i", "clear", "c"];

        public static void RunCommand(string input, SortedSet<double> invList)
        {
            ConsoleCommand fullCommand = ParseCommand(input);
            string command = fullCommand.Command;
            string[] args = fullCommand.Args;
            double sizeParsed = 0;
            double[] boxDims = [];

            if (IsBoxCommand(command))
            {
                boxDims = TryBoxDims(args);
            }
            else if (ArgsNeeded.Contains(command))
            {
                if (!double.TryParse(args[0], out sizeParsed))
                    Console.WriteLine("Size Error: Ensure the size only contains numbers");
            }

            switch (command)
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
                    Box.BoxCommand(boxDims, invList);            // box command gives needs a size error
                    break;
                case "b":
                    Box.BoxCommand(boxDims, invList);
                    break;
                default:
                    Console.Write("");
                    break;
            }
        }

        public static ConsoleCommand ParseCommand(string input)
        {
            ConsoleCommand fullCommand = InputToFullCommand(input);
            string command = fullCommand.Command;
            string[] args = fullCommand.Args;

            if (args.Length >= 1 && ArgsNotNeeded.Contains(command))
                Console.WriteLine($"Command Error: No size needed for command '{command}'");
            else if (args.Length != 1 && ArgsNeeded.Contains(command))
                Console.WriteLine($"Command Error: Command '{command}' needs a size");
            else if (args.Length == 3 && ArgsNeeded.Contains(command))
            {
                if (!double.TryParse(args[1], out _) && !IsBoxCommand(command))
                    Console.WriteLine("Size Error: Ensure the size only contains numbers");
            }

            return fullCommand;
        }

        private static double[] TryBoxDims(string[] commandArgs)
        {
            double[] boxDims = [0, 0];
            if (commandArgs.Length != 2)
                return boxDims;

            if (!double.TryParse(commandArgs[0], out boxDims[0]))
                Console.WriteLine("Size Error: Ensure the size only contains numbers");
            if (!double.TryParse(commandArgs[1], out boxDims[1]))
                Console.WriteLine("Size Error: Ensure the size only contains numbers");

            return boxDims;
        }

        private static bool IsBoxCommand(string command)
        {
            return command == "box" || command == "b";
        }

        private static ConsoleCommand InputToFullCommand(string input)
        {
            ConsoleCommand fullCommand = new("", Array.Empty<string>());
            input = input.Trim();
            int spaceIndex = input.IndexOf(' ');

            if (spaceIndex < 0)
            {
                fullCommand.Command = input;
                return fullCommand;
            }

            fullCommand.Command = input[..spaceIndex];
            string argString = input[(spaceIndex + 1)..].Trim();

            if (IsBoxCommand(fullCommand.Command))
            {
                argString = argString.Replace(" ", "");
                fullCommand.Args = argString.Split('x');
                return fullCommand;
            }

            
            fullCommand.Args = input[(spaceIndex + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return fullCommand;
        }
    }
}
