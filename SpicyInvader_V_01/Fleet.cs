using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Fleet
    {
        private int _numberOfInvader;

        List<Invader> _invaders;
        List<Boss> _bosses;

        private bool _BossStage;

        public Fleet() : this(3) { }

        public Fleet(int a_fleetLevel)
        {
            _invaders = new List<Invader>();
            _bosses = new List<Boss>(); // TODO : réunir les deuc liste en une liste d'ennemis

            _BossStage = false;

            InitInvaders(a_fleetLevel);
        }
        public Fleet(int a_fleetLevel, bool a_bossStage)
        {
            _invaders = new List<Invader>();
            _bosses = new List<Boss>();

            _BossStage = a_bossStage;

            if (a_bossStage)
            {
                InitBosses(a_fleetLevel);
            }
            else
            {
                InitInvaders(a_fleetLevel);
            }
        }

        public void InitBosses(int a_lvl)
        {
            // TODO : voir si j'ai pas oublié un truc ici, spoiler : surement oui, comparer avec InnitInvaders juste en dessous !!
            _bosses.Add(new Boss(a_lvl, "test4testtest4  test"));
        }

        public void InitInvaders(int a_fleet_lvl)
        {
            _numberOfInvader = 3 + a_fleet_lvl*2;
            
            int invaderSize = 4;
            int y = 0;
            int decalage = 0;
            int gap = 2;

            bool rightDisplay = true;

            for (int i = 0, j = 0; i < _numberOfInvader; i++, j++)
            {
                if (rightDisplay && j * (invaderSize + 1) + invaderSize + 1 >= Console.WindowWidth)
                {
                    j = 0;
                    y++;
                    rightDisplay = false;
                    decalage = 5;
                }
                else if (!rightDisplay && Console.WindowWidth - (j * (invaderSize + 1) + invaderSize - 2) < 0)
                {
                    j = 0;
                    y++;
                    rightDisplay = true;
                    decalage = 0;
                }

                if (rightDisplay)
                {
                    _invaders.Add(new Invader(j * (invaderSize + 1), y, rightDisplay));
                }
                else
                {
                    _invaders.Add(new Invader(Console.WindowWidth - (j * (invaderSize + 1) + invaderSize - 2), y, rightDisplay));
                }
            }
        }

        public void Update()
        {
            if (_BossStage)
            {
                foreach (Boss boss in _bosses)
                {
                    //boss.Move(); 
                    //boss.Draw();
                }
            }
            else
            {
                foreach (Invader invader in _invaders)
                {
                    invader.Move();
                    invader.Draw();
                }
            }
        }

        public List<Invader> GetMembers()
        {
            return _invaders;
        }

        public List<Boss> GetMembersBoss() // surement supprimer cette méthode quand les deux liste seront mises ensembles
        {
            return _bosses;
        }

        public bool FleetIsDefeated()
        {
            if (_invaders.Count() == 0)
            {
                return true;
            }

            return false;
        }

        public bool BossIsDefeated() // surement supprimer cette méthode quand les deux liste seront mises ensembles
        {
            if (_bosses.Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
