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

            try
            {
                inputSplit = input.Split(' ');
            }
            catch
            {
                Console.Write("Parsing Error: Command and size must be separated by a single space");
            }

            try
            {
                command = inputSplit[0].ToLower();
            }
            catch
            {
                Console.WriteLine("Command Error: Ensure the command only contains text");
            }

            // need to check if command is in list of commands to avoid incorrect error messages (help triggered Size Error
            try
            {
                size = Convert.ToDouble(inputSplit[1]);
            }
            catch
            {
                Console.WriteLine("Size Error: Ensure the size only contains numbers");
            }

            FullCommand r = new(command, size);
            return r;
        }
    }
}
