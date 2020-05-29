/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Janvier à Mai 2020
 * Desciption : la classe Position
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Position
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private int x;
        private int y;

        /// <summary>
        /// Propriétés
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_x">coordonnées horizontale</param>
        /// <param name="a_y">coordonnées verticale</param>
        public Position(int a_x, int a_y)
        {
            x = a_x;
            y = a_y;
        }
        /// <summary>
        /// constructeur renseigné
        /// </summary>
        /// <param name="a_position">position que l'on veut clonner</param>
        public Position(Position a_position)
        {
            x = a_position.X;
            y = a_position.Y;
        }

        /// <summary>
        /// redéfinition de la méthode Equals, vérifie que les coordonnées sont identiques
        /// </summary>
        /// <param name="a_position">position que l'on veut comparer à l'instance de l'objet</param>
        /// <returns>true si la coordonnée x et la coordonnée y sont égales, false sinon</returns>
        public bool Equals(Position a_position)
        {
            if (a_position.X == x && a_position.Y == y)
            {
                return true;
            }
            return false;
        }
    }
}
