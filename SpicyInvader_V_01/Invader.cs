using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Invader : Enemy
    {

        public Invader() : this(new Position(5, 0)) { }

        public Invader(Position a_position) : this (a_position, true) { }

        public Invader(Position a_position, bool a_rightDisplay) : this ("=()=", a_position, a_rightDisplay) { }

        public Invader(string a_shape, Position a_position, bool a_rightDisplay) : base (a_shape, a_position, 1, a_rightDisplay) { }

    }
}
