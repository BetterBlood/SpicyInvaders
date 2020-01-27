using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Boss
    {
        private int _lvl;
        private int _lifePoints;
        private Shape _shape;

        public Boss(int a_lvl, string a_shape)
        {
            _lvl = a_lvl;
            _lifePoints = a_lvl * 10;
            _shape = new Shape(a_shape);
        }


        // TODO : implémenter le déplacement, l'affichage et l'effacage (Move, Draw et Clear) ( surrement avec une interface)
    }
}
