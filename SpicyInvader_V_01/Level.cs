using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Level
    {
        private int _lvlNumber;
        private Fleet _fleet;

        private bool _bossStage;

        public Level(int a_lvl)
        {
            _lvlNumber = a_lvl;
            
            if (_lvlNumber % 5 == 0)
            {
                _bossStage = true;
            }
            else
            {
                _bossStage = false;
            }

            _fleet = new Fleet(a_lvl, _bossStage);

        }

        public Fleet GetFleet()
        {
            return _fleet;
        }

        public int GetLevel()
        {
            return _lvlNumber;
        }

        public void LevelUP()
        {
            _lvlNumber++;

            if (_lvlNumber % 5 == 0)
            {
                _bossStage = true;
            }
            else
            {
                _bossStage = false;
            }

            _fleet = new Fleet(_lvlNumber, _bossStage);
        }
    }
}
