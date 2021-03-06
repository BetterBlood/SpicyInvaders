﻿/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la classe Menu
 */
using System;
using System.IO;
using System.Linq;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Menu
    /// </summary>
    public class Menu // classe utilitaire à propos des menus
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private int _saveSelected = -1;
        private int _bonusSelected = -1;

        /// <summary>
        /// Constructeur par défaut qui initialise les full path des fichiers de sauvegarde
        /// </summary>
        public Menu() { }

        /// <summary>
        /// permet de multiplié un symbol
        /// </summary>
        /// <param name="a_symbol"></param>
        /// <param name="a_nbrOfMult"></param>
        /// <returns>une string contenant le symbole a_nbrOfMult fois</returns>
        public string MultThisSymbol(string a_symbol, int a_nbrOfMult)
        {
            string result = "";

            for (int i = 0; i < a_nbrOfMult; i++)
            {
                result += a_symbol;
            }

            return result;
        }

        /// <summary>
        /// Affichage du menu voulu
        /// </summary>
        /// <param name="a_menuType">type du menu souhaité pour l'affichage</param>
        /// <param name="a_game"></param>
        public void ShowMenu(string a_menuType, Game a_game)
        {
            if (a_menuType.Equals(UseFull.PAUSE)) // TODO : si on a le temps : modifier en switch mais il faudrait faire l'Enum EnumMenuType
            {
                ShowPauseMenu(a_game);
            }
            else if (a_menuType.Equals(UseFull.MAIN_MENU))
            {
                ShowMainMenu(a_game);
            }
            else if (a_menuType.Equals(UseFull.GAME_OVER))
            {
                ShowGameOverMenu(a_game);
            }
            else if (a_menuType.Equals(UseFull.STAGE_WIN))
            {
                ShowWinMenu(a_game);    
            }
            else if (a_menuType.Equals(UseFull.BONUS_STAGE))
            {
                ShowBonusMenu(a_game);
            }
            else if (a_menuType.Equals(UseFull.HIGH_SCORE))
            {
                ShowHighScore();
            }
            else if (a_menuType.Equals(UseFull.DIFFICULTY_CHOISE))
            {
                ShowDifficultyMenu(a_game);
            }
        }

        /// <summary>
        /// s'occupe de l'affichge des menus selon différents critères
        /// </summary>
        /// <param name="a_tab">tableau à afficher</param>
        /// <param name="a_place">place actuel (surbrillance du text)</param>
        /// <param name="a_selection">place séléctionnée</param>
        /// <param name="a_confirmation">si une confirmation est nécessaire</param>
        /// <param name="a_slotMenu">s'il sagit de l'affiche pour les sauvegardes</param>
        /// <param name="a_slotMenuLike">s'il s'agit d'un affichage presque comme les sauvegardes</param>
        private void DisplayMenu(string[] a_tab, int a_place, int a_selection = -1, bool a_confirmation = false, bool a_slotMenu = false, bool a_slotMenuLike = false)
        {
            if (a_slotMenu)
            {
                int x = 0;

                //affichage du menu
                foreach (string affichage in a_tab)
                {
                    string[] tab2 = { "╔═" + MultThisSymbol("═", affichage.Length) + "═╗", "║ " + affichage + " ║", "╚═" + MultThisSymbol("═", affichage.Length) + "═╝" };

                    if (x == a_place) // surlignement en jaune du text concerné en rouge si la save est selectionné
                    {
                        Console.ForegroundColor = x == _saveSelected ? ConsoleColor.Red : ConsoleColor.Yellow;

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = x == _saveSelected ? ConsoleColor.Red : ConsoleColor.Gray;

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }

                    x++;
                }
            }
            else if (a_confirmation)
            {
                int x = 0;

                //affichage du menu
                foreach (string affichage in a_tab)
                {
                    if (x == a_place) // surlignement en jaune du text concerné en rouge si la save est selectionné
                    {
                        Console.ForegroundColor = x == _bonusSelected ? ConsoleColor.Red : ConsoleColor.Yellow;
                        string[] tab2 = a_tab[x].Split('.');

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = x == _bonusSelected ? ConsoleColor.Red : ConsoleColor.Gray;
                        string[] tab2 = a_tab[x].Split('.');

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }

                    x++;
                }
            }
            else if (a_slotMenuLike) 
            {
                int x = 0;

                //affichage du menu
                foreach (string affichage in a_tab)
                {
                    string[] tab2 = { "╔═" + MultThisSymbol("═", affichage.Length) + "═╗", "║ " + affichage + " ║", "╚═" + MultThisSymbol("═", affichage.Length) + "═╝" };

                    if (x == a_place) // surlignement en jaune du text concerné en rouge si la save est selectionné
                    {
                        Console.ForegroundColor = x == a_selection ? ConsoleColor.Red : ConsoleColor.Yellow;

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = x == a_selection ? ConsoleColor.Red : ConsoleColor.Gray;

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }

                    x++;
                }
            }
            else
            {
                int x = 0;

                // affichage du menu
                foreach (string affichage in a_tab)
                {
                    if (x == a_place) // surlignement en jaune du text concerné
                    {
                        string[] tab2 = a_tab[x].Split('.');
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }
                    else // affichage en gris du reste du text
                    {
                        string[] tab2 = a_tab[x].Split('.');
                        Console.ForegroundColor = ConsoleColor.Gray;

                        int heightAjustment = -1;

                        foreach (string ligne in tab2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2 - tab2[0].Length / 2, Console.WindowHeight / 3 + x * 3 + heightAjustment);
                            Console.WriteLine(ligne);
                            heightAjustment++;
                        }
                    }

                    x++;
                }
            }
        }

        /// <summary>
        /// Affichage du menu principal
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowMainMenu(Game a_game)
        {
            Console.Clear();

            string[] tab = { UseFull.NEW_GAME, UseFull.CONTINUE, UseFull.LOAD, UseFull.SETTINGS, UseFull.LEAVE };

            int place = 0;

            bool newGame = false;
            bool loadLast = false;

            while (true)
            {
                DisplayMenu(tab, place); // affichage du menu

                ConsoleKeyInfo key = Console.ReadKey();

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
                            Console.Clear();
                            break;
                        case 1: // continuer
                            Console.Clear();
                            loadLast = true;
                            break;

                        case 2: // charger
                            ShowSlotMenu(false, a_game);
                            break;

                        case 3: // parametres
                            Console.Clear();
                            ShowSettingMenu(a_game);
                            break;

                        case 4: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }

                if (newGame)
                {
                    a_game.ResetGame();
                    break;
                }
                else if (loadLast)
                {
                    break; // TODO : si le temps le permet : charger les fichiers de sauvegarde et prendre le plus récent pour loader cette partie
                }
            }
        }

        /// <summary>
        /// affichage des réglages
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowSettingMenu(Game a_game)
        {
            bool retour = false;
            bool sound_on = UseFull.SoundIsON();
            string[] tab = new string[2]; // modifier en fonction du nombre de paramètre 

            if (sound_on) 
            {
                tab[0] = UseFull.SOUND_ON;
            }
            else
            {
                tab[0] = UseFull.SOUND_OFF;
            }
            tab[1] = UseFull.BACK;

            int place = 0;
            
            ConsoleKeyInfo key;

            while (true)
            {
                DisplayMenu(tab, place);// affichage du menu

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
                        case 0: // Modifier le son
                            if (sound_on)  // TODO : si on a le temps : utiliser une méthode pour écrire correctement les réglages (car là pour l'instant on overide tout le fichier pour mettre juste le reglage concernant le son)
                            {
                                sound_on = false;
                                tab[0] = UseFull.SOUND_OFF;
                                File.WriteAllText(UseFull.PATH_REGLAGE, "Sound?OFF!");
                            }
                            else
                            {
                                sound_on = true;
                                tab[0] = UseFull.SOUND_ON;
                                File.WriteAllText(UseFull.PATH_REGLAGE, "Sound?ON!");
                            }

                            Console.Clear();
                            break;
                        case 1: // retour
                            
                            Console.Clear();
                            retour = true;
                            break;
                    }
                }

                if (retour)
                {
                    break;
                }
            }
        }


        /// <summary>
        /// affichage du menu des sauvegarde
        /// </summary>
        /// <param name="abbleToSave">true : si l'on souhaite sauvegarder, false : si l'on souhaite charger une partie</param>
        /// <param name="a_game"></param>
        private void ShowSlotMenu(bool abbleToSave, Game a_game)
        {
            // TODO : si on a le temps : voir pour un try catch au cas où les fichiers n'existent pas pour créer les fichiers
            string save1 = File.ReadAllText(UseFull.PATH_SLOT_1);
            string save2 = File.ReadAllText(UseFull.PATH_SLOT_2);
            string save3 = File.ReadAllText(UseFull.PATH_SLOT_3);

            string[] tab = {
                // save 1:
                UseFull.SLOT_1 + " | Save date: " + save1.Split('!')[0].Split('?')[1] + 
                    " | Enemy lvl: " + save1.Split('!')[2].Split('?')[1] + 
                    " | Life: " + save1.Split('!')[3].Split('?')[1].Split('/')[0].Split('.')[1] + "/"+ save1.Split('!')[3].Split('?')[1].Split('/')[2].Split('.')[1] + 
                    " | Nombre de missiles: " + save1.Split('!')[3].Split('?')[1].Split('/')[1].Split('.')[1],
                // save 2:
                UseFull.SLOT_2 + " | Save date: " + save2.Split('!')[0].Split('?')[1] + 
                    " | Enemy lvl: " + save2.Split('!')[2].Split('?')[1] + 
                    " | Life: " + save2.Split('!')[3].Split('?')[1].Split('/')[0].Split('.')[1] + "/"+ save2.Split('!')[3].Split('?')[1].Split('/')[2].Split('.')[1] + 
                    " | Nombre de missiles: " + save2.Split('!')[3].Split('?')[1].Split('/')[1].Split('.')[1],
                // save 3:
                UseFull.SLOT_3 + " | Save date: " + save3.Split('!')[0].Split('?')[1] + 
                    " | Enemy lvl: " + save3.Split('!')[2].Split('?')[1] + 
                    " | Life: " + save3.Split('!')[3].Split('?')[1].Split('/')[0].Split('.')[1] + "/"+ save3.Split('!')[3].Split('?')[1].Split('/')[2].Split('.')[1] + 
                    " | Nombre de missiles: " + save3.Split('!')[3].Split('?')[1].Split('/')[1].Split('.')[1],

                UseFull.BACK
            };

            Console.Clear();

            int place = 0;
            bool back = false;

            ConsoleKeyInfo key;

            while (!back)
            {
                DisplayMenu(tab, place, -1, true, true); // affichage du menu

                key = Console.ReadKey();

                string save = "";

                switch (key.Key)
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
                                if (_saveSelected !=0)
                                {
                                    _saveSelected = 0;
                                }
                                else if (abbleToSave)
                                {
                                    File.WriteAllText(UseFull.PATH_SLOT_1, a_game.GetSaveStat());
                                }
                                else if (!save1.Equals(""))
                                {
                                    save = save1;
                                    back = true;
                                }
                                break;

                            case 1: // slot 2
                                if (_saveSelected != 1)
                                {
                                    _saveSelected = 1;
                                }
                                else if (abbleToSave)
                                {
                                    File.WriteAllText(UseFull.PATH_SLOT_2, a_game.GetSaveStat());
                                }
                                else if (!save2.Equals(""))
                                {
                                    save = save2;
                                    back = true;
                                }
                                break;

                            case 2: // slot 3
                                if (_saveSelected != 2)
                                {
                                    _saveSelected = 2;
                                }
                                else if (abbleToSave)
                                {
                                    File.WriteAllText(UseFull.PATH_SLOT_3, a_game.GetSaveStat());
                                }
                                else if (!save3.Equals(""))
                                {
                                    save = save3;
                                    back = true;
                                }
                                break;

                            case 3: // on retourne au menu précédent
                                back = true;
                                break;
                        }
                        break;
                }

                if (!save.Equals(""))
                {
                    a_game.LoadGame(save.Split('!'));

                    back = true;
                }

                if (back)
                {
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// affiche les menu des Bonus (après une victoire sur un boss)
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowBonusMenu(Game a_game)
        {
            bool resume = false;

            string[] tab = { UseFull.upgrade1, UseFull.upgrade2, UseFull.upgrade3 }; // TODO : si on a le temps : voir pour mettre des upgrades aléatoire et/ou différentes et/ou des missiles différents etc...

            Console.Clear();

            int place = 0;
            _bonusSelected = -1;

            ConsoleKeyInfo key;

            while (!resume)
            {
                DisplayMenu(tab, place, -1, true); // affichage du menu

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
                        case 0:
                            if (_bonusSelected != 0)
                            {
                                _bonusSelected = 0;
                            }
                            else
                            {
                                a_game.BonusShipWeaponSlot();
                                resume = true;
                            }
                            break;

                        case 1:
                            if (_bonusSelected != 1)
                            {
                                _bonusSelected = 1;
                            }
                            else
                            {
                                a_game.BonusShipMaxHeath();
                                resume = true;
                            } 
                            break;

                        case 2:
                            if (_bonusSelected != 2)
                            {
                                _bonusSelected = 2;
                            }
                            else
                            {
                                a_game.BonusShipHeal();
                                resume = true;
                            } 
                            break;
                    }
                }
            }

            Console.Clear();
        }

        /// <summary>
        /// Affiche le menu de victoire
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowWinMenu(Game a_game)
        {
            bool resume = false;

            string[] tab = { UseFull.CONTINUE, UseFull.NEW_GAME, UseFull.SAVE, UseFull.SETTINGS, UseFull.LEAVE };

            Console.Clear();

            int place = 0;

            ConsoleKeyInfo key;

            while (!resume)
            {

                DisplayMenu(tab, place);// affichage du menu

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
                            resume = true;
                            break;

                        case 1: // new game
                            a_game.ResetGame();
                            resume = true;
                            break;

                        case 2: // save
                            ShowSlotMenu(true, a_game);
                            Console.Clear();
                            break;

                        case 3: // parametres
                            Console.Clear();
                            ShowSettingMenu(a_game);
                            break;

                        case 4: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }
            }
            Console.Clear();
        }

        /// <summary>
        /// Affiche le menu de pause
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowPauseMenu(Game a_game)
        {
            bool resume = false;

            string[] tab = { UseFull.CONTINUE, UseFull.NEW_GAME, UseFull.LOAD, UseFull.SETTINGS, UseFull.LEAVE };

            Console.Clear();

            int place = 0;
            
            ConsoleKeyInfo key;

            while (!resume)
            {
                DisplayMenu(tab, place);// affichage du menu

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
                            resume = true;
                            Console.Clear();
                            break;

                        case 1: // new game
                            resume = true; 
                            a_game.ResetGame();
                            Console.Clear();
                            break;

                        case 2: // charger
                            ShowSlotMenu(false, a_game);
                            break;

                        case 3: // parametres
                            Console.Clear();
                            ShowSettingMenu(a_game);
                            break;

                        case 4: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Affiche le menu de défaite
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowGameOverMenu(Game a_game) // normalement plus utile, mais on le garde quand même au cas ou (remplacé pas le menu principale)
        {
            Console.Clear();
            string[] tab = { UseFull.NEW_GAME, UseFull.LOAD, UseFull.SETTINGS, UseFull.LEAVE };

            int place = 0;

            bool newGame = false;
            bool load = false;

            ConsoleKeyInfo key;
            
            while (true)
            {
                DisplayMenu(tab, place); // affichage du menu

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
                            Console.Clear();
                            load = true;
                            break;

                        case 2: // parametres
                            Console.Clear();
                            ShowSettingMenu(a_game);
                            break;

                        case 3: // quitter
                            Environment.Exit(0);
                            break;
                    }
                }

                if (newGame || load)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Affiche la séléction de la difficulté (au début d'une nouvelle partie)
        /// </summary>
        /// <param name="a_game"></param>
        private void ShowDifficultyMenu(Game a_game)
        {
            Console.Clear();
            string[] tab = { UseFull.DIFFICULTY_CHOISE, "EASY", "HARD", "NOT EASY" };

            int place = 0;
            int a_selection = -1;

            bool notChoiced = true;

            ConsoleKeyInfo key;

            while (notChoiced)
            {
                DisplayMenu(tab, place, a_selection, false, false, true); // affichage du menu

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
                        case 0: // titre
                            // nothing to do
                            a_selection = -1;
                            break;

                        case 1: // easy
                            if (a_selection != 1)
                            {
                                a_selection = 1;
                            }
                            else
                            {
                                a_game._difficulty = "easy";
                                notChoiced = false;
                            }
                            break;

                        case 2: // hard
                            if (a_selection != 2)
                            {
                                a_selection = 2;
                            }
                            else
                            {
                                a_game._difficulty = "hard";
                                notChoiced = false;
                            }
                            break;

                        case 3: // really hard
                            if (a_selection != 3)
                            {
                                a_selection = 3;
                            }
                            else
                            {
                                a_game._difficulty = "harder";
                                notChoiced = false;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// affiche les meilleures Scores (à la fin de la partie)
        /// </summary>
        private void ShowHighScore()
        {
            bool name_not_conforme = true;
            string pseudo = "Anne O'Nyme";

            while (name_not_conforme)
            {
                Console.Clear();
                Console.SetCursorPosition(2, Console.WindowHeight / 2);
                Console.Write("Voulez vous entrer votre pseudo ? (oui, entrez votre pseudo // non, entrez '-') : ");
                pseudo = Console.ReadLine();

                if (pseudo.Equals("-"))
                {
                    pseudo = "Anne O'Nyme";
                    name_not_conforme = false;
                }
                else if (pseudo.Contains('!') || pseudo.Contains('?') || pseudo.Contains('\\') ) // TODO : voir s'il y a pas besoin d'exclure d'autres caractères !
                {
                    pseudo = "Anne O'Nyme";
                    Console.WriteLine("le pseudo entré est invalide (press Enter to retry)");
                    Console.ReadLine();
                }
                else
                {
                    name_not_conforme = false;
                }
            }

            bool resume = false;

            string highScores = File.ReadAllText(UseFull.PATH_HIGH_SCORE); // récupère les données des meilleures scores
            string newHighScores = FindPlace(highScores, pseudo); // calcul le placement du score (depuis le haut) dans le tableau des meilleures scores
            File.WriteAllText(UseFull.PATH_HIGH_SCORE, newHighScores); // enregistre le nouveaux tableau des meilleures scores

            string[] highScoreSplit = newHighScores.Split('!');

            string[] tab = { UseFull.HIGH_SCORE, 
                            "1 " + highScoreSplit[0].Split('?')[0] + " : " + highScoreSplit[0].Split('?')[1],
                            "2 " + highScoreSplit[1].Split('?')[0] + " : " + highScoreSplit[1].Split('?')[1], 
                            "3 " + highScoreSplit[2].Split('?')[0] + " : " + highScoreSplit[2].Split('?')[1], 
                            "4 " + highScoreSplit[3].Split('?')[0] + " : " + highScoreSplit[3].Split('?')[1], 
                            "5 " + highScoreSplit[4].Split('?')[0] + " : " + highScoreSplit[4].Split('?')[1],
                            UseFull.CONTINUE};

            Console.Clear();

            int place = 0;

            ConsoleKeyInfo key;

            while (!resume)
            {
                DisplayMenu(tab, place);// affichage du menu

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
                        case 0: // nothing -> 1
                        case 1: // nothing -> 2
                        case 2: // nothing -> 3
                        case 3: // nothing -> 4
                        case 4: // nothing -> 5
                        case 5: // nothing -> break !
                            break;

                        case 6: // continuer
                            resume = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// s'occupe d'inscrire le pseudo et le score obtenu dans le tableau des meilleures scores
        /// </summary>
        /// <param name="a_highScores"> tableau des highScores actuel</param>
        /// <param name="a_pseudo"></param>
        /// <returns>le nouveau tableau des scores</returns>
        public string FindPlace(string a_highScores, string a_pseudo)
        {
            string newHighScores = "";

            string[] highScoreSplit = a_highScores.Split('!');

            int score = Game._score;
            bool placeFinded = false;

            for (int i = 0, j = 0; i < 5; i++)
            {
                if (!placeFinded && Convert.ToInt32(highScoreSplit[i].Split('?')[1]) <= score)
                {
                    placeFinded = true;

                    newHighScores += a_pseudo + "?" + score;
                    if (i != 4)
                    {
                        newHighScores += "!";
                    }
                }
                else
                {
                    newHighScores += highScoreSplit[j];
                    if (i != 4)
                    {
                        newHighScores += "!";
                    }
                    j++;
                }
            }

            return newHighScores;
        }

        /// <summary>
        /// Affiche le level actuel et le total de vie de la vague ennemie
        /// </summary>
        /// <param name="a_level"></param>
        /// <param name="a_enemyLife"></param>
        private void DisplayEnemyLevel(int a_level, int a_enemyLife)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.Write("Level : " + a_level + " / Enemy life : " + a_enemyLife);
        }

        /// <summary>
        /// Affiche le score actuel
        /// </summary>
        private void DisplayScore()
        {
            Console.SetCursorPosition(UseFull.SCORE_DISPLAY_POSITION_X, UseFull.SCORE_DISPLAY_POSITION_Y);
            Console.Write("score : {0}", Game._score);
        }

        /// <summary>
        /// Affiche les crédits
        /// </summary>
        private void DisplayCredits()
        {
            Console.SetCursorPosition(UseFull.CREDITS_DISPLAY_POSITION_X, UseFull.CREDITS_DISPLAY_POSITION_Y);
            Console.Write("Credits : Toine Riedo, Adrian Barreira, Laeticia Guidetti, Jeremiah Steiner");
        }

        /// <summary>
        /// Affiche tout les texts quand le jeu est lancé (l'HUD quoi)
        /// </summary>
        /// <param name="a_ship"></param>
        /// <param name="a_level"></param>
        /// <param name="a_enemyLife"></param>
        public void DisplayHUD(Ship a_ship, int a_level, int a_enemyLife)
        {
            // début // scores :
            DisplayScore();
            // fin // scores

            DisplayEnemyLevel(a_level, a_enemyLife);

            // début // missiles :
            int missileTotal = a_ship.GetMissilesCapacity();
            int CurrentNbrMissile = a_ship.HowManyMissilesLeft();
            string realoadind = "             ";

            Console.SetCursorPosition(UseFull.MISSILE_DISPLAY_POSITION_X, UseFull.MISSILE_DISPLAY_POSITION_Y);
            Console.Write("missiles : ");

            if (CurrentNbrMissile == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                realoadind = "realoading...";
            }
            
            Console.Write("{0}/{1} ", CurrentNbrMissile, missileTotal);

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write(realoadind); // efface ou bien écrit 
            // fin // missiles

            // début // vie :

            Console.SetCursorPosition(UseFull.LIFE_DISPLAY_POSITION_X, UseFull.LIFE_DISPLAY_POSITION_Y);
            Console.WriteLine("vies : " + a_ship.GetLife() + "/" + a_ship.GetMaxLife());

            // fin // vie

            // Credits
            DisplayCredits();
        }
    }
}
