using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Ship : Ally
    {
        public Ship() : base ("_/-\\_4 \\*/ ", new Position(50, 26), 2) { }

        public Ship(string a_shape) : base(a_shape, new Position(50, 26), 2) { }
    }
}
