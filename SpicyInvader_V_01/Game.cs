/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Game
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Game
    {
        /// <summary>
        /// Attributs
        /// </summary>
        static public int _score; // static public car on a besoin de pouvoir le modifier et de l'atteindre dans le main ainsi que dans d'autres classes

        private Fleet _fleet;
        private Ship _ship;
        private Menu _menu;

        private List<Entity> _allEntities;
        private List<List<Missile>> _allMissiles;

        private Level _level;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Game()
        {
            Console.WindowWidth = Console.LargestWindowWidth - 50;
            Console.WindowHeight = Console.LargestWindowHeight - 10;

            _fleet = new Fleet();
            _ship = new Ship();

            _score = 0;

            _level = new Level(1);

            _menu = new Menu();

            InitEntities();
        }

        /// <summary>
        /// Ajout des entités dans les listes
        /// </summary>
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

        /// <summary>
        /// Lance le menu principal
        /// </summary>
        public void Begin()
        {
            Intro intro = new Intro();

            intro.FallingIntro();

            _menu.ShowMenu(Menu.MAIN_MENU, this);
        }

        /// <summary>
        /// Actualise du jeu en cours de partie
        /// </summary>
        /// <param name="a_tics"></param>
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
                else
                {
                    _menu.DisplayHUD(_ship, _level.GetLevel());
                }

                if (_fleet.FleetIsDefeated())
                {
                    _level.LevelUP();
                    _fleet = _level.GetFleet();
                    
                    InitEntities();

                    _menu.ShowMenu(Menu.STAGE_WIN, this);

                    // TODO : afficher les bonus si c'est un bossStage
                    // TODO : afficher le menu pour sauver 
                    /*
                    if (_fleetLvl%5 == 0) // boss stage // TODO : voir avec la class Level
                    {
                        _fleet = new Fleet(_fleetLvl, true);
                        
                        InitEntities();
                    }
                    else
                    {
                        _fleet = new Fleet(_fleetLvl, false);
                        InitEntities();
                    }*/
                }
            }
        }

        /// <summary>
        /// Appel une méthode enlevant des points de vie à une unité
        /// </summary>
        /// <param name="a_power"></param>
        public void AllyIsHit(int a_power)
        {
            _ship.TakeDamage(a_power);
        }

        /// <summary>
        /// Mise à jour du score
        /// </summary>
        /// <param name="a_pointNumber"></param>
        public void IncreasePoint(int a_pointNumber)
        {
            _score += a_pointNumber;
        }

        /// <summary>
        /// Reignisialisation d'une partie
        /// </summary>
        public void ResetGame()
        {
            _fleet = new Fleet();
            _ship = new Ship();

            _score = 0; // TODO : ne pas oublié de récupéré le score dans le fichier adéquats si nécessaire

            _menu = new Menu();

            InitEntities();
        }

        /// <summary>
        /// String comptenant les informations de la sauvegarde
        /// </summary>
        /// <returns></returns>
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
            save += "fleet_lvl?" + _level.GetLevel();
            save += separator;
            save += "ship_State?" + _ship.GetSaveStat();



            return save;
        }

        /// <summary>
        /// Chargement d'une partie
        /// </summary>
        /// <param name="a_score"></param>
        /// <param name="a_fleetLevel"></param>
        /// <param name="a_shipLife"></param>
        public void LoadGame(int a_score, int a_fleetLevel, int a_shipLife)
        {
            _level = new Level(a_fleetLevel);
            _score = a_score;

            _fleet = _level.GetFleet();
            _ship = new Ship(a_shipLife);
            _menu = new Menu();

            InitEntities();
        }
    }
}
