using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Boss : IMovebale, IDrawable, ICanFire
    {
        private int _lvl;
        private int _lifePoints;

        private Position _position;
        private Shape _shape;

        public Boss(int a_lvl, string a_shape)
        {
            _lvl = a_lvl;
            _lifePoints = a_lvl * 10;

            _position = new Position(Console.WindowWidth/2, Console.WindowHeight / 8);
            _shape = new Shape(a_shape);
        }
        
        public void Draw()
        {
            _shape.Draw(_position);
        }

        public void Clear()
        {
            _shape.Clear(_position);
        }

        public void PrivateMove(string a_direction)
        {
            // TODO : fair la classe mère des ennemis pour pouvoir override PrivateMove ainsi que la classe mère des entitées !!!!!!!!!!!! !!!!!!!!!!!! !!!!!!!!!!
        }

        // TODO : implémenter le déplacement, l'affichage et l'effacage (Move, Draw et Clear) ( surrement avec une interface)
    }
}
