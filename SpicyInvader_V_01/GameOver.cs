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
using System.Threading;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Game=ver
    /// </summary>
    class GameOver
    {
        /// <summary>
        /// Attributs
        /// </summary>
        const int SCROLLINGSPEED = 50;
        const int _INT_TITLE_INTRO = 6;

        bool scroll = true;
        string[] text = new string[_INT_TITLE_INTRO];

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public GameOver()
        {
            Console.CursorVisible = false;

            //http://patorjk.com/software/taag/#p=display&f=Slant&t=GAME%20OVER
            text[0] = "   _________    __  _________   ____ _    ____________ ";
            text[1] = "  / ____/   |  /  |/  / ____/  / __ \\ |  / / ____/ __ \\";
            text[2] = " / / __/ /| | / /|_/ / __/    / / / / | / / __/ / /_/ /";
            text[3] = "/ /_/ / ___ |/ /  / / /___   / /_/ /| |/ / /___/ _, _/ ";
            text[4] = "\\____/_/  |_/_/  /_/_____/   \\____/ |___/_____/_/ |_|  ";
            text[5] = "=======================================================";
        }

        /// <summary>
        /// Animation de la chute du texte
        /// </summary>
        public void FallingOutro()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int tick = 0;
            int x = 0;

            while (true)
            {
                Console.Clear();

                if (scroll)//(x <= 5)
                {
                    for (int xx = 0; xx <= x; xx++)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2 - text[2].Length / 2, xx);
                        Console.WriteLine(text[5 - x + xx]);

                        if (xx == 5) // nb elements dans liste
                        {
                            x = 0;
                            scroll = false;
                        }
                    }
                    Thread.Sleep(SCROLLINGSPEED);
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text[2].Length / 2, x);
                    for (int xx = 0; xx <= 5; xx++)
                    {
                        Console.CursorLeft = Console.WindowWidth / 2 - text[2].Length / 2;
                        Console.WriteLine(text[xx]);
                    }
                    Thread.Sleep(SCROLLINGSPEED);
                }
                x++;

                if (x > Console.WindowHeight / 2)
                {
                    while (Console.KeyAvailable) //vide le buffer
                    {
                        Console.ReadKey();
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                    PressAKeyToStart(ref tick);
                    break;
                }
            }

            Console.ReadKey();
            
        }

        /// <summary>
        /// Permet de changer de menus
        /// </summary>
        /// <param name="a_tick"></param>
        public void PressAKeyToStart(ref int a_tick)
        {
            string textIntro = "Appuyez sur une touche pour Continuer";

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(200);

                Console.SetCursorPosition(Console.WindowWidth / 2 - textIntro.Length / 2, Console.WindowHeight / 2 + 7);

                if (a_tick % 5 < 3)
                {
                    for (int x = 0; x <= textIntro.Length / 2; x++)
                    {
                        Console.Write("- ");
                    }

                    Console.SetCursorPosition(Console.WindowWidth / 2 - textIntro.Length / 2, Console.WindowHeight / 2 + 8);
                    Console.WriteLine(textIntro);
                }
                else if (a_tick % 5 >= 3)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(Console.WindowWidth / 2 - textIntro.Length / 2, Console.WindowHeight / 2 + 8);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }

                a_tick++;

                if (a_tick == int.MaxValue)
                {
                    a_tick = 0;
                }
            }

            Console.Clear();
        }
    }
}
