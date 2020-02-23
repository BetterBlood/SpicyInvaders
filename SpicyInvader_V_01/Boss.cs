using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Boss : Enemy, IMovebale, IDrawable, ICanFire
    {
        private int _lvl;

        public Boss(int a_lvl, string a_shape) : base (a_shape, new Position(Console.WindowWidth / 2, Console.WindowHeight / 8), 3, true) 
        {
            _lvl = a_lvl;
            _lifePoints = a_lvl * 5;
            _pointNumber = a_lvl * 2;
        }

    }
}
