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
        public static void RunCommand(string input, SortedSet<InventoryPiece> invList)
        {
            InventoryPiece invPiece = new();
            ConsoleCommand fullCommand = InputToConsoleCommand(input);
            string command = fullCommand.Command;
            string[] args = fullCommand.Args;
            double sizeParsed = 0;
            double[] boxDims = [];

            if (IsBoxCommand(command))
            {
                boxDims = TryBoxDims(args);
            }
            else if (AreArgsNeeded(command))
            {
                if (args.Length < 1)
                {
                    ConsoleHelper.CommandNeedsArgsWarn();
                    return;
                }

                if (!double.TryParse(args[0], out sizeParsed))
                {
                    ConsoleHelper.SizeNumbersOnlyWarn();
                    return;
                }
                
                if ((IsAddCommand(command) || IsDelCommand(command)) && args.Length < 3)
                {
                    ConsoleHelper.NotEnoughEndsWarn();
                    return;
                }

                if (IsAddCommand(command) || IsDelCommand(command))
                {
                    invPiece.End1 = char.ToUpperInvariant(args[1][0]);
                    invPiece.End2 = char.ToUpperInvariant(args[2][0]);
                }
            }

            try
            {
                invPiece.Length = sizeParsed;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Error creating the inventory piece");
                Console.ResetColor();
            }
            

            switch (command)
            {
                case "help":
                case "h":
                    Help.HelpCommand();
                    break;
                case "add":
                case "a":
                    Add.AddCommand(invPiece, invList);
                    break;
                case "del":
                case "d":
                    Delete.DeleteCommand(invPiece, invList);
                    break;
                case "inv":
                case "i":
                    Inv.InvCommand(invList);
                    break;
                case "clear":
                case "c":
                    Console.Clear();
                    ConsoleHelper.MainWrite();
                    break;
                case "box":
                case "b":
                    Box.BoxCommand(boxDims, invList);
                    break;
                default:
                    Console.Write("");
                    break;
            }
        }

        public static ConsoleCommand InputToConsoleCommand(string input)
        {
            ConsoleCommand fullCommand = InputToFullCommand(input);
            string command = fullCommand.Command;
            string[] args = fullCommand.Args;

            return fullCommand;
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

            if (IsAddCommand(fullCommand.Command) || IsDelCommand(fullCommand.Command))
            {
                string[] tokens = argString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 0)
                {
                    fullCommand.Args = Array.Empty<string>();
                    return fullCommand;
                }

                string sizePart = tokens[0];
                string endSpec = tokens.Length > 1 ? tokens[1] : "f/f";

                string[] endParts = endSpec.Split('/', StringSplitOptions.RemoveEmptyEntries);

                string end1 = endParts.Length > 0 ? endParts[0] : "f";
                string end2 = endParts.Length > 1 ? endParts[1] : end1;

                fullCommand.Args = new[] { sizePart, end1, end2 };
                return fullCommand;
            }


            fullCommand.Args = input[(spaceIndex + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return fullCommand;
        }

        private static double[] TryBoxDims(string[] commandArgs)
        {
            double[] boxDims = [0, 0];
            if (commandArgs.Length != 2)
                return boxDims;

            if (!double.TryParse(commandArgs[0], out boxDims[0]))
                ConsoleHelper.SizeNumbersOnlyWarn();
                
            if (!double.TryParse(commandArgs[1], out boxDims[1]))
                ConsoleHelper.SizeNumbersOnlyWarn();

            return boxDims;
        }

        private static bool AreArgsNeeded(string command)
        {
            return command == "add" || command == "a" || 
                   command == "del" || command == "d" || 
                   command == "box" || command == "b";
        }

        private static bool IsDelCommand(string command)
        {
            return command == "del" || command == "d";
        }

        private static bool IsAddCommand(string command)
        {
            return command == "add" || command == "a";
        }

        private static bool IsBoxCommand(string command)
        {
            return command == "box" || command == "b";
        }
    }
}
