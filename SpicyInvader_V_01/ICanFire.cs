using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    interface ICanFire
    {

        void InitBasesMissiles(int a_missileNumber);

        bool IsAMissileNotFired();

        int GetMissileYPos();

        int GetMissilesCapacity();

        int HowManyMissilesLeft();

        void Fire();

        void UpdateMissile(Fleet a_fleet);

    }
}
