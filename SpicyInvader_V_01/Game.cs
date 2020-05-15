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
    /// <summary>
    /// Class Game
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Attributs
        /// </summary>
        static public int _score; // static public car on a besoin de pouvoir le modifier et de l'atteindre dans le main ainsi que dans d'autres classes
        public string _difficulty;
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
            _difficulty = null;

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

        private void PlayBossThem()
        {
            if (Menu.SoundIsON())
            {
                PlayAudioFile("..//..//Sounds//BossThemSpicyInvaders.wav");
            }
        }

        private void PlayBossVictory()
        {
            if (Menu.SoundIsON())
            {
                PlayAudioFile("..//..//Sounds//EnnemyDeath.wav");
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
        /// Lance le son des combats de boss
        /// </summary>
        private void PlayBossThem()
        {
            if (Menu.SoundIsON())
            {
                PlayAudioFile("..//..//Sounds//BossThemSpicyInvaders.wav");
            }
        }

        /// <summary>
        /// Lance le son de la victoire contre un boss
        /// </summary>
        private void PlayBossVictory()
        {
            if (Menu.SoundIsON())
            {
                PlayAudioFile("..//..//Sounds//EnnemyDeath.wav");
            }
        }

        /// <summary>
        /// Joue le son séléctionné
        /// </summary>
        /// <param name="a_relativePath"></param>
        private void PlayAudioFile(object a_relativePath) 
        {
            if (Menu.SoundIsON())
            {
                new SoundPlayer(a_relativePath.ToString()).Play();
            }
        }

        /// <summary>
        /// Lance la cinématique de début (intro, ou un game over si le vaisseau est mort)
        /// </summary>
        public void Begin(bool a_begin = true)
        {
            if (a_begin)
            {
                Thread playingMusicThread = new Thread(new ParameterizedThreadStart(PlayAudioFile));
                playingMusicThread.Start("..//..//Sounds//IntroSpicyInvaders.wav");

                Intro intro = new Intro();

                intro.FallingIntro();

                _menu.ShowMenu(Menu.MAIN_MENU, this);
                playingMusicThread.Abort(); // TODO : à tester pas sur que ce soit juste
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

            if(_difficulty == null)
            {
                _menu.ShowMenu(Menu.DIFFICULTY_CHOISE, this);
                // TODO : prendre la _difficulty et orienté le ship selon ça

                switch (_difficulty)
                {
                    case "easy":
                        _ship.UpgradLife();
                        _ship.Heal();
                        break;
                    case "hard":
                        // normal
                        break;
                    case "harder":
                        _ship.SetHardMode();
                        break;
                    default:
                        // au cas ou
                        break;
                }
                InitEntities();
                Console.Clear();
            }

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
                            _ship.Fire(_fleet.IsBossStage());
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
                    _menu.ShowMenu(Menu.HIGH_SCORE, this);
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
                        PlayBossVictory(); // TODO : vérifier si ça close l'ancien son
                        _menu.ShowMenu(Menu.BONUS_STAGE, this);
                    }

                    _level.LevelUP();
                    _fleet = _level.GetFleet();

                    InitEntities();

                    _menu.ShowMenu(Menu.STAGE_WIN, this);

                    if (_fleet.IsBossStage())
                    {
                        PlayBossThem();
                    }
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

            if (_difficulty.Equals("harder"))
            {
                _score -= 5; // TODO : voir si ça vaut la peine
            }
        }

        /// <summary>
        /// Mise à jour du score
        /// </summary>
        /// <param name="a_pointNumber"></param>
        public void IncreasePoint(int a_pointNumber)
        {
            int difficultyMultiplier = 1;

            switch(_difficulty)
            {
                case "easy":
                    difficultyMultiplier = 1;
                    break;
                case "hard":
                    difficultyMultiplier = 2;
                    break;
                case "harder":
                    difficultyMultiplier = 3;
                    break;
                default:
                    // normalement inutil
                    break;
            }
            _score += a_pointNumber * difficultyMultiplier;
        }

        /// <summary>
        /// Réinisialisation d'une partie
        /// </summary>
        public void ResetGame()
        {
            _ship = new Ship();
            _level = new Level();
            _fleet = _level.GetFleet();
            _score = 0;

            _menu = new Menu();
            _isLost = false;
            _difficulty = null;

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
            save += separator;
            save += "difficulty?" + _difficulty; 

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

            _difficulty = a_saveStat[4].Split('?')[1];

            InitEntities();
        }

        /// <summary>
        /// Retourne l'état finale de la partie
        /// </summary>
        /// <returns></returns>
        public bool IsLost()
        {
            return _isLost;
        }

        /// <summary>
        /// Appele la méthode d'amélioration du nombre de missile
        /// </summary>
        public void BonusShipWeaponSlot()
        {
            _ship.UpgradWeaponSlot();
        }

        /// <summary>
        /// Appele la méthode d'amélioration du nombre de points de vie
        /// </summary>
        public void BonusShipMaxHeath()
        {
            _ship.UpgradLife();
        }

        /// <summary>
        /// Appele la méthode qui soigne le vaisseau
        /// </summary>
        public void BonusShipHeal()
        {
            _ship.Heal();
        }
    }
}
