/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la Classe Usefull 
 */
using System.IO;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Usefull
    /// </summary>
    public static class UseFull
    {
        /// <summary>
        /// attributs
        /// </summary>
        public static string NEW_GAME = "╔═════════════════╗." +
                                        "║ nouvelle partie ║." +
                                        "╚═════════════════╝";
        public static string CONTINUE = "╔═════════════════╗." +
                                        "║    continuer    ║." +
                                        "╚═════════════════╝";
        public static string SAVE = "╔═════════════════╗." +
                                    "║   sauvegarder   ║." +
                                    "╚═════════════════╝";
        public static string LOAD = "╔═════════════════╗." +
                                    "║     charger     ║." +
                                    "╚═════════════════╝";
        public static string SETTINGS = "╔═════════════════╗." +
                                        "║    parametres   ║." +
                                        "╚═════════════════╝";
        public static string LEAVE = "╔═════════════════╗." +
                                     "║     quitter     ║." +
                                     "╚═════════════════╝";
        public static string SOUND_ON = "╔═════════════════╗." +
                                        "║     Sound ON    ║." +
                                        "╚═════════════════╝";
        public static string SOUND_OFF = "╔═════════════════╗." +
                                         "║     Sound OFF   ║." +
                                         "╚═════════════════╝";

        public static string BACK = "retour";

        public static string SLOT_1 = "Slot 1";
        public static string SLOT_2 = "Slot 2";
        public static string SLOT_3 = "Slot 3";

        private static string _PATH_SLOT_1 = Path.GetFullPath("slot_1.txt");
        private static string _PATH_SLOT_2 = Path.GetFullPath("slot_2.txt");
        private static string _PATH_SLOT_3 = Path.GetFullPath("slot_3.txt");
        private static string _PATH_REGLAGE = Path.GetFullPath("reglage.txt");
        private static string _PATH_HIGH_SCORE = Path.GetFullPath("high_score.txt");


        /// <summary>
        /// Propriétés
        /// </summary>
        public static string PATH_SLOT_1
        {
            get { return _PATH_SLOT_1; }
            private set { PATH_SLOT_1 = value; } // TODO : voir si c'est mieux comme ça ou de pas mettre le setter (comme pour le slot 2)
        }
        public static string PATH_SLOT_2
        {
            get { return _PATH_SLOT_2; }
            set { }
        }
        public static string PATH_SLOT_3
        {
            get { return _PATH_SLOT_3; }
            set { }
        }
        public static string PATH_REGLAGE
        {
            get { return _PATH_REGLAGE; }
            set { }
        }
        public static string PATH_HIGH_SCORE
        {
            get { return _PATH_HIGH_SCORE; }
            set { }
        }




        public static string upgrade1 = "╔═════════════════════════╗." +
                                        "║  Increase Weapon Slot   ║." +
                                        "╚═════════════════════════╝";
        public static string upgrade2 = "╔═════════════════════════╗." +
                                        "║   Get one more life     ║." +
                                        "╚═════════════════════════╝";
        public static string upgrade3 = "╔═════════════════════════╗." +
                                        "║ Regen one point of life ║." +
                                        "╚═════════════════════════╝";


        public static string MAIN_MENU = "╔═════════════════╗." +
                                        "║ menu principale ║." +
                                        "╚═════════════════╝";
        public static string PAUSE = "pause";
        public static string GAME_OVER = "game over";
        public static string STAGE_WIN = "stage win";
        public static string BONUS_STAGE = "bonus stage";
        public static string HIGH_SCORE = "╔════════════╗." +
                                         "║ high score ║." +
                                         "╚════════════╝";
        public static string DIFFICULTY_CHOISE = "choix de la difficultée";

        public static int MISSILE_DISPLAY_POSITION_X = 30;
        public static int MISSILE_DISPLAY_POSITION_Y = 40;
        // TODO : faire aussi les coordonnée des autre HUD en const

        public const char STRING_SHAPE_SEPARATOR = '4';  // ATTENTION : on ne peut donc pas utiliser le chiffre 4 pour la construction de silhouette

        public const string ALLY_SHIP_SKIN_1 = "      ■      4     ■■■     4    ■■■■■    4 ///■■■■■\\\\\\ 4////■■■■■\\\\\\\\"; // ALLY
        public const string ALLY_SHIP_SKIN_2 = "     |     4    /■\\    4  //■■■\\\\  4█--■■■■■--█4 \\\\ ■■■ // "; // ALLY

        public const string ENNEMY_SKIN_1 = "    /■\\    4¥  /■■■\\  ¥4| /■■■■■\\ |4|/■■■■■■■\\|4/--O---O--\\"; // BOSS
        public const string ENNEMY_SKIN_2 = "    /**\\   4----0  0----4    /**\\  4   /    \\  "; // BOSS
        public const string ENNEMY_SKIN_3 = "     ■     ■     4|  ■■ ■■■■■ ■■  |4|    ■■■■■■■    |4\\----■ ■■■ ■----/4      ■■■■■      "; // BOSS

        //public const string ENNEMY_SKIN_4 = "\\_||_/4-0||0-4__/\\__"; // Invader : X = 6, Y = 3
        public const string ENNEMY_SKIN_4 = "/00\\4|--|"; // Invader : X = 4, Y = 2
        public const string ENNEMY_SKIN_5 = " ITI 4|/¨\\|"; // Invader : X = 5, Y = 2
        public const string ENNEMY_SKIN_6 = "-\\_/-4 ||| "; // Invader : X = 5, Y = 2
        public const string ENNEMY_SKIN_7 = " /|\\ 4.<|>."; // Invader : X = 5, Y = 2





        /// <summary>
        /// permet de connaitre les réglages du son
        /// </summary>
        /// <returns></returns>
        public static bool SoundIsON()
        {
            string reglage = File.ReadAllText(PATH_REGLAGE);

            return reglage.Split('!')[0].Split('?')[1].Equals("ON");
        }
    }
}
