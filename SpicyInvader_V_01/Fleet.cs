/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la classe Fleet
 */
using System.Collections.Generic;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Fleet
    /// </summary>
    public class Fleet
    {
        /// <summary>
        /// Attribut
        /// </summary>
        private int _numberOfInvader;
        private int _fleetLevel;

        private List<Enemy> _enemies;

        private bool _bossStage;

        static public int _yFleet = 0;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Fleet() : this(3) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_fleetLevel"></param>
        public Fleet(int a_fleetLevel) : this (a_fleetLevel, a_fleetLevel%5 == 0) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_fleetLevel"></param>
        /// <param name="a_bossStage"></param>
        public Fleet(int a_fleetLevel, bool a_bossStage)
        {
            _fleetLevel = a_fleetLevel;
            _enemies = new List<Enemy>();

            _bossStage = a_bossStage; 
            InitEnemies();
            SetFireRight();
        }
        
        /// <summary>
        /// Lance un level de bosses ou de flote d'ennemi
        /// </summary>
        private void InitEnemies()
        {
            if (_bossStage)
            {
                InitBosses(_fleetLevel / 5); // TODO : no pourrait mettre le 5 en constante peut-être
            }
            else
            {
                InitInvaders(_fleetLevel);
            }
        }

        /// <summary>
        /// Ajout d'un boss
        /// </summary>
        /// <param name="a_lvl"></param>
        private void InitBosses(int a_lvl)
        {
            string skinEnm = UseFull.ENNEMY_SKIN_1;

            if (a_lvl > 14)
            {
                skinEnm = UseFull.ENNEMY_SKIN_2;
            }

            if (a_lvl > 24)
            {
                skinEnm = UseFull.ENNEMY_SKIN_3;
            }

            _enemies.Add(new Boss(a_lvl, skinEnm));
        }

        /// <summary>
        /// Ajout d'une flote d'ennemi
        /// </summary>
        /// <param name="a_fleet_lvl"></param>
        public void InitInvaders(int a_fleet_lvl)
        {
            _numberOfInvader = 3 + (a_fleet_lvl * 2)%20; // le modulo 20 c'est pour ne pas avoir plus de 22 invaders 

            int invaderSize = Invader.HORIZONTAL_SIZE; // ptetre mettre en static dans Invader la taille par défaut genre un Invader.Width() ?
            int y = 0;
            string skin = UseFull.ENNEMY_SKIN_4; // ENNEMY_SKIN_7 // ENNEMY_SKIN_4

            for (int i = 0, j = 0; i < _numberOfInvader; i++, j++)
            {
                if (i % 10 == 0) // 10 = nombre d'ennemis par ligne
                {
                    j = 0;
                    y += Invader.VERTICAL_SIZE + 1;
                }

                if (a_fleet_lvl > 10)
                {
                    skin = UseFull.ENNEMY_SKIN_5;
                    invaderSize = 4;
                }
                if (a_fleet_lvl > 20)
                {
                    skin = UseFull.ENNEMY_SKIN_6;
                    invaderSize = 5;
                }

                _enemies.Add(new Invader(skin, new Position(j * (invaderSize + 2), y), true));
            }


            // géstion du lvl des invaders : // TODO : voir quelle manière on préfère :
            
            // 1)
            int indice = a_fleet_lvl / 10; // on casse un int volontairement pour avoir un entier

            for (int i = 0; i < indice; i++)
            {
                foreach (Enemy enemy in _enemies)
                {
                    enemy.Upgrad();
                }
            }

            // 2)
            /*
            if (a_fleet_lvl > 10)
            {
                foreach (Enemy enemy in _enemies)
                {
                    enemy.Upgrad();
                }
            }

            if (a_fleet_lvl > 20)
            {
                foreach (Enemy enemy in _enemies)
                {
                    enemy.Upgrad();
                }
            }*/

        }

        /// <summary>
        /// Met à jour la localisation de tout les ennemis et les ennemis tentent de tirer
        /// </summary>
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

                enemy.TryToFire(_bossStage);
            }
        }

        /// <summary>
        /// Supprime un ennemi 
        /// </summary>
        /// <param name="a_enemy"></param>
        public void RemoveMember(Enemy a_enemy)
        {
            a_enemy.Clear();
            _enemies.Remove(a_enemy);

            SetFireRight(); // new Position(a_enemy.GetX(), a_enemy.GetY())
        }

        /// <summary>
        /// Détérmine si l'ennemi peut tirer
        /// </summary>
        /// <param name="a_position"></param>
        public void SetFireRight(Position a_position)
        {
            int x = a_position.X;
            int y = a_position.Y;

            Enemy enemy_tmp = null;

            foreach (Enemy enemy in _enemies)
            {
                if (enemy.GetX() == x)
                {
                    if (enemy.GetY() >= y)
                    {
                        enemy_tmp = enemy;
                        y = enemy_tmp.GetY();
                    }
                }
            }

            if (enemy_tmp != null)
            {
                enemy_tmp.SetFireStatue(true);
            }
        }

        /// <summary>
        /// Fait parcourir la liste _enemies et chaque élément appele la méthode SetFireRight(Position a_position)
        /// </summary>
        private void SetFireRight()
        {
            // TODO : éventuellement trouver un moyen pour que l'on ne fasse que la première ligne mais pas grave (optimisation)
            foreach(Enemy enemy in _enemies)
            {
                SetFireRight(new Position(enemy.GetX(), enemy.GetY()));
            }
        }

        /// <summary>
        /// Retourne une liste d'ennemi
        /// </summary>
        /// <returns></returns>
        public List<Enemy> GetMembers()
        {
            return _enemies;
        }

        /// <summary>
        /// Vérifie s'il reste des membres dans la flotte ou non
        /// </summary>
        /// <returns></returns>
        public bool FleetIsDefeated()
        {
            if (_enemies.Count == 0)
            {
                _yFleet = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fait descendre la flotte
        /// </summary>
        public static void GoDown()
        {
            _yFleet++;
        }

        /// <summary>
        /// Retourne une chaine de caractère coréspondant au level actuel
        /// </summary>
        /// <returns></returns>
        public string GetLvl()
        {
            return _fleetLevel.ToString();
        }

        /// <summary>
        /// Retourne le type de niveau
        /// </summary>
        /// <returns></returns>
        public bool IsBossStage()
        {
            return _bossStage;
        }

        /// <summary>
        /// Retourne la quantitée de vie possédée par les ennemis
        /// </summary>
        /// <returns></returns>
        public int GetEnemiesLife()
        {
            int lifeTot = 0;

            foreach(Enemy enemy in _enemies)
            {
                lifeTot += enemy.GetLife();
            }

            return lifeTot;
        }
    }
}
