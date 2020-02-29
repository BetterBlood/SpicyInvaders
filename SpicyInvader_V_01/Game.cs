using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Game
    {
        private int _fleetLvl;

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
            _fleetLvl = 1;

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
            _menu.ShowMenu(Menu.MAIN_MENU, this);
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
                        _menu.ShowMenu(Menu.PAUSE, this);
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
                    _menu.ShowMenu(Menu.GAME_OVER, this);
                }
                
                _menu.DisplayHUD(_ship);

                if (_fleet.FleetIsDefeated())
                {
                    _fleetLvl++;

                    // TODO : afficher les bonus si c'est un bossStage
                    // TODO : afficher le menu pour sauver 

                    if (_fleetLvl%5 == 0) // boss stage // TODO : voir avec la class Level
                    {
                        _fleet = new Fleet(_fleetLvl, true);
                        InitEntities();
                    }
                    else
                    {
                        _fleet = new Fleet(_fleetLvl, false);
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

        public void ResetGame()
        {
            _fleet = new Fleet();
            _ship = new Ship();

            _score = 0; // TODO : ne pas oublié de récupéré le score dans le fichier adéquats si nécessaire

            _menu = new Menu();

            InitEntities();
        }

        public string GetSaveStat() // return une string qui donne toutes les infos nécessaire pour la sauvegarde
        {
            // on va faire pour l'instant que l'on peut save le niveau mais pas l'état exact des ennemis
            // donc on a besoin pour ça d'avoir :

            //      la date de la sauvegarde ( ce serait bien si on pourrait l'afficher, voir avec adri pour le designe, afficher aussi le lvl, le nombre de vie et le score)
            //      le lvl de la fleet
            //      l'état du ship
            //      le score
            //      je crois que c'est tout

            //  ptetre en fait on va faire qu'on peut save seulement entre les lvl

            string save = "";
            string separator = "!";

            save += "Date?" + DateTime.Now;
            save += separator;
            save += "score?" + _score;
            save += separator;
            save += "fleet_lvl?" + _fleet.GetLvl();
            save += separator;
            save += "ship_State?" + _ship.GetSaveStat();



            return save;
        }

        public void LoadGame(int a_score, int a_fleetLevel, int a_shipLife)
        {
            _fleetLvl = a_fleetLevel;
            _score = a_score;

            _fleet = new Fleet(_fleetLvl);
            _ship = new Ship(a_shipLife);
            _menu = new Menu();

            InitEntities();
        }
    }
}
