using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpicyInvader_V_01
{
    class Intro
    {
        const int SCROLLINGSPEED = 80;
        const int _INT_TITLE_INTRO = 6;

        bool scroll = true;
        string[] text = new string[_INT_TITLE_INTRO];

        public Intro()
        {
            Console.CursorVisible = false;

            /*text[0] = "  _________                               .___                            .___            ";
            text[1] = " /   _____/_____________   ___        _   |   | _______  _______        __| _/___________ ";
            text[2] = " \\_____  \\____  \\__  \\_/ ___\\/ __ \\       |   |/    \\   \\ / / \\__ \\   / __ |/ __ \\_  __ \\";
            text[3] = " /        \\  |_> > __ \\  \\__\\  ___/       |   |   |  \\    /  / __ \\_ / /_/ \\  ___/|  | \\/";
            text[4] = "/_______  /   __(____  /\\___  >___ >      |___|___|  /\\ _/  (____  /  \\____ |\\___  >__|    ";
            text[5] = "        \\/|__|       \\/     \\/   \\/            \\/            \\/         \\/      \\/      ";*/

            /*text[0] = "            0";
            text[1] = "            1";
            text[2] = "            2";
            text[3] = "            3";
            text[4] = "            4";
            text[5] = "            5";*/

            text[0] = "    _____ ____  ____________  __     _____   ___    _____    ____  __________  _____ ";
            text[1] = "   / ___// __ \\/  _/ ____/\\ \\/ /    /  _/ | / / |  / /   |  / __ \\/ ____/ __ \\/ ___/";
            text[2] = "   \\__ \\/ /_/ // // /      \\  /     / //  |/ /| | / / /| | / / / / __/ / /_/ /\\__ \\";
            text[3] = "  ___/ / ____// // /___    / /    _/ // /|  / | |/ / ___ |/ /_/ / /___/ _, _/___/ / ";
            text[4] = " /____/_/   /___/\\____/   /_/    /___/_/ |_/  |___/_/  |_/_____/_____/_/ |_|/____/";
            text[5] = "===================================================================================";// test 7-9 9-3

        }

        public void FallingIntro()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int x = 0; x < Console.WindowHeight / 2; x++)
            {
                Console.Clear();
                //Console.SetCursorPosition(0,x);

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
            }
            /*
            for(int x = 0; x <= 5; x++)
            {
                Console.WriteLine(text[x]);
                Thread.Sleep(100);
            }
            */
            Console.ForegroundColor = ConsoleColor.Gray;
            PressAKeyToStart();
        }

        public void PressAKeyToStart()
        {
            string textIntro = "Appuyez sur une touche pour Continuer";
            int tick = 0;
            bool nextMenu = false;

            while (!nextMenu)
            {
                Thread.Sleep(1000);

                Console.SetCursorPosition(Console.WindowWidth / 2 - textIntro.Length / 2, Console.WindowHeight / 2 + 7);

                if (tick % 2 == 0)
                {
                    for (int x = 0; x <= textIntro.Length / 2; x++)
                    {
                        Console.Write("- ");
                    }

                    Console.SetCursorPosition(Console.WindowWidth / 2 - textIntro.Length / 2, Console.WindowHeight / 2 + 8);
                    Console.CursorLeft = Console.WindowWidth / 2 - textIntro.Length / 2;
                    Console.WriteLine(textIntro);
                }
                else if (tick % 2 == 1)
                {
                    Console.WriteLine("                                         ");
                    Console.CursorLeft = Console.WindowWidth / 2 - textIntro.Length / 2;
                    Console.WriteLine("                                         ");
                }
                tick++;

                if (Console.ReadKey().ToString().Length > 0)
                {
                    nextMenu = true;
                }
            }

            Console.Clear();
            
        }
    }
}
