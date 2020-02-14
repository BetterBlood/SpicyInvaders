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
        private int _lifePoints;

        private Position _position;
        private Shape _shape;

        public Boss(int a_lvl, string a_shape) : base (a_shape, new Position(7,8), 3) 
        {
            _lvl = a_lvl;
            _lifePoints = a_lvl * 10;

            _position = new Position(Console.WindowWidth/2, Console.WindowHeight / 8);
            _shape = new Shape(a_shape);
        }
        
    }
}
