using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Game
    {
        private int fleetLvl = 1;

        static public int _score; // static public car on a besoin de pouvoir le modifier et de l'atteindre dans le main ainsi que dans d'autres classes

        private Fleet _fleet;
        private Ship _ship;
        private Menu _menu;

        private int test = 3; // temporaire inutile

        public Game()
        {
            Console.WindowWidth = 71;
            Console.WindowHeight = Console.LargestWindowHeight - 10;

            _fleet = new Fleet();
            _ship = new Ship();

            _score = 0; // TODO : ne pas oublié de récupéré le score dans le fichier adéquats si nécessaire

            _menu = new Menu();
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
                        _ship.PrivateMove("left");

                        break;

                    //Touche fléchée droite
                    case ConsoleKey.RightArrow:
                        //Décalage de la position de référence
                        _ship.PrivateMove("right");

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
                _fleet.Update();

                if (_ship.IsDead(_fleet))
                {
                    _menu.ShowMenu(Menu.GAME_OVER);
                }
            }

            //Gestion des missiles
            if (a_tics % 5 == 0)
            {
                _ship.UpdateMissile(_fleet);
                _menu.DisplayScore();
                _menu.DisplayHUV(_ship);

                if (_fleet.IsDefeated())
                {
                    fleetLvl++;
                    _fleet.InitInvaders(fleetLvl);
                }
            }
        }
    }
}
