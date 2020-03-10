using System;
using System.IO;
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
        private const string BACK = "retour";

        private const string SLOT_1 = "Slot 1";
        private const string SLOT_2 = "Slot 2";
        private const string SLOT_3 = "Slot 3";
        
        private readonly string PATH_SLOT_1;
        private readonly string PATH_SLOT_2;
        private readonly string PATH_SLOT_3;

        public const string MAIN_MENU = "menu principale";
        public const string PAUSE = "pause";
        public const string GAME_OVER = "game over";
        public const string STAGE_WIN = "stage win";

        public const int MISSILE_DISPLAY_POSITION_X = 30;
        public const int MISSILE_DISPLAY_POSITION_Y = 29;
        // TODO : faire aussi les coordonnée des autre HUD en const

        public const char STRING_SHAPE_SEPARATOR = '4';  // ATTENTION : on ne peut donc pas utiliser le chiffre 4 pour la construction de silhouette 

        public Menu()
        {
            PATH_SLOT_1 = Path.GetFullPath("slot_1.txt");
            PATH_SLOT_2 = Path.GetFullPath("slot_2.txt");
            PATH_SLOT_3 = Path.GetFullPath("slot_3.txt");
        }

        public void ShowMenu(string a_menuType, Game a_game)
        {
            if (a_menuType.Equals(PAUSE))
            {
                ShowPauseMenu(a_game);
            }
            else if (a_menuType.Equals(MAIN_MENU))
            {
                ShowMainMenu(a_game);
            }
            else if (a_menuType.Equals(GAME_OVER))
            {
                ShowGameOverMenu(a_game);
            }
            else if (a_menuType.Equals(STAGE_WIN))
            {
                ShowWinMenu(a_game);
            }
        }

        private void ShowMainMenu(Game a_game)
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
                            ShowSlotMenu(false, a_game);
                            // TODO : méthode permettant d'atteindre les slot de sauvegarde de la partie en mode charger une partie (il doit être possible de revenir en arrière vers ce menu) normalement c'est bon
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

        private void ShowSlotMenu(bool abbleToSave, Game a_game)
        {
            // le boolean c'est pour dire si on est en mode écriture, en gros si c'est true alors on peut écraser les saves et sauver à la place
            string save1 = File.ReadAllText(PATH_SLOT_1);
            string save2 = File.ReadAllText(PATH_SLOT_2);
            string save3 = File.ReadAllText(PATH_SLOT_3);

            string[] tab = {SLOT_1 + " " + save1.Split('!')[0].Split('?')[1], SLOT_2 + " " + save2.Split('!')[0].Split('?')[1], SLOT_3 + " " + save3.Split('!')[0].Split('?')[1], BACK};

            Console.Clear();

            int place = 0;
            bool back = false;

            ConsoleKeyInfo key;

            while (!back)
            {
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

                switch(key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (place < tab.Length - 1)
                        {
                            place++;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (place > 0)
                        {
                            place--;
                        }
                        break;

                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        switch (place)
                        {
                            case 0: // slot 1
                                if (abbleToSave)
                                {
                                    File.WriteAllText(PATH_SLOT_1, a_game.GetSaveStat());
                                }
                                else if (!save1.Equals(""))
                                {
                                    string save = save1;
                                    string[] saveSplit = save.Split('!');
                                    // 0 = date
                                    string[] score = saveSplit[1].Split('?');
                                    string[] fleetLevel = saveSplit[2].Split('?');
                                    string[] shipStats = saveSplit[3].Split('?');
                                    string[] shipLife = shipStats[1].Split('.');

                                    a_game.LoadGame(Convert.ToInt32(score[1]), Convert.ToInt32(fleetLevel[1]), Convert.ToInt32(shipLife[1]));
                                }
                                break;

                            case 1: // slot 2
                                if (abbleToSave)
                                {
                                    File.WriteAllText(PATH_SLOT_2, a_game.GetSaveStat());
                                }
                                else if (!save2.Equals(""))
                                {
                                    string save = save2;
                                    string[] saveSplit = save.Split('!');
                                    // 0 = date
                                    string[] score = saveSplit[1].Split('?');
                                    string[] fleetLevel = saveSplit[2].Split('?');
                                    string[] shipStats = saveSplit[3].Split('?');
                                    string[] shipLife = shipStats[1].Split('.');

                                    a_game.LoadGame(Convert.ToInt32(score[1]), Convert.ToInt32(fleetLevel[1]), Convert.ToInt32(shipLife[1]));
                                }
                                break;

                            case 2: // slot 3
                                if (abbleToSave)
                                {
                                    File.WriteAllText(PATH_SLOT_3, a_game.GetSaveStat());
                                }
                                else if (!save3.Equals(""))
                                {
                                    string save = save3;
                                    string[] saveSplit = save.Split('!');
                                    // 0 = date
                                    string[] score = saveSplit[1].Split('?');
                                    string[] fleetLevel = saveSplit[2].Split('?');
                                    string[] shipStats = saveSplit[3].Split('?');
                                    string[] shipLife = shipStats[1].Split('.');

                                    a_game.LoadGame(Convert.ToInt32(score[1]), Convert.ToInt32(fleetLevel[1]), Convert.ToInt32(shipLife[1]));
                                }
                                break;

                            case 3: // on retourne au menu précédent
                                back = true;
                                Console.Clear();
                                break;
                        }
                        break;
                }
            }
        }

        private void ShowWinMenu(Game a_game)
        {
            bool reprendre = false;

            string[] tab = { CONTINUE, SAVE, SETTINGS, MAIN_MENU, LEAVE};

            Console.Clear();

            int place = 0;

            ConsoleKeyInfo key;

            while (!reprendre)
            {
                int x = 0;

                // affichage du menu
                foreach (string affichage in tab) // TODO : faire une méthode pour ça !!! et regarder dans les autre méthodes car c'est pareil !!!!
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
                        case 0: // continue
                            reprendre = true;
                            break;
                        case 1: // save
                            ShowSlotMenu(true, a_game);
                            Console.Clear();
                            break;

                        case 2: // parametres
                            // TODO : méthode permettant d'atteindre les réglages
                            break;

                        case 3: // menu principale
                            // TODO : méthode permettant d'atteindre le menu principale
                            // voir si ça perd la sauvegarde mais du coup depuis le menu principale si on fait continue on continue la partie courante
                            break;

                        case 4: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }
            }
            Console.Clear();
        }

        private void ShowPauseMenu(Game a_game)
        {
            bool reprendre = false;

            string[] tab = { CONTINUE, SAVE, LOAD, SETTINGS, MAIN_MENU, LEAVE };

            Console.Clear();

            int place = 0;
            
            ConsoleKeyInfo key;

            while (!reprendre)
            {
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
                            break;

                        case 1: // sauvegarder
                            ShowSlotMenu(true, a_game);
                            break;

                        case 2: // charger
                            ShowSlotMenu(false, a_game);
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
    
        private void ShowGameOverMenu(Game a_game)
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
                            a_game.ResetGame();
                            Console.Clear();
                            break;
                        case 1: // charger
                            ShowSlotMenu(false, a_game);
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

                if (newGame || load)
                {
                    break; // TODO : pour l'instant on break simplement le while(pour sortir du menu) mais après il faudra ptetre rediriger ailleurs
                }
            }
        }

        public void DisplayLevel(int a_level)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Level : " + a_level);
        }

        public void DisplayScore()
        {
            Console.SetCursorPosition(30, 33); // TODO : mettre en constante les valeur du display
            Console.Write("score : {0}", Game._score);
        }

        public void DisplayHUD(Ship a_ship, int a_level)
        {
            // début // scores :
            DisplayScore();
            // fin // scores

            DisplayLevel(a_level);

            // début // missiles :
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
            // fin // missiles

            // début // vie :

            Console.SetCursorPosition(30, 31); // TODO : mettre en constante les valeur du display
            Console.WriteLine("vies : " + a_ship.GetLife());


            // fin // vie

        }
    }
}
