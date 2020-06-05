/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : l'interface IMovebale
 */

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Interface IMovebale
    /// </summary>
    public interface IMovebale
    {
        /// <summary>
        /// Détérmine la nouvelle position de l'entité
        /// </summary>
        /// <param name="a_direction"></param>
        void PrivateMove(EnumDirection a_direction);
    }
}
