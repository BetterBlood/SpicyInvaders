﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
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
