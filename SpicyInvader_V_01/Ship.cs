/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Ship
 */

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
        public Ship() : this (UseFull.ALLY_SHIP_SKIN_2, new Position(50, 34), 2) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        public Ship(string a_shape) : this (a_shape, new Position(50, 34), 2) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_lifePoint"></param>
        public Ship(int a_lifePoint) : base (UseFull.ALLY_SHIP_SKIN_2, new Position(50, 34), 2, a_lifePoint) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_lifePoint"></param>
        /// <param name="a_nbrOfMissile"></param>
        public Ship(int a_lifePoint, int a_nbrOfMissile) : base(UseFull.ALLY_SHIP_SKIN_2, new Position(50, 34), a_nbrOfMissile, a_lifePoint) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_lifePoint"></param>
        public Ship(string a_shape, Position a_position, int a_lifePoint) : base(a_shape, a_position, a_lifePoint) { }


    }
}