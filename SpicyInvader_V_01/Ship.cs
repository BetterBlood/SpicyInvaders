/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Ship
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Ship
    /// </summary>
    public class Ship : Ally
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Ship() : this (Menu.ALLY_SHIP_SKIN_2, new Position(50, 34), 2) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        public Ship(string a_shape) : this (a_shape, new Position(50, 34), 2) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_lifePoint"></param>
        public Ship(int a_lifePoint) : base (Menu.ALLY_SHIP_SKIN_2, new Position(50, 34), 2, a_lifePoint) { }

        public Ship(int a_lifePoint, int a_nbrOfMissile) : base(Menu.ALLY_SHIP_SKIN_2, new Position(50, 34), a_nbrOfMissile, a_lifePoint) { }

        public Ship(string a_shape, Position a_position, int a_lifePoint) : base(a_shape, a_position, a_lifePoint) { }


    }
}