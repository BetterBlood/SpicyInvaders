using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Level
    {
        private int _lvlNumber;
        private Fleet fleet;

        private bool _bossStage;

        public Level(int a_lvl)
        {
            _lvlNumber = a_lvl;

            _bossStage = false;

            if (_lvlNumber % 9 == 0)
            {
                _bossStage = true;
            }

            fleet = new Fleet(a_lvl, _bossStage);

        }
    }
}
