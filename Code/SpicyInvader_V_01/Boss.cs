/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la classe boss
 */

using System;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Boss
    /// </summary>
    public class Boss : Enemy
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private int _lvl;

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_lvl"></param>
        /// <param name="a_shape"></param>
        public Boss(int a_lvl, string a_shape) : base (a_shape, new Position(Console.WindowWidth / 2, Console.WindowHeight / 8), 3, true) 
        {
            _lvl = a_lvl;
            _lifePoints = a_lvl * 5;
            _pointNumber = a_lvl * 2;
        }
    }
}
