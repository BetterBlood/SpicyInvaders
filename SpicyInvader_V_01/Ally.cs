using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    abstract public class Ally : Entity
    {
        public Ally(string a_shape, Position a_position, int a_nbrMissile) : this (a_shape, a_position, a_nbrMissile, 3) { }

        public Ally(string a_shape, Position a_position, int a_nbrMissile, int a_lifePoint) : base(a_shape, a_position, a_nbrMissile, EnumDirection.UP)
        {
            _lifePoints = a_lifePoint;
        }

        public bool IsDead(Fleet a_fleet) // TODO implémenter ça dans Ally je pense !
        {
            foreach (Enemy enemy in a_fleet.GetMembers())
            {
                if (enemy.GetPositions()[0].Y >= _position.Y)
                {
                    return true;
                }
            }

            return _lifePoints <= 0 ? true : false;
        }

        public override string ToString()
        {
            return "Ally";
        }

        public string GetSaveStat()
        {
            return "life." + _lifePoints;
        }
    }
}
