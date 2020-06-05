/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la classe Invader
 */

namespace SpicyInvader_V_01
{

    /// <summary>
    /// Class Invader
    /// </summary>
    public class Invader : Enemy
    {
        /// <summary>
        /// Attributs
        /// </summary>
        public static int HORIZONTAL_SIZE = 5;
        public static int VERTICAL_SIZE = 2;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Invader() : this(new Position(5, 0)) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_position"></param>
        public Invader(Position a_position) : this (a_position, true) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_position"></param>
        /// <param name="a_rightDisplay"></param>
        public Invader(Position a_position, bool a_rightDisplay) : this (UseFull.ENNEMY_SKIN_6, a_position, a_rightDisplay) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_rightDisplay"></param>
        public Invader(string a_shape, Position a_position, bool a_rightDisplay) : this (a_shape, a_position, 1, a_rightDisplay) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_missileNumber"></param>
        /// <param name="a_rightDisplay"></param>
        public Invader(string a_shape, Position a_position, int a_missileNumber, bool a_rightDisplay) : base(a_shape, a_position, a_missileNumber, a_rightDisplay) { }



    }
}
