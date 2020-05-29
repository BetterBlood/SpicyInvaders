/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : l'interface ICanFire
 */
using System.Collections.Generic;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Interface ICanFire
    /// </summary>
    public interface ICanFire
    {
        /// <summary>
        /// Ajoute le nombre nombre de missile possédé par une entité dans sa liste
        /// </summary>
        /// <param name="a_missileNumber"></param>
        /// <param name="a_missileDirection"></param>
        void InitBasesMissiles(int a_missileNumber, EnumDirection a_missileDirection);

        /// <summary>
        /// Vérifie s'il y a un missile non tiré
        /// </summary>
        /// <returns></returns>
        bool IsAMissileNotFired();

        /// <summary>
        /// Liste des missiles
        /// </summary>
        /// <returns></returns>
        List<Missile> GetMissiles();

        /// <summary>
        /// Position verticale du missile
        /// </summary>
        /// <returns></returns>
        int GetMissileYPos();

        /// <summary>
        /// Retourne le nombre de missiles présent dans la liste
        /// </summary>
        /// <returns></returns>
        int GetMissilesCapacity();

        /// <summary>
        /// Nombre de missiles disponible
        /// </summary>
        /// <returns></returns>
        int HowManyMissilesLeft();

        /// <summary>
        /// Tire un missile si les conditions sont remplies
        /// </summary>
        /// <param name="a_bossStage"></param>
        void Fire(bool a_bossStage);


    }
}
