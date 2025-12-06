using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrusion_Calculator
{
    public struct FullCommand(string c, double s)
    {
        public string Command { get; set; } = c;
        public double Size { get; set; } = s;
    }
}
