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
        private static readonly List<string> CommandList = [ "help", "h", "add", "a", "delete", "d", "inv", "i", "clear", "c" ];
        private static readonly List<string> CommandSizeList = [ "add", "a", "delete", "d", "box", "b" ];
        private static readonly List<string> CommandNoSizeList = ["help", "h", "inv", "i", "clear", "c"];

        public static void RunCommand(string command)
        {
            FullCommand fullCommandParsed = ParseCommand(command);
            string commandParsed = fullCommandParsed.Command;
            double sizeParsed = fullCommandParsed.Size;

            switch (commandParsed)
            {
                case "help":
                    Help.HelpCommand();
                    break;
                case "h":
                    Help.HelpCommand();
                    break;
                case "add":
                    break;
                case "a":
                    break;
                case "delete":
                    break;
                case "d":
                    break;
                case "inv":
                    break;
                case "i":
                    break;
                case "clear":
                    break;
                case "c":
                    break;
                case "box":
                    break;
                case "b":
                    break;
                default:
                    Console.Write("");
                    break;
            }
        }

        public static FullCommand ParseCommand(string input)
        {
            string[] inputSplit = [ "", "" ];
            string? command = "";
            double size = 0;

            try { inputSplit = input.Split(' '); }
            catch { Console.Write("Parsing Error: Command and size must be separated by a single space"); }

            try { command = inputSplit[0].ToLower(); }
            catch { Console.WriteLine("Command Error: Ensure the command only contains text"); }

            if (inputSplit.Length > 1 && CommandNoSizeList.Contains(command))
            {
                try
                {
                    size = Convert.ToDouble(inputSplit[1]);
                }
                catch
                {
                    Console.WriteLine($"Command Error: No size needed for command '{command}'");
                }
            }
            else if (inputSplit.Length == 1 && CommandSizeList.Contains(command))
            {
                try
                {
                    size = Convert.ToDouble(inputSplit[1]);
                }
                catch
                {
                    Console.WriteLine($"Command Error: Command '{command}' needs a size");
                }
            }
            else if (inputSplit.Length == 2 && CommandSizeList.Contains(command))
            {
                try
                {
                    size = Convert.ToDouble(inputSplit[1]);
                }
                catch
                {
                    Console.WriteLine("Size Error: Ensure the size only contains numbers");
                }
            }
            
            FullCommand r = new(command, size);
            return r;
        }
    }
}
