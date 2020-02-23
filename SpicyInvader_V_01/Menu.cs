using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Menu // classe utilitaire à propos des menus
    {
        private const string NEW_GAME = "nouvelle partie";
        private const string CONTINUE = "continuer";
        private const string SAVE = "sauvegarder";
        private const string LOAD = "charger";
        private const string SETTINGS = "parametres";
        private const string LEAVE = "quitter";

        public const string MAIN_MENU = "menu principale";
        public const string PAUSE = "pause";
        public const string GAME_OVER = "game over";

        public const int MISSILE_DISPLAY_POSITION_X = 30;
        public const int MISSILE_DISPLAY_POSITION_Y = 29;


        public const char STRING_SHAPE_SEPARATOR = '4';  // ATTENTION : on ne peut donc pas utiliser le chiffre 4 pour la construction de silhouette 

        public void ShowMenu(string a_menuType)
        {
            if (a_menuType.Equals(PAUSE))
            {
                ShowPauseMenu();
            }
            else if (a_menuType.Equals(MAIN_MENU))
            {
                ShowMainMenu();
            }
            else if (a_menuType.Equals(GAME_OVER))
            {
                ShowGameOverMenu();
            }
        }

        private void ShowMainMenu()
        {
            Console.Clear();

            string[] tab = { NEW_GAME, CONTINUE, LOAD, SETTINGS, LEAVE };

            int place = 0;

            bool newGame = false;
            bool loadLast = false;

            while (true)
            {
                ConsoleKeyInfo key;

                int x = 0;

                // affichage du menu
                foreach (string affichage in tab)
                {
                    if (x == place) // surlignement en jaune du text concerné
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - tab[x].Length / 2, Console.WindowHeight / 3 + x*2);
                        Console.WriteLine(tab[x]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - tab[x].Length / 2, Console.WindowHeight / 3 + x*2);
                        Console.WriteLine(tab[x]);
                    }

                    x++;
                }

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow && place < tab.Length - 1)
                {
                    place++;
                }
                else if (key.Key == ConsoleKey.UpArrow && place > 0)
                {
                    place--;
                }
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    switch (place)
                    {
                        case 0: // nouvelle partie
                            newGame = true;
                            // TODO : appeler une méthode qui réinitialise les stat de la partie (surment sans sauver la partie en cour s'il y en a une)
                            Console.Clear();
                            break;
                        case 1: // continuer
                            // TODO : méthode qui lance la dernière sauvegarde ou la partie en cour
                            Console.Clear();
                            loadLast = true;
                            break;

                        case 2: // charger
                            // TODO : méthode permettant d'atteindre les slot de sauvegarde de la partie en mode charger une partie (il doit être possible de revenir en arrière vers ce menu)
                            break;

                        case 3: // parametres
                            // TODO : méthode permettant d'atteindre les réglages
                            break;

                        case 4: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }

                if (newGame)
                {
                    break; // TODO : pour l'instant on break simplement mais après il faudra ptetre rediriger pour que ça lance une partie on verra
                }
                else if (loadLast)
                {
                    break; // TODO : charger les fichiers de sauvegarde et prendre le plus récent pour loader cette partie
                }
            }
        }

        private void ShowPauseMenu()
        {
            bool reprendre = false;

            string[] tab = { CONTINUE, SAVE, LOAD, SETTINGS, MAIN_MENU, LEAVE };

            Console.Clear();

            int place = 0;

            while (!reprendre)
            {
                ConsoleKeyInfo key;

                int x = 0;

                // affichage du menu
                foreach(string affichage in tab)
                {
                    if (x == place) // surlignement en jaune du text concerné
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - tab[x].Length / 2, Console.WindowHeight / 3 + x*2);
                        Console.WriteLine(tab[x]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - tab[x].Length / 2, Console.WindowHeight / 3 + x*2);
                        Console.WriteLine(tab[x]);
                    }

                    x++;
                }

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow && place < tab.Length - 1)
                {
                    place++;
                }
                else if (key.Key == ConsoleKey.UpArrow && place > 0)
                {
                    place--;
                }
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    switch (place)
                    {
                        case 0: // continuer
                            reprendre = true; // permet de continuer la partie
                            Console.Clear();
                            // TODO : méthode qui efface le menu !!!
                            break;

                        case 1: // sauvegarder
                            // TODO : méthode permettant d'atteindre les slot de sauvegarde de la partie en mode sauvegarde (il doit être possible de revenir en arrière vers ce menu)
                            break;

                        case 2: // charger
                            // TODO : méthode permettant d'atteindre les slot de sauvegarde de la partie en mode charger une partie (il doit être possible de revenir en arrière vers ce menu)
                            break;

                        case 3: // parametres
                            // TODO : méthode permettant d'atteindre les réglages
                            break;

                        case 4: // menu principale
                            // TODO : méthode qui permet de rejoindre le menu principal
                            break;

                        case 5: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }
            }


        }
    
        private void ShowGameOverMenu()
        {
            Console.Clear();
            string[] tab = {NEW_GAME, LOAD, SETTINGS, MAIN_MENU, LEAVE };

            int place = 0;

            bool newGame = false;
            bool load = false;

            while (true)
            {
                ConsoleKeyInfo key;

                int x = 0;

                // affichage du menu
                foreach (string affichage in tab)
                {
                    if (x == place) // surlignement en jaune du text concerné
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - tab[x].Length / 2, Console.WindowHeight / 3 + x * 2);
                        Console.WriteLine(tab[x]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - tab[x].Length / 2, Console.WindowHeight / 3 + x * 2);
                        Console.WriteLine(tab[x]);
                    }

                    x++;
                }

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow && place < tab.Length - 1)
                {
                    place++;
                }
                else if (key.Key == ConsoleKey.UpArrow && place > 0)
                {
                    place--;
                }
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    switch (place)
                    {
                        case 0: // nouvelle partie
                            newGame = true;
                            // TODO : appeler une méthode qui réinitialise les stat de la partie (surment sans sauver la partie en cour s'il y en a une)
                            Console.Clear();
                            break;
                        case 1: // charger
                            // TODO : méthode permettant d'atteindre les slot de sauvegarde de la partie en mode charger une partie (il doit être possible de revenir en arrière vers ce menu)
                            Console.Clear();
                            load = true;
                            break;

                        case 2: // parametres
                            // TODO : méthode permettant d'atteindre les réglages
                            break;

                        case 3: // menu principale
                            // TODO : méthode permettant d'atteindre le menu principale
                            break;

                        case 4: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }

                if (newGame)
                {
                    // Game.newGame = true;
                    break; // TODO : pour l'instant on break simplement mais après il faudra ptetre rediriger pour que ça lance une partie on verra
                }
                else if (load)
                {
                    break; // TODO : charger les fichiers de sauvegarde et prendre le plus récent pour loader cette partie
                }
            }
        }

        public void DisplayScore()
        {
            Console.SetCursorPosition(30, 25);
            Console.Write("score : {0}", Game._score);
        }

        public void DisplayHUV(Ship a_ship)
        {
            int missileTotal = a_ship.GetMissilesCapacity();
            int CurrentNbrMissile = a_ship.HowManyMissilesLeft();
            string realoadind = "             ";

            Console.SetCursorPosition(MISSILE_DISPLAY_POSITION_X, MISSILE_DISPLAY_POSITION_Y);
            Console.Write("missiles : ");

            if (CurrentNbrMissile == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                realoadind = "realoading...";
            }
            
            Console.Write("{0}/{1} ", CurrentNbrMissile, missileTotal);

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write(realoadind);
        }
    }
}
