using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public struct DoubleCommand(string c, double d)
    {
        public string Command { get; set; } = c;
        public double Size { get; set; } = d;
    }

    public struct DoubleArrayCommand(string c, double[] d)
    {
        public string Command { get; set; } = c;
        public double[] Args { get; set; } = d;
    }

    public struct ConsoleCommand(string c, string[] a)
    {
        public string Command { get; set; } = c;
        public string[] Args { get; set; } = a;
    }
}
