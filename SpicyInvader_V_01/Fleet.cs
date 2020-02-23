using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Fleet
    {
        private int _numberOfInvader;

        private List<Enemy> _enemies;

        private bool _bossStage;

        static public int _yFleet = 0;

        public Fleet() : this(3) { }

        public Fleet(int a_fleetLevel) : this (a_fleetLevel, false) { }

        public Fleet(int a_fleetLevel, bool a_bossStage)
        {
            _enemies = new List<Enemy>();

            _bossStage = a_bossStage; 
            //_bossStage = true; // TODO : c'est un test pour calibrer les missile du boss
            InitEnemies(a_fleetLevel);
        }

        private void InitEnemies(int a_fleetLevel)
        {
            if (_bossStage)
            {
                InitBosses(a_fleetLevel / 5); // TODO : utiliser un chiffre prédéfinit quelquepart car il represente la distance entre deux stage de BOSS
            }
            else
            {
                InitInvaders(a_fleetLevel);
            }
        }

        public void InitBosses(int a_lvl)
        {
            // TODO : voir si j'ai pas oublié un truc ici, spoiler : surement oui, comparer avec InnitInvaders juste en dessous !!
            _enemies.Add(new Boss(a_lvl, "  \\__/4 -<==>-4  \\__/"));
        }

        public void InitInvaders(int a_fleet_lvl)
        {
            _numberOfInvader = 3 + a_fleet_lvl * 2;

            int invaderSize = 4; // ptetre mettre en static dans Invader la taille par défaut genre un Invader.Width() ?
            int y = 0;

            for (int i = 0, j = 0; i < _numberOfInvader; i++, j++)
            {
                if (i % 5 == 0)
                {
                    j = 0;
                    y++;
                }

                _enemies.Add(new Invader(new Position(j * (invaderSize + 1), y), true));
            }
        }

        public void Update()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.DetectorBridge();
            }

            Enemy.ChangMove();

            foreach (Enemy enemy in _enemies)
            {
                enemy.Move();
                enemy.Draw();

                enemy.TryToFire();
            }
        }

        public void RemoveMember(Enemy a_enemy)
        {
            a_enemy.Clear();
            _enemies.Remove(a_enemy);
        }

        public List<Enemy> GetMembers()
        {
            return _enemies;
        }

        public bool FleetIsDefeated()
        {
            if (_enemies.Count == 0)
            {
                _yFleet = 0;
                return true;
            }

            return false;
        }

        public static void GoDown()
        {
            _yFleet++;
        }
    }
}
