/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Game
 */
using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;



namespace SpicyInvader_V_01
{
    public class Game
    {
        /// <summary>
        /// Attributs
        /// </summary>
        static public int _score; // static public car on a besoin de pouvoir le modifier et de l'atteindre dans le main ainsi que dans d'autres classes
        private bool _isLost;

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

            _ship = new Ship();

            _score = 0;
            _isLost = false;

            _level = new Level();
            _fleet = _level.GetFleet();

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

        private void PlayAudioFile(object a_relativePath) 
        {
            if (Menu.SoundIsON())
            {
                new SoundPlayer(a_relativePath.ToString()).Play();
            }
        }

        /// <summary>
        /// Lance le menu principal
        /// </summary>
        public void Begin(bool a_begin = true)
        {
            if (a_begin)
            {
                Thread playingMusicThread = new Thread(new ParameterizedThreadStart(PlayAudioFile));
                playingMusicThread.Start("..//..//Sounds//IntroSpicyInvaders.wav");
                //PlayAudioFile("..//..//Sounds//IntroSpicyInvaders.wav"); // TODO trouver un moyen de faire un delegate ptetre ?

                Intro intro = new Intro();

                intro.FallingIntro();

                _menu.ShowMenu(Menu.MAIN_MENU, this);
            }
            else
            {
                GameOver game_over = new GameOver();

                game_over.FallingOutro();

                _menu.ShowMenu(Menu.MAIN_MENU, this);
            }

            _isLost = false;
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
                    //_menu.ShowMenu(Menu.GAME_OVER, this);
                    _isLost = true;
                    
                }
                else
                {
                    _menu.DisplayHUD(_ship, _level.GetLevel(), _fleet.GetEnemiesLife());
                }

                if (_fleet.FleetIsDefeated())
                {
                    if (_fleet.IsBossStage())
                    {
                        _menu.ShowMenu(Menu.BONUS_STAGE, this);
                    }

                    _level.LevelUP();
                    _fleet = _level.GetFleet();

                    InitEntities();

                    _menu.ShowMenu(Menu.STAGE_WIN, this);
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
            _ship = new Ship();
            _level = new Level();
            _fleet = _level.GetFleet();
            _score = 0; // TODO : ne pas oublié de récupéré le score dans le fichier adéquats si nécessaire normalement c'est bon mais à vérifier

            _menu = new Menu();
            _isLost = false;

            InitEntities();
        }

        /// <summary>
        /// String comptenant les informations de la sauvegarde
        /// </summary>
        /// <returns></returns>
        public string GetSaveStat()
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
            save += "ship_State?" + _ship.GetSaveStat(separator);

            return save;
        }

        /// <summary>
        /// Chargement d'une partie
        /// </summary>
        /// <param name="a_saveStat">string contenant les info récupérée du fichier de sauvegarde</param>
        public void LoadGame(string[] a_saveStat)
        {
            _score = Convert.ToInt32(a_saveStat[1].Split('?')[1]);
            _level = new Level(Convert.ToInt32(a_saveStat[2].Split('?')[1]));
            _fleet = _level.GetFleet(); 

            string[] shipStats = a_saveStat[3].Split('?');
            
            //TODO : faire deux saves de vie, tot et actuelle
            _ship = new Ship(Convert.ToInt32(shipStats[1].Split('/')[2].Split('.')[1]), Convert.ToInt32(shipStats[1].Split('/')[1].Split('.')[1])); // life et nombre de missile
            _ship.TakeDamage(Convert.ToInt32(shipStats[1].Split('/')[2].Split('.')[1]) - Convert.ToInt32(shipStats[1].Split('/')[0].Split('.')[1])); // ajustement de la vie
            _menu = new Menu(); // ptetre pas util

            InitEntities();
        }

        public bool IsLost()
        {
            return _isLost;
        }

        public void BonusShipWeaponSlot()
        {
            _ship.UpgradWeaponSlot();
        }

        public void BonusShipMaxHeath()
        {
            _ship.UpgradLife();
        }

        public void BonusShipHeal()
        {
            _ship.Heal();
        }
    }
}
