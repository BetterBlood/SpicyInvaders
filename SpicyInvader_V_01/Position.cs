using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="a_x">coordonnées horizontale</param>
        /// <param name="a_y">coordonnées verticale</param>
        public Position(int a_x, int a_y)
        {
            X = a_x;
            Y = a_y;
        }

        /// <summary>
        /// redéfinition de la méthode Equals, vérifie que les coordonnées sont identiques
        /// </summary>
        /// <param name="a_position">position que l'on veut comparer à l'instance de l'objet</param>
        /// <returns>true si la coordonnée x et la coordonnée y sont égales, false sinon</returns>
        public bool Equals(Position a_position)
        {
            if (a_position.X == X && a_position.Y == Y)
            {
                return true;
            }
            return false;
        }
    }
}
