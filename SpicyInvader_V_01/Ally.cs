using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Ally : Entity
    {
        public Ally(string a_shape, Position a_position, int a_nbrMissile) : base (a_shape, a_position, a_nbrMissile, EnumDirection.UP) { }
    }
}
