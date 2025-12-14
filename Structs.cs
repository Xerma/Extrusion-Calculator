using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public struct ConsoleCommand(string c, string[] a)
    {
        public string Command { get; set; } = c;
        public string[] Args { get; set; } = a;
    }
}
