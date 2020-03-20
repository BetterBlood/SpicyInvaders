/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : l'interface ICanFire
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Interface ICanFire
    /// </summary>
    public interface ICanFire
    {

        void InitBasesMissiles(int a_missileNumber, EnumDirection a_missileDirection);

        bool IsAMissileNotFired();

        List<Missile> GetMissiles();

        int GetMissileYPos();

        int GetMissilesCapacity();

        int HowManyMissilesLeft();

        void Fire();


    }
}
