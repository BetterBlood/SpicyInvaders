using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Game
    {
        private int fleetLvl = 1;

        static public int _score; // static public car on a besoin de pouvoir le modifier et de l'atteindre dans le main ainsi que dans d'autres classes

        private Fleet _fleet;
        private Ship _ship;
        private Menu _menu;

        private List<Entity> _allEntities;
        private List<List<Missile>> _allMissiles;

        public Game()
        {
            Console.WindowWidth = 71;
            Console.WindowHeight = Console.LargestWindowHeight - 10;

            _fleet = new Fleet();
            _ship = new Ship();

            _score = 0; // TODO : ne pas oublié de récupéré le score dans le fichier adéquats si nécessaire

            _menu = new Menu();

            InitEntities();
        }

        private void InitEntities()
        {
            _allEntities = new List<Entity>();
            _allMissiles = new List<List<Missile>>();

            _allEntities.Add(_ship);
            _allMissiles.Add(_ship.GetMissiles());

            foreach (Entity entity in _fleet.GetMembers())
            {
                _allEntities.Add(entity);
                _allMissiles.Add(entity.GetMissiles());
            }
        }

        public void Begin()
        {
            _menu.ShowMenu(Menu.MAIN_MENU);
        }

        public void Update(int a_tics)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    //Touche fléchée gauche
                    case ConsoleKey.LeftArrow:
                        //Décalage de la position de référence
                        _ship.PrivateMove(EnumDirection.LEFT);

                        break;

                    //Touche fléchée droite
                    case ConsoleKey.RightArrow:
                        //Décalage de la position de référence
                        _ship.PrivateMove(EnumDirection.RIGHT);

                        break;

                    //MISSILE
                    case ConsoleKey.Spacebar:
                        //Un seul missile à la fois
                        if (_ship.IsAMissileNotFired())
                        {
                            _ship.Fire();
                        }
                        break;

                    //MENU PAUSE
                    case ConsoleKey.I:
                        _menu.ShowMenu(Menu.PAUSE);
                        break;

                }
            }

            //On affiche le vaisseau à la bonne positition
            _ship.Draw();

            if (a_tics % 10 == 0)
            {
                _fleet.Update(); // principalement le mouvement des invaders ou bien du boss
            }

            //Gestion des missiles
            if (a_tics % 5 == 0)
            {
                foreach (List<Missile> missileList in _allMissiles)
                {
                    foreach(Missile missile in missileList)
                    {
                        missile.UpdateMissile(_allEntities, _fleet, this);
                    }
                }

                if (_ship.IsDead(_fleet))
                {
                    _menu.ShowMenu(Menu.GAME_OVER);
                }

                _menu.DisplayScore();
                _menu.DisplayHUV(_ship);

                if (_fleet.FleetIsDefeated())
                {
                    fleetLvl++;

                    if (fleetLvl%5 == 0) // boss stage // TODO : voir avec la class Level
                    {
                        _fleet = new Fleet(fleetLvl, true);
                        InitEntities();
                    }
                    else
                    {
                        _fleet = new Fleet(fleetLvl, false);
                        InitEntities();
                    }
                }
            }
        }

        public void AllyIsHit(int a_power)
        {
            _ship.TakeDamage(a_power);
        }

        public void IncreasePoint(int a_pointNumber)
        {
            _score += a_pointNumber;
        }
    }
}
